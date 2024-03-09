using System.Collections.ObjectModel;
using System.ComponentModel;
using augalinga.Data.Access;
using augalinga.Data.Entities;

namespace LandscapeProjectsManager;

public class CalendarViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Meeting> _events;
    public ObservableCollection<Meeting> Events
    {
        get => _events;
        set
        {
            _events = value;
            OnPropertyChanged(nameof(Events));
        }
    }

    public CalendarViewModel()
    {
        LoadEvents();
    }

    private void LoadEvents()
    {
        var meetings = new DataContext().Meetings.ToList();
        Events = new ObservableCollection<Meeting>(meetings);
    }

    public void AddEventToCollection(Meeting newMeeting)
    {
        Events.Add(newMeeting);
        LoadEvents();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
