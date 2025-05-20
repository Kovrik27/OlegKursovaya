using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.View;

namespace курсачь_Олег_важно.ViewModel
{
    public class EventsVM : BaseVM
    {
        private ObservableCollection<Events> events;
        public ObservableCollection<Events> Events
        {
            get => events;
            set
            {
                events = value;
                Signal();
            }
        }
        public CommandVM OpenAddEventWindow
        {
            get; set;
        }

        public EventsVM() 
        {
            string sql = "SELECT * FROM Events";

            Events = new ObservableCollection<Events>(EventsRepository.Instance.GetAllEvents(sql));
            OpenAddEventWindow = new CommandVM(() =>
            {
                AddEventWindow addEventWindow = new AddEventWindow();
                addEventWindow.Show();
            });
        }

    }
}
