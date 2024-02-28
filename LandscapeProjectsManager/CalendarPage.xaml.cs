using System.Globalization;
namespace LandscapeProjectsManager
{
    public partial class CalendarPage : ContentPage
    {
        public CalendarPage()
        {
            InitializeComponent();
            PopulateCalendar(DateTime.Today);
        }

        private void PopulateCalendar(DateTime date)
        {
            // Clear existing calendar grid
            calendarGrid.Children.Clear();
            calendarGrid.RowDefinitions.Clear();
            calendarGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < 7; i++)
            {
                calendarGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                Label dayLabel = new Label
                {
                    Text = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames[i],
                    HorizontalTextAlignment = TextAlignment.Center
                };
                calendarGrid.Children.Add(dayLabel);
                Grid.SetColumn(dayLabel, i);
                Grid.SetRow(dayLabel, 0);
            }

            // Calculate start day and end day for the current month
            DateTime startOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
            int totalDays = endOfMonth.Day;
            int currentDay = 1;

            // Populate calendar grid with days
            for (int row = 1; row <= 6; row++)
            {
                calendarGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                for (int column = 0; column < 7; column++)
                {
                    if ((row == 1 && column < (int)startOfMonth.DayOfWeek) || currentDay > totalDays)
                        continue;

                    Label dayLabel = new Label
                    {
                        Text = currentDay.ToString(),
                        HorizontalTextAlignment = TextAlignment.Center
                    };
                    calendarGrid.Children.Add(dayLabel);
                    Grid.SetColumn(dayLabel, column);
                    Grid.SetRow(dayLabel, row);
                    currentDay++;
                }
            }
        }
        private void OnPreviousMonthClicked(object sender, EventArgs e)
        {
            // Navigate to previous month
            DateTime previousMonth = DateTime.Today.AddMonths(-1);
            PopulateCalendar(previousMonth);
        }

        private void OnNextMonthClicked(object sender, EventArgs e)
        {
            // Navigate to next month
            DateTime nextMonth = DateTime.Today.AddMonths(1);
            PopulateCalendar(nextMonth);
        }
    }
}