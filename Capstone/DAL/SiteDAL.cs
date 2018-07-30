using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAL
{
    public class SiteDAL
    {
        private string _connectionString;

        // Single Parameter Constructor
        public SiteDAL(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<Site> GetSites(CampGround camp)
        {
            List<Site> output = new List<Site>();

            //Always wrap connection to a database in a try-catch block

            //Create a SqlConnection to our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlSites = "SELECT site_id, campground_id, site_number, max_occupancy, accessible, max_rv_length, utilities " +
                                        "FROM site " +
                                        "WHERE campground_id = @campgroundId " +
                                        "Order By campground_id;";

                SqlCommand cmd = new SqlCommand();
                Site site = new Site();
                cmd.CommandText = sqlSites;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@campgroundId", camp.CampgroundId);

                // Execute the query to the database
                SqlDataReader reader = cmd.ExecuteReader();

                // The results come back as a SqlDataReader. Loop through each of the rows
                // and add to the output list
                while (reader.Read())
                {
                    CampGround campground = new CampGround();
                    site = new Site();
                    // Read in the value from the reader
                    // Reference by index or by column_name
                    site.SiteId = Convert.ToInt32(reader["site_id"]);
                    site.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    site.SiteNumber = Convert.ToInt32(reader["site_number"]);
                    site.MaxOccupancy = Convert.ToInt32(reader["max_occupancy"]);
                    site.Accessible = Convert.ToBoolean(reader["accessible"]);
                    site.MaxRvLength = Convert.ToInt32(reader["max_rv_length"]);
                    site.Utilities = Convert.ToBoolean(reader["utilities"]);
                    

                    // Add the continent to the output list
                    output.Add(site);
                }
            }


            // Return the list of continents
            return output;
        }
    }
}
