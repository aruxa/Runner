using System.Device.Location;

namespace runner.Geo
{
    public class GeoCoordinateWatcherAdapter : GeoCoordinateWatcher, IGeoCoordinateWatcher
    {
        public GeoCoordinateWatcherAdapter()
        {
            
        }

        public GeoCoordinateWatcherAdapter(GeoPositionAccuracy accuracy)
            :base(accuracy)
        {
            
        }
    }
}
