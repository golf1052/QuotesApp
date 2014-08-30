using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace QuotesApp
{
    public partial class RoomPage : PhoneApplicationPage
    {
        public RoomPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            roomNameTextBlock.Text = (string)AppConstants.pageParameters[0];
        }

        private void newQuoteAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddQuotePage.xaml", UriKind.Relative));
        }
    }
}