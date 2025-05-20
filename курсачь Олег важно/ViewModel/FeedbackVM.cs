using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using курсачь_Олег_важно.Model;

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


        public FeedbackVM()
        {
            string sql = "SELECT * FROM Feedback";
            Feedbacks = new ObservableCollection<Feedback>(FeedbacksRepository.Instance.GetAllFeedbacks(sql));


        }
    }
}
