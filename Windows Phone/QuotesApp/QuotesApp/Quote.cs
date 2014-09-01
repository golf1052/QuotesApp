using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Parse;

namespace QuotesApp
{
    public class Quote
    {
        public ParseUser submitter;
        public ParseObject room;
        public int favorites;
        public List<Blurb> blurbs;
        public List<ParseObject> blurbObjects;

        public Quote(ParseUser submitter,
            ParseObject group,
            List<Blurb> blurbs)
        {
            this.submitter = submitter;
            this.room = group;
            this.favorites = 0;
            this.blurbs = blurbs;
            blurbObjects = new List<ParseObject>();
        }

        public Quote(ParseObject parseObject)
        {
            this.submitter = parseObject.Get<ParseUser>("submitter");
            this.room = parseObject.Get<ParseObject>("room");
            this.favorites = parseObject.Get<int>("favorites");
            this.blurbs = new List<Blurb>();
            this.blurbObjects = new List<ParseObject>();
            foreach (ParseObject blurbObject in parseObject.Get<List<object>>("blurbs"))
            {
                this.blurbObjects.Add(blurbObject);
            }
        }

        public async Task FetchObjects()
        {
            await submitter.FetchIfNeededAsync();
            await room.FetchIfNeededAsync();
            await blurbObjects.FetchAllIfNeededAsync();
            foreach (ParseObject blurbObject in blurbObjects)
            {
                this.blurbs.Add(new Blurb(blurbObject));
            }
        }

        public ParseObject ToParseObject()
        {
            ParseObject tmp = new ParseObject("Quote");
            tmp["submitter"] = submitter;
            tmp["room"] = room;
            tmp["favorites"] = favorites;
            foreach (Blurb blurb in blurbs)
            {
                blurbObjects.Add(blurb.ToParseObject());
            }
            tmp["blurbs"] = blurbObjects;
            return tmp;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj.GetType() != typeof(Quote))
            {
                return false;
            }
            else
            {
                Quote quote = obj as Quote;
                if (quote.submitter.ObjectId == submitter.ObjectId &&
                    quote.room.ObjectId == room.ObjectId &&
                    quote.favorites == favorites &&
                    quote.blurbObjects.Count == blurbObjects.Count)
                {
                    for (int i = 0; i < blurbObjects.Count; i++)
                    {
                        if (blurbObjects[i].ObjectId != quote.blurbObjects[i].ObjectId)
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override int GetHashCode()
        {
            int hashCode = submitter.GetHashCode() +
                room.GetHashCode() +
                favorites.GetHashCode();

            foreach (Blurb blurb in blurbs)
            {
                hashCode += blurb.blurb.GetHashCode() + blurb.misattributedTo.GetHashCode();
            }

            return hashCode;
        }
    }
}
