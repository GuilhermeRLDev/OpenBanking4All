using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using PSD2Authentication;
using PSD2AuthenticationDomain.Interfaces.Entity;
using PSD2AuthenticationDomain.Models;

namespace PSD2AuthenticationDomain.Interfaces.Repository.Base
{
    public interface IRepositoryBase<Entity>    
        where Entity : IEntity
    {
        Task<Result<Entity>> GetById(int id);
        Task<Result<Entity, ErrorModel>> Insert(Entity entity);
        Task<Result> DeleteById(int id);
        Task<Result<IEnumerable<Entity>, ErrorModel>> GetAll();
    }
}
