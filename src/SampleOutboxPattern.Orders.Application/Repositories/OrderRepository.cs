using Dapper;

using SampleOutboxPattern.Orders.Application.Models;

using System.Data.SqlClient;

namespace SampleOutboxPattern.Orders.Application.Repositories
{
    internal class OrderRepository
    {
        public async Task PlaceOrder(Order customerOrder)
        {
            string productsSqlStmt = "INSERT INTO ORDERDETAILS(ORDERID, SKU, NAME, UNITPRICE, QUANTITY, LINENUMBER) VALUES(@ORDERID, @SKU, @NAME, @UNITPRICE, @QUANTITY, @LINENUMBER)";

            try
            {
                using SqlConnection connection = new SqlConnection("Server=localhost,1433;Database=OrderManagement;User Id=sa;Password=ec_om@2022;");

                connection.Open();

                var customerOrderId = await connection.InsertAsync<int, Order>(customerOrder);
                int lineNumber = 1;
                foreach (var orderProduct in customerOrder.Products)
                {
                    await connection.ExecuteAsync(productsSqlStmt, new
                    {
                        OrderId = customerOrderId,
                        Sku = orderProduct.Sku,
                        Name = orderProduct.Name,
                        UnitPrice = orderProduct.Value,
                        Quantity = orderProduct.Quantity,
                        LineNumber = lineNumber++
                    });
                }

                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
