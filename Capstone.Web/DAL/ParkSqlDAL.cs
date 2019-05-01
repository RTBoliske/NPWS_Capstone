using Capstone.Web.DAL.Interfaces;
using Capstone.Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkDAL
    {
        private const string SQL_GetAllParks = "SELECT * FROM park ORDER BY parkName ASC;";

        private const string SQL_GetParkById = "SELECT * FROM park WHERE parkCode = @parkCode";

        private const string SQL_GetWeather = "SELECT parkCode, fiveDayForecastValue, low as lowtemp, high as hightemp, forecast  " +
                                              "FROM weather WHERE parkCode = @parkCode;";

        private const string SQL_AddNewSurvey = "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                                                "VALUES (@parkCode, @emailAddress, @state, @activityLevel);SELECT CAST(SCOPE_IDENTITY() as int);";

		private const string SQL_GetSurveyResults = "SELECT park.parkCode, park.parkName, COUNT(survey_result.parkCode) as surveyCount " +
													"FROM park " +
													"JOIN survey_result ON park.parkCode = survey_result.parkCode " +
													"GROUP BY park.parkName, park.parkCode " +
													"ORDER BY surveyCount DESC;";
		//private const string SQL_GetSurveyResults = "SELECT TOP(3) survey_result.parkCode, park.parkName, COUNT(survey_result.parkCode) as surveyCount " +
		//                                            "FROM survey_result " +
		//                                            "JOIN park ON survey_result.parkCode = park.parkCode " +
		//                                            "GROUP BY survey_result.parkCode, park.parkName " +
		//                                            "ORDER BY parkCount DESC;";


		private string _connectionString = "";

        public ParkSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Park> GetAllParks()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    List<Park> parks = conn.Query<Park>(SQL_GetAllParks).ToList();
                    foreach (Park list in parks)
                    {
                        list.WeatherForecasts = GetWeatherForPark(list.ParkCode);
                    }

                    return parks;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public Park GetPark(string parkCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    Park park = conn.Query<Park>(SQL_GetParkById,
                         new { parkCode = parkCode }).FirstOrDefault();

                    park.WeatherForecasts = GetWeatherForPark(parkCode);

                    return park;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Weather> GetWeatherForPark(string parkCode)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    List<Weather> weatherForecasts = conn.Query<Weather>(SQL_GetWeather,
                        new { parkCode = parkCode }).ToList();

                    return weatherForecasts;
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public int AddSurvey(Survey newSurvey)
        {
            int result = 0;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = SQL_AddNewSurvey;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                cmd.Parameters.AddWithValue("@state", newSurvey.State);
                cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                result = (int)cmd.ExecuteScalar();
            }

            return result;
        }

		//public List<Park> GetSurveyResults()
		//{
		//	try
		//	{
		//		using (SqlConnection conn = new SqlConnection(_connectionString))
		//		{
		//			conn.Open();

		//			List<Park> results = conn.Query<Park>(SQL_GetSurveyResults).ToList();

		//			return results;
		//		}
		//	}
		//	catch (SqlException ex)
		//	{
		//		throw;
		//	}
		//}

		public List<SurveyResult> GetSurveyResults()
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(_connectionString))
				{
					conn.Open();

					List<SurveyResult> results = conn.Query<SurveyResult>(SQL_GetSurveyResults).ToList();

					return results;
				}
			}
			catch (SqlException ex)
			{
				throw;
			}
		}

		private Park PopulateParkFromReader(SqlDataReader reader)
        {
            Park park = new Park();

            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.ParkName = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acreage = Convert.ToInt32(reader["acreage"]);
            park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToDecimal(reader["milesofTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
            park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.ParkDescription = Convert.ToString(reader["parkDescription"]);
            park.EntryFee = Convert.ToInt16(reader["entryFee"]);
            park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

            return park;
        }

        private Weather PopulateWeatherFromReader(SqlDataReader reader)
        {
            Weather weather = new Weather();

            weather.ParkCode = Convert.ToString(reader["parkCode"]);
            weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
            weather.LowTemp = Convert.ToInt32(reader["low"]);
            weather.HighTemp = Convert.ToInt32(reader["high"]);
            weather.Forecast = Convert.ToString(reader["forecast"]);

            return weather;
        }
    }
}