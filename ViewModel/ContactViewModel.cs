using System;
using System.Windows;
using System.Windows.Input;
using GestionContactsEF.Model;
using GestionContactsEF.Server;

namespace GestionContactsEF.ViewModel
{
    public class ContactViewModel : ViewModelBase
    {
        public Person Contact { get; set; }

        public event EventHandler OnDeleted;

        public ContactViewModel(Person person)
        {
            Contact = person;
        }


        public string Nom
        {
            get { return Contact.Nom; }
            set
            {
                Contact.Nom = value;
                OnPropertyChanged("PrenomNom");
            }
        }

        public string Prenom
        {
            get { return Contact.Prenom; }
            set
            {
                Contact.Prenom = value;
                OnPropertyChanged("PrenomNom");
            }
        }


        public string PrenomNom
        {
            get
            {
                return Contact.Prenom + " " + Contact.Nom;
            }
        }

        public Visibility VisibilityClient
        {
            get
            {
                if (Contact.GetType() == typeof(Client))
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        public Visibility VisibilityFrere
        {
            get
            {
                if (Contact.GetType() == typeof(Brother))
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        private ICommand commandSave;
        public ICommand CommandSave
        {
            get
            {
                if (commandSave == null)
                {
                    commandSave = new RelayCommand(sender => {

                        Contact.Id = ContactsService.Instance.SaveContact(Contact);
                    },
                    sender => {
                        //can execute ?
                        if (string.IsNullOrWhiteSpace(Contact.Nom) || string.IsNullOrWhiteSpace(Contact.Prenom))
                            return false;
                        return true;
                    });
                }
                return commandSave;
            }
        }

        private ICommand commandDelete;
        public ICommand CommandDelete
        {
            get
            {
                if (commandDelete == null)
                {
                    commandDelete = new RelayCommand(sender => {

                        if (Contact.Id > 0) ContactsService.Instance.DeleteContact(Contact);
                        OnDeleted?.Invoke(this, EventArgs.Empty);
                    });
                }
                return commandDelete;
            }
        }


    }
}
