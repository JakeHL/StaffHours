using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Reflection;

namespace StaffHours.Utils.Extensions
{
    /// <summary>
    /// Provides extensions methods for the SqlCommand object
    /// </summary>
    public static class SqlCommandExtensions
    {
        /*
         * This static method assumes T has the exact same names and types as the columns in the DB table.
         * Given more time, error handling and type checking should be implemented 
         * so we can safely use this wherever necessary.
         * we also assume that T has a paramaterless constructor
         */

         /// <typeparam name="T">Specified class type to recive from the DB</typeparam>
         /// <param name="com">The command on which to execute against</param>
         /// <returns>An instantiated list of T returned from the database</returns>
        public static List<T> ExecuteReaderToList<T>(this SqlCommand com) where T : new()
        {
            List<T> result = new List<T>();

            // Get metadata of T
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();

            using (SqlDataReader reader = com.ExecuteReader())
            {
                while(reader.Read())
                {
                    // Create a default instance of T so all string values will be null, ints 0, etc.
                    var tInstance = new T();

                    foreach(var propertyInfo in propertyInfos)
                    {
                        // assuming T has the same property names as columns in the table,
                        // we can use them to retrive data from the reader
                        var value = reader[propertyInfo.Name];
                        try
                        {
                            // set the value we retreived on T 
                            propertyInfo.SetValue(tInstance, value, null);
                        }
                        catch
                        {
                            /*
                             * TODO As stated above, given more time we could check for type mismatches.
                             * The main exception we'd expect to get is an ArgumentException with the message:
                             * "value cannot be converted to the type of PropertyType." thrown in propertyInfo.SetValue
                             * Right now the values will just stay at the default value for the respective type.
                             * (This is desired in this simple application)
                             */
                        }                       
                    }
                    result.Add(tInstance);
                }
            }

            return result;
        }
    }
}