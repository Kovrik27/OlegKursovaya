using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсачь_Олег_важно.Model
{
    public class EventParticipants
    {
        public int EventID { get; set; }
        public int ParticipantID { get; set; }

        public virtual Events Event { get; set; }
        public virtual Participant Participant
        {
            get; set;
        }
    }
}
