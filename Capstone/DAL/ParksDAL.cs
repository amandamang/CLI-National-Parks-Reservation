using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ParksDAL
    {
        private string _connectionString;

        // Single Parameter Constructor
        public ParksDAL(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<Park> GetParks()
        {
            List<Park> output = new List<Park>();

            //Always wrap connection to a database in a try-catch block

            //Create a SqlConnection to our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlParks = "SELECT park_id, name, location, establish_date, area, visitors, description " +
                                        "FROM park " +
                                        "Order By name;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlParks;
                cmd.Connection = connection;
                //cmd.Parameters.AddWithValue("@employee", employee);

                // Execute the query to the database
                SqlDataReader reader = cmd.ExecuteReader();

                // The results come back as a SqlDataReader. Loop through each of the rows
                // and add to the output list
                while (reader.Read())
                {
                    Park park = new Park();

                    // Read in the value from the reader
                    // Reference by index or by column_name
                    park.ParkId = Convert.ToInt32(reader["park_id"]);
                    park.Name = Convert.ToString(reader["name"]);
                    park.Location = Convert.ToString(reader["location"]);
                    park.EstablishDate = Convert.ToDateTime(reader["establish_date"]);
                    park.Area = Convert.ToInt32(reader["area"]);
                    park.Visitors = Convert.ToInt32(reader["visitors"]);
                    park.Description = Convert.ToString(reader["description"]);

                    // Add the continent to the output list
                    output.Add(park);
                }
            }


            // Return the list of continents
            return output;
        }
    }
}
