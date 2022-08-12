using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace SqlDataAccess
{
    class SqlDBManagerFactoryClass
    {
        //public enum DataProvider
        //{
        //    Oracle, SqlServer, Odbc, OleDb
        //}

        /// <summary>
        /// constructor with no parameter
        /// </summary>
        private SqlDBManagerFactoryClass()
        {
        }

        /// <summary>
        /// Returns a connection instance based on the provider type
        /// </summary>
        /// <param name="providerType"> Provider type from the DataProvider enumerator </param>
        /// <returns> The connection instance specific to the provider type </returns>

        //Sql
        public static SqlConnection GetConnection()
        {
            return new SqlConnection();
        }

        /// <summary>
        /// Returns a command instance based on the provider type
        /// </summary>
        /// <param name="providerType">Provider type from the DataProvider enumerator</param>
        /// <returns>The command instance specific to the provider type </returns>      

        //Sql
        public static SqlCommand GetCommand()
        {
            return new SqlCommand();
        }


        /// <summary>
        ///  Returns a data adapter instance based on the provider type
        /// </summary>
        /// <param name="providerType">Provider type from the DataProvider enumerator</param>
        /// <returns>The data adapter instance specific to the provider type</returns>

        public static SqlDataAdapter GetDataAdapter()
        {
            return new SqlDataAdapter();
        }

        /// <summary>
        ///  Returns a parameter instance based on the provider type
        /// </summary>
        /// <param name="providerType">Provider type from the DataProvider enumerator</param>
        /// <returns>The parameter instance specific to the provider type</returns>    


        public static SqlParameter GetParameter()
        {
            return new SqlParameter();

        }
        /// <summary>
        ///  Returns an array of parameter instances based on the provider type and number of parameters
        /// </summary>
        /// <param name="providerType">Provider type from the DataProvider enumerator</param>
        /// <param name="paramsCount">The number of parameters in the command</param>
        /// <returns>Array of parameters</returns>

        //Oracel
        public static SqlParameter[] GetParameters(int paramsCount)
        {
            SqlParameter[] dbParameters = new SqlParameter[paramsCount];
            for (int i = 0; i < paramsCount; ++i)
            {
                dbParameters[i] = new SqlParameter();
            }
            return dbParameters;
        }
    }
}
