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
using MySqlConnector;
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.ViewModel;

namespace курсачь_Олег_важно.View
{
    /// <summary>
    /// Логика взаимодействия для AddOrganizerWindow.xaml
    /// </summary>
    public partial class AddOrganizerWindow : Window
    {

        public AddOrganizerWindow()
        {
            InitializeComponent();
        }

        public AddOrganizerWindow(Organizer selectedOrganizer)
        {
            InitializeComponent();
            ((AddOrganizerVM)DataContext).SetEditOrganizer(selectedOrganizer);
        }
    }
}