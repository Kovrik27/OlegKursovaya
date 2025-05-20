using System;
using System.Collections.Generic;
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
        public AddFeedbackVM()
        {

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

