using System;

namespace NotifyMe.Models
{
    public class Reminder
    {
        public string Type { get; set; }
        public DateTime NextReminder { get; set; }
    }
}
