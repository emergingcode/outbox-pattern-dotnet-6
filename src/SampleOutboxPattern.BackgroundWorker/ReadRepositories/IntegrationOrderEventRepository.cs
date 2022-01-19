using Dapper;

using SampleOutboxPattern.BackgroundWorker.DataModels;

using System.Data.SqlClient;

namespace SampleOutboxPattern.BackgroundWorker.ReadRepositories
{
    internal class IntegrationOrderEventRepository
    {
        public async Task<IEnumerable<OutboxIntegrationOrderEventsData>> ReadOutboxUnprocessedOrderEventsAsync()
        {
            try
            {
                using SqlConnection connection = new SqlConnection("Server=localhost,1433;Database=Outbox_OrderManagement;User Id=sa;Password=ec_om@2022;");

                connection.Open();

                var outboxOrderEvents = await connection.GetListAsync<OutboxIntegrationOrderEventsData>("WHERE ProcessedOn IS NULL");
                
                connection.Close();

                return outboxOrderEvents;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateOutboxOrderEventAsProcessed(Guid id)
        {
            try
            {
                using SqlConnection connection = new SqlConnection("Server=localhost,1433;Database=Outbox_OrderManagement;User Id=sa;Password=ec_om@2022;");

                connection.Open();

                await connection.ExecuteAsync("UPDATE OutboxIntegrationOrderEvents SET ProcessedOn = @ProcessedDate WHERE Id = @Id",
                    new
                    {
                        ProcessedDate = DateTime.UtcNow,
                        Id = id
                    });

                connection.Close();                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
