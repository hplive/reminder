using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NotifyMe.Models;

public static class ReminderStorage
{
    private static readonly string filePath = "reminders.json";

    // Método para guardar um lembrete no histórico
    public static void SaveReminder(Reminder reminder)
    {
        List<Reminder> reminders = LoadReminders();
        reminders.Add(reminder);

        string json = JsonConvert.SerializeObject(reminders, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    // Método para carregar lembretes armazenados
    public static List<Reminder> LoadReminders()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Reminder>>(json) ?? new List<Reminder>();
        }

        return new List<Reminder>();
    }

    // Método para obter os últimos lembretes
    public static List<Reminder> GetLastReminders(int count)
    {
        List<Reminder> reminders = LoadReminders();
        return reminders.Count > count ? reminders.GetRange(reminders.Count - count, count) : reminders;
    }
}
