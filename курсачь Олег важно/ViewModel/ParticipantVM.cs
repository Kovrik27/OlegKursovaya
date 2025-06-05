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
        private ObservableCollection<Participant> participant;
        public ObservableCollection<Participant> Participant
        {
            get => participant;
            set
            {
                participant = value;
                Signal();
            }
        }



        public  Participant SelectedParticipant { get; set; }
        public CommandVM OpenAddParticipantWindow
        {
            get; set;
        }

        public CommandVM OpenEditParticipanttWindow
        {
            get; set;
        }

        public ParticipantVM()
        {

            Participant = new ObservableCollection<Participant>(ParticipantRepository.Instance.GetAllParticipant());

            OpenAddParticipantWindow = new CommandVM(() =>
            {
                AddEventWindow addEventWindow = new AddEventWindow();
                addEventWindow.Show();
            });

            OpenEditParticipanttWindow = new CommandVM(() =>
            {
                if (SelectedParticipant == null)
                    return;
                AddParticipantWindow addEventWindow = new AddParticipantWindow(SelectedParticipant);
                addEventWindow.Show();
            });
        }

    }
}

