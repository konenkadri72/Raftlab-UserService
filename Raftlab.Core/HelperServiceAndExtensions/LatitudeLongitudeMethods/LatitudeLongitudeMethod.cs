using System;
using System.Collections.Generic;
using System.Text;

namespace Raftlab.Core.HelperServiceAndExtensions.LatitudeLongitudeMethods
{
    public class LatitudeLongitudeMethod
    {
        public static double GetDistanceFromHaverSin(double sourceLatitude, double sourceLongitude, double destinationLatitude, double destinationLongitude)
        {
            var p = Math.PI / 180;
            var a = 0.5 - Math.Cos((destinationLatitude - sourceLatitude) * p) / 2 +
                            Math.Cos(sourceLatitude * p) * Math.Cos(destinationLatitude * p) *
                                            (1 - Math.Cos((destinationLongitude - sourceLongitude) * p)) / 2;
            return (12742 * Math.Asin(Math.Sqrt(a)) * 0.621371);
        }
    }
}
