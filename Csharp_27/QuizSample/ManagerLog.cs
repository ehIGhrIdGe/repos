using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace QuizSample
{
    static class ManagerLog
    {
        /// <summary>
        /// ログしたい内容の種類
        /// </summary>
        public enum LOGTYPE
        {
            Message,Delete,Update,Insert,Rollback
        }
            

        private static Logger Logger { get; set; }

        static ManagerLog()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 例外をログする
        /// </summary>
        /// <param name="inputException">詳細をログする例外。またこれを使うことで、ログ内でエラーログの記録部分が見つけやすい。</param>
        public static void Logging(Exception inputException)
        {
            Logger.Error("エラーログの出力開始");
            Logger.Error(inputException);
            Logger.Error($"エラーログの出力終了");
        }

        /// <summary>
        /// メッセージをログする
        /// </summary>
        /// <param name="logType">ログのタイプ</param>
        /// /// <param name="value">追加でログしたい内容（なくてもOK）</param>
        public static void Logging(LOGTYPE logType, string value="")
        {
            switch (logType)
            {
                case LOGTYPE.Message:
                    Logger.Info($"メッセージ : {value}");
                    break;
                case LOGTYPE.Delete:
                    Logger.Info($"削除 : {value}");
                    break;
                case LOGTYPE.Update:
                    break;
                case LOGTYPE.Insert:
                    break;
                case LOGTYPE.Rollback:
                    Logger.Info($"ロールバック :{value}");
                    break;
            }            
        }
    }
}
