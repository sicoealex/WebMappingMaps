using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebMappingMaps.Model;

namespace WebMappingMaps.DBInteraction
{
    public class DBTransaviaF11
    {

        public List<LocationPolygonCoordinates> getPolygonGeoLocation()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["TransavF11"].ConnectionString;
            SqlConnection conn;
            conn = new SqlConnection(connectionString);
            conn.Open();

            List<LocationPolygonCoordinates> locations = new List<LocationPolygonCoordinates>();
            SqlCommand sqlcmd = new SqlCommand("select OBJECTID, TIP_CULTUR, Suprafata, MAP_COLLOR, geom.STAsText() as LAT_LAG from Culturi_Ferma_11", conn);
            SqlDataReader dr = sqlcmd.ExecuteReader();

            while (dr.Read())
            {
                List<List<SpatialGeo>> spatialList = new List<List<SpatialGeo>>();
                String tempresult = Convert.ToString(dr["LAT_LAG"]);
                spatialList = getPolygonSpatialLoc(tempresult);

                TransaviaFerma tf = new TransaviaFerma(dr["TIP_CULTUR"].ToString(), dr["Suprafata"].ToString(), dr["MAP_COLLOR"].ToString());

                locations.Add(new LocationPolygonCoordinates(dr["OBJECTID"].ToString(), spatialList, dr["TIP_CULTUR"].ToString(), tf));

            }

            dr.Close();
            conn.Close();
            return locations;

        }

        public List<LegendModel> getLegentModel()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["TransavF11"].ConnectionString;
            SqlConnection conn;
            conn = new SqlConnection(connectionString);
            conn.Open();

            List<LegendModel> leg = new List<LegendModel>();
            SqlCommand sqlcmd = new SqlCommand("select distinct TIP_CULTUR, MAP_COLLOR from Culturi_Ferma_11", conn);
            SqlDataReader dr = sqlcmd.ExecuteReader();

            while (dr.Read())
            {
                leg.Add(new LegendModel(dr["TIP_CULTUR"].ToString(), dr["MAP_COLLOR"].ToString()));
            
            }

            dr.Close();
            conn.Close();
            return leg;
        }

        public List<List<SpatialGeo>> getPolygonSpatialLoc (String tempresult)
        {
            List<List<SpatialGeo>> spatialList = new List<List<SpatialGeo>>();

            tempresult = tempresult.Substring(tempresult.IndexOf("(") + 1, tempresult.LastIndexOf(")") - tempresult.IndexOf("(") - 1);
            List<String> tempPolygonSplit = tempresult.Split(new[] { "), (" }, StringSplitOptions.None).ToList();

            for (int i = 0; i < tempPolygonSplit.Count; i++)
            {
                String tempStr = tempPolygonSplit[i].Replace('(', ' ').Replace(')', ' ').Trim();

                List<SpatialGeo> spList = new List<SpatialGeo>();

                List<String> pointDataList = tempStr.Split(',').ToList();
                for (int j = 0; j < pointDataList.Count; j++)
                {
                    String tempLocation = pointDataList[j].ToString().Trim();
                    String latLocat = tempLocation.Substring(tempLocation.IndexOf(' ') + 1, tempLocation.Length - tempLocation.IndexOf(' ') - 1);

                    String lngLocat = tempLocation.Substring(0, tempLocation.IndexOf(' '));
                    spList.Add(new SpatialGeo(Convert.ToDouble(latLocat), Convert.ToDouble(lngLocat)));
                }

                spatialList.Add(spList);
            }

            return spatialList;
        }


    }
}