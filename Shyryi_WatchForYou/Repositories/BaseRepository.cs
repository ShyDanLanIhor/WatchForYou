using Microsoft.Data.SqlClient;

namespace Shyryi_WatchForYou.Models.Repositories
{
    public class RepositoryBase
    {
        private readonly string _connectionString;

        public RepositoryBase()
        {
            _connectionString = "Server=(localdb)\\mssqllocaldb;Database=WatchForYou;Trusted_Connection=True;";
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
