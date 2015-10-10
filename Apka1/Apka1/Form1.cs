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
        // składniki
        System.Drawing.Graphics g1;  // grafika
        System.Drawing.Pen p1; // pióro

        // bufory wspolrzednych punktow figur
        fig1[] tabFig;
        // odbijacze
        odb LL;
        odb RR;
        // licznik figur
        int lf;
        int lfMax = 10;
        // tablica przekierowań
        int[] pk1;
        int ipk1; // indeks tablicy przekierowan

        // metody
        public Form1()
        {
            InitializeComponent();
            p1 = new System.Drawing.Pen(Color.Red, 10);
            g1 = pictureBox1.CreateGraphics();  // przydział pikseli do grafiki

            // bufory wspolrzednych punktow krzywej
            tabFig = new fig1[lfMax];
            int i;
            for (i = 0; i < lfMax; ++i)
            { tabFig[i] = new fig1(); }
            lf = 0; // licznik figur
            pk1 = new int[lfMax];
            ipk1 = 0;

            // odbijacze, położenie początkowe
            LL = new odb();
            LL.X = 10;
            LL.Y = pictureBox1.Size.Height / 2;
            LL.pset();
            // LL.draw (g1,p1);
            RR = new odb();
            RR.X = pictureBox1.Size.Width - 10;
            RR.Y = pictureBox1.Size.Height / 2;
            RR.fig.c = Color.Orange;
            RR.pset();
        }

        private void odbijacz(odb o1, int y)
        {
            // usuń odbijacz
            p1.Color = pictureBox1.BackColor;
            o1.draw(g1, p1);
            // narysuj odbijacz
            p1.Color = o1.fig.c;
            o1.Y = y;
            o1.pset();
            o1.draw(g1, p1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g1.DrawLine(p1, 1, 1, pictureBox1.Width, pictureBox1.Height);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // scroll
            int y;
            y = (pictureBox1.Height / 28) * trackBar1.Value;
            odbijacz(LL, pictureBox1.Height - y);
        }

        // klasy dodatkowe
        public class odb
        {
            public int X;
            public int Y;
            public int width;
            public int height;
            public fig1 fig;

            public void pset()
            {
                fig.p[0].X = X; fig.p[1].X = X;
                fig.p[0].Y = Y - height / 2; fig.p[1].Y = Y + height / 2;
            }

            public void draw(System.Drawing.Graphics g1, System.Drawing.Pen pen1)
            {
                g1.DrawLine(pen1, fig.p[0], fig.p[1]);
            }

            public odb()
            {
                fig = new fig1();
                width = 4;
                height = 60;
                fig.c = Color.Purple;
            }
        }


        public class fig1
        {
            public System.Drawing.Point[] p;
            public char f;
            public Color c;
            public int m;  // 0 - lewo, 1 - prawo
            public int k;  // przesunięcie

            public fig1()
            {
                int i;
                p = new Point[4];
                for (i = 0; i < 4; ++i)
                { p[i].X = 0; p[i].Y = 0; }
                f = 'X';
                m = 1; k = 2;
                c = new Color();
            }

        }


    }
}
