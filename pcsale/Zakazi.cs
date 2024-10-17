using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace pcsale
{
    public partial class Zakazi : Form
    {
        public Zakazi()
        {
            InitializeComponent();
            FillDataGridView();
            if (Form1.admin)
            {
                guna2Button1.Visible = true;
                guna2Button2.Visible = true;
            }
            else
            {
                guna2Button1.Visible = false;
                guna2Button2.Visible = false;
            }
        }
        int id_of_order;
        int id_of_computer;
        bool chechzakazclick = false;
        //DataTable table = new DataTable();
        private void FillDataGridView()
        {
            string connectionString = "URI=file:database.db";
            string query = Form1.admin ? "SELECT * FROM orders" : "SELECT * FROM orders WHERE login = @login";//name_of_computer, date, count

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter())
                {
                    adapter.SelectCommand = new SQLiteCommand(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@login", Form1.login);

                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "orders");
                    guna2DataGridView1.DataSource = dataSet.Tables["orders"];
                    //MessageBox.Show(dataSet.Tables["orders"].Rows[0]["login"].ToString());
                    guna2DataGridView1.Columns["login"].Visible = Form1.admin;
                    guna2DataGridView1.Columns["id_of_order"].Visible = Form1.admin;
                    guna2DataGridView1.Columns["id_of_computer"].Visible = Form1.admin;
                }
            }
        }
        private void Zakazi_Load(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                label3.Text = row.Cells["login"].Value.ToString();
                label4.Text = row.Cells["name_of_computer"].Value.ToString();
                label6.Text = row.Cells["date"].Value.ToString();
                label8.Text = row.Cells["count"].Value.ToString();
                id_of_order = Convert.ToInt32(row.Cells["id_of_order"].Value);
                string connectionString = "URI=file:database.db";
                string query = "SELECT id_of_computer FROM orders WHERE id_of_order=@Idorder",
                    query1= "SELECT image FROM computers WHERE id_of_computer=@Idpc";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Idorder", id_of_order);
                        id_of_computer = Convert.ToInt32(command.ExecuteScalar());
                    }
                    using (SQLiteCommand command = new SQLiteCommand(query1, connection))
                    {
                        command.Parameters.AddWithValue("@Idpc", id_of_computer);
                        pictureBox1.ImageLocation = command.ExecuteScalar().ToString();
                    }
                    connection.Close();
                }
                chechzakazclick = true;
            }
            else
            {
                MessageBox.Show("заказ не выбран в таблицеы");
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (chechzakazclick)
            {
                string connectionString = "URI=file:database.db";
                string insertProdazhi = "INSERT INTO sales (login,name_of_computer,date,count,id_of_computer) VALUES (@Login, @Name_of_computer, @Date, @Count,@Idpc)";
                string deleteZakaz = "DELETE FROM orders WHERE id_of_order=@Id;";

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(insertProdazhi, connection))
                    {
                        command.Parameters.AddWithValue("@Login", label3.Text);
                        command.Parameters.AddWithValue("@Name_of_computer", label4.Text);
                        command.Parameters.AddWithValue("@Date", label6.Text);
                        command.Parameters.AddWithValue("@Count", label8.Text);
                        command.Parameters.AddWithValue("@Idpc", id_of_computer);

                        command.ExecuteNonQuery();
                    }
                    using (SQLiteCommand command = new SQLiteCommand(deleteZakaz, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id_of_order);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                FillDataGridView();
            }
            else
            {
                MessageBox.Show("заказ не выбран");
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (chechzakazclick)
            {
                string connectionString = "URI=file:database.db";
                string deleteZakaz = "DELETE FROM orders WHERE id_of_order=@Id;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(deleteZakaz, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id_of_order);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                FillDataGridView();
            }
            else
            {
                MessageBox.Show("заказ не выбран");
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Zakazi_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    Application.Exit();
            //}
        }
    }
}
