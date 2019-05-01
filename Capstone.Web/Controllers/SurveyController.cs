using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
		private IParkDAL _parkDal;

		public SurveyController(IParkDAL parkDal)
		{
			_parkDal = parkDal;
		}

		//GET: Park/Survey
		public ActionResult Survey()
		{
			return View("Survey");
		}

		//POST: Park/Survey
		[HttpPost]
		public ActionResult NewSurvey(Survey survey)
		{
			try
			{
				Survey newSurvey = new Survey();

				newSurvey.ParkCode = survey.ParkCode;
				newSurvey.EmailAddress = survey.EmailAddress;
				newSurvey.State = survey.State;
				newSurvey.ActivityLevel = survey.ActivityLevel;

				_parkDal.AddSurvey(survey);
			}
			catch (Exception ex)
			{
				throw;
			}
			return RedirectToAction("SurveyResults");
		}

		// GET: SurveyResults from redirect
		public ActionResult SurveyResults()
		{
			var surveyResults = _parkDal.GetSurveyResults();

			return View("SurveyResults", surveyResults);
		}

	}
}