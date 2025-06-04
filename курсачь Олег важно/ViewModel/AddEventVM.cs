using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using курсачь_Олег_важно.Model;

namespace курсачь_Олег_важно.ViewModel
{
    public class AddEventVM: BaseVM 
    {
        public CommandVM Save {  get; set; }
        public Events events = new Events();
        public Events Events
        {
            get => events; 
            set {  events = value; Signal(); }
        }

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

        public AddEventVM() 
        {
            string sql = "SELECT * FROM Organizer";

            Organizers = new ObservableCollection<Organizer>(OrganizersRepository.Instance.GetAllOrganizers(sql));

            Save = new CommandVM(()=>
            {
                if (Events.Id == 0)
                {
                    EventsRepository.Instance.AddEvent(Events);
                }
                else
                {
                    EventsRepository.Instance.UpdateEvent(Events);
                }
            });

        }

        internal void SetEditEvent(Events selectedEvent)
        {
            Events = selectedEvent;
        }

    }
}
