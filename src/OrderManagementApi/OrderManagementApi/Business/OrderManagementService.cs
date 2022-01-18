using Dapper;

using OrderManagementApi.Models;

using System.Data;
using System.Data.SqlClient;

namespace OrderManagementApi.Business
{
    public class OrderManagementService
    {
        public OrderManagementService()
        {

        }

        public void PlaceOrder(int customerId, List<Product> products, )
        {
            try
            {
                using SqlConnection connection = new SqlConnection("Server=localhost,1433;Database=OrderManagement;User Id=sa;Password=ec_om@2022;");

                connection.Open();

                connection.InsertAsync<int, Order>(order);

                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
            
            //TODO: Publish a message to a message broker
        }
    }
}
