﻿using System;
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
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.ViewModel;

namespace курсачь_Олег_важно.View
{
    /// <summary>
    /// Логика взаимодействия для AddEventWindow.xaml
    /// </summary>
    public partial class AddEventWindow : Window
    {
        public AddEventWindow()
        {
            InitializeComponent();
        }

        public AddEventWindow(Events selectedEvent)
        {
            InitializeComponent();
            ((AddEventVM)DataContext).SetEditEvent(selectedEvent);
        }
    }
}
