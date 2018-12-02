using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebMappingMaps.DBInteraction;
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


        [HttpGet]
        public List<LocationPolygonCoordinates> getTransaviaSpatialLocation()
        {
            DBTransaviaF11 dbtrsv = new DBTransaviaF11();
            List<LocationPolygonCoordinates> locations = dbtrsv.getPolygonGeoLocation();

            return locations;
        }

        [HttpGet]
        public List<LegendModel> getLegendModels()
        {
            DBTransaviaF11 dbtrsv = new DBTransaviaF11();
            List<LegendModel> retList = dbtrsv.getLegentModel();
            return retList;
        }

    }
}
