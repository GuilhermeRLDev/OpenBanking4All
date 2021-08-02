using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using CSharpFunctionalExtensions;
using PSD2AuthenticationDomain.Entities;
using PSD2AuthenticationDomain.Interfaces.Repository;
using PSD2AuthenticationDomain.Interfaces.Repository.Base;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using PSD2AuthenticationRepository.Concrete.Repositories.Base;
using Microsoft.Extensions.Configuration;
using PSD2AuthenticationDomain.Models;
using PSD2Authentication;
using PSD2AuthenticationRepository.Concrete.Helpers;


namespace PSD2AuthenticationRepository.Concrete.Repositories
{
    public class ConfigurationRepository : Repository<Configuration>, IConfigurationRepository
    {
        private readonly IConfiguration _configuration;

        public ConfigurationRepository(IConfiguration configuration) : base(configuration) 
        {
            _configuration = configuration;
        }

        public override async Task<Result<IEnumerable<Configuration>, ErrorModel>> GetAll()
        {
            try
            {
                var query = @"SELECT * FROM Configuration c LEFT JOIN ApplicationConfiguration a on a.ConfigurationId = c.ConfigurationId LEFT JOIN  CertificateConfiguration CC on CC.ConfigurationId = c.ConfigurationId ORDER BY c.Creation DESC";

                var result = await RunQuery<Configuration, ApplicationConfiguration, CertificateConfiguration>(query);

                return Result.Success<IEnumerable<Configuration>, ErrorModel>(result.ToList().Distinct());
            }
            catch (Exception e)
            {
                return Result.Failure<IEnumerable<Configuration>, ErrorModel>(new ErrorModel(ErrorCode.TechnicalIssues, e.Message));
            }
        }

        public override async Task<Result<Configuration, ErrorModel>> Insert(Configuration configuration) 
        {
            try
            {
                return await configuration.ToInsertCommand()
                    .Bind(query => ExecuteInsert(query))
                    .Map(results => MapModel(configuration, results))
                    .Finally(result => result.Value); 
            }
            catch (Exception e)
            {
                return Result.Failure<Configuration, ErrorModel>(new ErrorModel(ErrorCode.TechnicalIssues, e.Message));
            }
        }

        private Result<Configuration, ErrorModel> MapModel(Configuration model, IDictionary<string, object> items)
        {
            model.ConfigurationId = (Guid)items[nameof(model.ConfigurationId)];
            model.Creation = (DateTime)items[nameof(model.Creation)];
            return Result.Success<Configuration, ErrorModel>(model);  
        }

    }
}
