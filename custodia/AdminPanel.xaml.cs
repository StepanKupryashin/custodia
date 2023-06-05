using Newtonsoft.Json;
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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private static async Task<dynamic> FetchUsers()
        {
            return await Requester.Get("api/user/all", null, true, AppState.GetValue<string>("token"));
        }

        private async void InitHandler(object sender, RoutedEventArgs e)
        {
            var response = await FetchUsers();
            MessageBox.Show($"{response}");
        }

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(UsersTable.ItemsSource != null) return;
            var response = await FetchUsers();
            UsersTable.ItemsSource = response.response;
            GuardsTable.ItemsSource = response.response;
        }

        private void UsersTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            //if (e.EditAction == DataGridEditAction.Commit)
            //{
            //    dynamic data = e.Row.DataContext;
            //    var el = e.EditingElement as TextBox;
            //    var p = el.Parent as DataGridCell;
            //    MessageBox.Show($"{data} {el.Text} {el.Name} {p.DataContext}");
            //}

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var param = UsersTable.ItemsSource;
            var content = new StringContent(param.ToString(), Encoding.UTF8, "application/json");

            MessageBox.Show($"{param} {content.ToString()}");
            var response = await Requester.Post("api/user/update_users", content, true, AppState.GetValue<string>("token"));

            }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var response = await FetchUsers();
            UsersTable.ItemsSource = response.response;
            GuardsTable.ItemsSource = response.response;
        }
    }
}
