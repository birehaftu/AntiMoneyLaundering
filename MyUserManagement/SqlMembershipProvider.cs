using System;
using System.Text;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Data;
using System.Configuration.Provider;
using System.Web.Security;
using System.Configuration;
using System.Web.Configuration;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

namespace MyUserManagement
{
    public sealed class SqlMembershipProvider : MembershipProvider
    {

        //
        // Global connection string, generated password length, generic exception message, event log info.
        //

        private int newPasswordLength = 8;
        private string eventSource = "SqlMembershipProvider";
        private string eventLog = "Application";
        static int failureCount = 0;
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";
        private string connectionString;
        public string connstr;
        public DateTime DOB;
        public String Address;
        //public String PalmIP;
        //public String Department;
        public string EmployeeId;
        public Guid CompanyId;
        //
        // Used when determining encryption key values.
        //

        private MachineKeySection machineKey;

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog;

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
                name = "SqlMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample Sql Membership Provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            pApplicationName = GetConfigValue(config["applicationName"],
                                            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "10"));
            pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["PasswordAttemptWindow"], "10"));
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["PasswordStrengthRegularExpression"], ""));
            pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            string temp_format = config["PasswordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

            //
            // Initialize SqlConnection.
            //

            ConnectionStringSettings ConnectionStringSettings =
              ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            //if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            //{
            //    throw new ProviderException("Connection string cannot be blank.");
            //}
            //connectionString = "Data Source=PTS-DBS2-HQ;User ID=PTSQLUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;"; //ConnectionStringSettings.ConnectionString;
            //connectionString = "Data Source=PTS-DBS1-HQ;User ID=PTSITSUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;"; //ConnectionStringSettings.ConnectionString;
            //connectionString = "Data Source=PTS-APS2-HQ;User ID=PTSITSUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;";//
            //connectionString = "Data Source=172.16.20.5;User ID=PTSITSUSR;Password=AdminHoHoServer@2012;Initial Catalog=UserManagement;Integrated Security=false;";//
           // connectionString = "Data Source=localhost;User ID=RentUSR;Password=RentUSR;Initial Catalog=UMIntegrated;Integrated Security=false;";//
            //connectionString = "Data Source=HoHoServer;User ID=RentUSR;Password=RentUSR;Initial Catalog=UserManagement;Integrated Security=false;";//
            // Get encryption and decryption key information from the configuration.
           connectionString = ConnectionStringSettings.ConnectionString;
            connstr = connectionString;
            Configuration cfg = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            machineKey = (MachineKeySection)cfg.GetSection("system.web/machineKey");

            //if (machineKey.ValidationKey.Contains("AutoGenerate"))
            //    if (PasswordFormat != MembershipPasswordFormat.Clear)
            //        throw new ProviderException("Hashed or Encrypted Passwords are not supported with auto-generated keys.");
        }


        //
        // A helper function to retrieve config values from the configuration file.
        //

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue)) return defaultValue;

            return configValue;
        }

        //
        // System.Web.Security.MembershipProvider properties.
        //
        private string pApplicationName;
        private bool pEnablePasswordReset;
        private bool pEnablePasswordRetrieval;
        private bool pRequiresQuestionAndAnswer;
        private bool pRequiresUniqueEmail;
        private int pMaxInvalidPasswordAttempts;
        private int pPasswordAttemptWindow;
        private MembershipPasswordFormat pPasswordFormat;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return pEnablePasswordReset; }
        }


        public override bool EnablePasswordRetrieval
        {
            get { return pEnablePasswordRetrieval; }
        }


        public override bool RequiresQuestionAndAnswer
        {
            get { return pRequiresQuestionAndAnswer; }
        }


        public override bool RequiresUniqueEmail
        {
            get { return pRequiresUniqueEmail; }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return pMaxInvalidPasswordAttempts; }
        }


        public override int PasswordAttemptWindow
        {
            get { return pPasswordAttemptWindow; }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get { return pPasswordFormat; }
        }

        private int pMinRequiredNonAlphanumericCharacters;

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return pMinRequiredNonAlphanumericCharacters; }
        }

        private int pMinRequiredPasswordLength;
        //public bool UserExits;
        public override int MinRequiredPasswordLength
        {
            get { return pMinRequiredPasswordLength; }
        }

        private string pPasswordStrengthRegularExpression;

        public override string PasswordStrengthRegularExpression
        {
            get { return pPasswordStrengthRegularExpression; }
        }

        //
        // System.Web.Security.MembershipProvider methods.
        //

        //
        // MembershipProvider.ChangePassword
        //

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            if (!ValidateUser(username, oldPwd))
                return false;


            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPwd, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                if (args.FailureInformation != null) throw args.FailureInformation;
                else throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
            }

            //SEQ_PKID
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users SET Password ='" + EncodePassword(newPwd) + "', LastPasswordChangedDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ", conn);

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (InvalidCastException ice)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(ice, "ChangePassword");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw ice;
                //}
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "ChangePassword");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }
        public bool ChangePassword(string username, string newPwd)
        {
            //if (!ValidateUser(username, oldPwd))
            //    return false;


            //ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPwd, true);

            //OnValidatingPassword(args);

            //if (args.Cancel)
            //{
            //    if (args.FailureInformation != null) throw args.FailureInformation;
            //    else throw new MembershipPasswordException("Change password canceled due to new password validation failure.");
            //}

            //SEQ_PKID
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users SET Password ='" + EncodePassword(newPwd) + "', LastPasswordChangedDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ", conn);

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (InvalidCastException ice)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(ice, "ChangePassword");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw ice;
                //}
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "ChangePassword");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }


        //
        // MembershipProvider.ChangePasswordQuestionAndAnswer
        //

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPwdQuestion, string newPwdAnswer)
        {
            if (!ValidateUser(username, password)) return false;

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users SET PasswordQuestion = '" + newPwdQuestion + "', PasswordAnswer = '" + EncodePassword(newPwdAnswer) + "' WHERE USERNAME = '" + username + "' ", conn);

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (InvalidCastException ice)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(ice, "ChangePasswordQuestionAndAnswer");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw ice;
                //}
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "ChangePasswordQuestionAndAnswer");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }


        public override MembershipUser CreateUser(string username,
                                                   string password,
                                                   string email,
                                                   string Comment,
                                                   string Phone,
                                                   bool isApproved,
                                                   object providerUserKey,
                                                   out MembershipCreateStatus status)
        {
            return this.CreateUser(null, username, password, email,
                                  Comment, Phone,
                                  isApproved, providerUserKey, EmployeeId,
                                  out status);
        }

        public MyCustomMembershipUser CreateUser(string FullName, string username,
                                                string password,
                                                string email,
                                                string Comment,
                                                string Phone,
                                                bool isApproved,
                                                object providerUserKey,
                                                string employeeId,
                                                out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {

                //OracleDateTime createDate = 'to_date('"+ DateTime.Today.ToShortDateString() +"','mm/dd/yyyy');
                SqlConnection conn = new SqlConnection(connectionString);
                string sysdate = DateTime.Now.ToString("yyyy-MM-dd h:mm tt");
                string DateBirth = DOB.ToString("yyyy-MM-dd h:mm tt");
                String insert = "INSERT INTO Users (FullName,USERNAME, Password, Email, CreationDate, LastPasswordChangedDate, LastActivityDate, IsLockedOut, LastLockedOutDate, FailedPasswordAttemptCount, FailedPasswordAttemptWS,Description,Phone,DOB,Address, EmployeeId,CompanyId) " +
                                                                    "Values('" + FullName + "','" + username + "', '" + EncodePassword(password) + "', '" + email + "', '" + sysdate + "', '" + sysdate + "' , '" + sysdate + "', '0', '" + sysdate + "', '" + 0 + "', '" + sysdate + "','" + Comment + "','" + Phone + "','" + DateBirth + "','" + Address + "','" + employeeId + "','" + CompanyId + "')";
                SqlCommand cmd = new SqlCommand(insert, conn);
                try
                {
                    conn.Open();
                    int recAdded = cmd.ExecuteNonQuery();
                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET LastActivityDate = '" + sysdate + "' WHERE USERNAME = '" + username + "' ", conn);
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch (SqlException e)
                {
                    //if (WriteExceptionsToEventLog)
                    //{
                    //    WriteToEventLog(e, "CreateUser");
                    //}

                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    conn.Close();
                }
                return (MyCustomMembershipUser)GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        //
        // MembershipProvider.DeleteUser
        //

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            String lastLoggedInDate = GetUserLastLogDate(username);
            if (!String.IsNullOrEmpty(lastLoggedInDate))
            {
                return false;
            }
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd1 = new SqlCommand("DELETE FROM Users WHERE USERNAME = '" + username + "' ", conn);
            SqlCommand cmd = new SqlCommand("DELETE FROM USERSINROLES WHERE USERNAME = '" + username + "' ", conn);
            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (deleteAllRelatedData)
                {
                    // Process commands to delete all data for the user in the database.
                    cmd1.ExecuteNonQuery();
                }
            }

            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "DeleteUser");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }



        //
        // MembershipProvider.GetAllUsers
        //

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users ", conn);

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();
                //Decimal.ToInt32((decimal)cmd.ExecuteScalar());
                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT  * FROM Users   ORDER BY USERNAME Asc";

                reader = cmd.ExecuteReader();



                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MyCustomMembershipUser u = GetUserFromReader(reader);
                        //users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }

            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetAllUsers");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return users;
        }


        //
        // MembershipProvider.GetNumberOfUsersOnline
        //

        public override int GetNumberOfUsersOnline()
        {

            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users WHERE LastActivityDate > '" + compareTime + "' ", conn);


            int numOnline = 0;

            try
            {
                conn.Open();
                numOnline = (int)cmd.ExecuteScalar();
                //numOnline = Decimal.ToInt32((decimal)cmd.ExecuteScalar());
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetNumberOfUsersOnline");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            return numOnline;
        }



        //
        // MembershipProvider.GetPassword
        //

        public override string GetPassword(string username, string answer)
        {
            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            if (PasswordFormat == MembershipPasswordFormat.Hashed)
            {
                throw new ProviderException("Cannot retrieve Hashed passwords.");
            }

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password, PasswordAnswer, IsLockedOut FROM Users WHERE USERNAME = '" + username + "' ", conn);

            string password = "";
            string passwordAnswer = "";
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    char[] tmpIsLockedOut = new char[1];
                    reader.GetChars(2, 0, tmpIsLockedOut, 0, 1);
                    bool isLockedOut = (tmpIsLockedOut[0] == '1') ? true : false;

                    if (isLockedOut)
                        throw new MembershipPasswordException("The supplied user is locked out.");

                    password = reader.GetString(0);
                    passwordAnswer = reader.GetString(1);
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetPassword");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }


            //if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            //{
            //    UpdateFailureCount(username, "passwordAnswer");

            //    throw new MembershipPasswordException("Incorrect password answer.");
            //}


            if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            {
                password = UnEncodePassword(password);
            }

            return password;
        }



        //
        // MembershipProvider.GetUser(string, bool)
        //
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT  * FROM Users WHERE USERNAME = '" + username + "' ", conn);

            MyCustomMembershipUser u = null;
            SqlDataReader reader = null;

            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    u = GetUserFromReader(reader);
                    reader.Close();
                    if (userIsOnline)
                    {
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET LastActivityDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ", conn);
                        updateCmd.ExecuteNonQuery();
                    }
                }

            }
            //catch (InvalidCastException ice)
            //{
            //    if (WriteExceptionsToEventLog)
            //    {
            //        WriteToEventLog(ice, "GetUser(String, Boolean)");

            //        throw new ProviderException(exceptionMessage);
            //    }
            //    else
            //    {
            //        throw ice;
            //    }
            //}
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetUser(String, Boolean)");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return u;
        }


        //
        // MembershipProvider.GetUser(object, bool)
        //
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT  * FROM Users WHERE UserName = '" + providerUserKey + "'", conn);


            MyCustomMembershipUser u = null;
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    u = GetUserFromReader(reader);

                    if (userIsOnline)
                    {
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET LastActivityDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE PKID = '" + providerUserKey + "'", conn);

                        updateCmd.ExecuteNonQuery();
                    }
                }

            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetUser(Object, Boolean)");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return u;
        }

        //
        // GetUserFromReader
        //    A helper function that takes the current row from the SqlDataReader
        // and hydrates a MembershiUser from the values. Called by the 
        // MembershipUser.GetUser implementation.
        //
        private MyCustomMembershipUser GetUserFromReader(SqlDataReader reader)
        {
            string username, email, passwordQuestion, comment,  FullName;
            String employeeId;
            int counter = 0;
            object providerUserKey;
            try
            {
                providerUserKey = reader.GetValue(0);
                username = reader.GetString(0);
                FullName = reader.GetValue(1) != DBNull.Value ? reader.GetString(1) : "";
                email = reader.GetString(5);
                employeeId = reader.GetValue(2) != DBNull.Value ? reader.GetString(2) : "";
                //string psscount = reader.GetValue(15) != DBNull.Value ? reader.GetString(15) : "0";
                //Int16 val =(short) reader.GetSqlInt16(15);
                counter = int.Parse(reader.GetSqlInt16(15).ToString());
                passwordQuestion = "";
                comment = "";
            }
            catch (InvalidCastException ice)
            {
                providerUserKey = "";
                FullName = "";
                username = "";
                //PalmIP = "";
                email = "";
                employeeId = "";
                passwordQuestion = "";
                comment = "";
                counter = 0;
            }

            bool isApproved = true, isLockedOut;
            //int FAILEDPASSWORDATTEMPTCOUNT;
            try
            {
                //isApproved = reader.GetString(5) == "1" ? true : false;
                isLockedOut = reader.GetBoolean(14);
            }
            catch (InvalidCastException ice)
            {
                isLockedOut = false;
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(ice, "GetUserFromReader (isapproved/isLockedOut)");
                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw ice;
                //}
            }
            //catch (NotSupportedException nse)
            //{
            //    if (WriteExceptionsToEventLog)
            //    {
            //        WriteToEventLog(nse, "GetUserFromReader (isapproved/isLockedOut)");

            //        throw new ProviderException(exceptionMessage);
            //    }
            //    else
            //    {
            //        throw nse;
            //    }
            //}

            DateTime creationDate, lastLoginDate, lastActivityDate, lastPasswordChangedDate, lastLockedOutDate;

            try
            {
                creationDate = reader.GetDateTime(11);

                lastLoginDate = reader.GetValue(9) != DBNull.Value ? reader.GetDateTime(9) : new DateTime();
                lastActivityDate = reader.GetValue(8) != DBNull.Value ? reader.GetDateTime(8) : new DateTime();
                lastPasswordChangedDate = reader.GetValue(10) != DBNull.Value ? reader.GetDateTime(10) : new DateTime();
                lastLockedOutDate = reader.GetValue(14) != DBNull.Value ? reader.GetDateTime(14) : new DateTime();
            }
            catch (InvalidCastException ice)
            {
                creationDate = new DateTime();
                lastLoginDate = new DateTime();
                lastActivityDate = new DateTime();
                lastPasswordChangedDate = new DateTime();
                lastLockedOutDate = new DateTime();
            }
            //catch (NotSupportedException nse)
            //{
            //    if (WriteExceptionsToEventLog)
            //    {
            //        WriteToEventLog(nse, "GetUserFromReader (creationDate/lastLogin/activity/lockedOutDate)");
            //        throw new ProviderException(exceptionMessage);
            //    }
            //    else
            //    {
            //        throw nse;
            //    }
            //}

            MyCustomMembershipUser u = null;
            try
            {

                u = new MyCustomMembershipUser(this.Name, username, providerUserKey, email, passwordQuestion,
                                                FullName, isApproved, isLockedOut, creationDate, lastLoginDate,
                                                lastActivityDate, lastPasswordChangedDate, lastLockedOutDate, employeeId, counter);
            }
            catch (InvalidCastException ice)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(ice, "GetAllUsers3 ");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw ice;
                //}
            }
            return u;
        }


        //
        // MembershipProvider.UnlockUser
        //

        public override bool UnlockUser(string username)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users SET IsLockedOut = '0', FAILEDPASSWORDATTEMPTCOUNT='0', LastLockedOutDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ", conn);


            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (InvalidCastException ice)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ice, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
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
                    WriteToEventLog(e, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
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

            if (rowsAffected > 0)
                return true;

            return false;
        }


        //
        // MembershipProvider.GetUserNameByEmail
        //

        public override string GetUserNameByEmail(string email)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT USERNAME FROM Users WHERE Email = '" + email + "' ", conn);

            string username = "";


            try
            {
                conn.Open();

                username = (string)cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetUserNameByEmail");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            if (username == null)
                username = "";

            return username;
        }
        public string GetUserLastLogDate(string Uname)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT LASTLOGINDATE FROM Users WHERE USERNAME = '" + Uname + "' ", conn);

            string LLogin = "";


            try
            {
                conn.Open();
                var executed = cmd.ExecuteScalar() ?? "";
                if (!String.IsNullOrEmpty(executed.ToString()))
                    LLogin = ((DateTime)executed).ToString();
            }
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "GetUserNameByEmail");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }

            if (LLogin == null)
                LLogin = "";

            return LLogin;
        }
        //
        // MembershipProvider.ResetPassword
        //

        public override string ResetPassword(string username, string answer)
        {
            //if (!EnablePasswordReset)
            //{
            //    throw new NotSupportedException("Password reset is not enabled.");
            //}

            //if (answer == null && RequiresQuestionAndAnswer)
            //{
            //    UpdateFailureCount(username, "passwordAnswer");

            //    throw new ProviderException("Password answer required for password reset.");
            //}

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT PasswordAnswer, IsLockedOut FROM Users WHERE USERNAME ='" + username + "' ", conn);

            int rowsAffected = 0;
            string passwordAnswer = "";
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    char[] tmpIsLockedOut = new char[1];
                    reader.GetChars(1, 0, tmpIsLockedOut, 0, 1);
                    bool isLockedOut = (tmpIsLockedOut[0] == '1') ? true : false;

                    if (isLockedOut)
                        throw new MembershipPasswordException("The supplied user is locked out.");

                    passwordAnswer = reader.GetString(0);
                }
                else
                {
                    //throw new MembershipPasswordException("The supplied user name is not found.");
                }

                if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
                {
                    UpdateFailureCount(username, "passwordAnswer");

                    //throw new MembershipPasswordException("Incorrect password answer.");
                }

                SqlCommand updateCmd = new SqlCommand("UPDATE Users  SET Password = '" + EncodePassword(newPassword) + "', LastPasswordChangedDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "'  AND IsLockedOut = '0'", conn);


                rowsAffected = updateCmd.ExecuteNonQuery();
            }
            //catch (InvalidCastException ice)
            //{
            //    if (WriteExceptionsToEventLog)
            //    {
            //        WriteToEventLog(ice, "ResetPassword");

            //        throw new ProviderException(exceptionMessage);
            //    }
            //    else
            //    {
            //        throw ice;
            //    }
            //}
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "ResetPassword");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }

        //private void OnValidatingPassword(ValidatePasswordEventArgs args)
        //{
        //    throw new NotImplementedException();
        //}


        /// <summary>
        /// MembershipProvider.UpdateUser
        /// </summary>
        /// <param name="user"></param>
        public override void UpdateUser(MembershipUser user)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            MyCustomMembershipUser u = (MyCustomMembershipUser)user;
            char isap = (bool)(user.IsApproved) ? '1' : '0';
            SqlCommand cmd;
            if (!String.IsNullOrEmpty(user.Comment))
                cmd = new SqlCommand("UPDATE Users SET LastActivityDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "',FullName='" + user.Comment + "' WHERE USERNAME = '" + user.UserName + "' ", conn);
            else
                cmd = new SqlCommand("UPDATE Users SET LastActivityDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + user.UserName + "' ", conn);

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            //catch (InvalidCastException ice)
            //{
            //    if (WriteExceptionsToEventLog)
            //    {
            //        WriteToEventLog(ice, "UpdateUser");

            //        throw new ProviderException(exceptionMessage);
            //    }
            //    else
            //    {
            //        throw ice;
            //    }
            //}
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "UpdateUser");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                conn.Close();
            }
        }

        // MembershipProvider.ValidateUser
        //
        public bool UserExits(string username)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE USERNAME = '" + username + "'", conn);
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    reader.Read();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ;//return false;
            }
            return false;
        }
        public bool EmployeeExits(string empid)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE EmployeeId = '" + empid + "' and ISLOCKEDOUT=0", conn);
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    reader.Read();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ;//return false;
            }
            return false;
        }
        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;
            if (!username.All(char.IsLetterOrDigit))
            {
                return false;
            }
            //if (System.DateTime.Now.Year > 2020)
            //    return false;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password,IsLockedOut FROM Users WHERE USERNAME = '" + username + "'", conn);
            SqlDataReader reader = null;
            bool isApproved = false;
            bool isblocked = false;
            string pwd = "";

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();
                    pwd = reader.GetString(0);
                    isblocked = reader.GetBoolean(1);
                    if (!isblocked)
                        isApproved = true;
                    //else

                    //char[] tmpIsApproved = new char[1];
                    //reader.GetChars(1, 0, tmpIsApproved, 0, 1);
                    //isApproved = (tmpIsApproved[0] == '1') ? true : false;
                }
                else
                {
                    return false;
                }

                reader.Close();
                if (isApproved)
                {
                    if (CheckPassword(password, pwd))
                    {
                        isValid = true;
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET FailedPasswordAttemptCount=0,LastLoginDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "', LastActivityDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ", conn);
                        SqlCommand RegiterLoggedInCmd = new SqlCommand("INSERT INTO LoginHistory(UserName,DateLogged) values ('" + username + "','" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "') ", conn);
                        SqlCommand DeleteLoggedInCmd = new SqlCommand("DELETE FROM LoginHistory where DATEDIFF(DAY,DateLogged,GETDATE())>30 ", conn);

                        updateCmd.ExecuteNonQuery();
                        RegiterLoggedInCmd.ExecuteNonQuery();
                        DeleteLoggedInCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        conn.Close();

                        UpdateFailureCount(username, "password");
                    }
                }
            }
            catch (InvalidCastException ice)
            {
                return false;
            }
            catch (SqlException e)
            {
                return false;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return isValid;
        }


        //
        // UpdateFailureCount
        //   A helper method that performs the checks and updates associated with
        // password failure tracking.
        //
        private void UpdateFailureCount(string username, string failureType)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT FailedPasswordAttemptCount, FailedPasswordAttemptWS FROM Users WHERE USERNAME ='" + username + "' ", conn);

            SqlDataReader reader = null;
            DateTime windowStart = new DateTime();

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (failureType == "password")
                    {
                        failureCount = reader.GetInt16(0);
                        windowStart = reader.GetDateTime(1);
                    }

                    //if (failureType == "passwordAnswer")
                    //{
                    //    failureCount = reader.GetInt16(2);
                    //    windowStart = reader.GetDateTime(3);
                    //}
                }
                reader.Close();

                DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    // First password failure or outside of PasswordAttemptWindow. 
                    // Start a new password failure count from 1 and a new window starting now.

                    //if (failureType == "password")
                    cmd.CommandText = "UPDATE Users SET FailedPasswordAttemptCount = '" + 1 + "', FailedPasswordAttemptWS = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ";

                    //if (failureType == "passwordAnswer")
                    //    cmd.CommandText = "UPDATE Users SET FailedPasswordAnswerAttemptC = '" + 1 + "', FailedPasswordAnswerAttemptWS = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ";
                    cmd.ExecuteNonQuery();
                    //if (cmd.ExecuteNonQuery() < 0)
                    //    throw new ProviderException("Unable to update failure count and window start.");
                    failureCount++;
                }
                else
                {
                    failureCount++;
                    if (failureCount >= pMaxInvalidPasswordAttempts)
                    {
                        // Password attempts have exceeded the failure threshold. Lock out
                        // the user.

                        cmd.CommandText = "UPDATE Users SET FailedPasswordAttemptCount ='" + failureCount + "', IsLockedOut = '" + '1' + "', LastLockedOutDate = '" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "' WHERE USERNAME = '" + username + "' ";
                        cmd.ExecuteNonQuery();
                        //if (cmd.ExecuteNonQuery() < 0)
                        //throw new ProviderException("Unable to lock out user.");
                    }
                    else
                    {
                        // Password attempts have not exceeded the failure threshold. Update
                        // the failure counts. Leave the window the same.
                        //if (failureType == "password")
                        cmd.CommandText = "UPDATE Users SET FailedPasswordAttemptCount ='" + failureCount + "' WHERE USERNAME = '" + username + "' ";
                        cmd.ExecuteNonQuery();
                        //if (failureType == "passwordAnswer")
                        //    cmd.CommandText = "UPDATE Users SET FailedPasswordAnswerAttemptC = '" + failureCount + "' WHERE USERNAME = '" + username + "' ";

                        //if (cmd.ExecuteNonQuery() < 0)
                        //    throw new ProviderException("Unable to update failure count.");
                    }
                }
            }
            //catch (InvalidCastException ice)
            //{
            //    if (WriteExceptionsToEventLog)
            //    {
            //        WriteToEventLog(ice, "UpdateFailureCount");
            //        throw new ProviderException(exceptionMessage);
            //    }
            //    else
            //    {
            //        throw ice;
            //    }
            //}
            catch (SqlException e)
            {
                //if (WriteExceptionsToEventLog)
                //{
                //    WriteToEventLog(e, "UpdateFailureCount");

                //    throw new ProviderException(exceptionMessage);
                //}
                //else
                //{
                //    throw e;
                //}
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }
        }


        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }
            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// EncodePassword
        /// Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword = Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }
            return encodedPassword;
        }

        //
        // UnEncodePassword
        //   Decrypts or leaves the password clear based on the PasswordFormat.
        //

        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password = Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        //
        // HexToByte
        //   Converts a hexadecimal string to a byte array. Used to convert encryption
        // key values from the configuration.
        //

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++) returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        //
        // MembershipProvider.FindUsersByName
        //

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users WHERE USERNAME LIKE '" + usernameToMatch + "' ", conn);

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;

            try
            {
                conn.Open();

                //totalRecords = Decimal.ToInt32((decimal)cmd.ExecuteScalar());
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT  * " +
                  " FROM Users " +
                  " WHERE USERNAME LIKE '" + usernameToMatch + "'  " +
                  " ORDER BY USERNAME Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);                        
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (InvalidCastException ice)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ice, "FindUsersByName");

                    throw new ProviderException(exceptionMessage);
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
                    WriteToEventLog(e, "FindUsersByName");

                    throw new ProviderException(exceptionMessage);
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

            return users;
        }

        //
        // MembershipProvider.FindUsersByEmail
        //

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users WHERE Email LIKE '" + emailToMatch + "' ", conn);

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = Decimal.ToInt32((decimal)cmd.ExecuteScalar());

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT  * FROM Users " +
                         " WHERE Email LIKE '" + emailToMatch + "'  " +
                         " ORDER BY USERNAME Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (InvalidCastException ice)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(ice, "FindUsersByEmail");

                    throw new ProviderException(exceptionMessage);
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
                    WriteToEventLog(e, "FindUsersByEmail");

                    throw new ProviderException(exceptionMessage);
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
            return users;
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
            //EventLog log = new EventLog();
            //log.Source = eventSource;
            //log.Log = eventLog;

            //string message = "An exception occurred communicating with the data source.\n\n";
            //message += "Action: " + action + "\n\n";
            //message += "Exception: " + e.ToString();

            //log.WriteEntry(message);
            String exception = e.ToString();
            String fPath = "C://Farka Software";

            String curDir = Environment.CurrentDirectory;
            if (curDir.Contains("C:"))
            {
                if (Directory.Exists(fPath) == false)
                {
                    createDirectory(fPath);
                }
            }
            else if (curDir.Contains("D:"))
            {
                fPath = "D://ACTCollege";
                if (Directory.Exists(fPath) == false)
                {
                    createDirectory(fPath);
                }
            }
            FileStream fStream = null;
            String filePath = fPath + "/msis_log.txt";
            if (File.Exists(filePath))
            {
                fStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

            }
            StreamWriter sw = new StreamWriter(fStream);
            String now = DateTime.Now.ToString();
            sw.Write(exception);
            sw.Close();
            fStream.Close();
        }
        private static void createDirectory(String path)
        {
            Directory.CreateDirectory(path);
        }

    }
}
