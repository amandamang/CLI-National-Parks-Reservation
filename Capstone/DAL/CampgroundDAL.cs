using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class CampgroundDAL
    {
        private string _connectionString;

        // Single Parameter Constructor
        public CampgroundDAL(string dbConnectionString)
        {
            _connectionString = dbConnectionString;
        }

        public List<Campground> GetDepartments()
        {
            List<Department> output = new List<Department>();

            //Always wrap connection to a database in a try-catch block

            //Create a SqlConnection to our database
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                const string sqlEmployeeDepartments = "SELECT department_id, name " +
                                        "FROM department " +
                                        "Order By department_id;";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlEmployeeDepartments;
                cmd.Connection = connection;
                //cmd.Parameters.AddWithValue("@employee", employee);

                // Execute the query to the database
                SqlDataReader reader = cmd.ExecuteReader();

                // The results come back as a SqlDataReader. Loop through each of the rows
                // and add to the output list
                while (reader.Read())
                {
                    Department department = new Department();

                    // Read in the value from the reader
                    // Reference by index or by column_name
                    department.Id = Convert.ToInt32(reader["department_id"]);
                    department.Name = Convert.ToString(reader["name"]);

                    // Add the continent to the output list
                    output.Add(department);
                }
            }


            // Return the list of continents
            return output;
        }
    }
}
