using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NACAFosilGenerator
{
    internal class Program
    {
        //Chord length
        public static double chord = 1;

        //Maximum camber
        public static double m = 0.02f;

        //Position of the maximum camber
        public static double p = 0.4f;

        //Maximum thickness
        public static double th = 0.14f;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to use this tool made by Nitload");

            //get users' inout
            Console.WriteLine("Please type in the Chord length");
            double.TryParse(Console.ReadLine(), out chord);
            Console.WriteLine("Please type in the Maximum camber");
            double.TryParse(Console.ReadLine(), out m);
            Console.WriteLine("Please type in the Position of the maximum camber");
            double.TryParse(Console.ReadLine(), out p);
            Console.WriteLine("Please type in the Maximum thickness");
            double.TryParse(Console.ReadLine(), out th);

            Console.WriteLine("Press Enter to start!");
            Console.ReadLine();

            System.IO.StreamWriter file
                = new System.IO.StreamWriter(@"fosil.txt", false);
            file.Flush();

            Console.WriteLine("s\t\tx_up\t\t\ty_up\t\t\tx_down\t\t\ty_down");

            for (double s = 0; s < 1.11; s += 0.01)
            {
                Console.WriteLine(
                    s.ToString("0.00") + "\t\t" +
                    x_up(s).ToString("0.00000000") + "\t\t" +
                    y_up(s).ToString("0.00000000") + "\t\t" +
                    x_down(s).ToString("0.00000000") + "\t\t" +
                    y_down(s).ToString("0.00000000") );


            }
            Console.Read();
        }



            public static double yt(double s)
            {
                return th / 0.2  * ((0.2969 * Math.Sqrt(s)
                    - 0.1260 * (s)
                    - Math.Pow(0.3516 * (s), 2)
                    + Math.Pow(0.2843 * (s), 3)
                    - Math.Pow(0.1036 * (s), 4)));
            }

        public static double yc(double s)
            {
                double cache;
                if (s < p)
                {
                    cache = m / Math.Pow(p, 2) * (2 * p * s - Math.Pow(s, 2));
                }
                else
                {
                    cache = m / Math.Pow(1 - p, 2)
                        * ((1 - 2 * p) + 2 * p * s
                        - Math.Pow(s, 2));
                }
                return cache;
            }

        public static double theta(double s)
            {
                return Math.Atan(dycds(s));
            }

        public static double dycds(double s)
            {
                double cache;
                if (s < p)
                {
                    cache = 2 * m / Math.Pow(p, 2) * (p - s);
                }
                else
                {
                    cache = 2 * m / Math.Pow(1 - p, 2) * (p - s);
                }
                return cache;
            }

        public static double x_up(double s)
        {
            return chord * (s - yt(s) * Math.Sin(theta(s)));
            }

        public static double x_down(double s)
            {
                return chord * (s + yt(s) * Math.Sin(theta(s)));
            }

        public static double y_up(double s)
            {
                return chord * (yc(s) + yt(s) * Math.Cos(theta(s)));
            }

        public static double y_down(double s)
            {
                return chord*(yc(s) - yt(s) * Math.Cos(theta(s)));
            }
    }
}
