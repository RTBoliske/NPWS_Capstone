using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;

namespace Capstone.Web.DAL.Interfaces
{
    public interface IParkDAL
    {
        //park
        Park GetPark(string parkCode);
        List<Park> GetAllParks();

        //weather
        List<Weather> GetWeatherForPark(string parkCode);

		//Survey
		//List<Park> GetSurveyResults();
		List<SurveyResult> GetSurveyResults();
		int AddSurvey(Survey newSurvey);

    }
}
