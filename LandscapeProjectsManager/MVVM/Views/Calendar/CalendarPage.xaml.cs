using LandscapeProjectsManager.MVVM.Views;
using System.Globalization;

namespace LandscapeProjectsManager
{
    public partial class CalendarPage : ContentPage
    {
        private CalendarViewModel viewModel;
        private bool baronaite = true;
        private bool gudaityte = true;
        private bool both = true;
        public CalendarPage()
        {
            InitializeComponent();
            viewModel = new CalendarViewModel(baronaite, gudaityte, both);
            this.BindingContext = viewModel;
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
            DateTime selectedDateTime = DateTime.Now;
            var modalPage = new AddEventPage(selectedDateTime, viewModel);
            await Navigation.PushModalAsync(modalPage);
        }

        private async void OnSchedulerDoubleTapped(object sender, EventArgs e)
        {
            var selectedDateTime = Convert.ToDateTime(Scheduler.SelectedDate); 
            var modalPage = new AddEventPage(selectedDateTime, viewModel);
            await Navigation.PushModalAsync(modalPage);
        }

        private void yellowCircle_Clicked(object sender, EventArgs e)
        {
            yellowEmptyCircle.IsEnabled = true;
            yellowEmptyCircle.IsVisible = true;
            yellowCircle.IsVisible = false;
            yellowCircle.IsEnabled = false;
            baronaite = false;
            viewModel.LoadEvents(baronaite, gudaityte, both);
            // ImageButton button = (ImageButton)sender;
            // 
            // if (button.Source.ToString() == "FileImageSource{yellowcircle.png}")
            // {
            //     button.Source = ImageSource.FromFile("yellowemptycircle.png");
            // }
            // else if (button.Source.ToString() == "FileImageSource{yellowemptycircle.png}")
            // {
            //     button.Source = ImageSource.FromFile("yellowcircle.png");
            // }
        }

        private void yellowEmptyCircle_Clicked(object sender, EventArgs e)
        {
            yellowEmptyCircle.IsEnabled = false;
            yellowEmptyCircle.IsVisible = false;
            yellowCircle.IsVisible = true;
            yellowCircle.IsEnabled = true;
            baronaite = true;
            viewModel.LoadEvents(baronaite, gudaityte, both);
        }

        private void blueCircle_Clicked(object sender, EventArgs e)
        {
            blueEmptyCircle.IsEnabled = true;
            blueEmptyCircle.IsVisible = true;
            blueCircle.IsVisible = false;
            blueCircle.IsEnabled = false;
            gudaityte = false;
            viewModel.LoadEvents(baronaite, gudaityte, both);
        }

        private void blueEmptyCircle_Clicked(object sender, EventArgs e)
        {
            blueEmptyCircle.IsEnabled = false;
            blueEmptyCircle.IsVisible = false;
            blueCircle.IsVisible = true;
            blueCircle.IsEnabled = true;
            gudaityte = true;
            viewModel.LoadEvents(baronaite, gudaityte, both);
        }

        private void redCircle_Clicked(object sender, EventArgs e)
        {
            redEmptyCircle.IsEnabled = true;
            redEmptyCircle.IsVisible = true;
            redCircle.IsVisible = false;
            redCircle.IsEnabled = false;
            both = false;
            viewModel.LoadEvents(baronaite, gudaityte, both);
        }

        private void redEmptyCircle_Clicked(object sender, EventArgs e)
        {
            redEmptyCircle.IsEnabled = false;
            redEmptyCircle.IsVisible = false;
            redCircle.IsVisible = true;
            redCircle.IsEnabled = true;
            both = true;
            viewModel.LoadEvents(baronaite, gudaityte, both);
        }
    }
}
