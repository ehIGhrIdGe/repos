using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Calculation
{
    class Program
    {
        static void Main()
        {
            var result = StartCalculation();

            Console.Write($"{result}\n入力を続ける場合：「Y」を入力\n終了する場合：「Y」以外を入力\n\n");

            var inputKey = Console.ReadKey(true);
            if (inputKey.Key == ConsoleKey.Y)
            {
                Main();
            }
            else
            {
                Console.WriteLine("終了します");
                Environment.Exit(0);
            }
        }

        static object StartCalculation()
        {
            var inputFormula = InputFormula();
            string[] allayFormula = inputFormula.Split(' ');
            List<string[]> listArrayFormula = ConvertListFormula(allayFormula);
            object objResult = 0;

            //リストに入れた式が括弧つきであれば、括弧内の式を1つずつ計算する。
            if (listArrayFormula.Count == 1)
            {
                objResult = CaluculationMechanism(listArrayFormula[0]);

                if (int.TryParse(objResult.ToString(), out int intResult) == false)
                {
                    return objResult;
                }
                else
                {
                    return $"\n答えは {intResult} です。";
                }
            }
            else
            {
                for (var i = listArrayFormula.Count - 1; i > 0; i--)
                {
                    objResult = CaluculationMechanism(listArrayFormula[i]);

                    if (int.TryParse(objResult.ToString(), out int tempResult) == false)
                    {
                        return objResult;
                    }

                    string tempStr = string.Join(" ", listArrayFormula[i - 1]);

                    tempStr = tempStr.Replace("x", objResult.ToString());
                    listArrayFormula.RemoveAt(i - 1);
                    listArrayFormula.Insert(i - 1, tempStr.Split(" "));
                }

                objResult = CaluculationMechanism(listArrayFormula[0]);

                if (int.TryParse(objResult.ToString(), out int intResult) == false)
                {
                    return objResult;
                }
                else
                {
                    return $"\n答えは {intResult} です。";
                }
            }
        }

        //input formula
        static string InputFormula()
        {
            Console.Write("数式を入力してください\n※ただし、数字と演算子は半角スペースで区切ること\n=>");
            string inputFormula = Console.ReadLine();

            string[] allayFormula = inputFormula.Split(' ');
            var checkResult = CheckFormura(inputFormula);

            while (checkResult != true)
            {
                if (checkResult != true)
                {
                    Console.Write("\n式が不正です。\nもう一度正しい式を入力してください。\n=>");
                    inputFormula = Console.ReadLine();
                }
                else
                {
                    break;
                }

                allayFormula = inputFormula.Split(' ');
                checkResult = CheckFormura(inputFormula);
            }

            return inputFormula;
        }


        static List<string[]> ConvertListFormula(string[] in_ArrayFormula)
        {
            var openIdxNum = 0;
            var closeIdxNum = 0;
            string[] arrayFormula = in_ArrayFormula;
            List<string> listFormula = new List<string>();
            List<string> tempListFormula = new List<string>();
            List<string[]> returnListFormula = new List<string[]>();

            while (true)
            {
                openIdxNum = Array.IndexOf(arrayFormula, "(");
                closeIdxNum = Array.LastIndexOf(arrayFormula, ")");

                listFormula.Clear();
                tempListFormula.Clear();
                listFormula.AddRange(arrayFormula);

                if (openIdxNum < 0)
                {
                    returnListFormula.Add(arrayFormula);
                    return returnListFormula;
                }
                else
                {
                    for (var i = openIdxNum + 1; i < closeIdxNum; i++)
                    {

                        tempListFormula.Add(arrayFormula[i].ToString());
                        listFormula.RemoveAt(openIdxNum + 1);

                    }

                    arrayFormula = tempListFormula.ToArray();

                    listFormula.RemoveRange(openIdxNum, 2);
                    listFormula.Insert(openIdxNum, "x");
                    returnListFormula.Add(listFormula.ToArray());
                }
            }
        }

        /// <summary>
        /// 計算したい式を入れる。答えを返す。
        /// </summary>
        /// <param name="in_ArrayFormula"></param>
        /// <returns></returns>
        static object CaluculationMechanism(string[] in_ArrayFormula)
        {
            var roopCnt = OutRoopCount(in_ArrayFormula);
            var numCnt = OutNumberCount(in_ArrayFormula);
            var smallerIdx = 0;

            //式の数値とそのインデックス番号を格納する２次元配列
            List<List<int>> numbersList = new List<List<int>>();
            numbersList = OutNumbersList(in_ArrayFormula);

            if (Array.IndexOf(in_ArrayFormula, "*") >= 0 || Array.IndexOf(in_ArrayFormula, "/") >= 0)
            {
                smallerIdx = OutSmallIndexInMultiOrDvision(in_ArrayFormula, 0);

                for (var i = 0; i < roopCnt; i++)
                {
                    if (smallerIdx == -1)
                    {
                        break;
                    }
                    else if (in_ArrayFormula[smallerIdx] == "*" && smallerIdx > 0)
                    {
                        var tempIdx = 0;

                        foreach (var item in numbersList)
                        {
                            if (item[0] == smallerIdx + 1)
                            {
                                numbersList[tempIdx - 1][1] = numbersList[tempIdx - 1][1] * item[1];

                                smallerIdx = OutSmallIndexInMultiOrDvision(in_ArrayFormula, smallerIdx + 1);

                                break;
                            }

                            tempIdx++;
                        }

                        numbersList.RemoveAt(tempIdx);
                    }
                    else if (in_ArrayFormula[smallerIdx] == "/" && smallerIdx > 0)
                    {
                        var tempIdx = 0;

                        foreach (var item in numbersList)
                        {
                            if (item[0] == smallerIdx + 1)
                            {
                                if (item[1] == 0)
                                {
                                    //ゼロ除算のため
                                    return "\nゼロでは割れません";
                                }
                                else
                                {
                                    numbersList[tempIdx - 1][1] = numbersList[tempIdx - 1][1] / item[1];

                                    smallerIdx = OutSmallIndexInMultiOrDvision(in_ArrayFormula, smallerIdx + 1);
                                }
                                break;
                            }

                            tempIdx++;
                        }

                        numbersList.RemoveAt(tempIdx);
                    }
                }
            }

            smallerIdx = OutSmallIndexInSumOrSub(in_ArrayFormula, 0);

            for (var i = 0; i < roopCnt; i++)
            {
                if (smallerIdx == -1)
                {
                    return numbersList[0][1];
                }
                else if (in_ArrayFormula[smallerIdx] == "+" && smallerIdx > 0)
                {
                    var tempIdx = 0;

                    foreach (var item in numbersList)
                    {
                        if (item[0] == smallerIdx + 1)
                        {
                            numbersList[tempIdx - 1][1] = numbersList[tempIdx - 1][1] + item[1];

                            smallerIdx = OutSmallIndexInSumOrSub(in_ArrayFormula, smallerIdx + 1);

                            break;
                        }

                        tempIdx++;
                    }

                    numbersList.RemoveAt(tempIdx);

                }
                else if (in_ArrayFormula[smallerIdx] == "-" && smallerIdx > 0)
                {
                    var tempIdx = 0;
                    foreach (var item in numbersList)
                    {
                        if (item[0] == smallerIdx + 1)
                        {
                            numbersList[tempIdx - 1][1] = numbersList[tempIdx - 1][1] - item[1];

                            smallerIdx = OutSmallIndexInSumOrSub(in_ArrayFormula, smallerIdx + 1);

                            break;
                        }
                        tempIdx++;
                    }
                    numbersList.RemoveAt(tempIdx);
                }
            }
            var result = numbersList[0][1];
            return result;
        }



        //「+」「-」のうち、常にインデックスが小さい方を返すため
        static int OutSmallIndexInSumOrSub(string[] in_ArrayFormula, int in_startIdx)
        {
            var sumIdx = Array.IndexOf(in_ArrayFormula, "+", in_startIdx);
            var subIdx = Array.IndexOf(in_ArrayFormula, "-", in_startIdx);

            if (sumIdx == -1)
            {
                return subIdx;
            }
            else if (subIdx == -1)
            {
                return sumIdx;
            }

            if (sumIdx > subIdx)
            {
                return subIdx;
            }
            else
            {
                return sumIdx;
            }
        }

        //「*」「/」のうち、常にインデックスが小さい方を返すため
        static int OutSmallIndexInMultiOrDvision(string[] in_ArrayFormula, int in_startIdx)
        {
            var multiIdx = Array.IndexOf(in_ArrayFormula, "*", in_startIdx);
            var divisionIdx = Array.IndexOf(in_ArrayFormula, "/", in_startIdx);

            if (multiIdx == -1)
            {
                return divisionIdx;
            }
            else if (divisionIdx == -1)
            {
                return multiIdx;
            }

            if (multiIdx > divisionIdx)
            {
                return divisionIdx;
            }
            else
            {
                return multiIdx;
            }
        }

        //Out list numbers
        static List<List<int>> OutNumbersList(string[] in_ArrayFormula)
        {
            //すべての数値のインデックスとその値を格納するための2次元配列
            List<List<int>> numbersList = new List<List<int>>();


            for (var i = 0; i < in_ArrayFormula.Length; i++)
            {
                var tempChk = int.TryParse(in_ArrayFormula[i], out int temp);

                if (tempChk == true)
                {
                    //インデックスとその値を格納するため
                    List<int> tempNumbersList = new List<int> { i, temp };

                    numbersList.Add(tempNumbersList);
                }
            }

            return numbersList;
        }

        //Out array numbers
        static int[] OutArrayNumbers(string[] in_ArrayFormula, int numCnt)
        {
            int[] arrayNumbers = new int[numCnt];
            var indexNum = 0;

            for (var i = 0; i < in_ArrayFormula.Length; i++)
            {
                var tempChk = int.TryParse(in_ArrayFormula[i], out arrayNumbers[indexNum]);

                if (tempChk == true)
                {
                    indexNum++;
                }
            }

            return arrayNumbers;
        }

        //Check roop count
        static int OutNumberCount(string[] in_ArrayFormula)
        {
            var numCnt = 0;
            for (var i = 0; i < in_ArrayFormula.Length; i += 2)
            {
                var item = in_ArrayFormula[i];
                if (int.TryParse(in_ArrayFormula[i], out int temp))
                {
                    numCnt += 1;
                }
            }

            return numCnt;
        }

        //Check roop count
        static int OutRoopCount(string[] in_ArrayFormula)
        {
            var roopCnt = 0;
            for (var i = 1; i < in_ArrayFormula.Length - 1; i += 2)
            {
                var item = in_ArrayFormula[i];
                if (item == "+" || item == "-" || item == "*" || item == "/")
                {
                    roopCnt += 1;
                }
            }

            return roopCnt;
        }


        /// <summary>
        /// 式のチェックを行い、式が正しければ、配列に格納したのものを返す（ようにする）
        /// </summary>
        /// <param name="in_StFormula"></param>
        /// <returns></returns>
        static bool CheckFormura(string in_StFormula)
        {
            string[] arrayFormula = in_StFormula.Split(' ');

            if (arrayFormula.Length == 1)
            {
                return false;
            }
            else if (arrayFormula.Length % 2 == 0)
            {
                return false;
            }

            /*以下は括弧の存在チェックとちゃんと閉じられているかをチェック。
             問題ないか括弧がなければ、（括弧を取り除き）後続の数字と演算子チェックのために式を配列に格納する*/
            if (Array.IndexOf(arrayFormula, "(", 0) < 0)
            {

            }
            else
            {
                var idxNum = 0;
                var openCount = 0;
                var closeCount = 0;
                var list = new List<string>();
                list.AddRange(arrayFormula);

                while (idxNum < arrayFormula.Length)
                {
                    idxNum = Array.IndexOf(arrayFormula, "(");
                    if (idxNum < 0)
                    {
                        idxNum = Array.IndexOf(arrayFormula, ")");
                        if (idxNum < 0)
                        {
                            if (openCount == closeCount)
                            {
                                arrayFormula = list.ToArray();
                                break;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            closeCount = closeCount + 1;
                            list.RemoveAt(idxNum);
                            arrayFormula = list.ToArray();
                        }
                    }
                    else
                    {
                        openCount = openCount + 1;
                        list.RemoveAt(idxNum);
                        arrayFormula = list.ToArray();
                    }
                }
            }

            var roopCnt = 0;
            var result = true;

            foreach (string item in arrayFormula)
            {
                result = int.TryParse(item, out int tempNum);

                //Check the validity of the formula
                if (result == true && (roopCnt == 0 || roopCnt % 2 == 0))
                {
                    roopCnt += 1;
                }
                else if (result == false && (roopCnt == 0 || roopCnt % 2 == 0))
                {
                    return false;
                }
                else if (result == true && roopCnt % 2 == 1)
                {
                    return false;
                }
                else if (result == false && roopCnt % 2 == 1)
                {
                    if (item == "+" || item == "-" || item == "*" || item == "/")
                    {
                        roopCnt += 1;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
