using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace pcsale
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string login;
        public static bool admin;
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SQLiteConnection dbconnection = new SQLiteConnection("URI=file:database.db");
            try
            {
                dbconnection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE login = @Login;";
                using (SQLiteCommand cmd = new SQLiteCommand(query, dbconnection))
                {
                    cmd.Parameters.AddWithValue("@Login", guna2TextBox1.Text);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        using (SQLiteCommand passwordCmd = new SQLiteCommand("SELECT password FROM users WHERE login = @Login;", dbconnection))
                        {
                            passwordCmd.Parameters.AddWithValue("@Login", guna2TextBox1.Text);

                            object result = passwordCmd.ExecuteScalar();

                            if (result != null)
                            {
                                string password = result.ToString();
                                if (password == guna2TextBox2.Text)
                                {
                                    using (SQLiteCommand cr = new SQLiteCommand("SELECT role FROM users WHERE login = @Login;", dbconnection))
                                    {
                                        cr.Parameters.AddWithValue("@Login", guna2TextBox1.Text);
                                        string role = cr.ExecuteScalar().ToString();
                                        admin = role == "admin";
                                        //MessageBox.Show(admin.ToString());
                                    }

                                    login = guna2TextBox1.Text;
                                    new Form3().Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Непароль правильный.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("пароль не найден");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Данный логин не существует.");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Что то не сработало: {ex.Message}");
            }
            finally
            {
                dbconnection.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)//для того чтобы программа полностью закрылась
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    Application.Exit();
            //}
        }
    }
}
