using System.ComponentModel;
using Oracle.ManagedDataAccess.Client;
using MySql.Data.MySqlClient;

namespace AutoShopApp.Data
{
    public class Store : IDisposable
    {
        private readonly MySqlCommand command; //OracleCommand

        //String ConnectionString = "Data Source=localhost:1521;Persist Security Info=True;User ID=pgdac01;Password=welcome";
                                               //OracleConnection con = new OracleConnection(ConnectionString);
        public Store()
        {
            //string ConnectionString = "server=db4free.net;user id=autoshop_admin;Password=rishi2007;persistsecurityinfo=True;database=autoshop_admin";
            var con = new AutoConfig(new AutoMyConnection()).getAutoConnection(); //new MySqlConnection(ConnectionString)
            command = con.CreateCommand();
            command.Connection.Open();
        }

        /*          command.CommandText = "SELECT count FROM auto_counter WHERE entity = 'orders'";
            using var reader = command.ExecuteReader();
            int orderno = 0;
            reader.Read();
            int count = reader.GetInt32(0);
            orderno = (count + 1001);
            reader.Close();
            command.CommandText = $"SELECT price FROM auto_parts WHERE partno = {order.Partno}";
            using var reader1 = command.ExecuteReader();
            reader1.Read();
            double price = reader 1.GetInt32(0);
            string date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            reader1.Close();
            command.CommandText = $"INSERT INTO auto_orders(orderno, orderdate, customername, partno, qty, amount)" +
                                  $"VALUES({(orderno)},'{date}','{order.CustomerName}','{order.Partno }',{order.Qty},{(price * order.Qty)})";
            command.ExecuteNonQuery();
*/
        public void UpdateStore(Order order)
        {
            command.CommandText = $"call create_order('{order.CustomerName}',{order.Partno},{order.Qty})";
            command.ExecuteNonQuery();
        }

        public void insertParts(Parts parts)
        {
            command.CommandText = $"call insert_part('{parts.PartName}', {parts.AddStock}, {parts.Price});";
        }

        public void LoadStore(IBindingList order, IBindingList custPartOrd)
        {
            command.CommandText = "SELECT partno, stock FROM auto_parts";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                order.Add(new Order
                {
                    Partno = reader.GetInt32(0),
                    Stock = reader.GetInt32(1)
                });
            }
            reader.Close();
            command.CommandText = "SELECT orderno, orderdate, customername, partname, qty, amount FROM vw_auto_orders ORDER BY 1 DESC";
            using var reader1 = command.ExecuteReader();
            while (reader1.Read())
            {
                custPartOrd.Add(new CustPartOrd
                                {
                                    OrderNo = reader1.GetInt32(0),
                                    OrderDate = reader1.GetDateTime(1),
                                    CustomerName = reader1.GetString(2),
                                    PartName = reader1.GetString(3),
                                    Qty = reader1.GetInt32(4),
                                    Amount = reader1.GetDouble(5),
                                });
            }
            reader1.Close();
        }

        public void LoadAdmin(IBindingList obj)
        {
            obj.Add(new Admin());
        }
        public void LoadParts(IBindingList parts)
        {
            command.CommandText = "SELECT partname, stock, price FROM auto_parts";
                using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                parts.Add(new Parts
                {
                    PartName = reader.GetString(0),
                    Stock = reader.GetInt32(1),
                    Price = reader.GetDouble(2)
                });
            }
            reader.Close();
            
        }
        public void Dispose() => command.Connection.Close();
    }
}
