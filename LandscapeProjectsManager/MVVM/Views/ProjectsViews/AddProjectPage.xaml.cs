using augalinga.Data.Access;
using LandscapeProjectsManager.MVVM.ViewModels;

namespace LandscapeProjectsManager.MVVM.Views.Projects
{
    public partial class AddProjectPage : ContentPage
    {
        public DataContext DataContext { get; set; } = new DataContext();
        ProjectsViewModel projectsViewModel;

        public AddProjectPage(ProjectsViewModel projectsViewModel)
        {
            InitializeComponent();
            this.projectsViewModel = projectsViewModel;
            BindingContext = this;
        }

        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }

        private async void AddProjectButton_Clicked(object sender, EventArgs e)
        {
            string projectName = EntryProjectName.Text;

            if (!string.IsNullOrWhiteSpace(projectName))
            {
                var newProject = new augalinga.Data.Entities.Project(projectName);

                await DataContext.Projects.AddAsync(newProject);
                await DataContext.SaveChangesAsync();

                projectsViewModel.AddProjectToCollection(newProject);

                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Alert", "Please enter a project name.", "OK");
            }
        }

        private void PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e)
        {
            ((Button)sender).BackgroundColor = Color.FromRgb(240, 240, 240);
        }

        private void PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e)
        {
            ((Button)sender).BackgroundColor = Color.FromRgb(255, 255, 255);
        }
    }
}
