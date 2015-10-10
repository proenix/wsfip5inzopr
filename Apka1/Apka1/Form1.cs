using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Apka1
{
    public partial class Form1 : Form
    {
        // Składniki
        System.Drawing.Graphics g1; // grafika
        System.Drawing.Pen p1;  // pióro
        
        // Metody

        

        // Eventy

        public Form1()
        {
            InitializeComponent();
            p1 = new System.Drawing.Pen(Color.Blue, 10);
            g1 = pictureBox1.CreateGraphics();

            p1.Color = Color.Green;
            p1.Width = 4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g1.DrawLine(p1, 1, 1, pictureBox1.Width, pictureBox1.Height);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // 
        }
    }
}
