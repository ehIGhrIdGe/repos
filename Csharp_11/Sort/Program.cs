using System;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var strInt = "5 3 22 7 66";
            var arrStrInt = strInt.Split(' ');
            var arrIntInt = new int[arrStrInt.Length];

            for(int i = 0; i < arrIntInt.Length; i++)
            {
                arrIntInt[i] = int.Parse(arrStrInt[i]);
            }
            


            var intInt = int.Parse(strInt);
        }

        static void Sort(int[] in_arrayInt)
        {
            var tmpInt1 = 0;
            var tmpInt2 = 0;
            var tmpIdx1 = 0;
            var tmpIdx2 = 0;

            for (var i = in_arrayInt.Length - 1; i >= 0; i++)
            {
                var pibot = in_arrayInt[i];

                for (var x = tmpIdx1; x < in_arrayInt.Length; x++)
                {
                    if (in_arrayInt[i] > pibot)
                    {
                        tmpInt1 = in_arrayInt[i];
                    }
                    else
                    {
                        tmpInt1 = pibot;
                    }
                }
            }
        }
    }
}
