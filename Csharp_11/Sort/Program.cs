using System;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var strInt = "1 5 7 3 9 5";
            var arrStrInt = strInt.Split(' ');
            var arrIntInt = new int[arrStrInt.Length];

            for(int i = 0; i < arrIntInt.Length; i++)
            {
                arrIntInt[i] = int.Parse(arrStrInt[i]);
            }

            var result = Sort(arrIntInt);

            foreach(var item in arrIntInt)
            {
                Console.WriteLine(item);
            }
        }

        static int[] Sort(int[] in_arrayInt)
        {
            if(in_arrayInt.Length == 1)
            {
                return in_arrayInt;
            }

            var a = 0;
            var b = 0;
            var tmpIdx1 = 0;
            var tmpIdx2 = in_arrayInt.Length - 1;

            for (var i = in_arrayInt.Length - 1; i >= 0; i--)
            {
                var pibot = in_arrayInt[i];

                for (var x = tmpIdx1; x < in_arrayInt.Length; x++)
                {
                    if (in_arrayInt[x] > pibot)
                    {
                        a = in_arrayInt[x];
                        tmpIdx1 = x;
                        break;
                    }
                    else
                    {
                        a = pibot;
                        tmpIdx1 = i;
                    }
                }

                for (var x = tmpIdx2; x >= 0 ; x--)
                {
                    if (in_arrayInt[x] < pibot)
                    {
                        b = in_arrayInt[x];
                        tmpIdx2 = x;
                        break;
                    }
                    else
                    {
                        b = pibot;
                        tmpIdx2 = i;
                    }
                }

                if(tmpIdx1 < tmpIdx2)
                {
                    in_arrayInt[tmpIdx1] = b;
                    in_arrayInt[tmpIdx2] = a;
                    tmpIdx1 = tmpIdx1 + 1;
                    tmpIdx2 = tmpIdx2 - 1;
                }
                else
                {
                    var tmpArray = new int[tmpIdx1];
                    var tmpArray2 = new int[in_arrayInt.Length - tmpIdx1];


                    for (var y = 0; y < tmpArray.Length; y++)
                    {
                        tmpArray[y] = in_arrayInt[y];
                    }

                    for (var y = 0; y < tmpArray2.Length; y++)
                    {
                        tmpArray2[y] = in_arrayInt[y + tmpIdx1];
                    }

                    tmpArray = Sort(tmpArray);
                    tmpArray2 = Sort(tmpArray2);

                    for (var y = 0; y < tmpArray.Length; y++)
                    {
                        in_arrayInt[y] = tmpArray[y];
                    }

                    for (var y = 0; y < tmpArray2.Length; y++)
                    {
                        in_arrayInt[y + tmpIdx1] = tmpArray2[y];
                    }                    

                    

                    return in_arrayInt;
                }
            }

            return in_arrayInt;
        }
    }
}
