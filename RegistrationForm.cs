using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBot
{
    public partial class RegistrationForm : Form
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        public Boolean isUserExists()
        {
            DataBase db = new DataBase();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users1` WHERE `login` = @uL", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже занят, введите другой.");
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean isFieldsEmpty()
        {
            if (loginField.Text == "" || passwordField.Text == "")
            {
                MessageBox.Show("Заполните все поля");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (isUserExists() || isFieldsEmpty())
                {
                    return;
                }

                DataBase db = new DataBase();
                MySqlCommand command = new MySqlCommand("INSERT INTO `users1` (`login`, `password`, `tg_id`) VALUES (@login, @password, @tgid)", db.GetConnection());

                command.Parameters.Add("@login", MySqlDbType.VarChar).Value = loginField.Text;
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = passwordField.Text;
                command.Parameters.Add("@tgid", MySqlDbType.VarChar).Value = telegramField.Text;

                db.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт был создан");
                    this.Hide();
                    Login logForm = new Login();
                    logForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Аккаунт не был создан");
                }

                db.closeConnection();
            }
        }
    }
}
