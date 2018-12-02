﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMappingMaps.Model
{
    public class TransaviaFerma
    {
        public string tipCultura { get; set; }
        public string suprafata { get; set; }
        public string collorMap { get; set; }

        public TransaviaFerma() { }

        public TransaviaFerma(string tipCultura, string suprafata, string collorMap)
        {
            this.tipCultura = tipCultura;
            this.suprafata = suprafata;
            this.collorMap = collorMap;
        }
    }
}