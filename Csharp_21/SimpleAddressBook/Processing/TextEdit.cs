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
            }
            catch (Exception)
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
            //↓↓↓↓この「addressInfoPropNum（AddressInfoのプロパティの数を数える）」のためだけにAddressInfoクラスに空のコンストラクターを搭載したけどそれはOKなのか？
            var addressInfoPropNum = new AddressInfo().GetType().GetProperties().Length - 1;
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
                Func<string,bool> nows = string.IsNullOrWhiteSpace;
                string x = CheckName(textInfo) ?? CheckAge(textInfo) ?? CheckPhoneNumber(textInfo) ?? CheckAddress(textInfo) ?? string.Empty;
                return nows(x) ? true : false;

            }
        }

        private static string CheckName(TextInfo textInfo)
        {
            textInfo.Value = string.IsNullOrWhiteSpace(textInfo.Values[0]) ? "エラーです。\n名前が入力されていません。\n" : string.Empty;
            return textInfo.Value;
        }

        private static string CheckAge(TextInfo textInfo)
        {
            textInfo.Value = int.TryParse(textInfo.Values[1], out int x) || x >= 0 ? null : "エラーです。\n年齢が不正な値です。\n";
            return textInfo.Value;
        }

        private static string CheckPhoneNumber(TextInfo textInfo)
        {
            textInfo.Value = int.TryParse(textInfo.Values[2], out int x) || textInfo.Values[2].Length == 10 || textInfo.Values[2].Length == 11 ? null : "エラーです。\n数字以外の文字が含まれているか、桁数が間違っています。\n";
            return textInfo.Value;
        }

        private static string CheckAddress(TextInfo textInfo)
        {
            //var regexPattern = new Regex(@"(\p{IsCJKUnifiedIdeographs}|\p{IsCJKCompatibilityIdeographs}|\p{IsCJKUnifiedIdeographsExtensionA}|[\uD840-\uD869]|[\uDC00-\uDFFF]|\uD869|[\uDC00-\uDEDF])*[都道府県]
            //                               (\p{IsCJKUnifiedIdeographs}|\p{IsCJKCompatibilityIdeographs}|\p{IsCJKUnifiedIdeographsExtensionA}|[\uD840-\uD869]|[\uDC00-\uDFFF]|\uD869|[\uDC00-\uDEDF]|\p{IsHiragana})*[市区町村]
            //                               (\p{IsCJKUnifiedIdeographs}|\p{IsCJKCompatibilityIdeographs}|\p{IsCJKUnifiedIdeographsExtensionA}|[\uD840-\uD869]|[\uDC00-\uDFFF]|\uD869|[\uDC00-\uDEDF]|\p{IsHiragana}|\p{IsKatakana}|\d|-|ー|\p{Ll}|\p{Lu})*");
            //var regexPattern = new Regex(@"(\S)*[都道府県]");
            //var errorMsg = regexPattern.IsMatch(address) ? "" : "エラーです。\n年齢が不正な値です。\n";

            //正規表現は諦めて、とりあえず空白じゃなければいいや
            textInfo.Value = string.IsNullOrWhiteSpace(textInfo.Values[3]) ? "エラーです。\n年齢が不正な値です。\n" : null;
            return textInfo.Value;
        }

    }
}
