using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace TimeSeriesApi.Extension
{
    public static class DbHelper
    {
        #region EF Core Sql Query

        /// <summary>
        /// var data = _context.LoadStoredProc("StoredProcedureName").WithSqlParam("firstparamname", firstParamValue).WithSqlParam("secondparamname", secondParamValue).ExecuteStoredProcAsync<MyType>()
        /// </summary>
        /// <param name="storedProcName"></param>
        /// <returns></returns>
        public static DbCommand LoadStoredProc(this DbContext context, string storedProcName)
        {
            var cmd = context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = storedProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static DbCommand WithSqlParam(this DbCommand cmd, string paramName, object paramValue)
        {
            if (string.IsNullOrEmpty(cmd.CommandText))
                throw new InvalidOperationException("Call LoadStoredProc before using this method");
            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            cmd.Parameters.Add(param);
            return cmd;
        }

        /// <summary>
        /// var data = _context.LoadStoredProc("StoredProcedureName").WithSqlParam("firstparamname", firstParamValue).WithSqlParam("secondparamname", secondParamValue).ExecuteStoredProcAsync<MyType>()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<List<T>> ExecuteStoredProcAsync<T>(this DbCommand command)
        {
            using (command)
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();
                try
                {
                    using DbDataReader reader = await command.ExecuteReaderAsync();
                    //DataTable dt = new DataTable();
                    //dt.Load(reader);

                    return reader.MapToList<T>();
                }
                catch (DbException ex)
                {
                    throw ex;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task ExecuteNonQueryAsync(this DbContext context, string query)
        {
            try
            {
                using var command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                context.Database.OpenConnection();
                await command.ExecuteNonQueryAsync();
            }
            catch (DbException ex)
            {
                throw ex;
            }
            catch (Exception) { throw; }
            finally
            {
                context.Database.CloseConnection();
            }
        }

        private static List<T> MapToList<T>(this DbDataReader dr)
        {
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties();

            var dtc = dr.GetColumnSchema();

            var colMapping = dr.GetColumnSchema()
                              .Where(x => props.Any(y => y.Name.ToLower() == x.ColumnName.ToLower()))
                              .ToDictionary(key => key.ColumnName.ToLower());

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    T obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var val =
                          dr.GetValue(colMapping[prop.Name.ToLower()].ColumnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }
                    objList.Add(obj);
                }
            }
            return objList;
        }

        #endregion
    }
}
