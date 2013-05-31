using System.Windows;
using Microsoft.Phone.Controls.Maps;

namespace runner.Controls
{
    public class MapRouteControl : MapItemsControl
    {
        public MapRouteControl()
        {
        }

        public static readonly DependencyProperty RouteProperty =
            DependencyProperty.Register("Route", typeof (RouteModel), typeof (MapRouteControl), new PropertyMetadata(default(RouteModel), RouteChanged));

        private static void RouteChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var routeMapControl = dependencyObject as MapRouteControl;
            if (routeMapControl == null)
            {
                return;
            }
            var route = dependencyPropertyChangedEventArgs.NewValue as RouteModel;
            if (route == null)
            {
                return;
            }

            var viewRect = LocationRect.CreateLocationRect(route.Locations);
            routeMapControl.ParentMap.SetView(viewRect);
        }

        public RouteModel Route
        {
            get { return (RouteModel) GetValue(RouteProperty); }
            set { SetValue(RouteProperty, value); }
        }
    }
}
