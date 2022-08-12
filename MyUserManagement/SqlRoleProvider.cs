using System;
using System.Web.Security;
using System.Configuration;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace MyUserManagement
{
    public sealed class SqlRoleProvider : RoleProvider
    {

        //
        // Global connection string, generic exception message, event log info.
        //

        private string eventSource = "SqlRoleProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";

        private ConnectionStringSettings pConnectionStringSettings;
        private string connectionString;
        public String Description = "";

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }



        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public override void Initialize(string name, NameValueCollection config)
        {

            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "SqlRoleProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sql Role provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            if (config["applicationName"] == null || config["applicationName"].Trim() == "")
            {
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;
            }
            else
            {
                pApplicationName = config["applicationName"];
            }


            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                {
                    pWriteExceptionsToEventLog = true;
                }
            }


            //
            // Initialize OleDb Connection.
            //

            pConnectionStringSettings = ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (pConnectionStringSettings == null || pConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }
           //connectionString = "Data Source=PTS-DBS2-HQ;User ID=PTSQLUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;"; //ConnectionStringSettings.ConnectionString;
           //connectionString = "Data Source=PTS-DBS1-HQ;User ID=PTSITSUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;"; //ConnectionStringSettings.ConnectionString;
           //connectionString = "Data Source=PTS-APS2-HQ;User ID=PTSITSUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;";//
           //connectionString = "Data Source=172.16.20.5;User ID=PTSITSUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;";//
           //connectionString = "Data Source=localhost;User ID=RentUSR;Password=RentUSR;Initial Catalog=UMIntegrated;Integrated Security=false;";//
           //connectionString = "Data Source=HoHoServer;User ID=RentUSR;Password=RentUSR;Initial Catalog=UserManagement;Integrated Security=false;";//
           connectionString=  pConnectionStringSettings.ConnectionString;
            // Get encryption and decryption key information from the configuration.
        }



        //
        // System.Web.Security.RoleProvider properties.
        //


        private string pApplicationName;


        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        //
        // System.Web.Security.RoleProvider methods.
        //

        //
        // RoleProvider.AddUsersToRoles
        //

        public override void AddUsersToRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                    throw new ProviderException("Role name not found.");
            }

            foreach (string username in usernames)
            {
                if (username.Contains(","))
                    throw new ArgumentException("User names cannot contain commas.");


                foreach (string rolename in rolenames)
                {
                    if (IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is already in role.");
                    }
                }
            }


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            SqlTransaction tran = null;

            try
            {
                conn.Open();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

                foreach (string username in usernames)
                {
                    foreach (string rolename in rolenames)
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO UsersInRoles (Username, Rolename) Values('" + username + "', '" + rolename + "')";
                        cmd.ExecuteNonQuery();
                    }
                }

                tran.Commit();
            }
            catch (SqlException e)
            {
                try
                {
                    tran.Rollback();
                }
                catch { }


                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "AddUsersToRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
        }


        //// public override void AddUsersToRoles(string usernames, string rolenames)
        // {

        //     if (!RoleExists(rolenames))
        //         {
        //             throw new ProviderException("Role name not found.");
        //         }


        //       if (usernames.Contains(","))
        //         {
        //             throw new ArgumentException("User names cannot contain commas.");
        //         }

        //       if (IsUserInRole(usernames, rolenames))
        //             {
        //                 throw new ProviderException("User is already in role.");
        //             }



        //     SqlConnection conn = new SqlConnection(connectionString);
        //     SqlCommand cmd = new SqlCommand();

        //     SqlTransaction tran = null;

        //     try
        //     {
        //         conn.Open();
        //         tran = conn.BeginTransaction();
        //         cmd.Transaction = tran;                      
        //         cmd.Connection = conn;
        //         cmd.CommandText = "INSERT INTO UsersInRoles (Username, Rolename, ApplicationName) Values('" + usernames + "', '" + rolenames + "', '" + ApplicationName + "')";
        //         cmd.ExecuteNonQuery();
        //         tran.Commit();
        //     }
        //     catch (SqlException e)
        //     {
        //         try
        //         {
        //             tran.Rollback();
        //         }
        //         catch { }


        //         if (WriteExceptionsToEventLog)
        //         {
        //             WriteToEventLog(e, "AddUsersToRoles");
        //         }
        //         else
        //         {
        //             throw e;
        //         }
        //     }
        //     finally
        //     {
        //         conn.Close();
        //     }
        // }
        //
        // RoleProvider.CreateRole
        //


        public override void CreateRole(string rolename)
        {
            if (rolename.Contains(","))
                throw new ArgumentException("Role names cannot contain commas.");

            if (RoleExists(rolename))
                throw new ProviderException("Role name already exists.");


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Roles (Rolename, Description) Values('" + rolename + "', '" + Description + "')", conn);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (InvalidCastException ice)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ice, "CreateRole");
                }
                else
                {
                    throw ice;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
        }


        //
        // RoleProvider.DeleteRole
        //

        public override bool DeleteRole(string rolename, bool throwOnPopulatedRole)
        {
            if (!RoleExists(rolename))
            {
                throw new ProviderException("Role does not exist.");
            }

            if (throwOnPopulatedRole && GetUsersInRole(rolename).Length > 0)
            {
                throw new ProviderException("Cannot delete a populated role.");
            }

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Roles WHERE Rolename = '" + rolename + "' ", conn);
            //SqlCommand cmd2 = new SqlCommand("DELETE FROM UsersInRoles WHERE Rolename = '" + rolename + "' ", conn);

            SqlTransaction tran = null;

            try
            {
                conn.Open();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;
                //cmd2.Transaction = tran;

                //cmd2.ExecuteNonQuery();
                cmd.ExecuteNonQuery();

                tran.Commit();
            }
            catch (SqlException e)
            {
                try
                {
                    tran.Rollback();
                }
                catch { }


                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteRole");

                    return false;
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            return true;
        }

        public DataTable GetRoles_UserRoles()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            DataTable dt = new DataTable();

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT r.ROLENAME,r.Description, COUNT(ur.ROLENAME) AS COUNTROLE FROM Roles r left outer JOIN Usersinroles ur ON r.ROLENAME = ur.ROLENAME  GROUP BY r.ROLENAME,r.Description", conn);

            try
            {
                conn.Open();
                dataAdapter.Fill(dt);
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetRoles_UserRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }


            return dt;
        }

        //
        // RoleProvider.GetAllRoles
        //

        public override string[] GetAllRoles()
        {
            string tmpRoleNames = "";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Rolename FROM Roles ", conn);

            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tmpRoleNames += reader.GetString(0) + ",";
                }
            }
            catch (InvalidCastException ice)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ice, "GetAllRoles");
                }
                else
                {
                    throw ice;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetAllRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            if (tmpRoleNames.Length > 0)
            {
                // Remove trailing comma.
                tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1);
                return tmpRoleNames.Split(',');
            }

            return new string[0];
        }


        //
        // RoleProvider.GetRolesForUser
        //

        public override string[] GetRolesForUser(string username)
        {
            string tmpRoleNames = "";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Rolename FROM UsersInRoles WHERE Username = '" + username + "' ", conn);

            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tmpRoleNames += reader.GetString(0) + ",";
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetRolesForUser");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            if (tmpRoleNames.Length > 0)
            {
                // Remove trailing comma.
                tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1);
                return tmpRoleNames.Split(',');
            }

            return new string[0];
        }


        //
        // RoleProvider.GetUsersInRole
        //

        public override string[] GetUsersInRole(string rolename)
        {
            string tmpUserNames = "";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM UsersInRoles WHERE Rolename = '" + rolename + "' ", conn);

            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tmpUserNames += reader.GetString(0) + ",";
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUsersInRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            if (tmpUserNames.Length > 0)
            {
                // Remove trailing comma.
                tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
                return tmpUserNames.Split(',');
            }

            return new string[0];
        }


        //
        // RoleProvider.IsUserInRole
        //

        public override bool IsUserInRole(string username, string rolename)
        {
            bool userIsInRole = false;

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM UsersInRoles WHERE Username = '" + username + "' AND Rolename = '" + rolename + "' ", conn);

            try
            {
                conn.Open();
                userIsInRole = int.Parse(cmd.ExecuteScalar().ToString()) > 0 ? true : false;
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "IsUserInRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            return userIsInRole;
        }


        //
        // RoleProvider.RemoveUsersFromRoles
        //

        public override void RemoveUsersFromRoles(string[] usernames, string[] rolenames)
        {
            foreach (string rolename in rolenames)
            {
                if (!RoleExists(rolename))
                {
                    throw new ProviderException("Role name not found.");
                }
            }

            foreach (string username in usernames)
            {
                foreach (string rolename in rolenames)
                {
                    if (!IsUserInRole(username, rolename))
                    {
                        throw new ProviderException("User is not in role.");
                    }
                }
            }


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();

            SqlTransaction tran = null;

            try
            {
                conn.Open();
                tran = conn.BeginTransaction();
                cmd.Transaction = tran;

                foreach (string username in usernames)
                {
                    foreach (string rolename in rolenames)
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "DELETE FROM UsersInRoles WHERE Username = '" + username + "' AND Rolename = '" + rolename + "' ";
                        cmd.ExecuteNonQuery();
                    }
                }

                tran.Commit();
            }
            catch (SqlException e)
            {
                try
                {
                    tran.Rollback();
                }
                catch { }


                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "RemoveUsersFromRoles");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        //
        // RoleProvider.RoleExists
        //

        public override bool RoleExists(string rolename)
        {
            bool exists = false;

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Roles WHERE Rolename = '" + rolename + "' ", conn);

            try
            {
                conn.Open();
                exists = int.Parse(cmd.ExecuteScalar().ToString()) > 0 ? true : false;
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "RoleExists");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            return exists;
        }

        //
        // RoleProvider.FindUsersInRole
        //

        public override string[] FindUsersInRole(string rolename, string usernameToMatch)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM UsersInRoles WHERE Username LIKE '" + usernameToMatch + "' AND RoleName = '" + rolename + "' AND ApplicationName = '" + pApplicationName + "'", conn);

            string tmpUserNames = "";
            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tmpUserNames += reader.GetString(0) + ",";
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersInRole");
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            if (tmpUserNames.Length > 0)
            {
                // Remove trailing comma.
                tmpUserNames = tmpUserNames.Substring(0, tmpUserNames.Length - 1);
                return tmpUserNames.Split(',');
            }

            return new string[0];
        }

        //
        // WriteToEventLog
        //   A helper function that writes exception detail to the event log. Exceptions
        // are written to the event log as a security measure to avoid private database
        // details from being returned to the browser. If a method does not return a status
        // or boolean indicating the action succeeded or failed, a generic exception is also 
        // thrown by the caller.
        //

        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = exceptionMessage + "\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            //log.WriteEntry(message);
            //clsLogFile.WriteLog("WriteToEventLog", e);
        }

    }
}
