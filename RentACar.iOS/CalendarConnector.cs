using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventKit;
using Foundation;
using RentACar.Interfaces;
using RentACar.iOS;
using UIKit;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using System.Threading.Tasks;

[assembly: Dependency(typeof(CalendarConnector))]
namespace RentACar.iOS
{
    public class CalendarConnector : ICalendarConnector
    {
        public static void Init() { eventStore = new EKEventStore(); }
        public EKEventStore EventStore
        {
            get { return eventStore; }
        }
        protected static EKEventStore eventStore;

        public async void AddAppointment(DateTime startTime, DateTime endTime, string subject, string location, string details, bool isAllDay)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Calendar);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Calendar))
                    {
                        await App.Current.MainPage.DisplayAlert("App", "Permission needed", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar);
                    if (results.ContainsKey(Permission.Calendar))
                    {
                        status = results[Permission.Calendar];
                    }
                }

                if (status == PermissionStatus.Granted)
                {
                    await AddEvent(startTime, endTime, subject, location, details, isAllDay);
                    return;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("App", "Try again", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("App", "Erro", "Ok");
            }
        }

        public async Task AddEvent(DateTime startTime, DateTime endTime, string subject, string location, string details, bool isAllDay)
        {
            var calendars = await CrossCalendars.Current.GetCalendarsAsync();

            var selectedCalendar = calendars.Where(c => c.CanEditCalendar == true && c.CanEditEvents == true).FirstOrDefault();

            var calendarEvent = new CalendarEvent
            {
                Name = subject,
                Start = startTime,
                End = endTime,
                Reminders = new List<CalendarEventReminder> { new CalendarEventReminder() }
            };
            await CrossCalendars.Current.AddOrUpdateEventAsync(selectedCalendar, calendarEvent);

            await App.Current.MainPage.DisplayAlert("App", "Event added " + selectedCalendar.Name + " - " + subject + "\r\n Start date: " + startTime + "\r\n Return date: " + endTime, "ok");
        }
    }
}