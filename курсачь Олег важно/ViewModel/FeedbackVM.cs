using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Windows.Input;
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.View;

namespace курсачь_Олег_важно.ViewModel
{
    public class FeedbackVM: BaseVM
    {
        public ObservableCollection<Feedback> feedbacks;
        public ObservableCollection<Feedback> Feedbacks
        {
            get => feedbacks; set
            {
                feedbacks = value;Signal();
            }
        }

        public Feedback SelectedFeedback { get; set; }
        public CommandVM OpenAddFeedbackWindow
        {
            get; set;
        }

        public CommandVM OpenEditFeedbackWindow
        {
            get; set;
        }

        public FeedbackVM()
        {
            string sql = "SELECT f.Id as FeedbackId, f.EventId, e.Id as EventId, e.Name as EventName, f.ParticipantId, p.Id as ParticipantId, p.Name as ParticipantName, p.Lastname as ParticipantLastname,p.Surname as ParticipantSurname, p.Phone as ParticipantPhone, f.Rating, f.Comment FROM Feedback f JOIN Events e ON f.EventId = e.Id JOIN Participant p ON f.ParticipantId = p.Id";
            Feedbacks = new ObservableCollection<Feedback>(FeedbacksRepository.Instance.GetAllFeedbacks(sql));

            OpenAddFeedbackWindow = new CommandVM(() =>
            {
                AddFeedbackWindow addFeedbackWindow = new AddFeedbackWindow();
                addFeedbackWindow.Show();
            });

            OpenEditFeedbackWindow = new CommandVM(() =>
            {
                if (SelectedFeedback == null)
                    return;
                AddFeedbackWindow addFeedbackWindow = new AddFeedbackWindow(SelectedFeedback);
                addFeedbackWindow.Show();
            });
        }


       
    }
}

