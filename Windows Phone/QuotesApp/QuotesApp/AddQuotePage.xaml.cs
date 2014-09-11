using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
    public partial class AddQuotePage : PhoneApplicationPage
    {
        ObservableCollection<CreateQuoteListViewBinding> quotesCollection;
        string groupName;
        ParseObject groupObject;

        public AddQuotePage()
        {
            InitializeComponent();
            quotesCollection = new ObservableCollection<CreateQuoteListViewBinding>();
            groupObject = (ParseObject)((List<object>)PhoneApplicationService.Current.State["parameters"])[0];
            groupName = (string)groupObject["name"];
        }

        private void quotesListView_Loaded(object sender, RoutedEventArgs e)
        {
            quotesListView.ItemsSource = quotesCollection;
        }

        private void addBlurbAppBarButton_Click(object sender, EventArgs e)
        {
            quotesCollection.Add(new CreateQuoteListViewBinding());
        }

        private async void doneAppBarButton_Click(object sender, EventArgs e)
        {
            List<Blurb> blurbs = new List<Blurb>();
            foreach (CreateQuoteListViewBinding blurb in quotesCollection)
            {
                if (blurb.Quote == "")
                {
                    MessageBox.Show("One of your blurbs has an empty quote section. Fix it!");
                    return;
                }

                if (blurb.Misattribute == "")
                {
                    blurbs.Add(new Blurb(blurb.Quote, "Anonymous"));
                }
                else
                {
                    blurbs.Add(new Blurb(blurb.Quote, blurb.Misattribute));
                }
            }

            if (blurbs.Count == 0)
            {
                NavigationService.GoBack();
            }
            else
            {
                Quote quote = new Quote(AppConstants.user, groupObject, blurbs);
                groupObject.Get<List<object>>("quotes").Add(quote.ToParseObject());
                await groupObject.SaveAsync();
                NavigationService.GoBack();
            }
        }
    }
}