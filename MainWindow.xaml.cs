using System;
using System.Windows;

namespace GestionContactsEF
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel.ContactsViewModel();
        }
    }
}
