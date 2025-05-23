﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.ViewModel;

namespace курсачь_Олег_важно.View
{
    /// <summary>
    /// Логика взаимодействия для AddFeedbackWindow.xaml
    /// </summary>
    public partial class AddFeedbackWindow : Window
    {

        public AddFeedbackWindow()
        {
            InitializeComponent();
        }

        public AddFeedbackWindow(Feedback selectedFeedback)
        {
            InitializeComponent();
            ((AddFeedbackVM)DataContext).SetEditFeedback(selectedFeedback);
        }
    }
}