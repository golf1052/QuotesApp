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
using Parse;

namespace QuotesApp
{
    public partial class RoomPage : PhoneApplicationPage
    {
        List<Quote> quotes;
        ObservableCollection<QuoteListViewBinding> quotesCollection;
        Dictionary<Quote, List<QuoteListViewBinding>> quotesMapping;
        ParseObject thisRoom;

        public RoomPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            quotes = new List<Quote>();
            quotesCollection = new ObservableCollection<QuoteListViewBinding>();
            quotesMapping = new Dictionary<Quote, List<QuoteListViewBinding>>();
            thisRoom = (ParseObject)((List<object>)PhoneApplicationService.Current.State["parameters"])[0];
            roomNameTextBlock.Text = "";
            roomNameTextBlock.Text = (string)thisRoom["name"];
            ParseQuery<ParseObject> quotesQuery = ParseObject.GetQuery("Quote").WhereEqualTo("room", thisRoom);
            IEnumerable<ParseObject> quotesResults = await quotesQuery.FindAsync();
            foreach (ParseObject result in quotesResults)
            {
                Quote quote = new Quote(result);
                await quote.FetchObjects();
                quotes.Add(quote);
                for (int i = 0; i < quote.blurbs.Count;)
                {
                    if (!quotesMapping.ContainsKey(quote))
                    {
                        quotesMapping.Add(quote, new List<QuoteListViewBinding>());
                    }

                    if (quote.blurbs.Count - i == 1)
                    {
                        quotesMapping[quote].Add(new QuoteListViewBinding(quote.blurbs[i].blurb,
                            quote.blurbs[i].misattributedTo,
                            quote.submitter.Get<string>("displayName")));
                        i++;
                    }
                    else if (quote.blurbs.Count - i == 2)
                    {
                        quotesMapping[quote].Add(new QuoteListViewBinding(quote.blurbs[i].blurb,
                            quote.blurbs[i].misattributedTo,
                            quote.blurbs[i + 1].blurb,
                            quote.blurbs[i + 1].misattributedTo,
                            quote.submitter.Get<string>("displayName")));
                        i += 2;
                    }
                    else
                    {
                        quotesMapping[quote].Add(new QuoteListViewBinding(quote.blurbs[i].blurb,
                            quote.blurbs[i].misattributedTo,
                            quote.blurbs[i + 1].blurb,
                            quote.blurbs[i + 1].misattributedTo));
                        i += 2;
                    }
                }
            }

            foreach (KeyValuePair<Quote, List<QuoteListViewBinding>> pair in quotesMapping)
            {
                foreach (QuoteListViewBinding binding in pair.Value)
                {
                    quotesCollection.Add(binding);
                }
            }
        }

        private void newQuoteAppBarButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddQuotePage.xaml", UriKind.Relative));
        }

        private void quotesListView_Loaded(object sender, RoutedEventArgs e)
        {
            quotesListView.ItemsSource = quotesCollection;
        }
    }
}