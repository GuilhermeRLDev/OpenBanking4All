using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PSD2Authentication.Controllers.Base;
using PSD2AuthenticationDomain.Entities;
using PSD2AuthenticationDomain.Interfaces.Repository;
using PSD2AuthenticationDomain.Models;

namespace PSD2Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurationController : ApiControllerBase
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IConfigurationRepository _configurationRepository;

        public ConfigurationController(ILogger<ConfigurationController> logger, IConfigurationRepository configurationRepository) : base(logger)
        {
            _logger = logger;
            _configurationRepository = configurationRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get() => await _configurationRepository.GetAll()
            .Tap(result => _logger.LogInformation($"ControllerName: {nameof(ConfigurationController)} MethodName: {nameof(Get)} - 200"))
            .Finally(result => GetResponseFromResult(result));

        [HttpPost]
        public async Task<ActionResult> Post(Configuration configuration) =>  await configuration.Validate()
            .Tap(result => _configurationRepository.Insert((Configuration)result))
            .Tap(result => _logger.LogInformation($"ControllerName: {nameof(ConfigurationController)} MethodName: {nameof(Post)} - 200"))
            .Finally(result => GetResponseFromResult(result));

    }
}
