using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LittleCofeeShopWebApp.Domain.Entities
{
    public class Recipe
    {
        public string CofeeName { get; set; }
        public decimal CofeePriceCoef { get; set; }
        public decimal Volume { get; set; }
        public decimal CupPrice { get; set; }
        public class Options
        {
            public bool IsCupCap { get; set; }
            public string MilkUnitName { get; set; }
            public decimal MilkSize { get; set; }
            public decimal MilkPrice { get; set; }
            public int SugarUnitId { get; set; }
            public decimal SugarSize { get; set; }
            public decimal SugarPrice { get; set; }
        }
    }
}