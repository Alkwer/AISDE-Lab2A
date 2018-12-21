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
        public int type { get; set; }
        public Event(double mtime, int mtype)
        {
            time = mtime;
            type = mtype;           
        }
    }
    static class Program
    {
        enum TYPE {S,B};
        static void Main(string[] args)
        {
            double Randomnumbergenerator(double time)
            {
                Poisson rand = new Poisson(16.0);
                double number;
                number = rand.Sample() + time;
                return number;
            }

            const int HIGH = 6;
            const int LOW = 2;
            const double pack_size = 3;

            double current_time = 0;
            double start_time = 0;
            double end_time = 150;
            double time_t = 0;
            double buffer = 0;

            int bandwidth = HIGH;

            List<string> datatime = new List<string>();
            List<string> databuffer = new List<string>();
            List<string> databand = new List<string>();

            List<Event> events = new List<Event>();
            Event estream = new Event(Randomnumbergenerator(current_time), Convert.ToInt32(TYPE.S));
            Event ebuffer = new Event(current_time + (pack_size / bandwidth), Convert.ToInt32(TYPE.B));
            events.Add(estream);
            events.Add(ebuffer);

            Console.WriteLine(current_time + " " + bandwidth + " " + buffer);
            datatime.Add(Convert.ToString(current_time));
            databuffer.Add(Convert.ToString(buffer*10));
            databand.Add(Convert.ToString(bandwidth*10));
            while (current_time < end_time)
            {
               
                events.Sort((x, y) => x.time.CompareTo(y.time));
                Event my_event = new Event(events[0].time, events[0].type);

                if (my_event.type == 1)
                    start_time = current_time;
                if (my_event.type == 0)
                    time_t = my_event.time;
                else
                    current_time = my_event.time;
                
                if (my_event.type == 0)
                {
                    if (bandwidth == LOW)
                    {
                        bandwidth = HIGH;
                        //current_time = current_time + (pack_size / bandwidth);
                    }
                    else
                    {
                        bandwidth = LOW;
                        //current_time = current_time - (pack_size / bandwidth);
                    }

                    events.Add(new Event(Randomnumbergenerator(current_time), Convert.ToInt32(TYPE.S)));
                }
                else if (my_event.type == 1)
                {
                    buffer++;
                    buffer = buffer - (current_time - start_time);
                    if (buffer > 30) buffer = 30;
                    if (buffer < 0) buffer = 0;

                    events.Add(new Event(current_time + (pack_size / bandwidth), Convert.ToInt32(TYPE.B)));

                    datatime.Add(Convert.ToString(Convert.ToInt32(current_time)));
                    databuffer.Add(Convert.ToString(Convert.ToInt32(buffer * 10)));
                    databand.Add(Convert.ToString(Convert.ToInt32(bandwidth * 10)));
                }
               Console.WriteLine(current_time + " " + bandwidth + " " + buffer);
                /*datatime.Add(Convert.ToString(Convert.ToInt32(current_time)));
                databuffer.Add(Convert.ToString(Convert.ToInt32(buffer*10)));
                databand.Add(Convert.ToString(Convert.ToInt32(bandwidth*10)));
                */
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
