using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMappingMaps.Model
{
    public class LegendModel
    {

        public string tipCultura { get; set; }
        public string collorMap { get; set; }

        public LegendModel() { }

        public LegendModel(string tipCultura, string collorMap)
        {
            this.tipCultura = tipCultura;
            this.collorMap = collorMap;
        }
    }
}