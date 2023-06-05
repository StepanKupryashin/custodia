using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для UserMainWindow.xaml
    /// </summary>
    public partial class UserMainWindow : Window
    {

        public UserMainWindow()
        {
            InitializeComponent();

        }

  

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var reports = await Requester.Get("api/reports", null, true, (string)AppState.GetValue<string>("token"));
            foreach(var report in reports.response)
            {
                selectReport.Items.Add($"{report.id},{report.title}");
            }
            var user = await Requester.Get("api/user", null, true, (string)AppState.GetValue<string>("token"));
            usernameBox.Content = $"{user.last_name} {String.Copy((string)user.first_name)[0]}.{String.Copy((string)user.surname)[0]}";
            if(user.view_status == true)
            {
                guardBox.Content = "Доступ: Просмотр данных";
                selectReport.Visibility = Visibility.Visible;
                labelForSelect.Visibility = Visibility.Visible;
            }
            if(user.form_report == true)
            {
                guardBox.Content = "Доступ: Добавление данных";
                toCreateReport.Visibility = Visibility.Visible;
            }
        }

        private async void selectReport_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int reportId = Int32.Parse(selectReport.SelectedItem.ToString().Split(',')[0]);
            var report = await Requester.Get($"api/reports/{reportId}", null, true, (string)AppState.GetValue<string>("token"));
            report = report.response;
            reportBody.Content = report.body;
            titleReport.Content = report.title;
            reportCreater.Content = $"{report.user.name}\n {report.created_at}";
        }

        private void OpenCreateReportForm(object sender, RoutedEventArgs e)
        {
            CreationFormReport creationFormReport = new CreationFormReport();
            creationFormReport.Show();
            this.Close();
        }
    }
}
