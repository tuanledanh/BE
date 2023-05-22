using Dapper;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace MSIA.WebFresher032023.Demo.DL_Repositories.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {

        protected readonly string _connectionString;
        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"] ?? "";
        }

        public async Task<DbConnection> GetOpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public virtual async Task<TEntity?> GetAsync(Guid id)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Get" + tableName + "ById";
            var connection = await GetOpenConnectionAsync();
            var paramName = "p_" + tableName + "Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, id);
            var entity = await connection.QueryFirstOrDefaultAsync<TEntity>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entity;
        }

        public virtual async Task<List<TEntity>> GetListAsync(int pageNumber, int pageLimit, string filterName)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Filter" + tableName;
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_PageNumber = pageNumber,
                p_PageLimit = pageLimit,
                p_FilterName = filterName
            };
            var entities = await connection.QueryAsync<TEntity>(procedureName, parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return entities.ToList();
        }

        public virtual async Task<bool> PostAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> PutAsync(Guid id, TEntity entity)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var tableName = typeof(TEntity).Name;
            string procedureName = "Proc_Delete" + tableName + "ById";
            var paramName = "p_" + tableName + "Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, id);
            var connection = await GetOpenConnectionAsync();
            var affectedRow = await connection.ExecuteAsync(procedureName, dynamicParams, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }
    }
}
