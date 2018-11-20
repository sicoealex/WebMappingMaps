using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebMappingMaps.Model;

namespace WebMappingMaps
{
    public class GeoLocationDB
    {

        public List<LocationCoordinates> getGeoLocation()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["TopoDb"].ConnectionString;
            SqlConnection conn;
            conn = new SqlConnection(connectionString);
            conn.Open();

            List<LocationCoordinates> locations = new List<LocationCoordinates>();

            //SqlCommand sqlcmd = new SqlCommand("SELECT  ID1, OBJECTID, Categorie, SHAPE_Leng, geom  FROM Drumuri_Shape", conn);
            SqlCommand sqlcmd = new SqlCommand("SELECT geom.STAsText() , OBJECTID FROM Drumuri_Shape", conn);
            SqlDataReader dr = sqlcmd.ExecuteReader();
            while (dr.Read())
            {
                List<SpatialGeo>  spatialList = new List<SpatialGeo>();
                String tempresult = Convert.ToString(dr[0]);
                tempresult = tempresult.Substring(tempresult.IndexOf("(")+1, tempresult.IndexOf(")") - tempresult.IndexOf("(")-1);

                List<String>  pointDataList = tempresult.Split(',').ToList();
                for(int i = 0; i< pointDataList.Count; i++)
                {
                    String tempLocation = pointDataList[i].ToString().Trim();
                    String latLocat = tempLocation.Substring(tempLocation.IndexOf(' ') + 1, tempLocation.Length - tempLocation.IndexOf(' ')-1);

                    String lngLocat = tempLocation.Substring(0, tempLocation.IndexOf(' '));
                    spatialList.Add(new SpatialGeo(Convert.ToDouble(latLocat), Convert.ToDouble(lngLocat)));
                }
                locations.Add(new LocationCoordinates(dr[1].ToString(), spatialList, "test description"));

            }

            dr.Close();
            conn.Close();

            return locations; 

        }



        public List<LocationPolygonCoordinates> getPolygonGeoLocation()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["TopoDb"].ConnectionString;
            SqlConnection conn;
            conn = new SqlConnection(connectionString);
            conn.Open();

            List<LocationPolygonCoordinates> locations = new List<LocationPolygonCoordinates>();
            SqlCommand sqlcmd = new SqlCommand("SELECT geom.STAsText() , ID1 FROM Bistra_WGS", conn);
            SqlDataReader dr = sqlcmd.ExecuteReader();

            while (dr.Read())
            {
                List<List<SpatialGeo>> spatialList = new List<List<SpatialGeo>>();
                String tempresult = Convert.ToString(dr[0]);

                tempresult = tempresult.Substring(tempresult.IndexOf("(") + 1, tempresult.LastIndexOf(")") - tempresult.IndexOf("(") - 1);
                List<String> tempPolygonSplit = tempresult.Split(new[] { "), (" }, StringSplitOptions.None).ToList();

                for (int i=0; i< tempPolygonSplit.Count; i++)
                {
                    String tempStr = tempPolygonSplit[i].Replace('(',' ').Replace(')', ' ').Trim();

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

                locations.Add(new LocationPolygonCoordinates(dr[1].ToString(), spatialList, "test description"));

            }

            dr.Close();
            conn.Close();

            return locations;

        }

    }
}