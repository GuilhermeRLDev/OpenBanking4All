using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Entities;
using PSD2AuthenticationDomain.Interfaces.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PSD2AuthenticationDomain.Models;
using PSD2Authentication;
using PSD2AuthenticationDomain.Interfaces.Entity;
using PSD2AuthenticationRepository.Concrete.Helpers;
using static Dapper.SqlMapper;

namespace PSD2AuthenticationRepository.Concrete.Repositories.Base
{
    public class Repository<Entity> : IRepositoryBase<Entity>, IDapperBase<Entity>
        where Entity : IEntity
    {
        private const string _connectionStringName = "PSD2AuthenticationString";

        private readonly IConfiguration _configuration;
        private readonly string _tableName;

        protected readonly string ConnectionString;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
            _tableName = typeof(Entity).Name;
            ConnectionString = configuration.GetConnectionString(_connectionStringName);
        }

        public virtual async Task<IEnumerable<Entity>> RunQuery(string query)
        {
            IEnumerable<Entity> result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                result = await conn.QueryAsync<Entity>(query);
                conn.Close();
            }
            return result;
        }

        public virtual async Task<IEnumerable<TMain>> RunQuery<TMain, TFirst>(string query)
            where TFirst : IEntity
            where TMain : IParent
        {

            IEnumerable<TMain> result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var first = Activator.CreateInstance<TFirst>();

                conn.Open();
                result = await conn.QueryAsync<TMain, TFirst, TMain>(query, new DapperMapper<TMain>().Mapper, splitOn: first.GetPrimaryKeyName());
                conn.Close();
            }

            return result;
        }

        public virtual async Task<IEnumerable<TMain>> RunQuery<TMain, TFirst, TSecond>(string query)
            where TFirst : IEntity
            where TSecond : IEntity
            where TMain : IParent
        {
            IEnumerable<TMain> result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var childrens = new List<Type>() { typeof(TFirst), typeof(TSecond) };

                conn.Open();
                result = await conn.QueryAsync<TMain, TFirst, TSecond, TMain>(query, new DapperMapper<TMain>().Mapper, splitOn: GetSplitOn(childrens));
                conn.Close();
            }

            return result;
        }

        public virtual async Task<IEnumerable<TMain>> RunQuery<TMain, TFirst, TSecond, TThird>(string query)
            where TFirst : IEntity
            where TSecond : IEntity
            where TThird : IEntity
            where TMain : IParent
        {
            IEnumerable<TMain> result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var childrens = new List<Type>() { typeof(TFirst), typeof(TSecond) };

                conn.Open();
                result = await conn.QueryAsync<TMain, TFirst, TSecond, TThird, TMain>(query, new DapperMapper<TMain>().Mapper, splitOn: GetSplitOn(childrens));
                conn.Close();
            }

            return result;
        }

        public virtual async Task<IEnumerable<TMain>> RunQuery<TMain, TFirst, TSecond, TThird, TFourth>(string query)
           where TFirst : IEntity
           where TSecond : IEntity
           where TThird : IEntity
           where TFourth : IEntity
           where TMain : IParent
        {
            IEnumerable<TMain> result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                var childrens = new List<Type>() { typeof(TFirst), typeof(TSecond) };

                conn.Open();
                result = await conn.QueryAsync<TMain, TFirst, TSecond, TThird, TFourth, TMain>(query, new DapperMapper<TMain>().Mapper, splitOn: GetSplitOn(childrens));
                conn.Close();
            }

            return result;
        }

        public virtual async Task<Result<IDictionary<string, object>>> ExecuteInsert(string command)
        {
            IEnumerable<dynamic> result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                result = await conn.QueryAsync(command);
                conn.Close();
            }
            return Result.Success(ConvertToResponse(result));
        }

        public virtual async Task<Result<int>> Execute(string command)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                result = await conn.ExecuteAsync(command);
                conn.Close();
            }
            return Result.Success(result);
        }

        public virtual async Task<Result> DeleteById(int id)
        {
            var result = await Execute($"DELETE FROM {_tableName} WHERE {_tableName}Id = {id.ToString()}");
            return Result.SuccessIf<int>(true, result.Value, null);
        }

        public virtual async Task<Result<Entity>> GetById(int id)
        {
            var result = await RunQuery($"SELECT * FROM {_tableName} WHERE {_tableName}Id = {id.ToString()}");
            return Result.Success(result
                .ToList()
                .FirstOrDefault());
        }

        public virtual async Task<Result<IEnumerable<Entity>, ErrorModel>> GetAll()
        {
            try
            {
                var result = await RunQuery($"SELECT * FROM {_tableName}");
                return Result.Success<IEnumerable<Entity>, ErrorModel>(result);
            }
            catch (Exception e)
            {
                return Result.Failure<IEnumerable<Entity>, ErrorModel>(new ErrorModel(ErrorCode.TechnicalIssues, e.Message));
            }
        }

        public virtual async Task<Result<Entity, ErrorModel>> Insert(Entity entity)
        {
            try
            {
                var result = await RunQuery($"SELECT * FROM {_tableName} WHERE {_tableName}Id = {entity.ToString()}");
                return Result.Success<Entity, ErrorModel>(result.FirstOrDefault());
            }
            catch (Exception e)
            {
                return Result.Failure<Entity, ErrorModel>(new ErrorModel(ErrorCode.TechnicalIssues, e.Message));
            }
        }

        private string GetSplitOn(List<Type> types)
        {
            var items = new List<string>();

            foreach (Type t in types)
            {
                items.Add(((IEntity)Activator.CreateInstance(t)).GetPrimaryKeyName());
            }

            return string.Join(",", items);
        }

        private IDictionary<string, object> ConvertToResponse(IEnumerable<dynamic> reader)
        {
            return reader.FirstOrDefault() as IDictionary<string, object>;
        }
    }
}
