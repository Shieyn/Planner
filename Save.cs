using System;
using System.Collections.Generic;

namespace Planner
{
    [Serializable]
    public class Save
    {
        public List<Event> Plan;

        public Save()
        {
            Plan = new List<Event>();
        }
    }

    public class Event
    {
        public DateTime Date;
        public string Title;
        public string Keyword;
        public int Priority;

        public Event(DateTime d, string t, string k, int p) 
            : this ()
        {
            Date = d;
            Title = t;
            Keyword = k;
            Priority = p;
        }

        public Event(DateTime d)
            : this()
        {
            Date = d;
        }

        public Event()
        {

        }

        public override string ToString() => $"{Title} ({Priority})";
    }
}
