using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using GestionContactsEF.Model;

namespace GestionContactsEF.ViewModel
{
    public class ContactsViewModel : ViewModelBase
    {
        public ObservableCollection<ContactViewModel> ListeContacts { get; set; }
        private ICollectionView observer;

        public ContactViewModel ContactSelected
        {
            get { return observer.CurrentItem as ContactViewModel; }
        }

        public ContactsViewModel()
        {
            ListeContacts = new ObservableCollection<ContactViewModel>();
            List<Person> lst = Server.ContactsService.Instance.ChargerContacts();
            foreach (Person person in lst)
            {
                ContactViewModel vm = new ContactViewModel(person);
                vm.OnDeleted += ContactOnDeleted;
                ListeContacts.Add(vm);
            }

            observer = CollectionViewSource.GetDefaultView(ListeContacts);
            observer.MoveCurrentToFirst();
            observer.CurrentChanged += Observer_CurrentChanged;


            // **************************************************
            //   utilisation du TPL pour traitement asynchrone
            // ************************************************** //
            //Task.Factory.StartNew(() =>
            //{
            //    //Traitement long en tâche de fond
            //    Thread.Sleep(5000);
            //    return ContactsModel.Server.ContactsService.Instance.ChargerContacts();

            //}).ContinueWith((t) => {

            //    //on continue sur le thread principal d'affichage !
            //    List<Person> lst =(List<Person>) t.Result;
            //    foreach (Person person in lst)
            //    {
            //        ContactViewModel vm = new ContactViewModel(person);
            //        vm.OnDeleted += ContactOnDeleted;
            //        ListeContacts.Add(vm);                    
            //    }
            //    observer = CollectionViewSource.GetDefaultView(ListeContacts);
            //    observer.MoveCurrentToFirst();
            //    observer.CurrentChanged += Observer_CurrentChanged;
            //}
            //,CancellationToken.None,
            //TaskContinuationOptions.LongRunning,
            //TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ContactOnDeleted(object sender, EventArgs e)
        {
            ContactViewModel vm = sender as ContactViewModel;
            ListeContacts.Remove(vm);
            observer.MoveCurrentToFirst();
        }

        private void Observer_CurrentChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("ContactSelected");
        }




        private ICommand commandeNewClient;
        public ICommand CommandeNewClient
        {
            get
            {
                if (commandeNewClient == null)
                {
                    commandeNewClient = new RelayCommand(sender => {

                        Client client = new Client();
                        ContactViewModel vm = new ContactViewModel(client);
                        vm.OnDeleted += ContactOnDeleted;
                        ListeContacts.Add(vm);
                        OnPropertyChanged("ListeContact");
                        observer.MoveCurrentToLast();
                    });
                }
                return commandeNewClient;
            }
        }

        private ICommand commandeNewBrother;
        public ICommand CommandeNewBrother
        {
            get
            {
                if (commandeNewBrother == null)
                {
                    commandeNewBrother = new RelayCommand(sender => {

                        Brother client = new Brother();
                        ContactViewModel vm = new ContactViewModel(client);
                        vm.OnDeleted += ContactOnDeleted;
                        ListeContacts.Add(vm);
                        OnPropertyChanged("ListeContact");
                        observer.MoveCurrentToLast();
                    });
                }
                return commandeNewBrother;
            }
        }
    }
}
