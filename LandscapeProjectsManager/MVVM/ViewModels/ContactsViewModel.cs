﻿using augalinga.Data.Access;
using augalinga.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = augalinga.Data.Entities.Contact;

namespace LandscapeProjectsManager.MVVM.ViewModels
{
    public class ContactsViewModel : INotifyPropertyChanged
    {
        public ContactsViewModel(string category)
        {
            LoadContacts(category);
        }

        private ObservableCollection<Contact> _contacts;
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RemoveContact(Contact contact)
        {
            using (var dbContext = new DataContext())
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
            }
        }

        private void LoadContacts(string category)
        {
            using (var dbContext = new DataContext())
            {
                var contacts = dbContext.Contacts
                    .Where(c => c.Category == category)
                    .ToList();

                Contacts = new ObservableCollection<Contact>(contacts);
            }
        }
    }
}
