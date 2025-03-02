using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NotifyMe.Models;

namespace NotifyMe.Services
{
    public static class ReminderStorage
    {
        private static readonly string filePath = "reminders.json";

        public static void SaveReminders(List<Reminder> reminders)
        {
            string json = JsonConvert.SerializeObject(reminders, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public static List<Reminder> LoadReminders()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Reminder>>(json);
            }
            return new List<Reminder>();
        }
    }
}
