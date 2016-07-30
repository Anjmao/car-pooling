using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarPooling.IO;
using CarPooling.Models;

namespace CarPooling
{
    public class CarPoolingService
    {
        InputParser inputParser;
        MatchMaker matchMaker;
        Divider divider;

        public CarPoolingService(InputParser inputParser, MatchMaker matchMaker, Divider divider)
        {
            this.inputParser = inputParser;
            this.matchMaker = matchMaker;
            this.divider = divider;
        }

        public List<Journey> Process()
        {
            List<Booking> bookings = this.inputParser.ReadInput("input-2016-01-29-evening.json");

            var buckets = this.divider.Group(bookings);

            this.matchMaker.SetBuckets(buckets);
            return this.matchMaker.Process();
        }
    }
}
