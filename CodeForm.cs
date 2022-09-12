using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;

namespace WindowsFormsBot
{
    public partial class CodeForm : Form
    {
        private static string Token { get; set; } =
    "2021793118:AAGTNDL3llZ8ITzlPQuwvQQd0B8zmViDuM0";
        private static TelegramBotClient client;
        private static int code;
        public CodeForm()
        {
            client = new TelegramBotClient(Token);
            client.StartReceiving();
            InitializeComponent();
            client.StopReceiving();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CodeField.Text == Login.code.ToString())
            {
                MessageBox.Show("Вход выполнен");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Введен неверный код");
            }
        }
        private static int RandomNum()
        {
            Random rnd = new Random();
            int rndNum = rnd.Next(1000, 9999);
            return rndNum;
        }

        private void CodeForm_Load(object sender, EventArgs e)
        {
        }
    }
}
