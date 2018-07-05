using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using LoginAppService;
using LoginWebApi.Enums;
using LoginWebApi.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoginWebApi.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IService _service;
        ILogger<RoleController> _log;

        public RoleController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IService service,
            ILogger<RoleController> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _service = service;
            _log = logger;
        }

        [HttpGet]
        [Route("getallroles")]
        public IActionResult GetAllRoles()
        {

            _log.LogInformation("GetAllRoles has been called.");
            try
            {
                var data = _service.GetApplicationRoles();
                return new ObjectResult(new { data, Status = Status.Success }) { StatusCode = StatusCodes.Status200OK};
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new { error = ex.Message, Status = Status.Fail}) { StatusCode = StatusCodes.Status500InternalServerError };
            }


        }

        [HttpPost]
        [Route("createrole")]
        public IActionResult CreateRole([FromBody] ApplicationRole role)
        {
            _log.LogInformation("CreateRole has been called");
            try
            {
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Errors.Any())
                {
                    return new ObjectResult(new { error = result.Errors, Status = Status.Fail }) { StatusCode = StatusCodes.Status500InternalServerError };
                }
                return new ObjectResult(new { Status = Status.Success }) { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new { error = ex.Message, Status = Status.Fail }) { StatusCode = StatusCodes.Status500InternalServerError };
            }
        }
    }
}