using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Park
    {
        public string ParkCode { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public int Acreage { get; set; }
        public int ElevationInFeet { get; set; }
        public decimal MilesOfTrail { get; set; }
        public int NumberOfCampsites { get; set; }
        public string Climate { get; set; }
        public int YearFounded { get; set; }
        public int AnnualVisitorCount { get; set; }
        public string InspirationalQuote { get; set; }
        public string InspirationalQuoteSource { get; set; }
        public string ParkDescription { get; set; }
        public int EntryFee { get; set; }
        public int NumberOfAnimalSpecies { get; set; }

        public List<Weather> WeatherForecasts { get; set; }

        public static List<SelectListItem> GetParkNames()
        {
            List<SelectListItem> ParkNames = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Cuyahoga Valley National Park", Value = "CVNP" },
                new SelectListItem() { Text = "Everglades National Park", Value = "ENP" },
                new SelectListItem() { Text = "Grand Canyon National Park", Value = "GCNP" },
                new SelectListItem() { Text = "Glacier National Park", Value = "GNP" },
                new SelectListItem() { Text = "Great Smokey Mountains National Park", Value = "GSMNP" },
                new SelectListItem() { Text = "Grand Teton National Park", Value = "GTNP" },
                new SelectListItem() { Text = "Mount Rainier National Park", Value = "MRNP" },
                new SelectListItem() { Text = "Rocky Mountain National Park", Value = "RMNP" },
                new SelectListItem() { Text = "Yellowstone National Park", Value = "YNP" },
                new SelectListItem() { Text = "Yosemite National Park", Value = "YNP2" },
            };

            return ParkNames;
        }
    }
}