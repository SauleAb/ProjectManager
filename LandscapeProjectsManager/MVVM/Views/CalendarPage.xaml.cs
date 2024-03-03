using LandscapeProjectsManager.MVVM.Views;
using System.Globalization;

namespace LandscapeProjectsManager
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
        }

        private void OnPreviousMonthClicked(object sender, EventArgs e)
        {
            Scheduler.DisplayDate = Scheduler.DisplayDate.AddMonths(-1);
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            Scheduler.DisplayDate = Scheduler.DisplayDate.AddMonths(1);
        }

        private async void OnAddEventButtonClicked(object sender, EventArgs e)
        {
            var modalPage = new AddEventPage();
            await Navigation.PushModalAsync(modalPage);
        }
    }
}
