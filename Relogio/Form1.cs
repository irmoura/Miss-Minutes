using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Relogio
{
    public partial class Form1 : Form
    {

        private Bitmap offScreenBitmap1;
        Graphics offScreenGraphics = null;
        SolidBrush brushOlhoBranco = new SolidBrush(Color.White);
        SolidBrush brushOlhoPreto = new SolidBrush(Color.Black);
        int xCenter = 0;
        int yCenter = 0;
        int ponteiroX = 0;
        int ponteiroY = 0;
        double L;//comprimento do ponteiro segundos
        double Lm; // comprimento do ponteiro minutos
        double Lh; // comprimento do ponteiro horas
        double theta = 0;
        int ponteiroXM = 0;
        int ponteiroYM = 0;
        int hourX = 0;
        int hourY = 0;
        bool eyeMove = false;

        public Form1()
        {
            InitializeComponent();
            // Obtém a segunda tela do sistema
            Screen screen = Screen.AllScreens[0];

            // Define a posição da janela na segunda tela
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = screen.Bounds.Location;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //offScreenGraphics = this.CreateGraphics();//e.Graphics;
            //
            //backgroundWorker1.RunWorkerAsync();
        }

        private (int, int, int, int) GetPosition(double posicaoSegundos)
        {
            double theta_ = (posicaoSegundos / 60.0) * 2 * Math.PI - Math.PI / 2;
            double xFinal = Convert.ToInt32(xCenter + L * Math.Cos(theta_));
            double yFinal = Convert.ToInt32(yCenter + L * Math.Sin(theta_));
            double angulo = Math.Atan2(yCenter - yFinal, xCenter - xFinal) * 180 / Math.PI;
            double radianos = angulo * Math.PI / 180;
            return (Convert.ToInt32(xFinal + (L / 6) * Math.Cos(radianos)), Convert.ToInt32(yFinal + (L / 6) * Math.Sin(radianos)), (int)xFinal, (int)yFinal);
        }

        private void Draw()
        {
            FillEllipse(Color.DarkOrange, xCenter, yCenter, this.ClientSize.Height - 6);
            //
            //Font font = new Font("Arial", 26, FontStyle.Regular);
            //Brush brush_ = Brushes.White;
            //offScreenGraphics.DrawString("FOR ALL TIME. ALWAYS", font, brush_, xCenter - 206, 10);
            //
            var resultado = GetPosition(0);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 30);//MARCAÇÃO 12 HORAS
            resultado = GetPosition(5);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 13 HORAS
            resultado = GetPosition(10);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 14 HORAS
            resultado = GetPosition(15);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 30);//MARCAÇÃO 15 HORAS
            resultado = GetPosition(20);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 16 HORAS
            resultado = GetPosition(25);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 17 HORAS
            resultado = GetPosition(30);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 30);//MARCAÇÃO 18 HORAS
            resultado = GetPosition(35);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 19 HORAS
            resultado = GetPosition(40);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 20 HORAS
            resultado = GetPosition(45);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 30);//MARCAÇÃO 21 HORAS
            resultado = GetPosition(50);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 22 HORAS
            resultado = GetPosition(55);
            DrawDiagonalLine(Color.Black, resultado.Item1, resultado.Item2, resultado.Item3, resultado.Item4, 10);//MARCAÇÃO 23 HORAS
            //
            //offScreenGraphics.FillEllipse(brushOlhoBranco, xCenter - 200, 300, 180, 300);//olho esquerdo - parte branca
            //offScreenGraphics.FillEllipse(brushOlhoBranco, xCenter + 20, 300, 180, 300);//olho direito - parte branca
            //if (!eyeMove)
            //{
            //    offScreenGraphics.FillEllipse(brushOlhoPreto, xCenter - 180, 320, 90, 150);//olho esquerdo - parte preta
            //    offScreenGraphics.FillEllipse(brushOlhoPreto, xCenter + 40, 320, 90, 150);//olho esquerdo - parte preta
            //    eyeMove = true;
            //}
            //else
            //{
            //    offScreenGraphics.FillEllipse(brushOlhoPreto, xCenter - 130, 320, 90, 150);//olho esquerdo - parte preta
            //    offScreenGraphics.FillEllipse(brushOlhoPreto, xCenter + 90, 320, 90, 150);//olho esquerdo - parte preta
            //    eyeMove = false;
            //}
            //
            DrawDiagonalLine(Color.Red, xCenter, yCenter, ponteiroX, ponteiroY, 5);
            //
            DrawDiagonalLine(Color.Black, xCenter, yCenter, ponteiroXM, ponteiroYM, 15);
            //
            DrawDiagonalLine(Color.Black, xCenter, yCenter, hourX, hourY, 20);
            //
            using (var graphics = this.CreateGraphics())
            {
                // copia o bitmap auxiliar para a tela
                graphics.DrawImage(offScreenBitmap1, 0, 0);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Invalidate();
            //
            var dateTime = DateTime.Now;
            int segundoAtual = dateTime.Second;
            int minutoAtual = dateTime.Minute;
            double hourAtual = dateTime.Hour + dateTime.Minute / 60.0;
            double thetaM = (minutoAtual / 60.0) * 2 * Math.PI - Math.PI / 2;
            ponteiroXM = Convert.ToInt32(xCenter + Lm * Math.Cos(thetaM));
            ponteiroYM = Convert.ToInt32(yCenter + Lm * Math.Sin(thetaM));
            //
            this.Text = $"X:{ponteiroX} | Y:{ponteiroY}";
            //
            theta = (segundoAtual / 60.0) * 2 * Math.PI - Math.PI / 2;
            ponteiroX = Convert.ToInt32(xCenter + L * Math.Cos(theta));
            ponteiroY = Convert.ToInt32(yCenter + L * Math.Sin(theta));
            //
            double thetaHour = (hourAtual / 12.0) * 2 * Math.PI - Math.PI / 2;
            hourX = Convert.ToInt32(xCenter + Lh * Math.Cos(thetaHour));
            hourY = Convert.ToInt32(yCenter + Lh * Math.Sin(thetaHour));
            //
            Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            offScreenBitmap1 = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap1);
            //
            xCenter = this.ClientSize.Width / 2;
            yCenter = this.ClientSize.Height / 2;
            //
            int segundoAtual = DateTime.Now.AddSeconds(11).Second;
            theta = (segundoAtual / 60.0) * 2 * Math.PI - Math.PI / 2;
            ponteiroX = Convert.ToInt32(xCenter + L * Math.Cos(theta));
            ponteiroY = Convert.ToInt32(yCenter + L * Math.Sin(theta));
            //
            L = (this.ClientSize.Height / 2) - 15;//comprimento do ponteiro segundos
            Lm = L / 1.35;//comprimento do ponteiro minutos
            Lh = L / 1.87;//comprimento do ponteiro horas
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //timer1.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Draw();
        }

        private void FillEllipse(Color color, int x, int y, int size)
        {
            Rectangle rectangle = new Rectangle(x - (size / 2), y - (size / 2), size, size);
            offScreenGraphics.FillEllipse(new SolidBrush(color), rectangle);
        }

        private void DrawDiagonalLine(Color color, int x1, int y1, int x2, int y2, int size = 1)
        {
            offScreenGraphics.DrawLine(new Pen(new SolidBrush(color), size), x1, y1, x2, y2);
        }
    }
}
