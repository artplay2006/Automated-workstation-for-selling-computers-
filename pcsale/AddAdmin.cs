using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace pcsale
{
    public partial class AddAdmin : Form
    {
        public AddAdmin()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FutureAdmin.Text))
            {
                string connectionString = "URI=file:database.db";
                string checkLogin = "SELECT COUNT(*) FROM users WHERE login = @login;";
                string updateQuery = "UPDATE users SET role = @role WHERE login = @login";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(checkLogin, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", FutureAdmin.Text);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        if (count > 0)
                        {
                            string checkRole = "SELECT role FROM users WHERE login = @login";
                            string role = "";
                            using (SQLiteCommand command = new SQLiteCommand(checkRole, connection))
                            {
                                command.Parameters.AddWithValue("@login", FutureAdmin.Text);
                                role = command.ExecuteScalar().ToString();
                            }
                            if (role != "admin")
                            {
                                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@login", FutureAdmin.Text);
                                    command.Parameters.AddWithValue("@role", "admin");

                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                MessageBox.Show("он уже является админом");
                            }
                        }
                        else
                        {
                            MessageBox.Show("такого логина не существует");
                        }
                    }
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("такого ника нету");
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Close();
        }

        private void AddAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    //Application.Exit();
            //}
        }
    }
}
