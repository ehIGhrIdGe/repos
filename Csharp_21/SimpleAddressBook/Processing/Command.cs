using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SimpleAddressBook.Property;

namespace SimpleAddressBook.Processing
{
    class Command
    {
        public static void Execute(TextInfo textInfo)
        {
            try
            {
                if (!CommandCheck(textInfo))
                {
                    Console.WriteLine(textInfo.Value);
                }
                else
                {
                    var addressInfo = new AddressInfo();                    
                    switch (textInfo.Value)
                    {                        
                        case ":add":
                            CmAdd(textInfo, addressInfo);
                            break;
                        case ":delete":
                            break;
                        case ":list":
                            break;
                        case ":save":
                            break;
                        case ":load":
                            break;
                        case ":sort":
                            break;
                        case ":exit":
                            CmExit();
                            break;
                    }
                }                
            }
            catch(Exception)
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

            if(errorMsg.Length != 0)
            {
                textInfo.Value = errorMsg;
                return false;
            }
            else
            {
                return true;
            }            
        }

        private static void CmExit ()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 住所登録コマンド。入力内容のチェックとAddressInfo型への追加を同時に行う。
        /// </summary>
        /// <param name="textInfo"></param>
        /// <param name="addressInfo"></param>
        private static void CmAdd(TextInfo textInfo,AddressInfo addressInfo)
        {
            Console.Write("\n以下の順番でスペース区切りで入力をしてください。\n" +
                "順番：名前 年齢 電話番号（ハイフン無し） 住所（郵便番号無し）\n" +
                "*----------------------------------------------------------------------------------------------------*\n" +
                "例　：のび太　32　08055559999　東京都北区赤羽北1-2-5○○○マンション901号室\n" +
                "入力：");
            textInfo.Value = Console.ReadLine();

            //入力した内容のチェック（電話番号が正しいか、住所が正しいか等）。
            TextEdit.Execute(textInfo);

            //コマンドを実行            
        }

        private static void CmList()
        {

        }

        private static void CmSort()
        {

        }

        private static void CmSave()
        {

        }

        private static void CmLoad()
        {

        }

        private static void CmDelete()
        {

        }
    }
}
