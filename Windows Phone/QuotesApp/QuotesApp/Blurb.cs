using System;
using Parse;

namespace QuotesApp
{
    public class Blurb
    {
        public string blurb;
        public string misattributedTo;

        public Blurb(string blurb, string misattributedTo)
        {
            this.blurb = blurb;
            this.misattributedTo = misattributedTo;
        }

        public Blurb(ParseObject blurbObject)
        {
            this.blurb = blurbObject.Get<string>("blurb");
            this.misattributedTo = blurbObject.Get<string>("misattributedTo");
        }

        public ParseObject ToParseObject()
        {
            ParseObject tmp = new ParseObject("Blurb");
            tmp["blurb"] = blurb;
            tmp["misattributedTo"] = misattributedTo;
            return tmp;
        }
    }
}
