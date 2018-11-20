using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMappingMaps.Model
{
    public class LocationCoordinates
    {


        public string locationid { get; set; }
        public List<SpatialGeo> coordinates { get; set; }
        public string description { get; set; }

        public LocationCoordinates() { }

        public LocationCoordinates(string locationid, List<SpatialGeo> coordinates, string description)
        {
            this.locationid = locationid;
            this.coordinates = coordinates;
            this.description = description;
        }
    }
}