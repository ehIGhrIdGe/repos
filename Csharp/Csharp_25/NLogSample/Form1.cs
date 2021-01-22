using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace NLogSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var log = LogManager.GetCurrentClassLogger();

            log.Trace("トレース用情報");
            log.Debug("デバッグ情報");
            log.Info("開始ボタン押された");
            //log.Warn("警告・注意");
            //log.Error("エラー情報");
            //log.Fatal("致命的なエラー情報");

            MessageBox.Show("実行しました。", "メッセージ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            var log = LogManager.GetCurrentClassLogger();

            log.Info("終了ボタン押された");
            //log.Warn("警告・注意");
            //log.Error("エラー情報");
            //log.Fatal("致命的なエラー情報");

            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var log = LogManager.GetCurrentClassLogger();

            //log.Trace("トレース用情報");
            //log.Debug("デバッグ情報");
            log.Info("アプリを開始しました");
            //log.Warn("警告・注意");
            //log.Error("エラー情報");
            //log.Fatal("致命的なエラー情報");
        }
    }
}
