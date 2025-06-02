using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Markup;

namespace курсачь_Олег_важно.View
{
    /// <summary>
    /// Логика взаимодействия для FeedbackWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            if(login == "Admin" &&  password == "Admin")
            {
                MessageBox.Show("Успешно");
                DashboardWindow dashboardWindow = new DashboardWindow();
                dashboardWindow.Show();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

    }
}
