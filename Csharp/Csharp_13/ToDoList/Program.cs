using System;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            var dictionary = new ToDoLiat();
            var count = 0;

            while(true)
            {
                Console.Write("\n行いたい操作の番号を入力してください。\n1.ToDoの追加\n2.ToDoの表示\n3.ToDoの削除\n99.終了\n=>");
                var strCommand = Console.ReadLine();
                var comFlag = int.TryParse(strCommand, out int command);

                if (!comFlag)
                {
                    Console.WriteLine("数字を入力してください。");
                }
                else
                {
                    if((3 < command && command < 99) || 1 > command || 99 < command)
                    {
                        Console.WriteLine("操作ごとに正しい数字を入れてください。");
                    }
                    else
                    {
                        switch(command)
                        {
                            case 99:
                                Environment.Exit(0);
                                break;
                            case 1:
                                count += 1;
                                
                                Console.Write("↓↓-----ToDoを入力してください-----↓↓\n=>");
                                var contents = Console.ReadLine();
                                dictionary.AddDic(contents, count); 
                                break;
                            case 2:
                                Console.Write("↓↓-----あなたのToDoです-----↓↓\n");
                                dictionary.ViewDic();
                                break;
                            case 3:
                                Console.Write("↓↓-----削除したいToDoの番号を入力してください-----↓↓\n=>");
                                var sNumber = Console.ReadLine();
                                
                                if(!int.TryParse(sNumber, out int iNumber))
                                {
                                    Console.WriteLine("数字を入力してください");
                                }
                                else
                                {
                                    dictionary.DeleteDic(iNumber);
                                }
                                break;
                        }
                    }
                }

            }
        }

    }
}
