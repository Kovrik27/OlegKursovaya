using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсачь_Олег_важно.Model
{
    public class Participant
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<EventParticipants> EventParticipants { get; set; }

        public IEnumerable<Events> Events => EventParticipants?.Select(ep => ep.Event);

        public string EventsNames => Events != null ? string.Join(", ", Events.Select(e => e.Name)) : "";
    }
}
