namespace AutoShopApp.Data
{
    using MySql.Data.MySqlClient;

    public interface AutoConnection
    {
        MySqlConnection getAutoConnection();
    }
    class AutoMyConnection : AutoConnection
    {
        public MySqlConnection getAutoConnection()
        {
            string ConnectionString = "server=db4free.net;user id=autoshop_admin;Password=rishi2007;persistsecurityinfo=True;database=autoshop_admin";
            MySqlConnection _connection = new MySqlConnection(ConnectionString);
            return _connection;
        }

    }
    public class AutoConfig
    {
        private AutoConnection _connection;
        public AutoConfig(AutoConnection connection)
        {
            this._connection = connection;
        }
        public MySqlConnection getAutoConnection() => _connection.getAutoConnection();
    }
}