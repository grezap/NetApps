using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using LoginAppService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoginWebApi.Controllers
{
    public class RoleController : Controller
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

        public IActionResult Index()
        {
            return View();
        }
    }
}