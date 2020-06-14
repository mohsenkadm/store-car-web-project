using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using Dapper;
using System;
using store_car_web_project.Classes;

namespace store_car_web_project.Models
{
    public class MasterService<TEntity> where TEntity : class
    {
        private SqlConnection Connection => new SqlConnection(new Keys().connectionstring);

        /// <summary>
        /// This Func use to execute Insert, Update and Delete command
        /// </summary>
        /// <param name="Query">SQL query</param>
        public void RunScript(string Query)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    Connection.Open();

                Connection.Query(Query, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => RunScript");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute Insert, Update and Delete command
        /// </summary>
        /// <param name="Query">SQL query</param>
        public async Task RunScriptAsync(string Query)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    await Connection.OpenAsync();

                await Connection.QueryAsync(Query, commandType: CommandType.Text);
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => RunScriptAsync");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute stored procedure that not return any data 
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="pars">object of parameters</param>
        public void RunSp(string spName, object pars)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    Connection.OpenAsync();

                Connection.Query(spName, pars, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => RunSp");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute stored procedure that not return any data 
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="pars">object of parameters</param>
        public async Task RunSpAsync(string spName, object pars)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    await Connection.OpenAsync();

                await Connection.QueryAsync(spName, pars, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => RunSpAsync");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute stored procedure that return single object
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="pars">object of parameters</param>
        /// <returns>TEntity</returns>
        public TEntity GetEntity(string spName, object pars)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    Connection.Open();

                IEnumerable<TEntity> result = Connection.Query<TEntity>(spName, pars,
                   commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => GetEntity");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute stored procedure that return single object
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="pars">object of parameters</param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> GetEntityAsync(string spName, object pars)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    await Connection.OpenAsync();

                IEnumerable<TEntity> result = await Connection.QueryAsync<TEntity>(spName, pars,
                   commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => GetEntityAsync");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute stored procedure that return list of object
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="pars">object of parameters</param>
        /// <returns>List of TEntity</returns>
        public List<TEntity> GetEntityList(string spName, object pars)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    Connection.Open();

                IEnumerable<TEntity> result = Connection.Query<TEntity>(spName, pars,
                   commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => GetEntityList");
                throw;
            }
        }

        /// <summary>
        /// This Func use to execute stored procedure that return list of object
        /// </summary>
        /// <param name="spName">stored procedure name</param>
        /// <param name="pars">object of parameters</param>
        /// <returns>List of TEntity</returns>
        public async Task<List<TEntity>> GetEntityListAsync(string spName, object pars)
        {
            try
            {
                if (Connection == null && Connection.State == ConnectionState.Closed)
                    await Connection.OpenAsync();

                IEnumerable<TEntity> result = await Connection.QueryAsync<TEntity>(spName, pars,
                   commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
            catch (Exception ex)
            {
                new Logger().Write(ex, "MasterServices => GetEntityListAsync");
                throw;
            }
        }
    }
}
