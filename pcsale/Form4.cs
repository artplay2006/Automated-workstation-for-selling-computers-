using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace pcsale
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            FillDataGridView();
            guna2Button1.Visible = Form1.admin;
            guna2Button2.Visible = Form1.admin;
        }
        DataTable computers;
        private void FillDataGridView()
        {
            string connectionString = "URI=file:database.db";
            string query = "SELECT name, cpu, gpu FROM computers",query1= "SELECT * FROM computers";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "computers");
                    guna2DataGridView1.DataSource = dataSet.Tables["computers"];
                }
                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query1, connection))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet, "computers");
                    computers = dataSet.Tables["computers"];//нужно в computers закинуть таблицу computers, но я не знаю какого типа данных должна быть переменная computers
                }
            }
        }
        bool editclick = false;
        int idoccomputerForDelete = 0;
        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[e.RowIndex];
                // Обработка нажатия на строку
                // Вы можете получить доступ к данным ячеек строки, используя индексы столбцов или имена столбцов, например:
                string name = row.Cells["name"].Value.ToString();
                string cpu = row.Cells["cpu"].Value.ToString();
                string gpu = row.Cells["gpu"].Value.ToString();
                idoccomputerForDelete = Convert.ToInt32(computers.Rows[e.RowIndex]["id_of_computer"]);
                //MessageBox.Show(name + " " + cpu + " " + gpu);
                Form5.SetStaticPCInfo(name, cpu, gpu, computers.Rows[e.RowIndex]["ram"].ToString(), computers.Rows[e.RowIndex]["hdd"].ToString(), 
                    computers.Rows[e.RowIndex]["ssd"].ToString(), computers.Rows[e.RowIndex]["image"].ToString(), computers.Rows[e.RowIndex]["id_of_computer"].ToString());
                editclick = true;
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Form3().Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (idoccomputerForDelete == 0)
            {
                MessageBox.Show("пк для удаленияне не выбран");
            }
            else
            {
                string connectionString = "URI=file:database.db";
                string deletePc = "DELETE FROM computers WHERE id_of_computer=@Id;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand command = new SQLiteCommand(deletePc, connection))
                    {
                        command.Parameters.AddWithValue("@Id", idoccomputerForDelete);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                FillDataGridView();
                idoccomputerForDelete = 0;
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (editclick)
            {
                new Form5().Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("пк не выбран");
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {//добавление пк           
            new AddPC().Show();
            this.Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    //Application.Exit();
            //}
        }
    }
}