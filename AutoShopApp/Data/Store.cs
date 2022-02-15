using System.ComponentModel;
using Oracle.ManagedDataAccess.Client;

namespace AutoShopApp.Data
{
    public class Store : IDisposable
    {
        private readonly OracleCommand command;
        public Store()
        {
            String ConnectionString = "Data Source=localhost:1521;Persist Security Info=True;User ID=pgdac01;Password=welcome";
            OracleConnection con = new OracleConnection(ConnectionString);
            command = con.CreateCommand();
            command.Connection.Open();
        }
        public void UpdateStore(Order order)
        {
            command.CommandText = "SELECT count FROM auto_counter WHERE entity = 'orders'";
            using var reader = command.ExecuteReader();
            int orderno = 0;
            if (reader.Read())
            orderno = (reader.GetInt32(0) + 1001);

            command.CommandText = $"SELECT partname, prize FROM auto_parts WHERE partno = {order.Partno}";
            using var reader1 = command.ExecuteReader();
            string partname = String.Empty;
            if (reader1.Read())
            partname = reader1.GetString(0);
            double price = reader1.GetInt32(1);
            string date = DateTime.Now.ToString("dd MMM yyyy");

            command.CommandText = $"INSERT INTO auto_orders(orderno, orderdate, customername, partno, qty, amount)" +
                                  $"VALUES({orderno},'{date}','{order.CustomerName}','{order.Partno }',{order.Qty},{(price * order.Qty)})";
            command.ExecuteNonQuery();
            
            command.CommandText = $"UPDATE auto_counter SET count={(reader.GetInt32(0) + 1)} WHERE entity = 'orders'";
            command.ExecuteNonQuery();
        }

        public void LoadStore(IBindingList order, IBindingList custPartOrd)
        {
            command.CommandText = "SELECT orderno, orderdate, customername, partname, qty, amount FROM auto_store ORDER BY 1";
            using var reader = command.ExecuteReader();
            order.Add(new Order());
            while (reader.Read())
            {
                custPartOrd.Add(new CustPartOrd
                                {
                                    OrderNo = reader.GetInt32(0),
                                    OrderDate = reader.GetDateTime(1),
                                    CustomerName = reader.GetString(2),
                                    PartName = reader.GetString(3),
                                    Qty = reader.GetInt32(4),
                                    Amount = reader.GetDouble(5),
                                });
            }
        }

        public void Dispose()
        {
            command.Connection.Close();
        }
    }
}
