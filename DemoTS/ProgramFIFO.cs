using System;
using System.Threading.Tasks;
using TaskScheduler;
using DemoTS;
using TaskScheduler = TaskScheduler.TaskScheduler;

namespace DemoTS
{
    class ProgramFIFO
    {
        static void Main(string[] args)
        {
            global::TaskScheduler.TaskScheduler scheduler = new global::TaskScheduler.TaskScheduler(2);
            TimeSpan ts = new TimeSpan(0, 0, 2);
            DateTime startDate = new DateTime(2023, 5, 8, 16,02, 25);
            DateTime endDate = new DateTime(2023, 5, 5, 18, 14, 00);


            MyTask task1 = new MyTask { taskName = "Task 1", duration = 7 };
            MyTask task2 = new MyTask { taskName = "Task 2", duration = 10000 };
            MyTask task3 = new MyTask { taskName = "Task 3", duration = 5 };
            MyTask task4 = new MyTask { taskName = "Task 4", duration = 5 };
            MyTask task5 = new MyTask { taskName = "Task 5", duration = 2 };
            Thread.Sleep(2000);
            
            var Task1 = scheduler.Schedule(new TaskSpecification(task1,startDate:startDate,name:task1.taskName), true);
            //  task.Wait();
            // task.RequestPause();
            //   task.Cancel();

            var Task2 = scheduler.Schedule(new TaskSpecification(task2,endDate:endDate,name:task2.taskName),true);
            var Task3 = scheduler.Schedule(new TaskSpecification(task3,maxRunningTime:ts, name: task3.taskName,startDate:startDate), true);
          
            Task2.Pause();
            Thread.Sleep(1000);
          //  Task1.Cancel();
            // task.Cancel();

            //task.RequestContinue();
       //     var Task4 = scheduler.Schedule(task4);
           // Task4.Wait();
         
            // Thread.Sleep(2000);
            //  task44.Wait();
            // scheduler.Schedule(task5 );


            Thread.Sleep(3000); 
            Task2.Continue();
            // task.RequestContinue();

        }
    }
}