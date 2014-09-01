using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuotesApp
{
    public class QuoteListViewBinding
    {
        public Visibility TopDividerVisibility { get; set; }
        public Visibility RightQuoteVisibility { get; set; }
        public string RightQuote { get; set; }
        public string RightQuoteMisattrib { get; set; }
        public Visibility LeftQuoteVisibility { get; set; }
        public string LeftQuote { get; set; }
        public string LeftQuoteMisattrib { get; set; }
        public Visibility SubmitterVisibility { get; set; }
        public string Submitter { get; set; }
        public Visibility BottomDividerVisibility { get; set; }

        public QuoteListViewBinding(string quote, string misattrib, string submitter)
        {
            this.TopDividerVisibility = Visibility.Collapsed;
            this.RightQuoteVisibility = Visibility.Visible;
            this.RightQuote = quote;
            this.RightQuoteMisattrib = misattrib;
            this.LeftQuoteVisibility = Visibility.Collapsed;
            this.SubmitterVisibility = Visibility.Visible;
            this.Submitter = "- " + submitter;
            this.BottomDividerVisibility = Visibility.Visible;
        }

        public QuoteListViewBinding(string quote1, string misattrib1,
            string quote2, string misattrib2,
            string submitter)
        {
            this.TopDividerVisibility = Visibility.Collapsed;
            this.RightQuoteVisibility = Visibility.Visible;
            this.RightQuote = quote1;
            this.RightQuoteMisattrib = misattrib1;
            this.LeftQuoteVisibility = Visibility.Visible;
            this.LeftQuote = quote2;
            this.LeftQuoteMisattrib = misattrib2;
            this.SubmitterVisibility = Visibility.Visible;
            this.Submitter = "- " + submitter;
            this.BottomDividerVisibility = Visibility.Visible;
        }

        public QuoteListViewBinding(string quote1, string misattrib1,
            string quote2, string misattrib2)
        {
            this.TopDividerVisibility = Visibility.Collapsed;
            this.RightQuoteVisibility = Visibility.Visible;
            this.RightQuote = quote1;
            this.RightQuoteMisattrib = misattrib1;
            this.LeftQuoteVisibility = Visibility.Visible;
            this.LeftQuote = quote2;
            this.LeftQuoteMisattrib = misattrib2;
            this.SubmitterVisibility = Visibility.Collapsed;
            this.BottomDividerVisibility = Visibility.Collapsed;
        }
    }
}
