using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleAddressBook.Property;

namespace SimpleAddressBook.Processing
{
    class Command
    {
        public static void Execute(TextInfo textInfo, List<AddressInfo> addressInfos)
        {
            try
            {
                if (!CommandCheck(textInfo))
                {
                    Console.WriteLine(textInfo.Value);
                }
                else
                {
                    var tempStrArr = textInfo.Value.Split(" ");
                    switch (tempStrArr[0].ToLower())
                    {
                        case ":add":
                            CmAdd(textInfo, addressInfos);
                            break;
                        case ":delete":
                            CmDelete(textInfo, addressInfos);
                            break;
                        case ":list":
                            CmList(addressInfos);
                            break;
                        case ":save":
                            CmSave(textInfo, addressInfos);
                            break;
                        case ":load":
                            CmLoad(textInfo, addressInfos);
                            break;
                        case ":sort":
                            CmSort(textInfo, addressInfos);
                            break;
                        case ":exit":
                            CmExit();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static bool CommandCheck(TextInfo textInfo)
        {
            var regexCmds = new Regex(@":[(add)(list)(sort)(save)(load)(delete)(exit)]", RegexOptions.IgnoreCase);
            var errorMsg = "";
            errorMsg = regexCmds.IsMatch(textInfo.Value) ? errorMsg : errorMsg + "コマンドが不正です。コマンドが間違っています。";
            errorMsg = textInfo.Value.StartsWith(":") ? errorMsg : errorMsg + "先頭の「:」がありません。";

            if (errorMsg.Length != 0)
            {
                textInfo.Value = errorMsg;
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void CmExit()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 住所登録コマンド。入力内容のチェックとAddressInfo型への追加を同時に行う。
        /// エラーの場合は textInfo.Value にエラーの内容が入ってくる。
        /// </summary>
        /// <param name="textInfo"></param>
        /// <param name="addressInfos"></param>
        private static void CmAdd(TextInfo textInfo, List<AddressInfo> addressInfos)
        {
            while (true)
            {
                Console.Write("\n以下の順番でスペース区切りで入力をしてください。\n" +
                "順番：名前 年齢 電話番号（ハイフン無し） 住所（郵便番号無し）\n" +
                "*----------------------------------------------------------------------------------------------------*\n" +
                "例　：のび太　32　08055559999　東京都北区赤羽北1-2-5○○○マンション901号室\n" +
                "入力：");
                textInfo.Value = Console.ReadLine();

                //入力した内容のチェック（電話番号が正しいか、住所が正しいか等）。また、入力された内容を textInfo.Values[] に格納。
                TextEdit.Execute(textInfo);

                if (string.IsNullOrWhiteSpace(textInfo.Value))
                {
                    break;
                }
            }

            addressInfos.Add(new AddressInfo(textInfo.Values[0], textInfo.Values[1], textInfo.Values[2], textInfo.Values[3]));
            Console.WriteLine($"以下の住所を登録しました。\n" +
                $"*----------------------------------------------------------------------------------------------------*\n" +
                $"{addressInfos.Count} : {addressInfos[addressInfos.Count - 1].Name} {addressInfos[addressInfos.Count - 1].Age} {addressInfos[addressInfos.Count - 1].PhoneNumber} {addressInfos[addressInfos.Count - 1].Address}\n");
        }

        private static void CmList(List<AddressInfo> addressInfos)
        {
            if (addressInfos.Count == 0)
            {
                Console.WriteLine("\n住所が登録されていません。「:add」で住所を登録してから「:list」を実行してください。");
            }
            else
            {
                Console.WriteLine("\n登録されている住所は以下になります。\n" +
                    "*----------------------------------------------------------------------------------------------------*\n");

                var tempListNum = 0;

                foreach (var x in addressInfos)
                {
                    tempListNum = tempListNum + 1;
                    Console.WriteLine($"{tempListNum} : {x.Name} {x.Age} {x.PhoneNumber} {x.Address}");
                }
            }
        }

        private static void CmSort(TextInfo textInfo, List<AddressInfo> addressInfos)
        {
            if (addressInfos.Count == 0)
            {
                Console.WriteLine("住所が登録されていません。「:add」で住所を登録してから「:sort」を実行してください。");
            }
            else
            {
                var tempStrArray = textInfo.Value.Split(" ");

                if (tempStrArray.Length != 2)
                {
                    Console.WriteLine("余計な文字が入力されているか、ソートをかける項目名が不足しています。再入力してください。");
                }
                else
                {
                    switch (tempStrArray[1].ToLower())
                    {
                        case "name":
                            addressInfos.Sort(delegate (AddressInfo x, AddressInfo y)
                            {
                                if (x.Name == null && y.Name == null) return 0;
                                else if (x.Name == null) return -1;
                                else if (y.Name == null) return 1;
                                else return x.Name.CompareTo(y.Name);
                            });
                            break;

                        case "age":
                            addressInfos.Sort(delegate (AddressInfo x, AddressInfo y)
                            {
                                if (x.Age == null && y.Age == null) return 0;
                                else if (x.Age == null) return -1;
                                else if (y.Age == null) return 1;
                                else return x.Age.CompareTo(y.Age);
                            });
                            break;

                        case "phonenumber":
                            addressInfos.Sort(delegate (AddressInfo x, AddressInfo y)
                            {
                                if (x.PhoneNumber == null && y.PhoneNumber == null) return 0;
                                else if (x.PhoneNumber == null) return -1;
                                else if (y.PhoneNumber == null) return 1;
                                else return x.PhoneNumber.CompareTo(y.PhoneNumber);
                            });
                            break;

                        case "address":
                            addressInfos.Sort(delegate (AddressInfo x, AddressInfo y)
                            {
                                if (x.Address == null && y.Address == null) return 0;
                                else if (x.Address == null) return -1;
                                else if (y.Address == null) return 1;
                                else return x.Address.CompareTo(y.Address);
                            });
                            break;
                    }

                    Console.WriteLine("並び替えが完了しました。「:list」で確認してください。");
                }
            }            
        }

        private static void CmSave(TextInfo textInfo, List<AddressInfo> addressInfos)
        {
            if (addressInfos.Count == 0)
            {
                Console.WriteLine("\n住所が登録されていません。「:add」で住所を登録してから「:delete」を実行してください。");
            }
            else
            {
                Console.Write("*----------------------------------------------------------------------------------------------------*\n" +
                    "保存先のファイルのパスを入力してください。空白の場合は以下のフォルダへ保存されます。\n" +
                @"C:\Users\eij\source\repos\Csharp_21\SimpleAddressBook\TextFiles/default.txt" +
                "\n =>");

                textInfo.Value = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(textInfo.Value))
                {
                    textInfo.Value = @"C:\Users\eij\source\repos\Csharp_21\SimpleAddressBook\TextFiles/default.txt";
                }

                try
                {
                    using (var writer = new StreamWriter(textInfo.Value, false, Encoding.UTF8))
                    {
                        var tempListNum = 0;

                        foreach (var x in addressInfos)
                        {
                            tempListNum = tempListNum + 1;
                            writer.WriteLine($"{x.Name} {x.Age} {x.PhoneNumber} {x.Address}");
                        }

                        Console.WriteLine("書き込みが終了しました。\n");
                    }
                }
                catch (DirectoryNotFoundException)
                {
                    throw;
                }
                catch (FileNotFoundException)
                {
                    throw;
                }
                catch (IOException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private static void CmLoad(TextInfo textInfo, List<AddressInfo> addressInfos)
        {
            while (true)
            {
                Console.Write("*----------------------------------------------------------------------------------------------------*\n" +
                "読み込むファイルのパスを入力してください。\n =>");

                textInfo.Value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(textInfo.Value))
                {
                    break;
                }                
            }

            try
            {
                using (var reader = new StreamReader(textInfo.Value))
                {

                    while (reader.Peek() >= 0)
                    {
                        textInfo.Values = reader.ReadLine().Split(" ");
                        addressInfos.Add(new AddressInfo(textInfo.Values[0], textInfo.Values[1], textInfo.Values[2], textInfo.Values[3]));
                    }

                    Console.WriteLine("\n読み込みが完了しました。「:list」で確認してください。\n");
                }
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void CmDelete(TextInfo textInfo, List<AddressInfo> addressInfos)
        {
            if (addressInfos.Count == 0)
            {
                Console.WriteLine("住所が登録されていません。「:add」で住所を登録してから「:delete」を実行してください。");
            }
            else
            {
                var tempStrArray = textInfo.Value.Split(" ");

                if (tempStrArray.Length != 2)
                {
                    Console.WriteLine("余計な文字が入力されているか、番号が不足しています。再入力してください。");
                }
                else if (Regex.IsMatch(tempStrArray[1], "all", RegexOptions.IgnoreCase))
                {
                    addressInfos.Clear();
                    Console.WriteLine("現在登録されている住所をすべて削除しました。");
                }
                else
                {
                    var tempBool = int.TryParse(tempStrArray[1], out int x);

                    if (!tempBool)
                    {
                        Console.WriteLine("番号が入力されていません。再入力してください。");
                    }
                    else
                    {
                        if (addressInfos.Count >= x && x > 0)
                        {
                            Console.WriteLine("以下のアドレスを削除しました。\n" +
                            "*----------------------------------------------------------------------------------------------------*\n");

                            Console.WriteLine($"{x} : {addressInfos[x -1].Name} {addressInfos[x -1 ].Age} {addressInfos[x - 1].PhoneNumber} {addressInfos[x - 1].Address}");

                            addressInfos.RemoveAt(x - 1);
                        }
                        else
                        {
                            Console.WriteLine("指定の番号では住所が登録されていません。再入力してください。");
                        }
                    }
                }
            }
        }
    }
}
