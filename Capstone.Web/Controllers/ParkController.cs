using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class ParkController : Controller
    {
        private IParkDAL _parkDal;

        public ParkController(IParkDAL parkDal)
        {
            _parkDal = parkDal;
        }

        // GET: Park
        public ActionResult Index()
        {
            var parks = _parkDal.GetAllParks();

            return View("Index", parks);
        }

        // GET: Park/Detail
        public ActionResult Detail(string id)
        {
            Park park = _parkDal.GetPark(id);

            return View("ParkDetails", park);
        }

    }
}