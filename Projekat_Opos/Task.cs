using TaskScheduler;
using System;
using System.Threading.Tasks;


namespace TaskScheduler
{
    public class Task
    {
        private enum TaskState
        {
            NotStarted,
            Running,
            RunningWithPauseRequest,
            WaitingToResume,
            Paused,
            Postponed,
            Cancelled,
            Finished
        }

        private TaskState state = TaskState.NotStarted;
        public string name;
        public readonly Thread thread;
        private readonly object lock_ = new();
        private readonly Action<Task> loadNewTask;

        private readonly Action<Task> continueTask;

        private readonly Action<Task> postponeTask;
        public bool shouldCancel  = false;
        internal int priority;

        private readonly SemaphoreSlim semFinished = new(0);

        private readonly SemaphoreSlim semResume = new(0);

        private int waiterTasks = 0;

        public TimeSpan pausedTime = TimeSpan.Zero;

        private DateTime pauseStartTime;

        public DateTime startDate;

        public DateTime endDate;

        public DateTime startTime;

        public TimeSpan runningTime;

        public TimeSpan maxRunningTime;
        // public bool postponed = false;
        public Task(I_UserTask user_task, Action<Task> loadNewTask, Action<Task> continueTask, Action<Task> postponeTask,
            TimeSpan maxRunningTime,int priority,DateTime startDate,DateTime endDate, string taskName)
        {
            thread = new(() =>
            {
                try
                {
                    user_task.Run(this);
                }
                finally
                {
                    Finish();
                }
            });


            this.name = taskName;
            this.loadNewTask = loadNewTask;
            this.continueTask = continueTask;
            this.startDate = startDate;
            this.postponeTask = postponeTask;
            this.maxRunningTime = maxRunningTime;
            this.endDate = endDate;
            this.priority = priority;
        }

        public void Start()
        {
            lock (lock_)
            {
                if (state == TaskState.NotStarted)
                {
                    if (DateTime.Now < startDate)
                    {
                        Console.WriteLine($" {name} is waiting for " + startDate);
                        state = TaskState.Postponed;
                        postponeTask(this);
                        return;
                    }
                    else
                    {

                        state = TaskState.Running;
                        startTime = DateTime.Now;
                        thread.Start();
                    }
                }
                else if (state == TaskState.WaitingToResume)
                {
                    state = TaskState.Running;
                    startTime = DateTime.Now;
                    semResume.Release();
                }
                else if (state == TaskState.Postponed)
                {
                    state = TaskState.Running;
                    startTime = DateTime.Now;
                    thread.Start();
                }
                else
                {
                    //  throw new InvalidOperationException("Greska");
                }
            }
        }
        public void Cancel()
        {
            lock (lock_)
            {
                if (state == TaskState.Running || state == TaskState.RunningWithPauseRequest || state==TaskState.Postponed)
                {
                    state = TaskState.Cancelled;
                    shouldCancel = true;
                }
                else
                {
                    // throw new InvalidOperationException("Greska");
                }

            }
        }
        public void Pause()
        {
            lock (lock_)
            {
                if (state == TaskState.Running)
                {
                    state = TaskState.RunningWithPauseRequest;
                }
                else
                {
                    // throw new InvalidOperationException("Greska");
                }
            }
        }
        public void Continue()
        {
            lock (lock_)
            {
                if (state == TaskState.RunningWithPauseRequest)
                {
                    state = TaskState.Running;
                    pausedTime += DateTime.Now - pauseStartTime;
                }
                else if (state == TaskState.Paused)
                {
                    state = TaskState.WaitingToResume;

                    continueTask(this);
                }
                else
                {
                    //throw new InvalidOperationException("Greska");
                }
            }
        }
        public void Finish()
        {
            lock (lock_)
            {
                if (state == TaskState.Running || state == TaskState.RunningWithPauseRequest || state == TaskState.Cancelled)
                {
                    if (state == TaskState.Cancelled)
                    {
                        Console.WriteLine($" {name} is canceled");
                    }
                    state = TaskState.Finished;
                    if (waiterTasks > 0)
                    {
                        semFinished.Release(waiterTasks);
                    }
                    loadNewTask(this);
                }
                else
                {
                    //throw new InvalidOperationException("Greska");
                }
            }
        }
        public void Wait()
        {
            lock (lock_)
            {
                if (state == TaskState.NotStarted || state == TaskState.RunningWithPauseRequest || state == TaskState.Running)
                {
                    waiterTasks++;
                }
                else if (state == TaskState.Finished)
                {
                    return;
                }

                else
                {
                    throw new InvalidOperationException("Greska");
                }
            }
            semFinished.Wait();
        }
        public void checkForInterrupt()
        {
            bool shouldPause = false;
            lock (lock_)
            {
                if (state == TaskState.Running)
                {
                    runningTime = DateTime.Now - startTime - pausedTime;
                    if ((maxRunningTime != TimeSpan.MinValue) && runningTime > maxRunningTime)
                    {
                       Console.WriteLine($"{name} exceded max running time");
                        Cancel();
                    }
                    if ((endDate != DateTime.MinValue) && DateTime.Now > endDate)
                    {
                       Console.WriteLine($"{name} has reached its end date.");
                        Cancel();
                    }
                }
                if (state == TaskState.RunningWithPauseRequest)
                {
                    state = TaskState.Paused;
                    pauseStartTime = DateTime.Now;
                    loadNewTask(this);
                    shouldPause = true;
                }
                else
                {
                    //throw new InvalidOperationException("Greska");
                }
            }
            if (shouldPause)
            {
                Console.WriteLine($"{name} is paused");
                semResume.Wait();
            }
        }

    }
}

