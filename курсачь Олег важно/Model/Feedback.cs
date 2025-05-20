using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace курсачь_Олег_важно.Model
{
    public class Feedback
    {
        public int Id { get; set; }

        public int EventId  { get; set; }
        public Events Event { get; set; }
        public int ParticipantId { get; set; } 
        public Participant Participant { get; set; }
        
        public int Rating { get; set; }
        public string Comment { get; set; } 
    }
}
