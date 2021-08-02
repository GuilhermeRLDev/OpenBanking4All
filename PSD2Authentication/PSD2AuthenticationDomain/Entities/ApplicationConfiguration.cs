using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Interfaces.Entity;
using PSD2AuthenticationDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSD2AuthenticationDomain.Entities
{
    public class ApplicationConfiguration : IEntity
    {
        public Guid ApplicationId { get; set; }
        public Guid ConfigurationId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ApplicationName { get; set; }
        public string Scope { get; set; }

        public string GetPrimaryKeyName()
        {
            return nameof(ApplicationId);
        }

        public Guid GetPrimaryKeyValue()
        {
            return ApplicationId;
        }

        public Result<IEntity, ErrorModel> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
