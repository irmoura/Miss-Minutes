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

        Graphics graphics = null;
        Pen penMarcadorHora = new Pen(Color.Black);
        Pen penMarcadorHoraDiagonal = new Pen(Color.Black);
        SolidBrush brush = new SolidBrush(Color.DarkOrange);
        SolidBrush brushOlhoBranco = new SolidBrush(Color.White);
        SolidBrush brushOlhoPreto = new SolidBrush(Color.Black);
        Pen penS = new Pen(Color.Red);
        Pen penM = new Pen(Color.Black);
        Pen penH = new Pen(Color.Black);
        int xCenter = 0;
        int yCenter = 0;
        int ponteiroX = 0;
        int ponteiroY = 0;
        double L = 460; // comprimento do ponteiro segundos
        double Lm = 360; // comprimento do ponteiro minutos
        double Lh = 260; // comprimento do ponteiro horas
        double theta = 0;
        int ponteiroXM = 0;
        int ponteiroYM = 0;
        int hourX = 0;
        int hourY = 0;
        bool eyeMove = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            //
            Draw();
        }

        private void Draw()
        {
            int tamanhoD01 = 930;
            int tamanhoD02 = 85;
            //
            graphics.FillEllipse(brush, (this.Width / 2) - (tamanhoD01 - ((tamanhoD01 / 2) - 8)), (this.Height / 2) - (tamanhoD01 - ((tamanhoD01 / 2) - 7)), tamanhoD01, tamanhoD01);
            graphics.FillEllipse(brush, (this.Width / 2) - (tamanhoD02 - ((tamanhoD02 / 2) - 8)), (this.Height / 2) - (tamanhoD02 - ((tamanhoD02 / 2) - 7)), tamanhoD02, tamanhoD02);
            //
            Font font = new Font("Arial", 26, FontStyle.Regular);
            Brush brush_ = Brushes.White;
            graphics.DrawString("FOR ALL TIME. ALWAYS", font, brush_, xCenter - 206, 10);
            penMarcadorHora.Width = 30;
            penMarcadorHoraDiagonal.Width = 10;
            graphics.DrawLine(penMarcadorHora, 959, 56, 959, 126);//MARCAÇÃO 12 HORAS
            graphics.DrawLine(penMarcadorHora, 1349, 516, 1419, 516);//MARCAÇÃO 15 HORAS
            graphics.DrawLine(penMarcadorHora, 959, 906, 959, 976);//MARCAÇÃO 18 HORAS
            graphics.DrawLine(penMarcadorHora, 499, 516, 569, 516);//MARCAÇÃO 21 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 1154, 179, 1189, 118);//MARCAÇÃO 13 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 1296, 321, 1357, 286);//MARCAÇÃO 14 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 1296, 711, 1357, 746);//MARCAÇÃO 16 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 1154, 853, 1189, 914);//MARCAÇÃO 17 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 764, 853, 729, 914);//MARCAÇÃO 19 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 622, 711, 561, 746);//MARCAÇÃO 20 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 622, 321, 561, 286);//MARCAÇÃO 22 HORAS
            graphics.DrawLine(penMarcadorHoraDiagonal, 764, 179, 729, 118);//MARCAÇÃO 23 HORAS
            //
            graphics.FillEllipse(brushOlhoBranco, xCenter - 200, 300, 180, 300);//olho esquerdo - parte branca
            graphics.FillEllipse(brushOlhoBranco, xCenter + 20, 300, 180, 300);//olho direito - parte branca
            //
            if (!eyeMove)
            {
                graphics.FillEllipse(brushOlhoPreto, xCenter - 180, 320, 90, 150);//olho esquerdo - parte preta
                graphics.FillEllipse(brushOlhoPreto, xCenter + 40, 320, 90, 150);//olho esquerdo - parte preta
                eyeMove = true;
            }
            else
            {
                graphics.FillEllipse(brushOlhoPreto, xCenter - 130, 320, 90, 150);//olho esquerdo - parte preta
                graphics.FillEllipse(brushOlhoPreto, xCenter + 90, 320, 90, 150);//olho esquerdo - parte preta
                eyeMove = false;
            }
            //
            graphics.DrawLine(penS, xCenter, yCenter, ponteiroX, ponteiroY);
            //
            graphics.DrawLine(penM, xCenter, yCenter, ponteiroXM, ponteiroYM);
            //
            graphics.DrawLine(penH, xCenter, yCenter, hourX, hourY);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
            //
            var dateTime = DateTime.Now;
            int segundoAtual = dateTime.Second;
            int minutoAtual = dateTime.Minute;
            double hourAtual = dateTime.Hour + dateTime.Minute / 60.0;
            double thetaM = (minutoAtual / 60.0) * 2 * Math.PI - Math.PI / 2;
            ponteiroXM = Convert.ToInt32(xCenter + Lm * Math.Cos(thetaM));
            ponteiroYM = Convert.ToInt32(yCenter + Lm * Math.Sin(thetaM));
            //
            penH.Width = 20;
            penM.Width = 15;
            penS.Width = 5;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            xCenter = (this.Width / 2) - 9;
            yCenter = (this.Height / 2) - 8;
            //
            int segundoAtual = DateTime.Now.AddSeconds(11).Second;
            theta = (segundoAtual / 60.0) * 2 * Math.PI - Math.PI / 2;
            ponteiroX = Convert.ToInt32(xCenter + L * Math.Cos(theta));
            ponteiroY = Convert.ToInt32(yCenter + L * Math.Sin(theta));
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            //Cursor.Hide();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //timer1.Enabled = false;
        }
    }
}
