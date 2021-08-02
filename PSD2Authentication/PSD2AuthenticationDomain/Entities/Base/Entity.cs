using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Interfaces.Entity;
using PSD2AuthenticationDomain.Models;

namespace PSD2AuthenticationDomain.Entities.Base
{
    public class Entity : IEntity
    {
        // In order to be used it needs to be implemented in the child class
        public virtual string GetPrimaryKeyName()
        {
            throw new NotImplementedException();
        }
        // In order to be used it needs to be implemented in the child class
        public virtual Guid GetPrimaryKeyValue()
        {
            throw new NotImplementedException();
        }
        // In order to be used it needs to be implemented in the child class
        public virtual Result<IEntity, ErrorModel> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
