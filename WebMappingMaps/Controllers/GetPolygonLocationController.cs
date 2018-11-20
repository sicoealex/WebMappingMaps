using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMappingMaps.Model;

namespace WebMappingMaps.Controllers
{
    public class GetPolygonLocationController : ApiController
    {

        [HttpGet]
        public List<LocationPolygonCoordinates> GetSpatialGeos()
        {
            GeoLocationDB gdb = new GeoLocationDB();
            List<LocationPolygonCoordinates> locations = gdb.getPolygonGeoLocation();


            return locations;
        }


    }
}
