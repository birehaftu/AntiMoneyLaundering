using System;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace SqlDataAccess
{
    public class SqlDBManagerClass
    {
        private SqlConnection dbConnection;
        private SqlDataReader dataReader;
        private SqlCommand dbCommand;
        private SqlParameter[] dbParameters = null;
        private string strConnection;
        public enum ParamtDirection
        {
            Input, Output, returnValue, InputOutput
        }
        /// <summary>
        /// Constructor with no parameter
        /// </summary>
        public SqlDBManagerClass()
        {

        }

        /// <summary>
        /// Constructs the DBManagerClass with a provider type and connection string parameters
        /// </summary>
        /// <param name="providerType">Provider type from the SqlDBManagerFactoryClass</param>
        /// <param name="connectionString">A string which describes the connection string</param>      
        public SqlDBManagerClass(string connectionString)
        {
            this.strConnection = connectionString;
        }

        /// <summary>
        /// Gets the connection
        /// </summary>      
        public SqlConnection Connection
        {
            get { return dbConnection; }
        }

        /// <summary>
        /// Gets and sets the data reader
        /// </summary>            
        public SqlDataReader DataReader
        {
            get
            {
                return dataReader;
            }
            set
            {
                dataReader = value;
            }
        }

        /// <summary>
        /// Gets and sets the connection string
        /// </summary>        
        public string ConnectionString
        {
            get
            {
                return strConnection;
            }
            set
            {
                strConnection = value;
            }
        }

        /// <summary>
        /// Gets the command
        /// </summary>      
        public SqlCommand Command
        {
            get
            {
                return dbCommand;
            }
        }

        /// <summary>
        /// Gets the parameters
        /// </summary>        
        public SqlParameter[] Parameters
        {
            get
            {
                return dbParameters;
            }
            set
            {
                dbParameters = value;
            }
        }
        /// <summary>
        /// Open connection to the database
        /// </summary>       
        public void Open()
        {
            dbConnection = SqlDBManagerFactoryClass.GetConnection();
            dbConnection.ConnectionString = this.ConnectionString;
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            this.dbCommand = SqlDBManagerFactoryClass.GetCommand();
        }
        /// <summary>
        /// Closes an open connection
        /// </summary>       
        public void Close()
        {
            if (dbConnection.State != ConnectionState.Closed)
                dbConnection.Close();
        }

        /// <summary>
        /// Disposes the command and connecion
        /// </summary>    

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Close();
            this.dbCommand = null;
            this.dbConnection = null;
        }
        /// <summary>
        /// Creates parameters of paramsCount size
        /// </summary>
        /// <param name="paramsCount">An integer which describes the number of parameters</param> 
        /// 
        public void CreateParameters(int paramsCount)
        {
            dbParameters = new SqlParameter[paramsCount];
            dbParameters = SqlDBManagerFactoryClass.GetParameters(paramsCount);
        }

        /// <summary>
        /// Assigns the parameter name and value to each parameter in the array
        /// </summary>
        /// <param name="index">The index of the parameter</param>
        /// <param name="paramName">The name of the parameter</param>
        /// <param name="objValue">The value of the parameter</param>
        /// 
        public void AddParameters(int index, string paramName, object objValue, ParamtDirection parmDirection)
        {
            if (index < dbParameters.Length)
            {
                dbParameters[index].ParameterName = paramName;

                switch (parmDirection)
                {
                    case ParamtDirection.Input:
                        dbParameters[index].Value = objValue;
                        dbParameters[index].Direction = ParameterDirection.Input;
                        break;
                    case ParamtDirection.InputOutput:
                        dbParameters[index].Value = objValue;
                        dbParameters[index].Size = 80;
                        dbParameters[index].Direction = ParameterDirection.InputOutput;
                        break;
                    case ParamtDirection.Output:
                        dbParameters[index].Size = 80;
                        dbParameters[index].Direction = ParameterDirection.Output;
                        break;
                    case ParamtDirection.returnValue:
                        dbParameters[index].Direction = ParameterDirection.ReturnValue;
                        break;

                }
            }
        }
        public void AddParameters(int index, string paramName, object objValue, ParamtDirection parmDirection, SqlDbType type)
        {
            if (index < dbParameters.Length)
            {
                dbParameters[index].ParameterName = paramName;

                switch (parmDirection)
                {
                    case ParamtDirection.Input:
                        dbParameters[index].Value = objValue;
                        dbParameters[index].Direction = ParameterDirection.Input;
                        dbParameters[index].SqlDbType = type;
                        break;
                    case ParamtDirection.InputOutput:
                        dbParameters[index].Value = objValue;
                        dbParameters[index].Size = 80;
                        dbParameters[index].Direction = ParameterDirection.InputOutput;
                        dbParameters[index].SqlDbType = type;
                        break;
                    case ParamtDirection.Output:
                        dbParameters[index].Size = 80;
                        dbParameters[index].Direction = ParameterDirection.Output;
                        dbParameters[index].SqlDbType = type;
                        break;
                    case ParamtDirection.returnValue:
                        dbParameters[index].Direction = ParameterDirection.ReturnValue;
                        dbParameters[index].SqlDbType = type;
                        break;

                }
            }
        }
        public void AddParameters(string[] paramName, object[] objValue)
        {
            for (int i = 0; i < paramName.Length; i++)
            {
                if (i < dbParameters.Length)
                {
                    dbParameters[i].ParameterName = paramName[i];
                    dbParameters[i].Value = objValue[i];
                    dbParameters[i].Direction = ParameterDirection.Input;
                }
            }
        }
        public void AddParameters(string[] paramName, object[] objValue, object[] parmDirection)
        {
            for (int i = 0; i < paramName.Length; i++)
            {
                if (i < dbParameters.Length)
                {
                    dbParameters[i].ParameterName = paramName[i];
                    switch ((ParamtDirection)parmDirection[i])
                    {
                        case ParamtDirection.Input:
                            dbParameters[i].Value = objValue[i];
                            dbParameters[i].Direction = ParameterDirection.Input;
                            break;
                        case ParamtDirection.InputOutput:
                            dbParameters[i].Value = objValue[i];
                            dbParameters[i].Size = 80;
                            dbParameters[i].Direction = ParameterDirection.InputOutput;
                            break;
                        case ParamtDirection.Output:
                            dbParameters[i].Size = 80;
                            dbParameters[i].Direction = ParameterDirection.Output;
                            break;
                        case ParamtDirection.returnValue:
                            dbParameters[i].Direction = ParameterDirection.ReturnValue;
                            break;
                    }
                }
            }
        }
        public void AddParameters(string[] paramName, object[] objValue, object[] parmDirection, SqlDbType[] type)
        {
            for (int i = 0; i < paramName.Length; i++)
            {
                if (i < dbParameters.Length)
                {
                    dbParameters[i].ParameterName = paramName[i];
                    switch ((ParamtDirection)parmDirection[i])
                    {
                        case ParamtDirection.Input:
                            dbParameters[i].Value = objValue[i];
                            dbParameters[i].Direction = ParameterDirection.Input;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                        case ParamtDirection.InputOutput:
                            dbParameters[i].Value = objValue[i];
                            dbParameters[i].Size = 80;
                            dbParameters[i].Direction = ParameterDirection.InputOutput;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                        case ParamtDirection.Output:
                            dbParameters[i].Size = 80;
                            dbParameters[i].Direction = ParameterDirection.Output;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                        case ParamtDirection.returnValue:
                            dbParameters[i].Direction = ParameterDirection.ReturnValue;
                            dbParameters[i].SqlDbType = type[i];
                            break;

                    }
                }
            }
        }
        public void AddParameters(string[] paramName, object[] parmDirection, SqlDbType[] type)
        {
            for (int i = 0; i < paramName.Length; i++)
            {
                if (i < dbParameters.Length)
                {
                    dbParameters[i].ParameterName = paramName[i];
                    switch ((ParamtDirection)parmDirection[i])
                    {
                        case ParamtDirection.Input:
                            dbParameters[i].Value = (object)paramName[i];
                            dbParameters[i].Direction = ParameterDirection.Input;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                        case ParamtDirection.InputOutput:
                            dbParameters[i].Value = (object)paramName[i];
                            dbParameters[i].Size = 80;
                            dbParameters[i].Direction = ParameterDirection.InputOutput;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                        case ParamtDirection.Output:
                            dbParameters[i].Size = 80;
                            dbParameters[i].Direction = ParameterDirection.Output;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                        case ParamtDirection.returnValue:
                            dbParameters[i].Direction = ParameterDirection.ReturnValue;
                            dbParameters[i].SqlDbType = type[i];
                            break;
                    }
                }
            }
        }
        public void AddParameters(SqlDbType[] type, string[] paramName, object[] objValue)
        {
            for (int i = 0; i < paramName.Length; i++)
            {
                if (i < dbParameters.Length)
                {
                    dbParameters[i].ParameterName = paramName[i];

                    dbParameters[i].Value = objValue[i];
                    dbParameters[i].Direction = ParameterDirection.Input;
                    dbParameters[i].SqlDbType = type[i];
                }
            }
        }
        /// <summary>
        /// Returns a data reader taking command type and command text as an input
        /// </summary>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <returns>A data reader</returns>       
        public SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            this.dbCommand = SqlDBManagerFactoryClass.GetCommand();
            dbCommand.Connection = this.Connection;
            PrepareCommand(dbCommand, this.Connection, commandType, commandText, this.Parameters);
            this.DataReader = dbCommand.ExecuteReader();
            dbCommand.Parameters.Clear();
            return this.DataReader;
        }

        /// <summary>
        /// Closes an opened reader
        /// </summary>                
        public void CloseReader()
        {
            if (this.DataReader != null)
                this.DataReader.Close();
        }

        /// <summary>
        /// Adds the parameters taking command and command parameters as an input
        /// </summary>
        /// <param name="command">Describes whether it is SqlCommand, OleDbCommand, OdbcCommand or SqlCommand</param>
        /// <param name="commandParameters">Describes whether it is SqlParameter, OleDbParameter, OdbcParameter or SqlParameter</param>
        private void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            foreach (SqlParameter dbParameter in commandParameters)
            {
                if ((dbParameter.Direction == ParameterDirection.InputOutput)
                &&
                  (dbParameter.Value == null))
                {
                    dbParameter.Value = DBNull.Value;
                }
                command.Parameters.Add(dbParameter);
            }
        }

        /// <summary>
        /// Defines all the necessary properties for a command
        /// </summary>
        /// <param name="command">Describes whether it is SqlCommand, OleDbCommand, OdbcCommand or SqlCommand</param>
        /// <param name="connection">Describes whether it is SqlConnection, OleDbConnection, OdbcConnection or SqlConnection</param>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <param name="commandParameters">Describes whether it is SqlParameter, OleDbParameter, OdbcParameter or SqlParameter</param>
        private void PrepareCommand(SqlCommand command, SqlConnection connection,
          CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {
            command.Connection = connection;
            command.CommandText = commandText;
            command.CommandType = commandType;

            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
        }

        /// <summary>
        /// Executes a non query command
        /// </summary>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <returns>Number of rows affected</returns>       
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            this.dbCommand = SqlDBManagerFactoryClass.GetCommand();
            PrepareCommand(dbCommand, this.Connection, commandType, commandText, this.Parameters);
            int returnValue = dbCommand.ExecuteNonQuery();
            //dbCommand.Parameters.Clear();

            return returnValue;
        }

        /// <summary>
        /// Executes a scalar command
        /// </summary>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <returns>The first column of the first row</returns>        
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            this.dbCommand = SqlDBManagerFactoryClass.GetCommand();
            PrepareCommand(dbCommand, this.Connection, commandType, commandText, this.Parameters);
            object returnValue = dbCommand.ExecuteScalar();
            dbCommand.Parameters.Clear();
            return returnValue;
        }

        /// <summary>
        /// Executes a SQL/PLSQl query
        /// </summary>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <returns>A single or multiple tables</returns>       
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            this.dbCommand = SqlDBManagerFactoryClass.GetCommand();
            PrepareCommand(dbCommand, this.Connection, commandType, commandText, this.Parameters);
            DbDataAdapter dataAdapter = SqlDBManagerFactoryClass.GetDataAdapter();
            dataAdapter.SelectCommand = dbCommand;
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dbCommand.Parameters.Clear();

            return dataSet;
        }

        /// <summary>
        /// Executes a SQL/PLSQl query
        /// </summary>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <returns>A single table</returns>        
        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            this.dbCommand = SqlDBManagerFactoryClass.GetCommand();
            PrepareCommand(dbCommand, this.Connection, commandType, commandText, this.Parameters);
            DbDataAdapter odataAdapter = SqlDBManagerFactoryClass.GetDataAdapter();
            odataAdapter.SelectCommand = dbCommand;
            DataTable dataTable = new DataTable();
            odataAdapter.Fill(dataTable);
            dbCommand.Parameters.Clear();
            return dataTable;
        }

        /// <summary>
        /// Executes a SQL/PLSQl query
        /// </summary>
        /// <param name="commandType">Describes whether the command is a stored procedure, text or table direct</param>
        /// <param name="commandText">Describes either the name of the stored procedure, sql text command or the table name</param>
        /// <returns>A single table</returns>   
        public string GetParameter(string parameter)
        {
            return Command.Parameters[parameter].Value.ToString();

        }
    }
}    
