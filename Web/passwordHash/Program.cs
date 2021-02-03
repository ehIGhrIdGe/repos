using System;
using System.Security.Cryptography;
using System.Text;

namespace passwordHash
{
    class Program
    {
        private static SHA256CryptoServiceProvider SHA256CSP { get;} = new SHA256CryptoServiceProvider();

        static void Main(string[] args)
        {
            Console.Write(">");
            string password = Console.ReadLine();
            byte[] btSalt = new byte[24];

            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(btSalt);
            }

            byte[] btPass = SHA256CSP.ComputeHash(Encoding.UTF8.GetBytes(password));            

            var hsSalt = Convert.ToBase64String(btSalt);
            var hsPass = Convert.ToBase64String(btPass);

            Console.WriteLine($"{hsSalt} \r\n {hsSalt.Length}");
            Console.WriteLine($"{hsPass} \r\n {hsPass.Length}");

            string fullPass = password + hsSalt;
            Console.WriteLine($"{fullPass} \r\n {fullPass.Length}");    

            var tempBt = SHA256CSP.ComputeHash(Encoding.UTF8.GetBytes(fullPass));

            var hsFull = Convert.ToBase64String(tempBt);
            Console.WriteLine($"{hsFull} \r\n {hsFull.Length}");
        }
    }
}
