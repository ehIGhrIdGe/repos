using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMemo
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch(Exception e)
            {
                MessageBox.Show($"[GetType().Name]\n => {e.GetType().Name}\n\n" +
                    $"[Message]        \n=> {e.Message}\n\n" +
                    $"[StackTrace]     \n=> {e.StackTrace}\n\n" +
                    $"[Source]         \n=> {e.Source}\n\n" +
                    $"[InnerException] \n=> {e.InnerException}\n\n" +
                    $"[TargetSite]     \n=> {e.TargetSite}\n\n");
            }
            
        }
    }
}
