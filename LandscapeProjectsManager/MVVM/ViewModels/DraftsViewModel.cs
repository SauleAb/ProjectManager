﻿using augalinga.Data.Access;
using augalinga.Data.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeProjectsManager.MVVM.ViewModels
{
    public class DraftsViewModel
    {
        string _projectName;
        public DraftsViewModel(string projectName)
        {
            _projectName = projectName;
            LoadDrafts(_projectName);
        }

        private ObservableCollection<Draft> _drafts;
        public ObservableCollection<Draft> Drafts
        {
            get => _drafts;
            set
            {
                _drafts = value;
                OnPropertyChanged(nameof(Drafts));
            }
        }

        public void AddDraftToCollection(Draft draft)
        {
            Drafts.Add(draft);
            LoadDrafts(_projectName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadDrafts(string projectName)
        {
            using (var context = new DataContext())
            {
                var drafts = context.Drafts.Where(draft => draft.Project == projectName).ToList();

                Drafts = new ObservableCollection<Draft>(drafts);
            }
        }

        public void RemoveDraft(string draftLink)
        {
            //local
            var draftToRemove = Drafts.FirstOrDefault(p => p.Link == draftLink);
            Drafts.Remove(draftToRemove);

            //database
            using (var dbContext = new DataContext())
            {
                dbContext.Drafts.Remove(draftToRemove);
                dbContext.SaveChanges();
            }

            LoadDrafts(_projectName);
        }
    }
}
