using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class ReservationDAL
    {
        private string _connectionString;

        // Single Parameter Constructor
        public ReservationDAL(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public bool CreateReservation(string name, DateTime fromDate, DateTime toDate, int siteId)
        {
            bool wasSuccessful = true;
            //Connect to the database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Create sql statement
                const string sqlInsertReservation = "Insert Into reservation (site_id, name, from_date, to_date, create_date)" +
                                                 "Values (@siteId, @name, @fromDate, @toDate, CURRENT_TIMESTAMP);";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlInsertReservation;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@siteId", siteId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@fromDate", fromDate);
                cmd.Parameters.AddWithValue("@toDate", toDate);

                //Send command to database
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    wasSuccessful = false;
                }
                return wasSuccessful;
            }
        }

        public List<Reservation> GetTopFiveReservations(string campgroundName, DateTime startDate, DateTime endDate)
        {
            List<Reservation> output = new List<Reservation>();

            //Create a SqlConnection to our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlReservation = "SELECT TOP (5) campground_id, name, open_from_mm, open_to_mm, daily_fee " +
                                        "FROM campground;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlReservation;
                cmd.Connection = connection;
                //cmd.Parameters.AddWithValue("@firstName", firstname);
                //cmd.Parameters.AddWithValue("@lastName", lastname);


                // Execute the query to the database
                SqlDataReader reader = cmd.ExecuteReader();

                // The results come back as a SqlDataReader. Loop through each of the rows
                // and add to the output list
                // select * from reservation
                // where(from_date > '01-06-2017' and from_date > '02-06-2017')
                // and(to_date < '01-06-2017' and to_date > '01-06-2017');

                while (reader.Read())
                {
                    CampGround campground = new CampGround();

                    // Read in the value from the reader
                    // Reference by index or by column_name
                    campground.CampgroundId = Convert.ToInt32(reader["campground_id"]);
                    campground.Name = Convert.ToString(reader["name"]);
                    campground.OpenFromMm = Convert.ToInt32(reader["open_from_mm"]);
                    campground.OpenToMm = Convert.ToInt32(reader["open_to_mm"]);
                    campground.DailyFee = Convert.ToDouble(reader["daily_fee"]);

                    // Add the continent to the output list
                    //output.Add(campground);
                }
            }


            // Return the list of continents
            return output;
        }

        //public List<Reservation> SearchReservations(int campgroundId, DateTime startDate, DateTime endDate)
        public List<Reservation> SearchReservations(Reservation reservation)
        {
            List<Reservation> output = new List<Reservation>();
            //Create a SqlConnection to our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlReservation = "SELECT * " +
                                              "FROM reservation " +
                                              "JOIN site " +
                                              "ON reservation.site_id = site.site_id " +
                                              "WHERE from_date not BETWEEN @startDate AND @endDate;";


                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlReservation;
                cmd.Connection = connection;


                string SqlStartDate = reservation.ToDate.ToShortDateString();
                string SqlEndDate = reservation.FromDate.ToShortDateString();

                cmd.Parameters.AddWithValue("@startDate", SqlStartDate);
                cmd.Parameters.AddWithValue("@endDate", SqlEndDate);
                
                //int reservationCount = Convert.ToInt32(cmd.ExecuteScalar());
                //return reservationCount;
                
                output.Add(reservation);
                
            }
            return output;
        }
    }
}