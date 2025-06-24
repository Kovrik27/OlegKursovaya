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
    public class ParticipantVM : BaseVM
    {
        private ObservableCollection<Participant> participants;
        public ObservableCollection<Participant> Participants
        {
            get => participants;
            set
            {
                participants = value;
                Signal();
            }
        }



        public  Participant SelectedParticipant { get; set; }
        public CommandVM OpenAddParticipantWindow
        {
            get; set;
        }

        public CommandVM OpenEditParticipantWindow
        {
            get; set;
        }

        public ParticipantVM()
        {
            string sql = "SELECT * FROM Participants";
            Participants = new ObservableCollection<Participant>(ParticipantRepository.Instance.GetAllParticipant(sql));

            OpenAddParticipantWindow = new CommandVM(() =>
            {
                AddParticipantWindow addParticipantWindow = new AddParticipantWindow();
                addParticipantWindow.Show();
            });

            OpenEditParticipantWindow = new CommandVM(() =>
            {
                if (SelectedParticipant == null)
                    return;
                AddParticipantWindow addEventWindow = new AddParticipantWindow(SelectedParticipant);
                addEventWindow.Show();
            });
        }

    }
}

