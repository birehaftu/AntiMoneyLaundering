using System.Data.SqlClient;
using System.Web.Configuration;

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{
	public Connection()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string userName = "";
    private string password = "";
    private string _ConnectionString = "";
    private string hostName = "";
    private SqlConnection dbConn = null;

    public string UserName
    {
        set
        {
            userName = value;
        }
    }

    public string Password
    {
        set
        {
            password = value;
        }
    }
    public string ConnectionString
    {
        get { return _ConnectionString; }
        set { _ConnectionString = value; }
    }
    public string HostName
    {
        get { return hostName; }
        set { hostName = value; }
    }
    public SqlConnection DBConn
    {
        get
        {
            return dbConn;
        }
    }

    public bool OpenConnection()
    {
        _ConnectionString = WebConfigurationManager.ConnectionStrings["connectionStringName"].ConnectionString;
        dbConn = new SqlConnection(_ConnectionString);
        try
        {

            dbConn.Open();
            return true;
        }
        catch (SqlException ex)
        {
            ex.Message.ToString();
        }
        return false;

    }
}