using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Entities;
using PSD2AuthenticationDomain.Interfaces.Repository.Base;

namespace PSD2AuthenticationDomain.Interfaces.Repository
{
    public interface IConfigurationRepository : IRepositoryBase<Configuration>
    {
        //Result<IEnumerable<Configuration>> Any();
    }
}
