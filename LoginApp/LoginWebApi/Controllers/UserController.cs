using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using LoginAppService;
using LoginWebApi.Models;
using LoginWebApi.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoginWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IService _service;
        ILogger<UserController> _log;

        public UserController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IService service,
            ILogger<UserController> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _service = service;
            _log = logger;
        }

        [HttpGet]
        [Route("getallusers")]
        public IActionResult GetAllUsers()
        {
            _log.LogInformation("Called GetAllUsers.");
            try
            {
                var data = _service.GetUsers().Result;
                var response = new ResponseModel<List<ApplicationUser>>(data);
                //return new ObjectResult(new { data, Status = Status.Success }) { StatusCode = StatusCodes.Status200OK};
                return new ObjectResult(response);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<List<ApplicationUser>>(null, ex, "Could not get Users."));
            }
        }

        [HttpGet]
        [Route("GetUserByUserName")]
        public IActionResult GetUserByUserName([FromQuery] string username)
        {
            _log.LogInformation("Called GetUserByUserName");
            try
            {
                var user = _service.GetUserByUserName(username);
                return new ObjectResult(new ResponseModel<ApplicationUser>(user));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<ApplicationUser>(null, ex, "Could not get User."));
            }
        }

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody] ApplicationUserDto user, [FromQuery] string role)
        {
            _log.LogInformation("Called CreateUser");
            try
            {
                ApplicationUser applicationUser = new ApplicationUser { UserName = user.UserName };
                var result = _userManager.CreateAsync(applicationUser,user.Password).Result;
                if (result.Errors.Any())
                {
                    return new ObjectResult(new ResponseModel<ApplicationUser>(null, result.Errors, "User Could Not Be Created."));
                }
                var newUser = _service.GetUserByUserName(user.UserName);
                var roleresult = _userManager.AddToRoleAsync(newUser, role).Result;
                if (result.Errors.Any())
                {
                    return new ObjectResult(new ResponseModel<ApplicationUser>(null, result.Errors, "User was created Successfully But Could Noit Add Role On It."));
                }
                return new ObjectResult(new ResponseModel<ApplicationUser>(null, message: "Successfully Created User."));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<ApplicationUser>(null, ex, "Could not create User."));
            }
        }
        
        [HttpPost]
        [Route("CreateUsers")]
        public async Task<IActionResult> CreateUsers([FromBody] List<ApplicationUserDto> applicationUsers, [FromQuery] string role)
        {
            _log.LogInformation("Called CreateUsers");
            try
            {
                List<IdentityError> errors = new List<IdentityError>();
                foreach (var u in applicationUsers)
                {
                    ApplicationUser applicationUser = new ApplicationUser { UserName = u.UserName };
                    var result = await _userManager.CreateAsync(applicationUser, u.Password);//.Result;
                    if (result.Errors.Any())
                    {
                        errors.AddRange(result.Errors);
                        continue;
                    }
                    var newuser = _service.GetUserByUserName(u.UserName);
                    var roleresult = await _userManager.AddToRoleAsync(newuser, role);//.Result;
                    if (roleresult.Errors.Any())
                    {
                        errors.AddRange(roleresult.Errors);
                        continue;
                    }
                }
                if (errors.Any())
                {
                    return new ObjectResult(new ResponseModel<List<ApplicationUser>>(null, errors, message: "Users could not be created successfully or Users have been created and roles could not be successfully added to them."));
                }
                return new ObjectResult(new ResponseModel<List<ApplicationUser>>(null, message: "Successfully Created Users."));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<List<ApplicationUser>>(null, ex, "Could not create Users."));
            }
        }

        [HttpPost]
        [Route("RemoveUserFromRole")]
        public IActionResult RemoveUserFromRole([FromQuery] string username, [FromQuery] string role)
        {
            _log.LogInformation("Called RemoveUserFromRole");
            try
            {
                var user = _service.GetUserByUserName(username);
                var result = _userManager.RemoveFromRoleAsync(user, role).Result;
                if (result.Errors.Any())
                {
                    return new ObjectResult(new ResponseModel<ApplicationUser>(null, result.Errors, "Could not remove role from user."));
                }
                return new ObjectResult(new ResponseModel<ApplicationUser>(null, message: "Successfully removed role from user."));
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                return new ObjectResult(new ResponseModel<ApplicationUser>(null, ex, "Could not remove user from role."));
            }
        }


    }
}
