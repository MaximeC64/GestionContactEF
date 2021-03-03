using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GestionContactsEF.Model;

namespace GestionContactsEF.Server
{
    public class ContactsService
    {
        #region Singleton

        private static ContactsService instance = null;
        public static ContactsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContactsService();
                }
                return instance;
            }
        }

        //empeche l'instanciation de la classe !
        private ContactsService()
        {
        }

        #endregion
        
        public List<Person> ChargerContacts()
        {
            using (ContactsEntities context = new ContactsEntities())
            {
                return (from C in context.Persons select C).ToList();
            }
        }

        public int SaveContact(Person contact)
        {
            using (ContactsEntities context = new ContactsEntities())
            {
                if(contact.Id > 0)
                {
                    context.Persons.Attach(contact);
                    context.Entry(contact).State = EntityState.Modified;
                    context.SaveChanges();
                }
                else
                {
                    context.Persons.Add(contact);
                    context.SaveChanges();
                }
            }
            return contact.Id;
        }

        public bool DeleteContact(Person contact)
        {
            if (contact.Id > 0)
            {
                using (ContactsEntities context = new ContactsEntities())
                {
                    context.Persons.Attach(contact);
                    context.Entry(contact).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                return true;
            }
            return false;
        }

    }
}
