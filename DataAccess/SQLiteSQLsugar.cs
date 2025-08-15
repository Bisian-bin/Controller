using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EntityRepositorys<T> : IDisposable where T : class, new()
    {
        private readonly SqlSugarScope _db;
        private readonly string _connectionString = "Data Source=Database.db;Version=3;Pooling=True;Max Pool Size=100;BusyTimeout=3000;Journal Mode=WAL;";

        public EntityRepositorys()
        {
            _db = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            },
            db =>
            {
                // 自动建表（仅当表不存在时）
                db.CodeFirst.InitTables(typeof(T));
            });
        }

        // 添加实体
        public async Task<long> Add(T entity)
        {
            return await _db.Insertable(entity).ExecuteReturnBigIdentityAsync();
        }

        // 更新实体
        public async Task<int> Update(T entity, string whereClause = null, object whereParams = null)
        {
            var update = _db.Updateable(entity);
            if (!string.IsNullOrEmpty(whereClause))
                update = update.Where(whereClause, whereParams);
            return await update.ExecuteCommandAsync();
        }

        // 删除实体
        public async Task<int> Delete(string whereClause, object whereParams)
        {
            return await _db.Deleteable<T>().Where(whereClause, whereParams).ExecuteCommandAsync();
        }

        // 查询实体列表
        public async Task<List<T>> Query(string whereClause = null, object parameters = null)
        {
            var query = _db.Queryable<T>();
            if (!string.IsNullOrEmpty(whereClause))
                query = query.Where(whereClause, parameters);
            return await query.ToListAsync();
        }

        // 根据主键获取实体
        public async Task<T> GetById(int id)
        {
            return await _db.Queryable<T>().InSingleAsync(id);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}