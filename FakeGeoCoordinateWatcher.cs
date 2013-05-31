using System;
using System.Device.Location;

namespace runner
{
    public class FakeGeoCoordinateWatcher : GeoCoordinateWatcher
    {
        public FakeGeoCoordinateWatcher(GeoPositionAccuracy accuracy):base(accuracy)
        {
            
        }

        public  void Start()
        {
            OnPositionStatusChanged(new GeoPositionStatusChangedEventArgs(GeoPositionStatus.Ready));
           
            OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.448074, 26.081837))));
            OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.447959, 26.082315))));
            OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.447924, 26.082518))));
            OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.448231, 26.082679))));
            OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.450681, 26.084096))));   
            OnPositionChanged(new GeoPositionChangedEventArgs<GeoCoordinate>(new GeoPosition<GeoCoordinate>(DateTime.Now, new GeoCoordinate(44.451187, 26.084364))));
        }

        public new void Stop()
        {
            OnPositionStatusChanged(new GeoPositionStatusChangedEventArgs(GeoPositionStatus.Disabled));
        }
    }
}
