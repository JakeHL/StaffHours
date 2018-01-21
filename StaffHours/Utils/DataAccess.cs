using StaffHours.Models;
using StaffHours.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace StaffHours.Utils
{
    /// <summary>
    /// Provides methods for us to retreive data from our DB
    /// </summary>
    public static class DataAccess
    {
        public static string ConnString => ConfigurationManager.ConnectionStrings["StaffHoursDb"].ConnectionString;

        /// <returns>Returns a list of all Employee from the connected DB</returns>
        public static List<Employee> GetEmployees()
        {
            List<Employee> result;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                StringBuilder query = new StringBuilder("SELECT * FROM [dbo].[Employee]");
                conn.Open();
                using (SqlCommand comm = new SqlCommand(query.ToString(), conn))
                    result = comm.ExecuteReaderToList<Employee>();                    
            }
            return result;
        }

        public static List<Shifts> GetShifts()
        {
            List<Shifts> result;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                StringBuilder query = new StringBuilder("SELECT * FROM [dbo].[Shifts]");
                conn.Open();
                using (SqlCommand comm = new SqlCommand(query.ToString(), conn))
                    result = comm.ExecuteReaderToList<Shifts>();
            }
            return result;
        }

        public static List<Employee_Works_Shift> GetEmployee_Works_Shift()
        {
            List<Employee_Works_Shift> result;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                StringBuilder query = new StringBuilder("SELECT * FROM [dbo].[Employee_Works_Shift]");
                conn.Open();
                using (SqlCommand comm = new SqlCommand(query.ToString(), conn))
                    result = comm.ExecuteReaderToList<Employee_Works_Shift>();
            }
            return result;
        }
    }
}