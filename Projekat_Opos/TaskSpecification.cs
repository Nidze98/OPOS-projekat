using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler
{
    public class TaskSpecification
    {
        public I_UserTask task;
        public string taskName;
        public int duration;
        public int priority;
        public DateTime startDate;
        public DateTime endDate;
        public TimeSpan maxRunningTime;
        public TaskSpecification(I_UserTask task,TimeSpan maxRunningTime=default, int priority = 0, DateTime startDate = default,
            DateTime endDate = default, string name="Task")
        {
            this.task=task;
            this.maxRunningTime = maxRunningTime == default ? TimeSpan.MinValue : maxRunningTime;
            this.priority = priority;
            this.startDate = startDate == default ? DateTime.MinValue : startDate;
            this.endDate = endDate == default ? DateTime.MinValue : endDate;
            this.taskName = name;
        }
     
    }
}
