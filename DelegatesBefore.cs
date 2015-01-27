using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesBefore
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

        //define event handlers - delegates which will be used to handle appropriate events
        public delegate void CountStartedEventHandler(object sender, CustomEventArgs e);
        public delegate void CountFinishedEventHandler(object sender, CustomEventArgs e);

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

            //define events for start and finish
            //notice, that events return a delegate
            public event CountStartedEventHandler CountStart;
            public event CountFinishedEventHandler CountFinished;

            public void Count()
            {
                //call function that will handle delegate calls
                OnCountStart(); 
 


                for (int a = 0; a < 10; a++)
                {
                    Console.WriteLine(a);
                    System.Threading.Thread.Sleep(500);
                }

                //call function that will handle delegate calls
                //and pass the values
                OnCountFinished();  
            }

            private void OnCountStart()
            {
                //create a delegate
                CountStartedEventHandler delStart = CountStart;

                if (delStart != null)
                {
                    //this is where real event handling action takes place.
                    //appropriate method arguments are pushed through a delegate to 
                    //connected methods
                    //notice that this class knows nothing about this method
                    //it just pushes the parameter
                    //all necessary plumbing is connected during runtime.
                    //And this is good, because it provides encapsulation
                    //and allows this class to be used in different scenarios,
                    //without modifying it
                    delStart(this,  new CustomEventArgs(){ 
                        eventDate = System.DateTime.Now, 
                        message= "Count Started!"} 
                    );

                    //Our example is very simple and only pushes message to print.
                    //But if we take for instance - mousemove event:
                    //The same scenario will take place every time 
                    //system registers new mouse coordinates and pushes them
                    //as the event arguments
                    //to the appropriate method, which will decide what to do next.
                }
            }


            private void OnCountFinished()
            {
                CountFinishedEventHandler delFinished = CountFinished;

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