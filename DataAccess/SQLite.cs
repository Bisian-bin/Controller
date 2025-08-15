using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace DataAccess
{
    public class EntityRepository<T> : IDisposable where T : class, new()
    {
        private const string ConnectionString = "Data Source=Database.db;Version=3;Pooling=True;Max Pool Size=100;BusyTimeout=3000;Journal Mode=WAL;";
        private readonly SQLiteConnection _connection;
        private readonly string _tableName;

        public EntityRepository()
        {
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();
            _tableName = typeof(T).Name;
            CreateTableIfNotExists();
        }

        // 自动创建表（基于实体属性）
        private void CreateTableIfNotExists()
        {
            var columns = GetPropertyDefinitions();
            var sql = $"CREATE TABLE IF NOT EXISTS {_tableName} (Id INTEGER PRIMARY KEY AUTOINCREMENT, {columns})";
            ExecuteNonQuery(sql);
        }

        // 获取属性定义
        private string GetPropertyDefinitions()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.Name != "Id" && p.CanWrite)
                .Select(p => $"{p.Name} {GetSqlType(p.PropertyType)}");

            return string.Join(", ", properties);
        }

        // 映射C#类型到SQLite类型
        private string GetSqlType(Type type)
        {
            if (type == typeof(int) || type == typeof(long) || type == typeof(bool))
                return "INTEGER";
            if (type == typeof(float) || type == typeof(double) || type == typeof(decimal))
                return "REAL";
            if (type == typeof(DateTime))
                return "DATETIME";
            return "TEXT";
        }

        // 添加实体
        public async Task<long> Add(T entity)
        {
            var properties = GetWritableProperties();
            var columns = string.Join(", ", properties.Select(p => p.Name));
            var parameters = string.Join(", ", properties.Select(p => $"@{p.Name}"));

            var sql = $"INSERT INTO {_tableName} ({columns}) VALUES ({parameters}); SELECT last_insert_rowid();";

            var paramDict = properties.ToDictionary(
                p => $"@{p.Name}",
                p => p.GetValue(entity) ?? DBNull.Value
            );

            return Convert.ToInt64(ExecuteScalar(sql, paramDict));
        }

        // 更新实体
        public async Task<int> Update(T entity, string whereClause = null, Dictionary<string, object> whereParams = null)
        {
            var properties = GetWritableProperties();
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));

            var sql = $"UPDATE {_tableName} SET {setClause}";

            if (!string.IsNullOrEmpty(whereClause))
                sql += $" WHERE {whereClause}";

            var paramDict = properties.ToDictionary(
                p => $"@{p.Name}",
                p => p.GetValue(entity) ?? DBNull.Value
            );

            if (whereParams != null)
            {
                foreach (var param in whereParams)
                    paramDict.Add(param.Key, param.Value);
            }

            return ExecuteNonQuery(sql, paramDict);
        }

        // 删除实体
        public async Task<int> Delete(string whereClause, Dictionary<string, object> parameters)
        {
            var sql = $"DELETE FROM {_tableName} WHERE {whereClause}";
            return ExecuteNonQuery(sql, parameters);
        }

        // 查询实体
        public async Task<List<T>>  Query(string whereClause = null, Dictionary<string, object> parameters = null)
        {
            var sql = $"SELECT * FROM {_tableName}";
            if (!string.IsNullOrEmpty(whereClause))
                sql += $" WHERE {whereClause}";

            using (var dt = ExecuteQuery(sql, parameters))
                return ConvertDataTableToList(dt);
        }

        // 获取单个实体
        public T GetById(int id)
        {
            var sql = $"SELECT * FROM {_tableName} WHERE Id = @id";
            var parameters = new Dictionary<string, object> { { "@id", id } };

            using (var dt = ExecuteQuery(sql, parameters))
                return ConvertDataTableToList(dt).FirstOrDefault();
        }

        // === 数据库底层操作 ===
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var transaction = _connection.BeginTransaction(IsolationLevel.Serializable))
            using (var command = new SQLiteCommand(sql, _connection, transaction))
            {
                try
                {
                    AddParameters(command, parameters);
                    int result = command.ExecuteNonQuery();
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private object ExecuteScalar(string sql, Dictionary<string, object> parameters = null)
        {
            using (var transaction = _connection.BeginTransaction(IsolationLevel.Serializable))
            using (var command = new SQLiteCommand(sql, _connection, transaction))
            {
                try
                {
                    AddParameters(command, parameters);
                    var result = command.ExecuteScalar();
                    transaction.Commit();
                    return result;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public DataTable ExecuteQuery(string sql, Dictionary<string, object> parameters = null)
        {
            using (var command = new SQLiteCommand(sql, _connection))
            {
                AddParameters(command, parameters);
                using (var adapter = new SQLiteDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private void AddParameters(SQLiteCommand command, Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            }
        }

        // === 辅助方法 ===
        private List<T> ConvertDataTableToList(DataTable dt)
        {
            var list = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                var entity = new T();
                foreach (var prop in GetWritableProperties())
                {
                    if (dt.Columns.Contains(prop.Name) && row[prop.Name] != DBNull.Value)
                    {
                        prop.SetValue(entity, Convert.ChangeType(row[prop.Name], prop.PropertyType));
                    }
                }
                list.Add(entity);
            }
            return list;
        }

        private PropertyInfo[] GetWritableProperties()
        {
            return typeof(T).GetProperties()
                .Where(p => p.CanWrite && p.Name != "Id")
                .ToArray();
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
