using LandscapeProjectsManager.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandscapeProjectsManager.MVVM.ViewModels
{
    public class ProjectsViewModel
    {
        public ObservableCollection<Project> Projects { get; set; }
        public ProjectsViewModel()
        {
            InitializeProjects();
        }

        private void InitializeProjects()
        {
            Projects = new ObservableCollection<Project>
            {
                new Project("Oak"),
                new Project("Pine"),
                new Project("Maple"),
                new Project("Lavender"),
                new Project("Rose"),
                new Project("Lilac")
            };
        }
    }
}
