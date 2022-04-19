using Oracle.ManagedDataAccess.Client;

namespace AutoShopApp.Data
{
    public class Admin : Authentication
    {
        public string AdminName { get; set; } = string.Empty;
        public string AdminPassword { get; set; } = string.Empty;
        public string Operation { get; set; } = string.Empty;
        public bool Authenticate(Admin admin)
        {
            string ConnectionString = $"Data Source=localhost:1521;Persist Security Info=True;User ID={admin.AdminName};Password={admin.AdminPassword}";
            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                    return true;
                return true;
            }
            catch
            {
                return true;
            }
        }
    }
}
