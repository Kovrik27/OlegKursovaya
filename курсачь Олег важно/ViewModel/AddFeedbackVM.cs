using Microsoft.Extensions.Logging;
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
   public class AddFeedbackVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private Feedback feedback = new();

        public Feedback Feedback
        {
            get => feedback;
            set
            {
                feedback = value;
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

        private Events selectedEvent;
        public Events SelectedEvent
        {
            get => selectedEvent;
            set
            {
                selectedEvent = value;
                Signal();
    
                if (Feedback != null && selectedEvent != null)
                    Feedback.EventId = selectedEvent.Id;
            }
        }

        private Participant selectedParticipant;
        public Participant SelectedParticipant
        {
            get => selectedParticipant;
            set
            {
                selectedParticipant = value;
                Signal();

                // Можно, например, связать участника с отзывом, если нужно
                if (Feedback != null && selectedParticipant != null)
                    Feedback.ParticipantId = selectedParticipant.Id;
            }
        }

        private ObservableCollection<Participant> participants = new();
        public ObservableCollection<Participant> Participants
        {
            get => participants;
            set
            {
                participants = value;
                Signal();
            }
        }
        public AddFeedbackVM()
        {
            Events = new ObservableCollection<Events>(EventsRepository.Instance.GetAllEvents());
            string sql = "SELECT * FROM Participant";
            Participants = new ObservableCollection<Participant>(ParticipantRepository.Instance.GetAllParticipant(sql));
            Save = new CommandVM(() =>
            {

                if (Feedback.Id == 0)
                    FeedbacksRepository.Instance.AddFeedback(Feedback);

                else
                    FeedbacksRepository.Instance.UpdateFeedback(Feedback);

                FeedbackWindow feedbackWindow = new FeedbackWindow();
                feedbackWindow.Show();

            });

        }


        internal void SetEditFeedback(Feedback selectedFeedback)
        {
            Feedback = selectedFeedback;
        }
    }
}

