using augalinga.Data.Access;
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
    public class DocumentsViewModel
    {

        public DocumentsViewModel()
        {
            LoadDocuments();
        }

        private ObservableCollection<Document> _documents;
        public ObservableCollection<Document> Documents
        {
            get => _documents;
            set
            {
                _documents = value;
                OnPropertyChanged(nameof(Documents));
            }
        }

        public void AddDocumentToCollection(Document document)
        {
            Documents.Add(document);
            LoadDocuments();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadDocuments()
        {
            var documents = new DataContext().Documents.ToList();
            Documents = new ObservableCollection<Document>(documents);
        }

        public void RemoveDocument(string documentLink)
        {
            //local
            var documentToRemove = Documents.FirstOrDefault(p => p.Link == documentLink);
            Documents.Remove(documentToRemove);

            //database
            using (var dbContext = new DataContext())
            {
                dbContext.Documents.Remove(documentToRemove);
                dbContext.SaveChanges();
            }

            LoadDocuments();
        }
    }
}
