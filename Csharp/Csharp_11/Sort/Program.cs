using System;

namespace Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var strInt = "1 5 7 3 9 5 3 1 66 3 294 13";
            var arrStrInt = strInt.Split(' ');
            var arrIntInt = new int[arrStrInt.Length];

            for(int i = 0; i < arrIntInt.Length; i++)
            {
                arrIntInt[i] = int.Parse(arrStrInt[i]);
            }

            //Sort(arrIntInt);
            QuickSort(arrIntInt, false);

            Console.WriteLine("↓↓昇順↓↓");
            foreach(var item in arrIntInt)
            {
                Console.Write($"{item} ");
            }

            QuickSort(arrIntInt, true);

            Console.WriteLine();

            Console.WriteLine("↓↓降順↓↓");
            foreach (var item in arrIntInt)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine();
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

        private static void  QuickSort(int[] values, bool desc)
        {
            var strInt = "1 1 3 3 3 5 5 7 9 13 66 294";
            if (values.Length > 1)
            {
                var p = 0;
                var leftNum = 0;
                var rightNum = values.Length - 1;

                if (rightNum % 2 == 0)
                {
                    p = values[rightNum / 2];
                }
                else
                {
                    p = values[(rightNum + 1) / 2];
                }

                var chabgeFlag = false;

                for (var i = leftNum; i < values.Length; i++)
                { 
                    if (values[i] >= p && !desc)
                    {
                        for (var x = rightNum; x >= 0; x--)
                        {
                            if (values[x] <= p)
                            {
                                if (i < x)
                                {
                                    var tempNum = values[x];
                                    values[x] = values[i];
                                    values[i] = tempNum;
                                    rightNum = x - 1;
                                    break;
                                }
                                else
                                {
                                    var tempValues1 = new int[i];
                                    var tempValues2 = new int[values.Length - i];

                                    for (var idx = 0; idx < values.Length; idx++)
                                    {
                                        if (idx < i)
                                        {
                                            tempValues1[idx] = values[idx];
                                        }
                                        else
                                        {
                                            tempValues2[idx - i] = values[idx];
                                        }
                                    }

                                    QuickSort(tempValues1, desc);
                                    QuickSort(tempValues2, desc);

                                    for (var idx = 0; idx < values.Length; idx++)
                                    {
                                        if (idx < i)
                                        {
                                            values[idx] = tempValues1[idx];
                                        }
                                        else
                                        {
                                            values[idx] = tempValues2[idx - i];
                                        }
                                    }

                                    chabgeFlag = true;
                                }
                            }
                            if(chabgeFlag)
                            {
                                break;
                            }
                        }
                    }
                    else if (values[i] <= p && desc)
                    {
                        for (var x = rightNum; x >= 0; x--)
                        {
                            if (values[x] >= p)
                            {
                                if (i < x)
                                {
                                    var tempNum = values[x];
                                    values[x] = values[i];
                                    values[i] = tempNum;
                                    rightNum = x - 1;
                                    break;
                                }
                                else
                                {
                                    var tempValues1 = new int[i];
                                    var tempValues2 = new int[values.Length - i];

                                    for (var idx = 0; idx < values.Length; idx++)
                                    {
                                        if (idx < i)
                                        {
                                            tempValues1[idx] = values[idx];
                                        }
                                        else
                                        {
                                            tempValues2[idx - i] = values[idx];
                                        }
                                    }

                                    QuickSort(tempValues1, desc);
                                    QuickSort(tempValues2, desc);

                                    for (var idx = 0; idx < values.Length; idx++)
                                    {
                                        if (idx < i)
                                        {
                                            values[idx] = tempValues1[idx];
                                        }
                                        else
                                        {
                                            values[idx] = tempValues2[idx - i];
                                        }
                                    }

                                    chabgeFlag = true;
                                }
                            }
                            if (chabgeFlag)
                            {
                                break;
                            }
                        }
                    }
                    if (chabgeFlag)
                    {
                        break;
                    }
                }
            }
        }
    }
}
