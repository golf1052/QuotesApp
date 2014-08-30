using System;
using System.Collections.Generic;
using Parse;

namespace QuotesApp
{
    public class Quote
    {
        public ParseUser submitter;
        public ParseObject group;
        public int disapproveVotes;
        public int favorites;
        public List<Blurb> blurbs;
        public List<ParseObject> blurbObjects;

        public Quote(ParseUser submitter,
            ParseObject group,
            List<Blurb> blurbs)
        {
            this.submitter = submitter;
            this.group = group;
            this.disapproveVotes = 0;
            this.favorites = 0;
            this.blurbs = blurbs;
            blurbObjects = new List<ParseObject>();
        }

        public ParseObject ToParseObject()
        {
            ParseObject tmp = new ParseObject("Quote");
            tmp["submitter"] = submitter;
            tmp["group"] = group;
            tmp["disapproveVotes"] = disapproveVotes;
            tmp["favorites"] = favorites;
            foreach (Blurb blurb in blurbs)
            {
                blurbObjects.Add(blurb.ToParseObject());
            }
            tmp["blurbs"] = blurbObjects;
            return tmp;
        }
    }
}
