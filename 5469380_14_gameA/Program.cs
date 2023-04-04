using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5469380_14_gameA
{
    class Player //player의 위치(x, y), 외형(스킨), item과 기본적인 속성이 같다. 그러므로 한 클래스로 구현은 가능하나, 하위 속성이 다를 수 있으므로
                //기본속성 class와 이 속성에 종속되는 하위 class를 만들면 객체지향적인 방법이 된다.
    {
        public int x;
        public int y;
        
    }
    //우리가 사용하는 콘솔창의 가로길이는 80이다. 시작지점은 0, 세로는 25칸이지만 좀 이상하게 동작함(무한 스크롤)
    class Item
    {
        public int x;
        public int y;
    }

    internal class Program
    {
        static Item item = new Item();
        static Random random = new Random();
        static Player player = new Player();
        static void GenerateItem()
        {

            while (true) //메소드로 빼면 좋다. 시험에선 메소드로 만들어야 함. 코드 리팩토링 과정에서 메소드를 만드는 것이 좋음.
            {
                item.x = random.Next() % 70 + 5;
                item.y = random.Next() % 20 + 2;

                if (item.x == player.x && item.y == player.y)
                {
                    continue;
                }
                else
                    break;
            }

            Console.SetCursorPosition(item.x, item.y);
            Console.WriteLine('*');
        }
        static void Main(string[] args)
        {
            int score = 0; //점수

            Player player = new Player(); //클래스를 사용하기 위한 문장
            player.x = 0;
            player.y = 1; //이동 경계 설정, 1로 설정한 이유(맨 윗줄은 점수판이나 각종 정보가 있으므로 1로 설정함)

            Console.SetCursorPosition(player.x, player.y);
            Console.WriteLine('A');

            GenerateItem(); //* 아이템을 만드는 함수

            Console.SetCursorPosition(50, 0); //글자 위치, 밑에 WriteLine을 사용하면 해당위치에 적힌다.
            Console.WriteLine(String.Format("Score : {0:00000}", score));

            while(true)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write(DateTime.Now.ToString()); //현재시간 출력

                if (!Console.KeyAvailable) //현재시간은 커서가 움직이는 걸 기준으로 출력되게 되지만 이 조건문을 쓰면 실시간으로 출력된다.
                    continue;

                var Key = Console.ReadKey(); //방향키를 통해 이동가능

                if (Key.Key == ConsoleKey.RightArrow) //방향키를 통해 이동가능
                {
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine(" ");
                    if(player.x < 80) //이동 경게 설정
                    player.x++;
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine("A");
                }
                else if(Key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine(" ");
                    if(player.y < 23) //이동 경계 설졍
                    player.y++;
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine("A");
                }

                else if (Key.Key == ConsoleKey.LeftArrow)
                {
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine(" ");
                    if(player.x > 0) //이동 경계 설정
                    player.x--;
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine("A");
                }

                else if (Key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine(" ");
                    if(player.y > 1) //이동 경계 설정, 1로 설정한 이유(맨 윗줄은 점수판이나 각종 정보가 있으므로 1로 설정함)
                        player.y--;
                    Console.SetCursorPosition(player.x, player.y);
                    Console.WriteLine("A");

                }

                else if(Key.Key == ConsoleKey.Escape) //esc 종료
                    break;

                if(player.x == item.x && player.y == item.y) // * 을 먹으면 10점씩 추가 반드시 if문으로.
                {
                    score += 10;
                    Console.SetCursorPosition(50, 0);
                    Console.WriteLine(String.Format("Score : {0:00000}", score));

                    if (score == 50) // 50점 달성시 종료
                    {
                        
                        Console.WriteLine("Clear");
                        break;
                    }

                    GenerateItem(); //* 아이템을 만드는 함수
                }
            }
        }
    }
}
