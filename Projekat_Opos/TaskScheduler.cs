using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace TaskScheduler
{
    public class TaskScheduler
    {

        private int maxConcurrentTasks = 1;
        private readonly object lock_ = new();

        private readonly HashSet<Task> runningTasks = new();
        private readonly PriorityQueue<Task, int> taskQueue = new();

        public TaskScheduler(int maxConcurrentTasks)
        {
            this.maxConcurrentTasks = maxConcurrentTasks;
        }

        public Task Schedule(TaskSpecification ts, bool startImmediately = false)
        {
            Task task = new Task(ts.task, LoadNewTask, ContinueTask, PostponeTask,ts.maxRunningTime,ts.priority,ts.startDate,ts.endDate,ts.taskName);
            lock (lock_)
            {
                if (startImmediately && runningTasks.Count < maxConcurrentTasks)
                {
                    runningTasks.Add(task);
                    task.Start();
                }
                else
                {
                    taskQueue.Enqueue(task, task.priority);
                }
            }

            return task;
        }

        private void LoadNewTask(Task task)
        {
            lock (lock_)
            {
                runningTasks.Remove(task);

                if (taskQueue.Count > 0)
                {
                    Task dequeuedTask = taskQueue.Dequeue();
                    runningTasks.Add(dequeuedTask);
                    dequeuedTask.Start();
                }
            }
        }
        private void ContinueTask(Task task)
        {
            lock (lock_)
            {
                if (runningTasks.Count < maxConcurrentTasks)
                {
                    runningTasks.Add(task);
                    Console.WriteLine($"{task.name} is continued");
                    task.Start();
                }
                else
                {
                    taskQueue.Enqueue(task, task.priority);
                }
            }
        }
        private void PostponeTask(Task task)
        {
            lock (lock_)
            {
                runningTasks.Remove(task);

                int waitTime = (int)(task.startDate - DateTime.Now).TotalMilliseconds;

                new Thread(() =>
                {
                    lock (lock_)
                    {
                        while (DateTime.Now < task.startDate)
                        {
                            Monitor.Wait(lock_, waitTime);
                        }
                        //if (runningTasks.Count < maxConcurrentTasks)
                        //{
                        //    runningTasks.Add(task);
                        //    Console.WriteLine($"{task.name} is continued");
                        //    task.Start();
                        //}
                        //else
                        //{
                        //    taskQueue.Enqueue(task, task.priority);
                        //}
                         ContinueTask(task);
                    }
                }).Start();

                Thread.Sleep(100);
                Monitor.Pulse(lock_);

            }
        }

        //    public static void PrintHashSet<T>(HashSet<T> runningTasks) 
        //    {
        //        foreach (T item in runningTasks)
        //        {
        //            Console.WriteLine("Running task: " + (item as Task)?.name);
        //        }
        //    }

    }

}
