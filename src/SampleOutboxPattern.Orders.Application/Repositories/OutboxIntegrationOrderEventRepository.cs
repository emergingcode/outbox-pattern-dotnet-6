using Dapper;

using SampleOutboxPattern.Orders.Application.DataModels;

using System.Data.SqlClient;

namespace SampleOutboxPattern.Orders.Application.Repositories
{
    internal class OutboxIntegrationOrderEventRepository
    {
        public async Task RegisterPlacedOrderEventAsync(OutboxIntegrationOrderEventsData outboxIntegrationOrderEventsData)
        {
            try
            {
                using SqlConnection connection = new SqlConnection("Server=localhost,1433;Database=OrderManagement;User Id=sa;Password=ec_om@2022;");

                connection.Open();

                await connection.InsertAsync<Guid, OutboxIntegrationOrderEventsData>(outboxIntegrationOrderEventsData);

                connection.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
