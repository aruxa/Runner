﻿using System;
using System.Device.Location;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace runner
{
    public class Pushpin
    {
        public GeoCoordinate Location { get; set; }

        public Uri Icon { get; set; } 
    }
}
