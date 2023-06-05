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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace custodia
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // выполнение запроса на бек
            var param = new Dictionary<string, string>
            {
                {"login", loginInput.Text},
                {"password",  paswordInput.Password},
                {"secret_word",  secretInput.Password},
            };
            try
            {
                var content = new FormUrlEncodedContent(param);
                var response = await Requester.Post("api/login", content);
                // сохранение токена 
                AppState.SetValue("token", (string)response.response.token);
                AppState.SetValue("UserType", UserType.Text);
                if(response.response.user.is_admin == true)
                {
                    AppState.SetValue("isAdmin", "1");
                }
                else
                {
                    AppState.SetValue("isAdmin", "0");
                }
                if(response.response.user.view_status == false)
                {
                    AppState.SetValue("accessDenied", "1");
                }
                else
                {
                    AppState.SetValue("accessDenied", "0");
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка");
                return;
            }
            if (AppState.GetValue<string>("token").Length > 0)
            {
                string userType = AppState.GetValue<string>("UserType");
                if(AppState.GetValue<string>("accessDenied") == "1")
                {
                    UserAccessDenied userAccessDenied = new UserAccessDenied();
                    userAccessDenied.Show();
                    this.Close();
                    return;
                }
                if (userType == "Админ" && AppState.GetValue<string>("isAdmin") == "1")
                {
                    AdminPanel adminPanel = new AdminPanel();
                    adminPanel.Show();
                    this.Close();

                }
                else
                {
                    UserMainWindow userMainWindow = new UserMainWindow();
                    userMainWindow.Show();
                    this.Close();

                }
                return;
            }
        }
    }
}
