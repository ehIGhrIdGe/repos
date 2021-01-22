using System;
using System.Collections.Generic;
using System.Text;
using SimpleAddressBook.Property;

namespace SimpleAddressBook.Processing
{
    class Process
    {
        public static void RunProcess()
        {
            var addressInfo = new AddressInfo();
            var addressList = new List<AddressInfo>();
            try
            {
                Console.Write("住所登録コンソールアプリです。");
                while(true)
                {
                    Console.Write("\n以下の説明を参考にコマンドを入力してください。\n" +
                    "*----------------------------------------------------------------------------------------------------*\n" +
                    " :add         => 新しい住所を登録します。\n" +
                    " :delete {no} => {no}部分で指定された番号の住所を削除します。番号は「:list」で確認してください。\n" +
                    " :list        => 登録した住所の一覧を表示します。\n" +
                    " :save        => 現在登録している住所をcsv形式で出力します。\n" +
                    " :load        => 指定のフォーマットにのっとったcsvファイルの住所データを読み取ります。\n" +
                    " :sort        => 項目名で指定された項目の昇順でデータを並び替えます。\n" +
                    " :exit        => プログラムを終了します。保存されていない住所データは失われます。\n" +
                    "*----------------------------------------------------------------------------------------------------*\n" +
                    "コマンドを入力：");

                    var textInfo = new TextInfo(Console.ReadLine().Trim());
                    Command.Execute(textInfo, addressList);
                }                
            }
            catch(Exception)
            {
                throw;
            }
            
        }
    }
}
