using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсачь_Олег_важно.Model
{
    public class Events
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Location { get; set; }
        public int OrganizerId  { get; set; }
        public Organizer Organizer { get; set; }
    }
}
