using Microsoft.Data.SqlClient;
using Microsoft.OpenApi.Writers;
using System.Data;
using WatchStock.Entities.Model.StockInfoModel;
using WatchStock.RepositoryContracts.StockRepositoryContract;
using WatchStock.ServiceContracts.StockContract;
using WatchStock.Utitlies.DBUtlities;

namespace WatchStock.Repositories.StockRepository
{
    public class CurrentValueRepository : ICurrentValueRepository
    {
        private readonly IConfiguration _dbConfiguration;
        public CurrentValueRepository(IConfiguration dbConfiguration)
        { 
            _dbConfiguration = dbConfiguration;
        }

        public async Task AddCurrentStockValue(CurrentValueResponseModel currentValueResponseModel)
        {
            string connectionString =  _dbConfiguration.GetConnectionString("stockDBConfig");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string postQuery = "INSERT INTO [dbo].[CurrentValue] (StockId, StockSymbol, CurrentPrice, PercentChange, HighPriceOfTheDay, LowPriceOfTheDay) VALUES (@StockId, @StockSymbol, @CurrentPrice, @PercentChange, @highPriceOfTheDay, @LowPriceOfTheDay)";
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(postQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("StockId", ConstraintUtilities.GeneratePrimaryKey());
                        sqlCommand.Parameters.AddWithValue("@StockSymbol", currentValueResponseModel.StockSymbol);
                        sqlCommand.Parameters.AddWithValue("@CurrentPrice", currentValueResponseModel.CurrentPrice);
                        sqlCommand.Parameters.AddWithValue("@PercentChange", currentValueResponseModel.PercentChange);
                        sqlCommand.Parameters.AddWithValue("@HighPriceOfTheDay", currentValueResponseModel.HighPriceOfTheDay);
                        sqlCommand.Parameters.AddWithValue("@LowPriceOfTheDay", currentValueResponseModel.LowPriceOfTheDay);
                        sqlCommand.ExecuteNonQuery();
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public async Task<CurrentValueResponseModel> GetCurrentStockValue(string StockSymbol)
        {
            CurrentValueResponseModel currentValueResponseModel = new CurrentValueResponseModel();
            string connectionString = _dbConfiguration.GetConnectionString("stockDBConfig");
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                string postQuery = "SELECT * FROM [dbo].[CurrentValue] WHERE StockSymbol = \'@StockSymbol\'";
                try
                {
                    sqlConnection.Open();
                    using (SqlDataAdapter sqlCommand = new SqlDataAdapter(postQuery, sqlConnection))
                    {
                        DataTable dataTable = new DataTable();
                        sqlCommand.Fill(dataTable);
                        DataRow row = dataTable.Rows[0];
                        currentValueResponseModel.StockId = (int)row["StockId"];
                        currentValueResponseModel.StockSymbol = (string)row["StockSymbol"];
                        currentValueResponseModel.CurrentPrice = (double)row["CurrentPrice"];
                        currentValueResponseModel.PercentChange = (double)row["PercentChange"];
                        currentValueResponseModel.HighPriceOfTheDay = (double)row["HighPriceOfTheDay"];
                        currentValueResponseModel.LowPriceOfTheDay = (double)row["LowPriceOfTheDay"];
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            currentValueResponseModel.Source = "DB";
            return currentValueResponseModel;
        }
    }
}
