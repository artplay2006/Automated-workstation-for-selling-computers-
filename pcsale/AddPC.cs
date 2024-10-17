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
using System.Xml.Linq;

namespace pcsale
{
    public partial class AddPC : Form
    {
        public AddPC()
        {
            InitializeComponent();
        }
        string imagepath;
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NamePCInput.Text))
            {
                MessageBox.Show("название пк не введено");
            }
            else if (string.IsNullOrEmpty(CpuInput.Text))
            {
                MessageBox.Show("название процессора не введено");
            }
            else if (string.IsNullOrEmpty(GpuInput.Text))
            {
                MessageBox.Show("название видеокарты не введено");
            }
            else if (string.IsNullOrEmpty(HddInput.Text))
            {
                MessageBox.Show("объем жесткого диска не введен");
            }
            else if (string.IsNullOrEmpty(RamInput.Text))
            {
                MessageBox.Show("объем оперативной памяти не введен");
            }
            else if (string.IsNullOrEmpty(SsdInput.Text))
            {
                MessageBox.Show("объем ssd не введен");
            }
            else if (string.IsNullOrEmpty(CountInput.Text))
            {
                MessageBox.Show("количество компьютеров не введено");
            }
            else if (!int.TryParse(CountInput.Text, out int c))
            {
                MessageBox.Show("количество должно быть числом");
            }
            else if (string.IsNullOrEmpty(imagepath))
            {
                MessageBox.Show("картинка не выбрана");
            }
            else
            {
                string connectionString = "URI=file:database.db";
                string insertPC = "INSERT INTO computers (name,cpu,gpu,ram,hdd,ssd,image,count) VALUES (@name, @cpu, @gpu, @ram, @hdd, @ssd, @image, @count)";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    try
                    {
                        using (SQLiteCommand command = new SQLiteCommand(insertPC, connection))
                        {
                            command.Parameters.AddWithValue("@name", NamePCInput.Text);
                            command.Parameters.AddWithValue("@cpu", CpuInput.Text);
                            command.Parameters.AddWithValue("@gpu", GpuInput.Text);
                            command.Parameters.AddWithValue("@ram", RamInput.Text);
                            command.Parameters.AddWithValue("@hdd", HddInput.Text);
                            command.Parameters.AddWithValue("@ssd", SsdInput.Text);
                            command.Parameters.AddWithValue("@image", imagepath);
                            command.Parameters.AddWithValue("@count", CountInput.Text);

                            command.ExecuteNonQuery();

                            MessageBox.Show("пк добавлен");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("пк с таким именем уже есть");
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog.Title = "Выберите изображение";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation=imagepath = openFileDialog.FileName;
            }
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
