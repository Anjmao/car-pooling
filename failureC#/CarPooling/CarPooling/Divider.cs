using CarPooling.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarPooling
{
    public static class Divider
    {
        private static void Group(IEnumerable<Booking> orderings)
        {
            var ordered = orderings.OrderBy(x => x.Pickup.Longitude);
            var riders = new HashSet<Booking>(ordered);




        }

        private static void SortByLongtitude()
        {
            
        }
    }
}
