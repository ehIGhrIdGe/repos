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
        public static void Execute(TextInfo textInfo, AddressInfo addressInfo)
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
                    switch (tempStrArr[0])
                    {
                        case ":add":
                            CmAdd(textInfo, addressInfo);
                            break;
                        case ":delete":
                            CmDelete(textInfo, addressInfo);
                            break;
                        case ":list":
                            CmList(addressInfo);
                            break;
                        case ":save":
                            CmSave(textInfo, addressInfo);
                            break;
                        case ":load":
                            CmLoad(textInfo, addressInfo);
                            break;
                        case ":sort":
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
        /// <param name="addressInfo"></param>
        private static void CmAdd(TextInfo textInfo, AddressInfo addressInfo)
        {
            while (true)
            {
                Console.Write("\n以下の順番でスペース区切りで入力をしてください。\n" +
                "順番：名前 年齢 電話番号（ハイフン無し） 住所（郵便番号無し）\n" +
                "*----------------------------------------------------------------------------------------------------*\n" +
                "例　：のび太　32　08055559999　東京都北区赤羽北1-2-5○○○マンション901号室\n" +
                "入力：");
                textInfo.Value = Console.ReadLine();

                //入力した内容のチェック（電話番号が正しいか、住所が正しいか等）。
                TextEdit.Execute(textInfo);

                if (string.IsNullOrWhiteSpace(textInfo.Value))
                {
                    break;
                }
            }
            addressInfo.AddressList.Add(textInfo.Values);
            Console.WriteLine($"以下の住所を登録しました。\n" +
                $"*----------------------------------------------------------------------------------------------------*\n" +
                $"{addressInfo.AddressList.Count} : {string.Join(" ", addressInfo.AddressList[addressInfo.AddressList.Count - 1])}\n");
        }

        private static void CmList(AddressInfo addressInfo)
        {
            if (addressInfo.AddressList.Count == 0)
            {
                Console.WriteLine("\n住所が登録されていません。「:add」で住所を登録してから「:list」を実行してください。");
            }
            else
            {
                Console.WriteLine("\n登録されている住所は以下になります。\n" +
                    "*----------------------------------------------------------------------------------------------------*\n");

                var tempListNum = 0;

                foreach (var x in addressInfo.AddressList)
                {
                    tempListNum = tempListNum + 1;
                    Console.WriteLine($"{tempListNum} : {string.Join(" ", x)}");
                }
            }
        }

        private static void CmSort(AddressInfo addressInfo)
        {
            
        }

        private static void CmSave(TextInfo textInfo, AddressInfo addressInfo)
        {
            if (addressInfo.AddressList.Count == 0)
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

                        foreach (var x in addressInfo.AddressList)
                        {
                            tempListNum = tempListNum + 1;
                            writer.WriteLine($"{string.Join(" ", x)}");
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

        private static void CmLoad(TextInfo textInfo, AddressInfo addressInfo)
        {
            while(true)
            {
                Console.Write("*----------------------------------------------------------------------------------------------------*\n" +
                "読み込むファイルのパスを入力してください。\n =>");

                textInfo.Value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(textInfo.Value))
                {
                    break;
                }

                Console.WriteLine("\n読み込みが完了しました。「:list」で確認してください。\n");
            }

            try
            {
                using (var reader = new StreamReader(textInfo.Value))
                {
                    
                    while(reader.Peek() >= 0)
                    {
                        addressInfo.AddressList.Add(reader.ReadLine().Split(" "));
                    }                    
                }
            }
            catch(OutOfMemoryException)
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

        private static void CmDelete(TextInfo textInfo, AddressInfo addressInfo)
        {
            if (addressInfo.AddressList.Count == 0)
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
                else if(Regex.IsMatch(tempStrArray[1], "all", RegexOptions.IgnoreCase))
                {
                    addressInfo.AddressList.Clear();
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
                        if (addressInfo.AddressList.Count >= x && x > 0)
                        {
                            Console.WriteLine("以下のアドレスを削除しました。\n" +
                            "*----------------------------------------------------------------------------------------------------*\n");

                            Console.WriteLine($"{x} : {string.Join(" ", addressInfo.AddressList[x - 1])}");

                            addressInfo.AddressList.RemoveAt(x - 1);
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
