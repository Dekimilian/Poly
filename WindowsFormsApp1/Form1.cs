using System;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace WindowsFormsApp1
{


    public partial class Form1 : Form
    {

        
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
            Graphics g = CreateGraphics();
            Pen pen = new Pen(Color.Blue, 3);
            Rectangle rect = new Rectangle(10, 10,ClientSize.Width - 20, ClientSize.Height - 20);
            PointF[] RectanglePoints =
    {
        new PointF(rect.MidX(), rect.Y),
        new PointF(rect.Right, rect.MidY()),
        new PointF(rect.MidX(), rect.Bottom),
        new PointF(rect.Left, rect.MidY()),
    };
            var PointsNumber = (int)numericUpDown1.Value;
            var PolygonPoints = MakeRandomPolygon(PointsNumber,rect);
            
            g.DrawPolygon(pen, PolygonPoints);




           

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        

        public static PointF[] MakeRandomPolygon(int PointsNumber, Rectangle bounds)
        {
            // Выбор случайных радиусов.
                Random rand = new Random();
                double[] radii = new double[PointsNumber];
                double min_radius = 0.5;
                double max_radius = 1.0;
                for (int i = 0; i < PointsNumber; i++)
                {
                    radii[i] = rand.NextDouble(min_radius, max_radius);
                }

                // Выбор случайных угловых весов.

                double[] angle_weights = new double[PointsNumber];
                const double min_weight = 1.0;
                const double max_weight = 10.0;
                double total_weight = 0;
                for (int i = 0; i < PointsNumber; i++)
                {
                    angle_weights[i] = rand.NextDouble(min_weight, max_weight);
                    total_weight += angle_weights[i];
                }

                // Преобразование весов во фракции 2 * Pi радианов.

                double[] angles = new double[PointsNumber];
                double to_radians = 2 * Math.PI / total_weight;
                for (int i = 0; i < PointsNumber; i++)
                {
                    angles[i] = angle_weights[i] * to_radians;
                }

                // Вычислить местоположения точек.

                PointF[] Randompoints = new PointF[PointsNumber];
                float rx = bounds.Width /2f;
                float ry = bounds.Height /2f;
                float cx = bounds.MidX();
                float cy = bounds.MidY();
                double theta = 0;
                for (int i = 0; i < PointsNumber; i++)
                {
                
                    Randompoints[i] = new PointF(
                        cx + (int)(rx * radii[i] * Math.Cos(theta)),
                        cy + (int)(ry * radii[i] * Math.Sin(theta)));
                    theta += angles[i];
                }

                // Вернем точки.

                return Randompoints;
            
            
        }

        

    }
    

    public static class RectangleExtensions
    {
        public static int MidX(this Rectangle rect)
        {
            return rect.Left + rect.Width / 2;
        }
        public static int MidY(this Rectangle rect)
        {
            return rect.Top + rect.Height / 2;
        }
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.MidX(), rect.MidY());
        }

        public static float MidX(this RectangleF rect)
        {
            return rect.Left + rect.Width / 2;
        }
        public static float MidY(this RectangleF rect)
        {
            return rect.Top + rect.Height / 2;
        }
        public static PointF Center(this RectangleF rect)
        {
            return new PointF(rect.MidX(), rect.MidY());
        }
    }
    public static class RandomExtensions
    {
        // Return a random value between 0 inclusive and max exclusive.
        public static double NextDouble(this Random rand, double max_radius)
        {
            return rand.NextDouble() * max_radius;
        }

        // Return a random value between min inclusive and max exclusive.
        public static double NextDouble(this Random rand,
            double min_radius, double max_radius)
        {
            return min_radius + (rand.NextDouble() * (max_radius - min_radius));
        }
    }
}
