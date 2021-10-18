using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.ServiceReference1;
using ConsoleApp2.ServiceReference2;

namespace ConsoleApp2
{
    class Program
    {
        static String format = "yyyy-MM-dd HH:mm:ss.fff";
        static void Main(string[] args)
        {
            String key = "";
            printMenu();
            key = Console.ReadLine();
            while(!key.Equals("0"))
            {
                if (key.Equals("1")){
                    NTP();
                }

                if (key.Equals("2"))
                {
                    Avg();
                }

                if (key.Equals("3"))
                {
                    Berkery();
                }

                printMenu();
                key = Console.ReadLine();
            }
            

        }

        private static void NTP()
        {
            int n = 4;
            DateTime[] data = new DateTime[n+1];
            for (int i=1; i <= 4; i++)
            {
                String s = Console.ReadLine();
                data[i] = DateTime.Parse(s);
            }

            long theta = (long)Math.Round(((data[2].Ticks - data[1].Ticks) + (data[3].Ticks - data[4].Ticks)) / 2.0, 0, MidpointRounding.AwayFromZero);
            Console.WriteLine(theta);
            Console.WriteLine(data[4].AddTicks(theta).ToString(format));
        }

        private static void Berkery()
        {

            int n = int.Parse(Console.ReadLine());
            DateTime host = DateTime.Parse(Console.ReadLine());
            DateTime[] client = new DateTime[n];
            long[] offset = new long[n + 1];
            offset[0] = 0;
            for (int i = 0; i < n; i++)
            {
                String s = Console.ReadLine();
                client[i] = DateTime.Parse(s);
                offset[i+1] = (client[i].Ticks - host.Ticks) / 10000;
                Console.WriteLine(offset[i+1]);
            }

            long avg = (long) Math.Round(offset.Average(), MidpointRounding.AwayFromZero);
           

            Console.WriteLine("====================");
          

            for (int i = 0; i< n+1; i++)
            {
                long millisecond = avg - offset[i];
                Console.WriteLine(millisecond);
            }

            Console.WriteLine(host.AddMilliseconds(avg).ToString(format));


        }


        private static void Avg()
        {
            
            Console.WriteLine("Enter n :");
            int n = int.Parse(Console.ReadLine());

            DateTime[] origin = new DateTime[n];

            for (int i = 0; i < n; i++)
            {
                String s = Console.ReadLine();
                origin[i] = DateTime.Parse(s);

            }

            Console.WriteLine("==============================================");

            for (int i = 0; i < n; i++)
            {
                long sum = 0;
                int count = 0;
                long min = long.MaxValue;
                long max = -1;

                for (int j = 0; j < n; j++)
                {

                    if (j != i)
                    {
                        if (origin[j].Ticks < min)
                        {
                            min = origin[j].Ticks;
                        }

                        if (origin[j].Ticks > max)
                        {
                            max = origin[j].Ticks;
                        }

                        sum += origin[j].Ticks;
                        count++;
                    }

                }
                count -= 2;
                sum = sum - max - min;
                // round with milliseconds
                long avg = (long)Math.Round(sum / (double)(count * 10000), MidpointRounding.AwayFromZero);
                avg = avg * 10000;
                Console.WriteLine(new DateTime(avg).ToString(format));
            }
        }

        private static void printMenu()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("===========================");
            Console.WriteLine("1.NTP");
            Console.WriteLine("2.Trung binh");
            Console.WriteLine("3.Berkery");
            Console.WriteLine("0.Exit");
        }

        private static long toLong(DateTime d)
        {
            return new DateTimeOffset(d).ToUnixTimeMilliseconds();
        }

        private static DateTime toDateTime(long p)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(p).DateTime;
           
        }
    }
}
