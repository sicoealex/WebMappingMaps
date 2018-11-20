using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMappingMaps
{
    public class SpatialGeo
    {
        public SpatialGeo() { }

        public SpatialGeo(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }

        public double lat { get; set; }
        public double lng { get; set; }
        

    }
}