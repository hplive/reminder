using System;

namespace NotifyMe.Models
{
    public class Reminder
    {
        public string Type { get; set; }
        public string Details { get; set; }
        public DateTime Date { get; set; } // Data do lembrete
        public int IntervaloMinutos { get; set; } // Intervalo entre lembretes
        public DateTime ProximoLembrete { get; set; } // Próxima ocorrência do lembrete
    }
}

