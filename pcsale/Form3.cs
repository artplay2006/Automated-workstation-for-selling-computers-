using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcsale
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            guna2Button4.Visible = Form1.admin;
            if (Form1.admin)
            {
                guna2Button2.Text = "ЗАКАЗЫ";
                guna2Button3.Text = "ПРОДАЖИ";
            }
            else
            {
                guna2Button2.Text = "МОИ ЗАКАЗЫ";
                guna2Button3.Text = "МОИ ПОКУПКИ";
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            new Form4().Show();
            this.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            new Zakazi().Show();
            this.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            new Prodazhi().Show();
            this.Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            new AddAdmin().Show();
            this.Close();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Close();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing)
            //{
            //    e.Cancel = true;
            //    //Application.Exit();
            //}
        }
    }
}
