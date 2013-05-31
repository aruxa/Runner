using System.Collections.Generic;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.Platform;

namespace runner
{
    public class RouteModel
    {
        private LocationCollection _locations;

        public ICollection<GeoCoordinate> Locations
        {
            get { return _locations; }
        }

        public RouteModel(IEnumerable<GeoCoordinate> locations)
        {
            _locations = new LocationCollection();
            foreach (var location in locations)
            {
                _locations.Add(location);
            }
        }
    }
}
