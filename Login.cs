using Telegram.Bot;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBot
{
    public partial class Login : Form
    {
        public static int tg_id;
        public Login()
        {
            InitializeComponent();
        }

        public Boolean isFieldsEmpty()
        {
            if (loginField.Text == "" || passwordField.Text == "")
            {
                MessageBox.Show("Ошибка!Заполните все поля");
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int RandomNum()
        {
            Random rnd = new Random();
            int rndNum = rnd.Next(1000, 9999);
            return rndNum;
        }
        public static int code = RandomNum();

        private void button1_Click(object sender, EventArgs e)
        {
            var token = "2021793118:AAGTNDL3llZ8ITzlPQuwvQQd0B8zmViDuM0";
            var telegramUrl = "https://api.telegram.org/bot" + token;

            if (isFieldsEmpty())
            {
                return;
            }

            String loginUser = loginField.Text;
            String passUser = passwordField.Text;

            DataBase db = new DataBase();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users1` WHERE `login` = @uL AND `password` = @uP", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                tg_id = (int)table.Rows[0].ItemArray[3];

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(telegramUrl + "/sendMessage?chat_id=" + tg_id + "&text=" + code);
                HttpWebResponse responese = (HttpWebResponse)request.GetResponse();
                responese.Close();

                
                this.Hide();
                CodeForm verCodeForm = new CodeForm();
                verCodeForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Неизвестный пользователь ");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrationForm regForm = new RegistrationForm();
            regForm.ShowDialog();
        }
    }
}
