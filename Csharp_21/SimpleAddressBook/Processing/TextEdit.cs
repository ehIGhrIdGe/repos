using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using SimpleAddressBook.Property;

namespace SimpleAddressBook.Processing
{
    class TextEdit
    {
        public static void Execute(TextInfo textInfo)
        {
            try
            {
                Edit(textInfo);

                if (!CheckText(textInfo))
                {
                    Console.WriteLine(textInfo.Value);
                }
                else
                {

                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// スペースで区切り、配列に格納する。
        /// </summary>
        /// <param name="textInfo"></param>
        private static void Edit(TextInfo textInfo)
        {
            //半角全角どちらのスペースでもOKにするため
            textInfo.Values = Regex.Split(textInfo.Value, "[ 　]");
        }

        /// <summary>
        /// TextInfoのチェックを行い、値が不正な場合はエラーを投げる。
        /// </summary>
        /// <param name="textInfo"></param>
        /// <returns></returns>
        private static bool CheckText(TextInfo textInfo)
        {
            try
            {
                //↓↓↓↓この「addressInfoPropNum（AddressInfoのプロパティの数を数える）」のためだけにAddressInfoクラスに空のコンストラクターを搭載したけどそれはOKなのか？
                var addressInfoPropNum = new AddressInfo().GetType().GetProperties().Length;
                var errorMsg = "";
                errorMsg = string.IsNullOrWhiteSpace(textInfo.Value) ? "エラーです。何も入力されていません。\n" :
                           textInfo.Values.Length < addressInfoPropNum ? "エラーです。入力内容が不足しています。\n" :
                           textInfo.Values.Length > addressInfoPropNum ? "エラーです。入力内容が多すぎます。余分な空白などが入っていないか確認してください。\n" : errorMsg;

                if (errorMsg.Length != 0)
                {
                    textInfo.Value = errorMsg;
                    return false;
                }
                else
                {
                    //The following is a detailed check of each item
                    //CheckName(textInfo.Values[0]);
                    //CheckAge(textInfo.Values[1]);
                    //CheckPhoneNumber(textInfo.Values[2]);
                    CheckAddress(textInfo.Values[3]);

                    return true;
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        private static void CheckName(string name)
        {

            var errorMsg = string.IsNullOrWhiteSpace(name) ? "エラーです。\n名前が入力されていません。\n" : "";

            if(errorMsg.Length == 0)
            {
                throw new ArgumentNullException(errorMsg);
            }
        }

        private static void CheckAge(string age)
        {
            var errorMsg = int.TryParse(age, out int x) || x < 0 ? "エラーです。\n年齢が不正な値です。\n" : "";

            if (errorMsg.Length == 0)
            {
                throw new ArgumentNullException(errorMsg);
            }
        }

        private static void CheckPhoneNumber(string phoneNumber)
        {
            var errorMsg = int.TryParse(phoneNumber, out int x) || phoneNumber.Length < 10 || phoneNumber.Length > 11 ? "エラーです。\n数字以外の文字が含まれているか、桁数が間違っています。\n" : "";

            if (errorMsg.Length == 0)
            {
                throw new ArgumentNullException(errorMsg);
            }
        }

        private static void CheckAddress(string address)
        {
            //var regexPattern = new Regex(@"(\p{IsCJKUnifiedIdeographs}|\p{IsCJKCompatibilityIdeographs}|\p{IsCJKUnifiedIdeographsExtensionA}|[\uD840-\uD869]|[\uDC00-\uDFFF]|\uD869|[\uDC00-\uDEDF])*[都道府県]
            //                               (\p{IsCJKUnifiedIdeographs}|\p{IsCJKCompatibilityIdeographs}|\p{IsCJKUnifiedIdeographsExtensionA}|[\uD840-\uD869]|[\uDC00-\uDFFF]|\uD869|[\uDC00-\uDEDF]|\p{IsHiragana})*[市区町村]
            //                               (\p{IsCJKUnifiedIdeographs}|\p{IsCJKCompatibilityIdeographs}|\p{IsCJKUnifiedIdeographsExtensionA}|[\uD840-\uD869]|[\uDC00-\uDFFF]|\uD869|[\uDC00-\uDEDF]|\p{IsHiragana}|\p{IsKatakana}|\d|-|ー|\p{Ll}|\p{Lu})*");
            var regexPattern = new Regex(@"(\w)*[都道府県]
                                           (\w)*[市区町村]
                                           (\w|\d)*");
            var errorMsg = regexPattern.IsMatch(address) ? "" : "エラーです。\n年齢が不正な値です。\n";

            if (errorMsg.Length == 0)
            {
                throw new ArgumentNullException(errorMsg);
            }
        }

    }
}
