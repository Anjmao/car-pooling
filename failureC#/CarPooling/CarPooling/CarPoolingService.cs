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

        public void Process()
        {
            List<Booking> bookings = this.inputParser.ReadInput("dd-input.json");

            var buckets = this.divider.Group(bookings);

            this.matchMaker.SetBuckets(buckets);
            var journeys = this.matchMaker.Process();

        }
    }
}
