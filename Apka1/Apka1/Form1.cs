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

            // bufor obiektów ruchomych, obiekt ruchomy jest instancją klasy fig1
            tabFig = new fig1[lfMax];
            int i;
            for (i = 0; i < lfMax; ++i)
            { tabFig[i] = new fig1(); }
            lf = 0; // licznik figur
            pk1 = new int[lfMax];  // utworzono lfmaxh obiektów dla obiektów ruchomych
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

            // tabFig[0] jest piłeczką numer 1
            // punk startu piłeczki nr 1
            tabFig[0].p[0].X = pictureBox1.Size.Width / 2;  // położenie piłeczki, współrzędna pozioma, zależna od położenia i szerokości lewego odbijacza
            tabFig[0].p[0].Y = pictureBox1.Size.Height /2;
            // Y - szerokość, wysokość elipsy
            // romiary i kolor piłeczki nr 1
            p1.Color = Color.Orange;
            tabFig[0].p[1].X = 50;
            tabFig[0].p[1].Y = 50;
            // kierunek i przesuniećie początkowe piłeczki nr 1
            tabFig[0].m = 1; // w prawo
            tabFig[0].k = 2; // renderuj dwa piksele
            tabFig[0].v = 0; // w górę
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
            // serwuj piłeczkę
            // narysuj piłeczkę w kolorze "kolor"
            // współrzędne piłeczki
            // współrzędna X = {x1, y2}   współrzędna Y = {x2, y2}
            // X - lewy góry róg kwadratu, w który będzie wpisana elipsa
            tabFig[0].p[0].X = pictureBox1.Size.Width / 2; 
            tabFig[0].p[0].Y = pictureBox1.Size.Height /2;

            p1.Color = Color.Orange;
            g1.DrawEllipse(p1, tabFig[0].p[0].X, tabFig[0].p[0].Y, tabFig[0].p[1].X, tabFig[0].p[1].Y);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            // scroll lewy
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
            public int m;  // 0 - lewo, 1 - prawo  kierunek ruchu piłeczki
            public int v;  // 0 - góra, 1 - dół  kierunek ruchu piłeczki 
            public int k;  // przesunięcie obiektu w pikselach

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

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            // scroll prawy
            int y;
            y = (pictureBox1.Height / 28) * trackBar2.Value;
            odbijacz(RR, pictureBox1.Height - y);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 1. usuń obiekt z picture box
            p1.Color = pictureBox1.BackColor;
            g1.DrawEllipse(p1, tabFig[0].p[0].X, tabFig[0].p[0].Y, tabFig[0].p[1].X, tabFig[0].p[1].Y);
  
            // 2. przesuń poziomo i pionowo obiekty ruchome
            // 2.1 kierunek zmieniany po kolizji z odbijaczem
            // 2.2 piłeczka nr 1
            if (tabFig[0].m == 1)  // sprawdzamy kierunek poziomy
            {
                tabFig[0].p[0].X = tabFig[0].p[0].X + tabFig[0].k;  // poziomo
            }
            else
            {
                tabFig[0].p[0].X = tabFig[0].p[0].X - tabFig[0].k;
            }

            if (tabFig[0].v == 1)  // sprawdzamy kierunek pionowy
            {
                tabFig[0].p[0].Y = tabFig[0].p[0].Y + tabFig[0].k;  // pionowo
            }
            else
            {
                tabFig[0].p[0].Y = tabFig[0].p[0].Y - tabFig[0].k;
            }

            // tabFig[0].p[0].Y = tabFig[0].p[0].Y + 20;
            // 3. sprawdź przekroczenie krawędzi prawej, lewej  piłeczki 1
            // if (tabFig[0].p[0].X > pictureBox1.Width) tabFig[0].p[0].X = 20;


            // if (tabFig[0].p[0].Y > pictureBox1.Height) tabFig[0].p[0].Y = 20;
            // 4. rysuj obiekt na picture box
            p1.Color = Color.DarkBlue;
            g1.DrawEllipse(p1, tabFig[0].p[0].X, tabFig[0].p[0].Y, tabFig[0].p[1].X, tabFig[0].p[1].Y);
           
            // 5.1 timer stop jeżeli piłeczka przekroczyła prawą krawędź
            // sprawdzamy położenie lewej krawędzi piłeczki nr 1 względem prawej krawędzi okienka
            //            zmienna   tabFig[0].p[0].X
            if (tabFig[0].p[0].X - (int)p1.Width / 2 >= pictureBox1.Width) timer1.Stop();
            // 5.2 timer stop jeżeli piłeczka przekroczyła lewą krawędź
            // sprawdzamy położenie prawej krawędzi piłeczki nr 1 względem lewej krawędzi okienka
            //            zmienna   tabFig[0].p[0].X
            if (tabFig[0].p[0].X + tabFig[0].p[1].X + (int)p1.Width / 2 <= 1) timer1.Stop();

            // 5.3 odbij piłeczkę nr 1 od górnej i dolnej krawędzi 
            if ((tabFig[0].p[0].Y - (int)p1.Width / 2 <= 1) || (tabFig[0].p[0].Y >= pictureBox1.Height ))
                if (tabFig[0].v == 1)
                    tabFig[0].v = 0;
                else
                    tabFig[0].v = 1;


            // 6.1 odbicie piłeczki nr 1 (prawa krawędź) od prawego odbijacza (lewa krwędź)
            if (/* wsp. X */ tabFig[0].p[0].X + tabFig[0].p[1].X + (int)p1.Width / 2 >= RR.X - (int)p1.Width / 2)
                /* wsp. Y - górna, dolna krwędź piłeczki */
                if ((/* górna krwędź piłeczki */ tabFig[0].p[0].Y - (int)p1.Width / 2 >= RR.Y - RR.height / 2 /* poniżej */  ) &&
                     (/* górna krwędź piłeczki */ tabFig[0].p[0].Y - (int)p1.Width / 2 <= RR.Y + RR.height / 2 /* powyżej */)
                   ||
                     (/* dolna krwędź piłeczki */
                      tabFig[0].p[0].Y + tabFig[0].p[1].Y <= RR.Y + RR.height / 2) &&
                     (tabFig[0].p[0].Y + tabFig[0].p[1].Y >= RR.Y - RR.height / 2)
                 )
                    //timer1.Stop();
                    // zmień kierunek piłeczki nr 1 na przeciwny
                    if (tabFig[0].m == 1)
                        tabFig[0].m = 0;
                    else
                        tabFig[0].m = 1;
              // koniec odbicia piłeczki nr 1
            // 6.2 odbicie piłeczki nr 1 (lewa krawędź) od lewa odbijacza (lewa krwędź)
            // tabFig[0].p[0].X - położenie poziome piłeczki,  tabFig[0].p[1].X - szerokość piłeczki, p1.Width - szerokość pióra
            if (/* wsp. X */ tabFig[0].p[0].X - (int)p1.Width / 2 <= LL.X + (int)p1.Width / 2)
                /* wsp. Y - górna, dolna krwędź piłeczki */
                if ((/* górna krwędź piłeczki */ tabFig[0].p[0].Y - (int)p1.Width / 2 >= LL.Y - LL.height / 2 /* poniżej */  ) &&
                     (/* górna krwędź piłeczki */ tabFig[0].p[0].Y - (int)p1.Width / 2 <= LL.Y + LL.height / 2 /* powyżej */)
                   ||
                     (/* dolna krwędź piłeczki */
                      tabFig[0].p[0].Y + tabFig[0].p[1].Y <= LL.Y + LL.height / 2) &&
                     (tabFig[0].p[0].Y + tabFig[0].p[1].Y >= LL.Y - LL.height / 2)
                 )
                    //timer1.Stop();
                    // zmień kierunek piłeczki nr 1 na przeciwny
                    if (tabFig[0].m == 1)
                        tabFig[0].m = 0;
                    else
                        tabFig[0].m = 1;
            // koniec odbicia piłeczki nr 1




        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();

        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Interval = 20 + 5 * trackBar3.Value;
            timer1.Start();
        }
    }
}
