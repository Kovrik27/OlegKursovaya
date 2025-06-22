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
    public class AddParticipantVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private Participant participant = new();

        public Participant Participant
        {
            get => participant;
            set
            {
                participant = value;
                Signal();
            }
        }

        private Events selectedEvent;
        public Events SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                Signal();
            }
        }

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
        public AddParticipantVM()
        {
            Events = new ObservableCollection<Events>(EventsRepository.Instance.GetAllEvents());
            Save = new CommandVM(() =>
            {

                if (Participant.Id == 0)
                    ParticipantRepository.Instance.AddParticipantOnEvent(Participant, SelectedEvent.Id);
                else
                            ParticipantRepository.Instance.UpdateParticipant(Participant);

                ParticipantsWindow participantsWindow = new ParticipantsWindow();
                participantsWindow.Show();

            });

        }


        internal void SetEditParticipant(Participant selectedParticipant)
        {
            Participant = selectedParticipant;
        }
    }
}
