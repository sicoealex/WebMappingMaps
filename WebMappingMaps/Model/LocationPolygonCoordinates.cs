using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMappingMaps.Model
{
    public class LocationPolygonCoordinates
    {
        public string locationid { get; set; }
        public List<List<SpatialGeo>> coordinates { get; set; }
        public string description { get; set; }
        public Object metadata { get; set; }

        public LocationPolygonCoordinates() { }

        public LocationPolygonCoordinates(string locationid, List<List<SpatialGeo>> coordinates, string description)
        {
            this.locationid = locationid;
            this.coordinates = coordinates;
            this.description = description;
        }
    }
}