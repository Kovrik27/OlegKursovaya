using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using курсачь_Олег_важно.Model;

namespace курсачь_Олег_важно.ViewModel
{
    public class DashboardVM : BaseVM
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
        public DashboardVM()
        {
            string sql = "SELECT * FROM Events";

            Events = new ObservableCollection<Events>(EventsRepository.Instance.GetAllEvents());
        }
    }
}

