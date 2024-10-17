using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcsale
{
    public partial class Prodazhi : Form
    {
        public Prodazhi()
        {
            InitializeComponent();
            FillDataGridView();
        }
        int id_of_sale;
        private void FillDataGridView()
        {
            string connectionString = "URI=file:database.db";
            string query = Form1.admin ? "SELECT * FROM sales" : "SELECT * FROM sales WHERE login = @login";//name_of_computer, date, count

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
                {
                    adapter.SelectCommand = new SQLiteCommand(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@login", Form1.login);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "sales");
                    guna2DataGridView1.DataSource = dataSet.Tables["sales"];
                    guna2DataGridView1.Columns["login"].Visible = Form1.admin;
                    guna2DataGridView1.Columns["id_of_sale"].Visible = Form1.admin;
                    guna2DataGridView1.Columns["id_of_computer"].Visible = Form1.admin;
                }
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Close();
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                    label2.Text = row.Cells["login"].Value.ToString();
                    label5.Text = row.Cells["name_of_computer"].Value.ToString();
                    label7.Text = row.Cells["date"].Value.ToString();
                    label9.Text = row.Cells["count"].Value.ToString();
                    id_of_sale = Convert.ToInt32(row.Cells["id_of_sale"].Value);
                    string connectionString = "URI=file:database.db";
                    string query = "SELECT id_of_computer FROM sales WHERE id_of_sale=@Idsale",
                        query1 = "SELECT image FROM computers WHERE id_of_computer=@Idpc";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        int id_of_computer;
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Idsale", id_of_sale);
                            id_of_computer = Convert.ToInt32(command.ExecuteScalar());
                        }
                        using (SQLiteCommand command = new SQLiteCommand(query1, connection))
                        {
                            command.Parameters.AddWithValue("@Idpc", id_of_computer);
                            pictureBox1.ImageLocation = command.ExecuteScalar().ToString();
                        }
                        connection.Close();
                    }
                    //chechzakazclick = true;
                }
                else
                {
                    MessageBox.Show("заказ не выбран в таблицеы");
                }
            }
        }

        private void Prodazhi_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    Application.Exit();
            //}
        }
    }
}
