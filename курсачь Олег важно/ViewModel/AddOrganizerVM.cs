using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using курсачь_Олег_важно.Model;
using курсачь_Олег_важно.View;

namespace курсачь_Олег_важно.ViewModel
{
    public class AddOrganizerVM : BaseVM
    {
        public CommandVM Save { get; set; }

        private Organizer organizer = new();

        public Organizer Organizer
        {
            get => organizer;
            set
            {
                organizer = value;
                Signal();
            }
        }
        public AddOrganizerVM()
        {

            Save = new CommandVM(() =>
            {

                if (Organizer.Id == 0)
                    OrganizersRepository.Instance.AddOrganizer(Organizer);
                else
                    OrganizersRepository.Instance.UpdateOrganizer(Organizer);

                OrganizersWindow organizersWindow = new OrganizersWindow();
                organizersWindow.Show();

            });

        }


        internal void SetEditOrganizer(Organizer selectedOrganizer)
        {
            Organizer = selectedOrganizer;
        }
    }
}

