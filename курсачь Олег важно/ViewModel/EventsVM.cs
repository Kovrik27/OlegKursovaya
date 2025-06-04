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



        public Events SelectedEvent { get; set; }
        public CommandVM OpenAddEventWindow
        {
            get; set;
        }

        public CommandVM OpenEditEventWindow
        {
            get; set;
        }

        public EventsVM() 
        {

            Events = new ObservableCollection<Events>(EventsRepository.Instance.GetAllEvents());

            OpenAddEventWindow = new CommandVM(() =>
            {
                AddEventWindow addEventWindow = new AddEventWindow();
                addEventWindow.Show();
            });

            OpenEditEventWindow = new CommandVM(() =>
            {
                if (SelectedEvent == null)
                    return;
                AddEventWindow addEventWindow = new AddEventWindow(SelectedEvent);
                addEventWindow.Show();
            });
        }

    }
}
