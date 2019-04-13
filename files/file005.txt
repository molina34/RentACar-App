using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RentACar.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(RentACar.Droid.CalendarConnector))]
namespace RentACar.Droid
{
    public class CalendarConnector : ICalendarConnector
    {
        private static Activity Activity;

        static string[] PERMISSIONS_CALENDAR = {
            Manifest.Permission.ReadCalendar,
            Manifest.Permission.WriteCalendar
        };

        public static void Init(Activity activity) { CalendarConnector.Activity = activity; }

        public async void AddAppointment(DateTime startTime, DateTime endTime, string subject, string location, string details, bool isAllDay)
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                await AddEvent(startTime, endTime, subject, location, details, isAllDay);
                return;
            }
            else
            {
                await RequestCalendarPermissions(startTime, endTime, subject, location, details, isAllDay);
            }
        }

        private async Task AddEvent(DateTime startTime, DateTime endTime, string subject, string location, string details, bool isAllDay)
        {
            ContentResolver cr = Activity.ContentResolver;
            ContentValues eventValues = new ContentValues();

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, subject);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, details);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(startTime));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(endTime));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");
            eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay, (isAllDay) ? "1" : "0");

            var eventUri = cr.Insert(CalendarContract.Events.ContentUri, eventValues);
            long eventID = long.Parse(eventUri.LastPathSegment);

            await App.Current.MainPage.DisplayAlert("App", subject + "\r\n Start date: " + startTime + "\r\n Return date: " + endTime, "ok");
        }

        private async Task RequestCalendarPermissions(DateTime startTime, DateTime endTime, string subject, string location, string details, bool isAllDay)
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
                    status = results[Permission.Calendar];
                }

                if (status == PermissionStatus.Granted)
                {
                    await AddEvent(startTime, endTime, subject, location, details, isAllDay);
                    return;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await App.Current.MainPage.DisplayAlert("App", "Tente de novo", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("App", "Contacte o administrador", "OK");
            }
        }

        long GetDateTimeMS(DateTime dateTime)
        {
            return (long)dateTime.ToUniversalTime().Subtract(
                     new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    ).TotalMilliseconds;
        }
    }
}