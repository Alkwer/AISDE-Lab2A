using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace Lab2A
{
    public class Event
    {
        public double time { get; set; }
        public string type { get; set; }
        public Event(double mtime, string mtype)
        {
            time = mtime;
            type = mtype;           
        }
    }
    static class Program
    {
        static void Main(string[] args)
        {
            double Randomnumbergenerator(double time)
            {
                Poisson rand = new Poisson(10.0);
                double number;
                number = rand.Sample()+time;
                return number;
            }

            const int HIGH = 5;
            const int LOW = 1;
            const double pack_size = 2.5;

            double current_time = 0;
            double start_time = 0;
            double end_time = 150;
            double buffer = 0;

            int bandwidth = HIGH;

            string[] datatime = new string[200];
            string[] databuffer = new string[200];
            string[] databand = new string[200];
            int i = 0;

            List<Event> events = new List<Event>();
            Event estream = new Event(Randomnumbergenerator(current_time), "Zmiana strumienia");
            Event ebuffer = new Event(current_time + (pack_size / bandwidth), "Zmiana bufora");
            events.Add(estream);
            events.Add(ebuffer);

            Console.WriteLine(current_time*10 + " " + bandwidth + " " + buffer);
            datatime[i] = Convert.ToString(current_time);
            databuffer[i] = Convert.ToString(buffer);
            databand[i] = Convert.ToString(bandwidth);
            while (current_time < end_time)
            {
               
                events.Sort((x, y) => x.time.CompareTo(y.time));
                Event my_event = new Event(events[0].time, events[0].type);

                if (my_event.type == "Zmiana bufora")
                    start_time = current_time;

                current_time = my_event.time;

                if (my_event.type == "Zmiana strumienia")
                {
                    if (bandwidth == LOW)
                        bandwidth = HIGH;
                    else bandwidth = LOW;

                    events.Add(new Event(Randomnumbergenerator(current_time), "Zmiana strumienia"));
                }
                else if (my_event.type == "Zmiana bufora")
                {
                    buffer++;
                    buffer = buffer - (current_time - start_time);
                    if (buffer > 30) buffer = 30;
                    if (buffer < 0) buffer = 0;

                    events.Add(new Event(current_time + (pack_size / bandwidth), "Zmiana bufora"));
                }
               Console.WriteLine(current_time * 10 + " " + bandwidth + " " + buffer);
                i++;
                datatime[i] = Convert.ToString(Convert.ToInt32(current_time));
                databuffer[i] = Convert.ToString(Convert.ToInt32(buffer));
                databand[i] = Convert.ToString(Convert.ToInt32(bandwidth));

                events.Remove(events[0]);
            }
            System.IO.File.WriteAllLines(@"D:\czas.txt", datatime);
            System.IO.File.WriteAllLines(@"D:\bufor.txt", databuffer);
            System.IO.File.WriteAllLines(@"D:\pasmo.txt", databand);

            var guiForm = new Form1();
           
            guiForm.ShowDialog();

            Console.ReadKey();
        }
    }
}
