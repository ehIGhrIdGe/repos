using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EChat.Models
{
    public class ManageHash
    {
        private static SHA256CryptoServiceProvider SHA256CSP { get; } = new SHA256CryptoServiceProvider();

        /// <summary>
        /// 文字列を SHA256CryptoServiceProvider を使ってハッシュ化する
        /// </summary>
        /// <param name="value">ハッシュ化したい文字列</param>
        /// <returns></returns>
        public static string GetHash(string value)
        {
            byte[] btPass = SHA256CSP.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToBase64String(btPass);
        }

        /// <summary>
        /// ランダムなハッシュ値を作成する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CreateHash()
        {
            byte[] btSalt = new byte[24];

            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(btSalt);
            }

            var hsSalt = Convert.ToBase64String(btSalt);
            
            return Convert.ToBase64String(btSalt);
        }

        /// <summary>
        /// ２つのハッシュ化された文字列を比べ、同一であれば True を返す。
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static bool CompareHashStr(string value1, string value2)
        {
            return Convert.FromBase64String(value1).SequenceEqual(Convert.FromBase64String(value2));
            //return value1.SequenceEqual(value2);
        }
    }
}
