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
                //throw new Exception("Test error");
                var data = _service.GetApplicationRoles();
                var response = new ResponseModel<List<ApplicationRole>>(data);
                //return new ObjectResult(new { data, Status = Status.Success }) { StatusCode = StatusCodes.Status200OK};
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<List<ApplicationRole>>(null,ex, "Could not get Roles."));
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
                    return new ObjectResult(new ResponseModel<ApplicationRole>(null,result.Errors,"Role Could Not Be Created."));
                }
                return new ObjectResult(new ResponseModel<ApplicationRole>(null,message: "Successfully Created Role."));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<ApplicationRole>(null,ex,"Role Could not be created."));
            }
        }

        [HttpPost]
        [Route("createroles")]
        public IActionResult CreateRoles([FromBody] List<ApplicationRole> roles)
        {
            _log.LogInformation("CreateRoles has been called");

            List<IdentityError> errors = new List<IdentityError>();
            try
            {
                roles.ForEach(r => 
                {
                    var result = _roleManager.CreateAsync(r).Result;
                    if (result.Errors.Any())
                    {
                        errors.AddRange(result.Errors);
                    }
                });
                if (errors.Any())
                {
                    return new ObjectResult(new ResponseModel<List<ApplicationRole>>(null, errors, message: "Roles could not be created successfully."));
                }
                return new ObjectResult(new ResponseModel<List<ApplicationRole>>(null, message: "Successfully Created All Roles."));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<List<ApplicationRole>>(null, ex, "Roles Could not be created."));
            }
        }

        [HttpGet]
        [Route("getrolesbyusername")]
        public IActionResult GetRolesByUserName([FromQuery] string username)
        {
            _log.LogInformation("GetRolesByUserName has been called");
            try
            {
                var user = _service.GetUserByUserName(username);
                var roles = _service.GetApplicationRolesByUser(user);
                return new ObjectResult(new ResponseModel<List<ApplicationRole>>(roles,"Successfully got roles for specified user."));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult ( new ResponseModel<List<ApplicationRole>>(null,ex,"Could not get roles by username ") );
            }
        }

        [HttpPost]
        [Route("addroletouser")]
        public IActionResult AddRoleToUser([FromQuery] string username, [FromQuery] string role)
        {
            _log.LogInformation("Called AddRoleToUser");
            try
            {
                var user =_service.GetUserByUserName(username);
                var result = _userManager.AddToRoleAsync(user, role).Result;
                if (result.Errors.Any())
                {
                    return new ObjectResult(new ResponseModel<ApplicationRole>(null, result.Errors, "Could not add role to user."));
                }
                return new ObjectResult(new ResponseModel<ApplicationRole>(null, message: "Successfully Added role to user."));
            }
            catch (Exception ex)
            {

                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<List<ApplicationRole>>(null, ex, "Could not add role to user. "));
            }
        }

    }
}