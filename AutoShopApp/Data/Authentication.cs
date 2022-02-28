namespace AutoShopApp.Data
{
    public interface Authentication
    {
        abstract bool Authenticate(Admin admin);
 /*       {
            string ConnectionString = $"Data Source=localhost:1521;Persist Security Info=True;User ID={admin.AdminName};Password={admin.AdminPassword}";
            OracleConnection con = new OracleConnection(ConnectionString);
            try
            {
                con.Open();
                if (con.State == System.Data.ConnectionState.Open)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
            //command = con.CreateCommand();
        }
 */
    }
}
