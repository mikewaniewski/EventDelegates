using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesAfter
{
    class Program
    {
        static void Main(string[] args)
        {

            //initialize counter object
            Counter counter = new Counter();

            //define which methods will handle CountStart and CountFinished events
            counter.CountStart += sendMessage;
            counter.CountFinished += sendMessage;

            //start counting
            counter.Count();

 
            Console.ReadLine();

        }

 
        public class CustomEventArgs : System.EventArgs
        {
            public DateTime eventDate { get; set; }
            public string message { get; set; }
        }


        static void sendMessage(object sender, CustomEventArgs e)
        {
            Console.WriteLine(
                String.Format("eventDate: {0}, message: {1}", e.eventDate, e.message));
        }

        public class Counter
        {

    
            public event EventHandler<CustomEventArgs> CountStart;
            public event EventHandler<CustomEventArgs> CountFinished;




            public void Count()
            {
    
                OnCountStart();



                for (int a = 0; a < 10; a++)
                {
                    Console.WriteLine(a);
                    System.Threading.Thread.Sleep(500);
                }

    
                OnCountFinished();
            }

            private void OnCountStart()
            {
    
                EventHandler<CustomEventArgs> delStart = CountStart;

                if (delStart != null)
                {
                 
                    delStart(this, new CustomEventArgs()
                    {
                        eventDate = System.DateTime.Now,
                        message = "Count Started!"
                    }
                    );
 
                }
            }


            private void OnCountFinished()
            {
                EventHandler<CustomEventArgs> delFinished = CountFinished;

                if (delFinished != null)
                {
                    delFinished(this, new CustomEventArgs()
                    {
                        eventDate = System.DateTime.Now,
                        message = "Count Finished!"
                    }
                    );
                }
            }
        }
    }
}
