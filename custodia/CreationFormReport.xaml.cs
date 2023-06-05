using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace custodia
{
    /// <summary>
    /// Логика взаимодействия для CreationFormReport.xaml
    /// </summary>
    public partial class CreationFormReport : Window
    {
        public CreationFormReport()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserMainWindow userMainWindow = new UserMainWindow();
            userMainWindow.Show();
            this.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var param = new Dictionary<string, string>
            {
                {"title", titleReport.Text},
                {"body",  bodyReport.Text},
            };
            var content = new FormUrlEncodedContent(param);
            var response = await Requester.Post("api/reports", content, true, (string)AppState.GetValue<string>("token"));
            MessageBox.Show("Отчёт успешно создан!");
        }
    }
}
