using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    class ToDoLiat
    {
        public Dictionary<int, string> List { get; set; }
        public string Contents { get; set ; }
        public int ContentsNum { get; set; }

        public ToDoLiat()
        {
            List = new Dictionary<int, string>();
            Contents = "";
            ContentsNum = 0;
        }

        public ToDoLiat(string contents, int contentsNum)
        {
            List = new Dictionary<int, string>();
            Contents = contents;
            ContentsNum = contentsNum;
        }

        public void AddDic(string in_cnts, int in_cntsNum)
        {
            List.Add(in_cntsNum, in_cnts);
        }

        public void ViewDic()
        {
            foreach(var item in List)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }

        public void DeleteDic(int in_cntsNum)
        {
            if(in_cntsNum > List.Count)
            {
                Console.WriteLine("**********ToDoリストにある数字を入力してください。**********");
            }
            else
            {
                List.Remove(in_cntsNum);
            }
        }
    }
}
