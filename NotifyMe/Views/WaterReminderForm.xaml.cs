using System.Windows;

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
        }

        private void Salvar_Click(object sender, RoutedEventArgs e)
        {
            LitrosAgua = sliderLitros.Value;
            Horas = sliderHoras.Value;
            Configurado = true;
            this.Close();
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
