using System;
using System.Collections.Generic;
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
        public AddParticipantVM()
        {

            Save = new CommandVM(() =>
            {

                //if (Participant.Id == 0)
                //    //ParticipantRepository.Instance.AddParticipantOnEvent(Participant);
                //else
                //    ParticipantRepository.Instance.UpdateParticipant(Participant);

                //OrganizersWindow organizersWindow = new OrganizersWindow();
                //organizersWindow.Show();

            });

        }


        internal void SetEditParticipant(Participant selectedParticipant)
        {
            Participant = selectedParticipant;
        }
    }
}
