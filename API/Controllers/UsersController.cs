using Bcrypt = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using API.Repository.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RolesController : ControllerBase
    {
        private readonly MyContext _context;
        public IConfiguration _configuration;
        RoleRepository _repo;

        public RolesController(MyContext myContext, IConfiguration config, RoleRepository _repo)
        {
            _context = myContext;
            _configuration = config;
            this._repo = _repo;
        }

        [HttpGet]
        public async Task<List<GetRoleVM>> GetAll()
        {
            List<GetRoleVM> list = new List<GetRoleVM>();
            var getData = await _context.Roles.Where(x => x.isDelete == false).ToListAsync();
            if (getData.Count == 0)
            {
                return null;
            }
            foreach (var item in getData)
            {
                var user = new GetRoleVM()
                {
                    Id = item.Id,
                    Name = item.Name
                };
                list.Add(user);
            }
            return list;
        }

        [HttpGet("{id}")]
        public GetRoleVM GetID(string id)
        {
            var getData = _context.Roles.SingleOrDefault(x => x.Id == id);
            if (getData == null)
            {
                return null;
            }
            var role = new GetRoleVM()
            {
                Id = getData.Id,
                Name = getData.Name
            };
            return role;
        }

        [HttpPost]
        public IActionResult Create(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                if (roleVM.Session == null)
                {
                    return BadRequest("Session ID must be filled");
                }
                var role = new RoleVM
                {
                    Name = roleVM.Name
                };
                var create = _repo.Create(role);
                if (create > 0)
                {
                    var getData = _context.Users.SingleOrDefault(x => x.Id == roleVM.Session);
                    Sendlog(getData.Email + " Create Role Successfully", getData.Email);

                    return Ok("Successfully Created");
                }
                return BadRequest("Not Successfully");

            }
            return BadRequest("Not Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                if (roleVM.Session == null)
                {
                    return BadRequest("Session ID must be filled");
                }
                var getData = _context.Roles.SingleOrDefault(x => x.Id == id);
                getData.Name = roleVM.Name;
                getData.UpdateDate = DateTimeOffset.Now;

                _context.Roles.Update(getData);
                _context.SaveChanges();

                var getDataUser = _context.Users.SingleOrDefault(x => x.Id == roleVM.Session);
                Sendlog(getDataUser.Email + " Update Role Successfully", getDataUser.Email);
                return Ok("Successfully Updated");
            }
            return BadRequest("Not Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var getData = _context.Roles.SingleOrDefault(x => x.Id == id);
            if (getData == null)
            {
                return BadRequest("Not Successfully");
            }
            getData.DeleteDate = DateTimeOffset.Now;
            getData.isDelete = true;

            _context.Entry(getData).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(new { msg = "Successfully Delete" });
        }

        private IActionResult Sendlog(string response, string mail)
        {
            LogsController logsController = new LogsController(_context, _configuration);
            var log = new LogVM
            {
                Response = response,
                Email = mail
            };
            return logsController.Create(log);
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsersController : ControllerBase
    {
        private readonly MyContext _context;
        public IConfiguration _configuration;
        UserRepository _repo;

        public UsersController(MyContext myContext, IConfiguration config, UserRepository repo)
        {
            _context = myContext;
            _configuration = config;
            _repo = repo;
        }

        [HttpGet]
        public async Task<List<GetUserVM>> GetAll()
        {
            List<GetUserVM> list = new List<GetUserVM>();
            var getData = await _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).Where(x => x.User.Employee.isDelete == false).ToListAsync();
            if (getData.Count == 0)
            {
                return null;
            }
            foreach (var item in getData)
            {
                var user = new GetUserVM()
                {
                    Id = item.User.Id,
                    Name = item.User.Employee.Name,
                    NIK = item.User.Employee.NIK,
                    Site = item.User.Employee.AssignmentSite,
                    Email = item.User.Email,
                    Password = item.User.Password,
                    RoleName = item.Role.Name,
                    Phone = item.User.Employee.Phone,
                    Address = item.User.Employee.Address,
                    Province = item.User.Employee.Address,
                    City = item.User.Employee.Address,
                    SubDistrict = item.User.Employee.Address,
                    Village = item.User.Employee.Address,
                    ZipCode = item.User.Employee.Address,
                };
                list.Add(user);
            }
            return list;
        }

        [HttpGet("{id}")]
        public GetUserVM GetID(string id)
        {
            var getData = _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).SingleOrDefault(x => x.UserId == id);
            if (getData == null || getData.Role == null || getData.User == null)
            {
                return null;
            }
            var user = new GetUserVM()
            {
                Id = getData.User.Id,
                Name = getData.User.Employee.Name,
                NIK = getData.User.Employee.NIK,
                Site = getData.User.Employee.AssignmentSite,
                Email = getData.User.Email,
                Password = getData.User.Password,
                RoleName = getData.Role.Name,
                Phone = getData.User.Employee.Phone,
                Address = getData.User.Employee.Address,
                Province = getData.User.Employee.Address,
                City = getData.User.Employee.Address,
                SubDistrict = getData.User.Employee.Address,
                Village = getData.User.Employee.Address,
                ZipCode = getData.User.Employee.Address,
            };
            return user;
        }

        [HttpPost]
        public IActionResult Create(GetUserVM getUserVM)
        {
            var getUser = _context.Users.Where(x => x.Email == getUserVM.Email);
            if (getUser.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    if (getUserVM.Session == null)
                    {
                        return BadRequest("Session ID must be filled");
                    }
                    var user = new UserVM
                    {
                        Email = getUserVM.Email,
                        Password = getUserVM.Password,
                        VerifyCode = null,
                    };
                    var create = _repo.Create(user);
                    if (create > 0)
                    {
                        var getUserId = getUser.SingleOrDefault();
                        var getRoleId = _context.Roles.SingleOrDefault(x => x.Name == getUserVM.RoleName);
                        var uRole = new UserRole
                        {
                            UserId = getUserId.Id,
                            RoleId = getRoleId.Id
                        };
                        _context.UserRole.Add(uRole);
                        var emp = new Employee
                        {
                            UserId = getUserId.Id,
                            Name = getUserVM.Name,
                            NIK = getUserVM.NIK,
                            AssignmentSite = getUserVM.Site,
                            Phone = getUserVM.Phone,
                            Address = getUserVM.Address,
                            CreateDate = DateTimeOffset.Now,
                            isDelete = false
                        };
                        _context.Employees.Add(emp);
                        _context.SaveChanges();

                        var getData = _context.Users.SingleOrDefault(x => x.Id == getUserVM.Session);
                        Sendlog(getData.Email + " Create User Successfully", getData.Email);

                        return Ok("Successfully Created");
                    }
                    return BadRequest("Input User Not Successfully");                
                }
                return BadRequest("Not Successfully");
            }
            return BadRequest("Email Already Exists ");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, GetUserVM userVM)
        {
            if (ModelState.IsValid)
            {
                if (userVM.Session == null)
                {
                    return BadRequest("Session ID must be filled");
                }
                var getData = _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).SingleOrDefault(x => x.UserId == id);
                getData.User.Employee.Name = userVM.Name;
                getData.User.Employee.NIK = userVM.NIK;
                getData.User.Employee.AssignmentSite = userVM.Site;
                getData.User.Employee.Phone = userVM.Phone;
                getData.User.Employee.Address = userVM.Address;
                getData.User.Email = userVM.Email;
                if (userVM.Password != null)
                {
                    if (!Bcrypt.Verify(userVM.Password, getData.User.Password))
                    {
                        getData.User.Password = Bcrypt.HashPassword(userVM.Password);
                    }
                }
                if (userVM.RoleName != null)
                {
                    var getRoleID = _context.Roles.SingleOrDefault(x => x.Name == userVM.RoleName);
                    getData.RoleId = getRoleID.Id;
                }
                _context.UserRole.Update(getData);
                _context.SaveChanges();

                var getDataUser = _context.Users.SingleOrDefault(x => x.Id == userVM.Id);
                Sendlog(getDataUser.Email + " Update User Successfully", getDataUser.Email);

                return Ok("Successfully Updated");
            }
            return BadRequest("Not Successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var getData = _context.Employees.Include("User").SingleOrDefault(x => x.UserId == id);
            if (getData == null)
            {
                return BadRequest("Not Successfully");
            }
            getData.DeleteDate = DateTimeOffset.Now;
            getData.isDelete = true;

            _context.Entry(getData).State = EntityState.Modified;
            _context.SaveChanges();

            //var getDataUser = _context.Users.SingleOrDefault(x => x.Id == userVM.Id);
            //Sendlog(getDataUser.Email + " Delete User Successfully", getDataUser.Email);

            return Ok(new { msg = "Successfully Delete" });
        }

        private IActionResult Sendlog(string response, string mail)
        {
            LogsController logsController = new LogsController(_context, _configuration);
            var log = new LogVM
            {
                Response = response,
                Email = mail
            };
            return logsController.Create(log);
        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        readonly MyContext _context;
        public IConfiguration _configuration;

        public LogsController(MyContext myContext, IConfiguration config)
        {
            _context = myContext;
            _configuration = config;
        }

        [HttpGet]
        public async Task<List<Log>> GetAll()
        {
            var getData = await _context.LogActivities
                                .Join(
                                    _context.Employees.Include("User"),
                                    log => log.Email,
                                    uRole => uRole.User.Email,
                                    (log, uRole) => new { Employees = uRole, LogActivities = log })
                                .Where(x => x.LogActivities.Email == x.Employees.User.Email)
                                .ToListAsync();
            if (getData.Count == 0)
            {
                return null;
            }
            List<Log> list = new List<Log>();
            foreach (var item in getData)
            {
                var log = new Log()
                {
                    Id = item.LogActivities.Id,
                    Response = item.LogActivities.Response,
                    Email = item.Employees.Name,
                    CreateDate = item.LogActivities.CreateDate,
                };
                list.Add(log);
            }
            return list;
        }

        [HttpPost]
        public IActionResult Create(LogVM logVM)
        {
            if (ModelState.IsValid)
            {
                var log = new Log
                {
                    Response = logVM.Response,
                    Email = logVM.Email,
                    CreateDate = DateTimeOffset.Now
                };
                _context.LogActivities.Add(log);
                _context.SaveChanges();
                return Ok("Successfully Created");
            }
            return BadRequest("Not Successfully");
        }
                
    }
}
