using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class CampgroundsDAL
    {
        private string _connectionString;

        // Single Parameter Constructor
        public CampgroundsDAL(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<CampGround> GetCampGrounds(Park park)
        {
            List<CampGround> output = new List<CampGround>();

            //Always wrap connection to a database in a try-catch block

            //Create a SqlConnection to our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlCampgrounds = "SELECT campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee " +
                                        "FROM campground " +
                                        "WHERE park_id = @parkId " +
                                        "Order By name;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlCampgrounds;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@parkId", park.ParkId);
                
                // Execute the query to the database
                SqlDataReader reader = cmd.ExecuteReader();

                // The results come back as a SqlDataReader. Loop through each of the rows
                // and add to the output list
                while (reader.Read())
                {
                    CampGround campground = new CampGround();

                    // Read in the value from the reader
                    // Reference by index or by column_name
                    campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    campground.ParkId = Convert.ToInt32(reader["park_id"]);
                    campground.Name = Convert.ToString(reader["name"]);
                    campground.OpenFromMm = Convert.ToInt32(reader["open_from_mm"]);
                    campground.OpenToMm = Convert.ToInt32(reader["open_to_mm"]);
                    campground.DailyFee = Convert.ToDouble(reader["daily_fee"]);

                    // Add the continent to the output list
                    output.Add(campground);
                }
            }


            // Return the list of continents
            return output;
        }
    }
}
