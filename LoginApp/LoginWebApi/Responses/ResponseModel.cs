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
    public class ResponseModel<T> //: ObjectResult
    {
        public List<ErrorModel> Errors { get; set; }
        public StatusModel StatusModel { get; set; }
        public T Data { get; set; }
        
        public ResponseModel(T data, Exception ex, string message = null)
        {
            Data = data;
            Errors = new List<ErrorModel> { new ErrorModel { Code = "Exception", Description = ex.Message } };
            StatusModel = new StatusModel {Status = Status.Fail, Message = message };
            //value = new {Errors,StatusModel };
            //StatusCode = StatusCodes.Status500InternalServerError;
        }

        public ResponseModel(T data, string message = null)
        {
            Data = data;
            Errors = null;
            StatusModel = new StatusModel { Status = Status.Success, Message = message };
            //StatusCode = StatusCodes.Status200OK;
        }

        public ResponseModel(T data, IEnumerable<IdentityError> value, string message = null)
        {
            Data = data;
            StatusModel = new StatusModel { Status = Status.Fail, Message = message };
            //StatusCode = StatusCodes.Status500InternalServerError;
            Errors = new List<ErrorModel>();
            foreach (var v in value)
            {
                Errors.Add(new ErrorModel {Code=v.Code,Description=v.Description });
            }
        }

    }
}
