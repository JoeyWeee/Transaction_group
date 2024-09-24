using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace group
{
    public struct DayTime
    {
        private long minutes;

        public DayTime(long minutes)
        {
            this.minutes = minutes;
        }

        public static DayTime operator +(DayTime lhs, int minutes)
        {
            return new DayTime(lhs.minutes + minutes);
        }

        public override string ToString()
        {
            long years = minutes / 518400;
            minutes %= 518400;

            long months = minutes / 43200;
            minutes %= 43200;

            long days = minutes / 1440;
            minutes %= 1440;

            long hours = minutes / 60;
            minutes %= 60;

            return $"{years}-{months:d2}-{days:d2} {hours:d2}:{minutes:d2}";
        
        }
    }
}
