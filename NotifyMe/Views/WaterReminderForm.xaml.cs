using NotifyMe.Models;
using NotifyMe.Services;
using System.Windows;
using Application = System.Windows.Application;

namespace NotifyMe.Views
{
    public partial class WaterReminderForm : Window
    {
        public double LitrosAgua { get; private set; }
        public double Horas { get; private set; }
        public bool Configurado { get; private set; } = false;

        public WaterReminderForm()
        {
            InitializeComponent();
            this.Loaded += (s, e) => AtualizarIntervalo();
            UpdateLabels();
        }

        private void AtualizarIntervalo()
        {
            if (sliderLitros == null || sliderHoras == null)
                return;

            double litros = sliderLitros.Value;
            double horas = sliderHoras.Value;
            double copoMl = 250;      
            double copoLitros = copoMl / 1000.0;    

            int numeroLembretes = (int)(litros / copoLitros);

            int intervaloMinutos = (int)((horas * 60) / numeroLembretes);

            lblIntervalo.Text = $"Interval: {intervaloMinutos} min";    

           
        }


        private void SliderLitros_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblLitrosValue == null || sliderLitros == null)
                return;
            lblLitrosValue.Text = $"{sliderLitros.Value:F1}L";
            AtualizarIntervalo();
        }

        private void SliderHoras_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (lblHorasValue == null || sliderHoras == null)
                return;
            lblHorasValue.Text = $"{sliderHoras.Value:F0}h";
            AtualizarIntervalo();
        }


        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            double litros = sliderLitros.Value;
            double horas = sliderHoras.Value;

            int intervalMinutes = (int)((horas * 60) / (litros * 4));     

            DateTime firstReminder = DateTime.Now.AddMinutes(intervalMinutes);

            var reminder = new Reminder
            {
                Type = "Water",
                Details = $"{litros:F1}L over {horas:F0}h ({intervalMinutes} min interval)",
                Date = firstReminder,
                IntervaloMinutos = intervalMinutes,
                ProximoLembrete = DateTime.Now.AddMinutes(intervalMinutes)
            };

            ReminderStorage.SaveReminder(reminder);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.LoadRecentReminders();
            }

            System.Windows.MessageBox.Show($"Reminder saved! First popup at {firstReminder:HH:mm}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }



        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateLabels()
        {
            lblLitrosValue.Text = $"{sliderLitros.Value:F1}L";
            lblHorasValue.Text = $"{sliderHoras.Value:F0}h";
        }
    }
}
