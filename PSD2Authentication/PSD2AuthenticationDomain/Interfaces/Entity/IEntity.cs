using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSD2AuthenticationDomain.Interfaces.Entity
{
    public interface IEntity
    {
        public Guid GetPrimaryKeyValue();
        public string GetPrimaryKeyName();
        Result<IEntity, ErrorModel> Validate();
    }
}
