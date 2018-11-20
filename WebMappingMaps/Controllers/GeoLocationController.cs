using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMappingMaps.Model;

namespace WebMappingMaps.Controllers
{
    public class GeoLocationController : ApiController
    {

        [HttpGet]
        public List<LocationCoordinates> GetSpatialGeos()
        {
            GeoLocationDB gdb = new GeoLocationDB();
            List<LocationCoordinates> locations =  gdb.getGeoLocation();


            return locations;
        }



    }
}
