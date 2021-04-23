using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Design;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsDraw8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Bitmap img = new Bitmap(640, 200);
            Fractale1(img);
            pictureBox1.Image = img;
        }

        private void Fractale1(Bitmap img)
        {
            // Quelle fraktal.pas vom 26.02.1988
            // Variablen
            int i;      // Schritt
            int j;
            int n;

            int zeil;   // Zeilen ohne Border
            int spal;   // Spalten ohne Border
            int limit;  // Abbruchgrenze
            int zyklen; // Zyklenzahl

            float x, x1, x2, xd, a;
            float y, y1, y2, yd, b;
            float m;
            // Boolean switch;

            // Konstanten
            int spalten = 640;
            int zeilen = 200;
            int border = 0;

            // Daten einlesen ... bzw. setzen
            x1 = -1.0f;
            x2 = 2.8f;
            y1 = -1.5f;
            y2 = 1.5f;
            zyklen = 100;
            limit = 1000; // G ... Grenze

            zeil = zeilen - border - 1;
            spal = spalten - border - 1;

            // aus FormsDraw7
            int skalierung = 90;
            float r = 100.0f;

            PointF[] fractalPoints = { new PointF(0.0F, 0.0F) };

            // i = Bildnummer;
            // Bildname erzeugen
            Console.WriteLine("\n Start Fractale1, namespace FormsDraw8");
            Console.WriteLine("Variablen x1={0}  x2={1}  y1={2}  y2={3}", x1, x2, y1, y2);
            Console.WriteLine("Variablen Zyklen={0}  Limit={1}", zyklen, limit);

            i = border;

            zeil = zeilen - border - 1;
            spal = spalten - 2 * border - 1;

            yd = (y2 - y1) / zeil;
            xd = (x2 - x1) / spal;
            x = (x1 - xd); // Anfangswert

            // Init Graphics; Draw Border; ...

            //do while (i < spal + border + 1) // and not keypressed
            for (i = 1; (i < spal + border + 1); i++)
                {
                    x = x + xd;
                    y = y1 + yd;
                    j = border;
                        for (j = border; (i < zeilen); j++)
                        { 
                            y = y + yd;
                            a = x;
                            b = y;
                            n = 1;

                    do
                    {
                        m = (a + b) * (a - b) - x;
                        b = a * b;
                        b = b + b - y;
                        a = m;
                        n++;
                        // until(n > zyklen) or(abs(a) + abs(b) > limit)

                        // explot2(i, zeilen - j, n mod 16);

                        if ((x < y2) && (x > y1))
                        {
                            Console.WriteLine("plotxypunkt Wert: Schritt {0}, xi-Skalierung {1}, r-Skalierung {2}", i, x * skalierung, r); // skalierung

                            if (i == 0)
                            {
                                fractalPoints[0] = new PointF(x * skalierung, r);
                            }
                            else
                            {
                                Array.Resize<PointF>(ref fractalPoints, i + 1);
                                fractalPoints[i] = new PointF(x * skalierung, r * 100);
                            }
                            //pointsno++;
                        }


                        // Datenaufbereitung, Datenspeicherung in Array + Ausgabe
                    }
                    while ((n > zyklen) || ((Math.Abs(a) + Math.Abs(b) > limit)));
                 }
             }

            // Grafische Ausgabe je nach case-Nr.
            Pen pen8 = new Pen(Color.Red, 1); // rot

            for (n = 0; n < i; n++)
            {
                x = fractalPoints[n].X;
                r = fractalPoints[n].Y;
                Graphics.FromImage(img).DrawRectangle(pen8, x, r, 1, 1);   // pen8: schwarz Breite 1, Höhe 1
                Console.WriteLine("Grafikausgabe Punktnummer {0} nebst Werten {1} und {2}", n, x, r);
            }
            Console.WriteLine("Ende");

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Diese Methode wird von Form1.Designer.cs angestoßen
            Graphics g = e.Graphics;
            g.FillEllipse(new SolidBrush(Color.Black), 600, 190, 1, 1); // schwarze Ellipse
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
