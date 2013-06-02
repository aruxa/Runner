using System;
using System.Device.Location;

namespace runner.Geo
{
    public interface IGeoCoordinateWatcher : IGeoPositionWatcher<GeoCoordinate>, IDisposable
    {
        GeoPositionAccuracy DesiredAccuracy { get; }
        double MovementThreshold { get; set; }
        GeoPositionPermission Permission { get; }
    }
}
