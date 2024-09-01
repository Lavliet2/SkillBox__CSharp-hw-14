using System;

namespace Homework_12
{
    public class ActionLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string ActionLog { get; set; }
        //public string UserName { get; set; }

        public ActionLogEntry(string actionLog) 
        {  
            Timestamp = DateTime.Now;
            ActionLog = actionLog; 
        }

        //public ActionLogEntry (DateTime timestamp, string action, string userName)
        //{
        //    Timestamp = timestamp;
        //    Action = action;
        //    UserName = userName;
        //}
    }
}
