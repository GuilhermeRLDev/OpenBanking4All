using PSD2Authentication;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PSD2AuthenticationDomain.Models
{
    public class ErrorModel
    {
        public ErrorModel(ErrorCode errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }

        public bool IsTechnicalIssue()
        {
            return (int)ErrorCode == (int)HttpStatusCode.InternalServerError; 
        }

    }
}
