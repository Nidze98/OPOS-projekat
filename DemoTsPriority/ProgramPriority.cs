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
            DateTime startDate = new DateTime(2023, 5, 6, 14, 51, 25);
            DateTime endDate = new DateTime(2023, 5, 5, 18, 14, 00);

            //PRIORYTY test
            //MyTask task1 = new MyTask { taskName = "Task 1", duration = 7, priority = 3 };

            //MyTask task2 = new MyTask { taskName = "Task 2", duration = 4, priority = 2 };

            //MyTask task3 = new MyTask { taskName = "Task 3", duration = 5, priority = 10 };
            //MyTask task4 = new MyTask { taskName = "Task 4", duration = 5, priority = 3 };
            //MyTask task5 = new MyTask { taskName = "Task 5", duration = 2, priority = 2 };


            MyTask task1 = new MyTask { taskName = "Task 1", duration = 7 };
            //MyTask task1 = new MyTask { taskName = "Task 1", duration = 7 };
            MyTask task2 = new MyTask { taskName = "Task 2", duration = 6 };

            MyTask task3 = new MyTask { taskName = "Task 3", duration = 5 };
            MyTask task4 = new MyTask { taskName = "Task 4", duration = 5 };
            MyTask task5 = new MyTask { taskName = "Task 5", duration = 2 };
            Thread.Sleep(2000);


            var Task1 = scheduler.Schedule(new TaskSpecification(task1, name: task1.taskName, priority:6));
            var Task2 = scheduler.Schedule(new TaskSpecification(task2, name: task2.taskName, priority: 3), true);
            var Task3 = scheduler.Schedule(new TaskSpecification(task3, name: task3.taskName, priority: 2), true);
            var Task4 = scheduler.Schedule(new TaskSpecification(task4, name: task4.taskName, priority: 1));
            var Task5 = scheduler.Schedule(new TaskSpecification(task5, name: task5.taskName, priority:4));

            // Task2.Pause();
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
          //  Task2.Continue();
            // task.RequestContinue();

        }
    }
}