using TaskScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = TaskScheduler.Task;

namespace DemoTS
{
    public class MyTask : I_UserTask
    {

        public string taskName;
        public int duration;

        public void Run(Task t)
        {
            for (int i = 1; i <= duration; i++)
            {
                t.checkForInterrupt();
                //if cancel is called
                if (t.shouldCancel)
                {
                    break;
                }
                Console.WriteLine($" {taskName} is running ({i} s)");
                Thread.Sleep(1000);

                if (i == duration)
                {
                    string formattedTime = string.Format("{0:%h} hours {0:%m} minutes {0:ss\\.ff} seconds", DateTime.Now - t.startTime - t.pausedTime);

                    Console.WriteLine($"{taskName} is completed and it lasted {formattedTime}");
                    //Console.WriteLine($"{taskName} is completed");

                    Thread.Sleep(1000);
                }
            }

        }

    }
}

