using augalinga.Data.Access;
using augalinga.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UnitTestingData
{
    [TestClass]
    public class UnitTest1
    {
        public DataContext DataContext { get; set; } = new DataContext();
        [TestMethod]
        public async Task AddMeeting()
        {
            await DataContext.Projects.AddAsync(new augalinga.Data.Entities.Project("Pina"));
            await DataContext.Projects.AddAsync(new augalinga.Data.Entities.Project("Lavender"));
            await DataContext.Projects.AddAsync(new augalinga.Data.Entities.Project("Rose"));
            await DataContext.Projects.AddAsync(new augalinga.Data.Entities.Project("Oak"));

            await DataContext.SaveChangesAsync();
        }
        // public async Task AddMeeting()
        // {
        //     await DataContext.Meetings.AddAsync(new augalinga.Data.Entities.Meeting
        //     {
        //         IsAllDay = false,
        //         From = DateTime.Now, 
        //         To = DateTime.Now.AddHours(1),
        //         EventName = "Test Event",
        //         Background = "#FFFFD700",
        //         Notes = "Test test test"
        //     });
        // 
        //     await DataContext.SaveChangesAsync();
        // }

        // public async Task FindMeeting()
        // {
        //     var meeting = await DataContext.Meetings.FirstOrDefaultAsync(w => w.EventName == "Test");
        // 
        //     Console.WriteLine(meeting.EventName + "   " + meeting.Notes);
        // }
    }
}