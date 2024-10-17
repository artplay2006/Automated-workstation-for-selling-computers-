using Guna.UI2.WinForms;
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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            if (Form1.admin)
            {
                TextForChange.Visible = true;
                guna2Button1.Visible = true;
                guna2CustomRadioButton1.Visible = true;
                guna2CustomRadioButton2.Visible = true;
                guna2CustomRadioButton3.Visible = true;
                guna2CustomRadioButton4.Visible = true;
                guna2CustomRadioButton5.Visible = true;
                guna2CustomRadioButton6.Visible = true;
                guna2ImageButton2.Visible = true;
            }
            else 
            {
                TextForChange.Visible = false;
                guna2Button1.Visible = false;
                guna2CustomRadioButton1.Visible = false;
                guna2CustomRadioButton2.Visible = false;
                guna2CustomRadioButton3.Visible = false;
                guna2CustomRadioButton4.Visible = false;
                guna2CustomRadioButton5.Visible = false;
                guna2CustomRadioButton6.Visible = false;
                guna2ImageButton2.Visible = false;
            }
            SetPCInfo();
        }
        public static string name;
        public static string cpu;
        public static string gpu;
        public static string ram;
        public static string hdd;
        public static string ssd;
        public static string image;
        public static int id;
        enum Components
        {
            Name,
            CPU,
            GPU,
            RAM,
            HDD,
            SSD,
            ImagePath
        }
        Components? changecomponent = null;
        public static void SetStaticPCInfo(string n,string c, string g, string r, string h, string s, string i, string _id)
        {
            name=n; cpu=c; gpu=g; ram=r; hdd=h; ssd=s; image=i; id = int.Parse(_id);
        }
        void SetPCInfo()
        {
            label11.Text = name;
            label6.Text = cpu;
            label10.Text = gpu;
            label8.Text = hdd;
            label9.Text = ram;
            label7.Text = ssd;
            label11.Text = name;
            pictureBox1.ImageLocation = image;
        }
        void UpdateComputer()
        {
            string connectionString = "URI=file:database.db";
            string updateQuery = "UPDATE computers SET name = @name, cpu = @cpu, gpu = @gpu, ram = @ram, hdd = @hdd, ssd = @ssd, image = @image WHERE id_of_computer = @computerId";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@cpu", cpu);
                    command.Parameters.AddWithValue("@gpu", gpu);
                    command.Parameters.AddWithValue("@ram", ram);
                    command.Parameters.AddWithValue("@hdd", hdd);
                    command.Parameters.AddWithValue("@ssd", ssd);
                    command.Parameters.AddWithValue("@image", image);
                    command.Parameters.AddWithValue("@computerId", id); // Замените 1 на идентификатор компьютера, который нужно обновить

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            label6.Text="asdsadas";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2CustomRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            changecomponent=Components.CPU;
        }

        private void guna2CustomRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            changecomponent = Components.GPU;
        }

        private void guna2CustomRadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            changecomponent = Components.HDD;
        }

        private void guna2CustomRadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            changecomponent = Components.RAM;
        }

        private void guna2CustomRadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            changecomponent = Components.SSD;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TextForChange.Text))
            {
                bool change = true;
                switch (changecomponent)
                {
                    case Components.Name:
                        name= TextForChange.Text;
                        break;
                    case Components.CPU:
                        cpu = TextForChange.Text;
                        break;
                    case Components.GPU:
                        gpu = TextForChange.Text;
                        break;
                    case Components.HDD:
                        hdd = TextForChange.Text;
                        break;
                    case Components.RAM:
                        ram = TextForChange.Text;
                        break;
                    case Components.SSD:
                        ssd = TextForChange.Text;
                        break;
                    case null:
                        MessageBox.Show("невыбран компонент для измения");
                        change = false;
                        break;
                    default:
                        MessageBox.Show("какой-то бреж");
                        change = false;
                        break;
                }
                if (change)
                {
                    MessageBox.Show("ща будет изменение в бд");
                    SetPCInfo();
                    UpdateComputer();
                }
            }
            else
            {
                MessageBox.Show("поле ввода измененного текста пустое");
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string connectionString = "URI=file:database.db";
            string updateQuery = "SELECT count FROM computers WHERE id_of_computer = @computerId";
            int countValue = 0;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@computerId", id); // Замените yourComputerId на фактический идентификатор компьютера

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        countValue = Convert.ToInt32(result);
                    }
                }
                connection.Close();
            }
            //MessageBox.Show(countValue.ToString());
            if(countValue > 0)
            {
                if (int.TryParse(BuyCount.Text, out int cb)/*&&cb>0&&cb<=countValue*/)
                {
                    if (cb > 0 && cb <= countValue)
                    {
                        updateQuery = "UPDATE computers SET count = @count WHERE id_of_computer = @computerId";
                        string insertZakaz = "INSERT INTO orders (login,name_of_computer,date,count,id_of_computer) VALUES (@Login, @Name_of_computer, @Date, @Count, @Idpc)";
                        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                        {
                            connection.Open();
                            using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@count", countValue - cb);
                                command.Parameters.AddWithValue("@computerId", id); // Замените 1 на идентификатор компьютера, который нужно обновить

                                command.ExecuteNonQuery();
                            }
                            using (SQLiteCommand command = new SQLiteCommand(insertZakaz, connection))
                            {
                                command.Parameters.AddWithValue("@Login", Form1.login);
                                command.Parameters.AddWithValue("@Name_of_computer", name);
                                command.Parameters.AddWithValue("@Date", DateTime.Now.ToString("dd.MM.yyyy"));
                                command.Parameters.AddWithValue("@Count", cb);
                                command.Parameters.AddWithValue("@Idpc", id);

                                command.ExecuteNonQuery();
                            }
                            connection.Close();
                        }
                        MessageBox.Show("пк заказан");
                        
                    }
                    else
                    {
                        MessageBox.Show("такого количества нет на складе");
                    }
                }
                else
                {
                    MessageBox.Show("количество введено не верно");
                }
            }
            else
            {
                MessageBox.Show("Их нет на складе");
            }
        }

        private void guna2CustomRadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            changecomponent = Components.Name;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Close();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog.Title = "Выберите изображение";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                // Здесь можете выполнить нужные действия с выбранным изображением
                // Например, загрузить изображение в PictureBox:
                image=pictureBox1.ImageLocation = selectedImagePath;
                MessageBox.Show(image);
                
            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    Application.Exit();
            //}
        }
    }
}
