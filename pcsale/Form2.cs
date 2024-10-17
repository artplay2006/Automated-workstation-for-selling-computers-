using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using System.Web.Security;

namespace pcsale
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        bool ValidationLogin(string login)
        {
            // Проверка длины логина
            if (login.Length > 8 || login.Length < 3)
            {
                return false;
            }

            // Проверка специальных символов
            Regex regex = new Regex("^[a-zA-Z0-9]*$"); // Регулярное выражение, разрешающее только буквы и цифры
            if (!regex.IsMatch(login))
            {
                return false;
            }

            // Логин прошел все проверки
            return true;
        }
        bool ValidationPassword(string password)
        {
            if (password.Length < 3) { return false; }
            else if (password.Length > 8) { return false; }
            return true;
        }
        private void label5_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!ValidationLogin(login.Text))
            {
                MessageBox.Show("Невалидынй логин. Логин должен состоять из 3-8 символов английского алфавита.");
            }
            else if (!ValidationPassword(password.Text))
            {
                MessageBox.Show("Невалидынй пароль. Пароль должен состоять из 3-8 символов.");
            }
            else if (password.Text != repassword.Text)
            {
                MessageBox.Show("Пароль несовподает с повторением пароля.");
            }
            else
            {
                //path = Application.dataPath + "/StreamingAssets/database.bytes.db";
                SQLiteConnection dbconnection = new SQLiteConnection("URI=file:database.db"/* + path*/);
                try
                {
                    dbconnection.Open();
                    string query = "INSERT INTO users (login,password,role) VALUES (@Login, @Password, @Role);";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, dbconnection))
                    {
                        // Задайте значения параметров
                        cmd.Parameters.AddWithValue("@Login", login.Text);
                        cmd.Parameters.AddWithValue("@Password", password.Text);
                        cmd.Parameters.AddWithValue("@Role", "user");

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Аккаунт упспешно зарегестрирован.");
                            new Form1().Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось добавить запись в таблицу users.");
                        }
                    }
                }
                catch/*(Exception ex)*/
                {
                    MessageBox.Show($"Пользователь с таким логином уже существует.");
                    Application.Exit();
                }
                finally 
                { 
                    dbconnection.Close();
                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    //Application.Exit();
            //    //new Form1().Close();
            //}
        }
    }
}
