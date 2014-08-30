using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotesApp
{
    public class CreateQuoteListViewBinding
    {
        public string Quote { get; set; }
        public string Misattribute { get; set; }

        public CreateQuoteListViewBinding()
        {
            this.Quote = "";
            this.Misattribute = "";
        }
    }
}
