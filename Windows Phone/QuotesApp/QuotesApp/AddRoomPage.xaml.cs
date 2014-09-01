using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Parse;

namespace QuotesApp
{
    public partial class AddRoomPage : PhoneApplicationPage
    {
        WaitingForParse waiting;

        public AddRoomPage()
        {
            InitializeComponent();
        }

        private async void createRoomTextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (roomNameTextBox.Text != "")
            {
                waiting.Activate();
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Room");
                IEnumerable<ParseObject> results = await query.FindAsync();
                foreach (ParseObject result in results)
                {
                    if (result.Get<string>("name").ToLower() == roomNameTextBox.Text.ToLower())
                    {
                        MessageBox.Show("There is a room with that name already. You need to pick a new name.");
                        waiting.Deactivate();
                        return;
                    }
                }

                ParseObject newRoom = new ParseObject("Room");
                newRoom["name"] = roomNameTextBox.Text;
                newRoom["quotes"] = new List<ParseObject>();
                List<ParseUser> founders = new List<ParseUser>();
                List<ParseUser> members = new List<ParseUser>();
                founders.Add(AppConstants.user);
                members.Add(AppConstants.user);
                newRoom["founders"] = founders;
                newRoom["members"] = members;
                newRoom["deleteVotes"] = new List<ParseUser>();
                await newRoom.SaveAsync();
                waiting.Deactivate();
                AppConstants.pageParameters.Clear();
                AppConstants.pageParameters.Add(newRoom);
                NavigationService.GoBack();
                //NavigationService.Navigate(new Uri("/RoomPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Your room needs to have a name");
            }
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            waiting = new WaitingForParse(createRoomTextBlock, LayoutRoot);
        }
    }
}