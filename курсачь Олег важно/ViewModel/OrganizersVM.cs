﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.View;

namespace курсачь_Олег_важно.ViewModel
{
    public class OrganizersVM:BaseVM
    {
        private ObservableCollection<Organizer> organizers;
        public ObservableCollection<Organizer> Organizers
        {
            get => organizers;
            set
            {
                organizers = value;
                Signal();
            }
        }
        public Organizer organizer {  get; set; }

        public Organizer Organizer
        {
            get => organizer;
            set
            {
                organizer = value;
                Signal();
            }
        }
        public CommandVM OpenEditOrganizerWindow { get; set; }

        public CommandVM OpenAddOrganizerWindow
        {
            get; set;
        }  
               
        public OrganizersVM()
        {
            string sql = "SELECT * FROM Organizer";
               
            Organizers = new ObservableCollection<Organizer>(OrganizersRepository.Instance.GetAllOrganizers(sql));
            OpenAddOrganizerWindow = new CommandVM(() =>
            {
                AddOrganizerWindow addOrganizerWindow = new AddOrganizerWindow();
                addOrganizerWindow.Show();
            });
            OpenEditOrganizerWindow = new CommandVM(() =>
            {
                if (Organizer == null)
                    return;
                AddOrganizerWindow addOrganizerWindow = new AddOrganizerWindow(Organizer);
                addOrganizerWindow.Show();
            });
        }
    }
}
