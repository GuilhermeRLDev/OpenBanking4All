using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PSD2AuthenticationDomain.Interfaces.Repository.Base
{
    public interface IDapperBase<Entity>
        where Entity : IEntity
    {
        Task<IEnumerable<Entity>> RunQuery(string query);
        Task<IEnumerable<TMain>> RunQuery<TMain, TFirst>(string query)
            where TFirst : IEntity
            where TMain : IParent;
        Task<IEnumerable<TMain>> RunQuery<TMain, TFirst, TSecond>(string query)
            where TFirst : IEntity
            where TSecond : IEntity
            where TMain : IParent;
        Task<IEnumerable<TMain>> RunQuery<TMain, TFirst, TSecond, TThird>(string query)
            where TFirst : IEntity
            where TSecond : IEntity
            where TThird : IEntity
            where TMain : IParent;
        Task<IEnumerable<TMain>> RunQuery<TMain, TFirst, TSecond, TThird, TFourth>(string query)
            where TFirst : IEntity
            where TSecond : IEntity
            where TThird : IEntity
            where TFourth : IEntity
            where TMain : IParent;
        Task<Result<int>> Execute(string command);
        //Execute method and gets back theprimary key 
        Task<Result<IDictionary<string, object>>> ExecuteInsert(string command);
    }
}
