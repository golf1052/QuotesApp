using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media;
using Parse;

namespace QuotesApp
{
    public partial class RoomsListPage : PhoneApplicationPage
    {
        ObservableCollection<RoomListViewBinding> allRoomsCollection;
        ObservableCollection<RoomListViewBinding> personalRoomsCollection;

        public RoomsListPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            allRoomsCollection = new ObservableCollection<RoomListViewBinding>();
            personalRoomsCollection = new ObservableCollection<RoomListViewBinding>();
            ParseQuery<ParseObject> personalRoomsQuery = ParseObject.GetQuery("Room").WhereEqualTo("members", AppConstants.user);
            IEnumerable<ParseObject> personalRoomsResults = await personalRoomsQuery.FindAsync();
            foreach (ParseObject result in personalRoomsResults)
            {
                List<object> foundersList = result.Get<List<object>>("founders");
                bool noFounderFound = true;
                foreach (ParseUser founder in foundersList)
                {
                    if (founder.ObjectId == AppConstants.user.ObjectId)
                    {
                        personalRoomsCollection.Add(new RoomListViewBinding(result.Get<string>("name"), AppConstants.appPrimaryColor1, result));
                        noFounderFound = false;
                        break;
                    }
                }

                if (noFounderFound)
                {
                    personalRoomsCollection.Add(new RoomListViewBinding(result.Get<string>("name"), AppConstants.appAccentColor1, result));
                }
            }

            ParseQuery<ParseObject> allRoomsQuery = ParseObject.GetQuery("Room");
            IEnumerable<ParseObject> allRoomsResults = await allRoomsQuery.FindAsync();
            foreach (ParseObject result in allRoomsResults)
            {
                List<object> foundersList = result.Get<List<object>>("founders");
                bool noFounderFound = true;
                foreach (ParseUser founder in foundersList)
                {
                    if (founder.ObjectId == AppConstants.user.ObjectId)
                    {
                        allRoomsCollection.Add(new RoomListViewBinding(result.Get<string>("name"), AppConstants.appPrimaryColor1, result));
                        noFounderFound = false;
                        break;
                    }
                }

                if (noFounderFound)
                {
                    allRoomsCollection.Add(new RoomListViewBinding(result.Get<string>("name"), AppConstants.appAccentColor1, result));
                }
            }
        }

        private void personalRoomsListView_Loaded(object sender, RoutedEventArgs e)
        {
            personalRoomsListView.ItemsSource = personalRoomsCollection;
        }

        private void newRoomAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddRoomPage.xaml", UriKind.Relative));
        }

        private void allRoomsListView_Loaded(object sender, RoutedEventArgs e)
        {
            allRoomsListView.ItemsSource = allRoomsCollection;
        }

        private void personalRoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                RoomListViewBinding selectedItem = (RoomListViewBinding)e.AddedItems[0];
                AppConstants.pageParameters.Clear();
                AppConstants.pageParameters.Add(selectedItem.parseObject);
                NavigationService.Navigate(new Uri("/RoomPage.xaml", UriKind.Relative));
            }
        }

        private void allRoomsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                RoomListViewBinding selectedItem = (RoomListViewBinding)e.AddedItems[0];
                AppConstants.pageParameters.Clear();
                AppConstants.pageParameters.Add(selectedItem.parseObject);
                NavigationService.Navigate(new Uri("/RoomPage.xaml", UriKind.Relative));
            }
        }
    }
}