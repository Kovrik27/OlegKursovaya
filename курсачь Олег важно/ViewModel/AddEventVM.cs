using System;
using System.Collections.Generic;
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
        public AddEventVM() 
        {
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
    }
}
