using LoginWebApi.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginWebApi.Responses
{
    public class ResponseModel : ObjectResult
    {
        public List<ErrorModel> Errors { get; set; }
        public StatusModel StatusModel { get; set; }

        public ResponseModel(Exception value) : base(value)
        {
            Errors = new List<ErrorModel> { new ErrorModel { Code = "Exception", Description = value.Message } };
            StatusModel = new StatusModel {Status = Status.Fail };
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ResponseModel(object value) : base(value)
        {
            StatusModel = new StatusModel { Status = Status.Success };
            StatusCode = StatusCodes.Status200OK;
        }

        public ResponseModel(IEnumerable<IdentityError> value) : base(value)
        {
            StatusModel = new StatusModel { Status = Status.Fail };
            StatusCode = StatusCodes.Status500InternalServerError;
            Errors = new List<ErrorModel>();
            foreach (var v in value)
            {
                Errors.Add(new ErrorModel {Code=v.Code,Description=v.Description });
            }
        }

    }
}
