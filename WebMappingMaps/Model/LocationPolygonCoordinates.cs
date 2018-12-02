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
        public TransaviaFerma metadata { get; set; }
        public List<String> mapLegend {get; set;}

        public LocationPolygonCoordinates() { }

        public LocationPolygonCoordinates(string locationid, List<List<SpatialGeo>> coordinates, string description)
        {
            this.locationid = locationid;
            this.coordinates = coordinates;
            this.description = description;
        }

        public LocationPolygonCoordinates(string locationid, List<List<SpatialGeo>> coordinates, string description, TransaviaFerma metadata)
        {
            this.locationid = locationid;
            this.coordinates = coordinates;
            this.description = description;
            this.metadata = metadata;
        }

        public LocationPolygonCoordinates(string locationid, List<List<SpatialGeo>> coordinates, string description, TransaviaFerma metadata, List<String> legend)
        {
            this.locationid = locationid;
            this.coordinates = coordinates;
            this.description = description;
            this.metadata = metadata;
            this.mapLegend = legend;
        }

    }
}