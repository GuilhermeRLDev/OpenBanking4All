using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Interfaces.Entity;
using PSD2AuthenticationDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSD2AuthenticationDomain.Entities
{
    public class CertificateConfiguration : IEntity
    {
        public Guid CertificateconfigurationId { get; set; }
        public Guid ConfigurationId { get; set; } 
        public string CertificatePath { get; set; }

        public string GetPrimaryKeyName()
        {
            return nameof(CertificateconfigurationId);
        }

        public Guid GetPrimaryKeyValue()
        {
            return CertificateconfigurationId;
        }

        public Result<IEntity, ErrorModel> Validate()
        {
            throw new NotImplementedException();
        }
    }
}
