using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using PSD2AuthenticationDomain.Models;
using CSharpFunctionalExtensions;
using System.Net;
using PSD2Authentication.Constants;
using Microsoft.Extensions.Logging;

namespace PSD2Authentication.Controllers.Base
{
    public class ApiControllerBase : ControllerBase
    {
        private const string _contentType = "application/json";
        private readonly ILogger _logger;

        public ApiControllerBase(ILogger logger)
        {
            _logger = logger;
        }
        protected ActionResult GetResponseFromResult<T>(Result<T, ErrorModel> result)
            where T : class
        {
            return result.IsSuccess ? GetOkResponse(result.Value) : GetErrorResponse(result.Error);
        }
        private ActionResult GetOkResponse<T>(T value)
            where T : class
        {
            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(value),
                StatusCode = (int)HttpStatusCode.OK,
                ContentType = _contentType
            };
        }

        private ActionResult GetErrorResponse(ErrorModel model) 
        {
            return Result.Success(model)
                .Ensure(result => result.IsTechnicalIssue(), model.Message)
                .Finally(result => result.IsFailure ? GetBadResponse(model) : GetInternalServerError(model));
        }
        
        private ActionResult GetBadResponse(ErrorModel model)
        {
            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(model),
                StatusCode = (int)HttpStatusCode.BadRequest,
                ContentType = _contentType
            };
        }

        private ActionResult GetInternalServerError(ErrorModel model)
        {
            // Logs default message and then returns customer friendly message. 
            _logger.LogError($"Error code {model.ErrorCode} - {model.Message}");

            return new ContentResult()
            {
                Content = JsonConvert.SerializeObject(new ErrorModel(model.ErrorCode, DefaultMessageConstants.TechnicalIssuesMessage)),
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ContentType = _contentType
            };
        }
    }
}
