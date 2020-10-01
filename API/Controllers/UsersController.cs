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
        public async Task<IEnumerable<GetRoleVM>> GetAll() => await _repo.getAll();

        [HttpGet("{id}")]
        public GetRoleVM GetID(string id) => _repo.getID(id);

        [HttpPost]
        public IActionResult Create(GetRoleVM dataVM)
        {
            if (ModelState.IsValid)
            {
                if (dataVM.Session == null)
                {
                    return BadRequest("Session ID must be filled");
                }
                var getSession = _context.Users.SingleOrDefault(x => x.Id == dataVM.Session);
                if (getSession != null)
                {
                    var role = new RoleVM
                    {
                        Name = dataVM.Name
                    };
                    var create = _repo.Create(role);
                    if (create > 0)
                    {
                        Sendlog(getSession.Email + " Create Role Successfully", getSession.Email);

                        return Ok("Successfully Created");
                    }
                    return BadRequest("Not Successfully");
                }
                return BadRequest("You Don't Have Access");

            }
            return BadRequest("Not Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, GetRoleVM dataVM)
        {
            if (ModelState.IsValid)
            {
                if (dataVM.Session == null)
                {
                    return BadRequest("Session ID must be filled");
                }
                var getSession = _context.Users.SingleOrDefault(x => x.Id == dataVM.Session);
                if (getSession != null)
                {
                    var getData = _context.Roles.SingleOrDefault(x => x.Id == id);
                    getData.Name = dataVM.Name;
                    getData.UpdateDate = DateTimeOffset.Now;

                    _context.Roles.Update(getData);
                    _context.SaveChanges();

                    Sendlog(getSession.Email + " Update Role Successfully", getSession.Email);
                    return Ok("Successfully Updated");
                }
                return BadRequest("You Don't Have Access");
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
        public async Task<IEnumerable<GetUserVM>> GetAll() => await _repo.getAll();

        [HttpGet("{id}")]
        public GetUserVM GetID(string id) => _repo.getID(id);

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
                    var getSession = _context.Users.SingleOrDefault(x => x.Id == getUserVM.Session);
                    if (getSession != null)
                    {
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
                                ProfileImage = getUserVM.ProfileImages,
                                Address = getUserVM.Address,
                                Province = getUserVM.Province,
                                City = getUserVM.City,
                                SubDistrict = getUserVM.SubDistrict,
                                Village = getUserVM.Village,
                                ZipCode = getUserVM.ZipCode,
                                DepartmentId = getUserVM.DepartmentID,
                                CreateDate = DateTimeOffset.Now,
                                isDelete = false
                            };
                            _context.Employees.Add(emp);
                            _context.SaveChanges();

                            Sendlog(getSession.Email + " Create User Successfully", getSession.Email);

                            return Ok("Successfully Created");
                        }
                        return BadRequest("Input User Not Successfully");
                    }
                    return BadRequest("You Don't Have access");
                }
                return BadRequest("Not Successfully");
            }
            return BadRequest("Email Already Exists ");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, GetUserVM dataVM)
        {
            if (ModelState.IsValid)
            {
                if (dataVM.Session == null)
                {
                    return BadRequest("Session ID must be filled");
                }
                var getSession = _context.Users.SingleOrDefault(x => x.Id == dataVM.Session);
                if (getSession != null)
                {
                    var getData = _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).SingleOrDefault(x => x.UserId == id);
                    getData.User.Employee.Name = dataVM.Name;
                    getData.User.Employee.NIK = dataVM.NIK;
                    getData.User.Employee.AssignmentSite = dataVM.Site;
                    getData.User.Employee.Phone = dataVM.Phone;
                    getData.User.Employee.ProfileImage = dataVM.ProfileImages;
                    getData.User.Employee.Address = dataVM.Address;
                    getData.User.Employee.Province = dataVM.Province;
                    getData.User.Employee.City = dataVM.City;
                    getData.User.Employee.SubDistrict = dataVM.SubDistrict;
                    getData.User.Employee.Village = dataVM.Village;
                    getData.User.Employee.ZipCode = dataVM.ZipCode;
                    getData.User.Employee.DepartmentId = dataVM.DepartmentID;
                    getData.User.Email = dataVM.Email;
                    if (dataVM.Password != null)
                    {
                        if (!Bcrypt.Verify(dataVM.Password, getData.User.Password))
                        {
                            getData.User.Password = Bcrypt.HashPassword(dataVM.Password);
                        }
                    }
                    if (dataVM.RoleName != null)
                    {
                        var getRoleID = _context.Roles.SingleOrDefault(x => x.Name == dataVM.RoleName);
                        getData.RoleId = getRoleID.Id;
                    }
                    _context.UserRole.Update(getData);
                    _context.SaveChanges();

                
                    Sendlog(getSession.Email + " Update User Successfully", getSession.Email);

                    return Ok("Successfully Updated");
                }
                return BadRequest("You Don't Have access");
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
    public class AbsentsController : ControllerBase
    {
        AbsentRepository _repo;
        public AbsentsController(AbsentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<GetAbsentVM>> GetAll() => await _repo.getAll();

        [HttpGet("{id}")]
        public GetAbsentVM GetID(int id) => _repo.getID(id);

        [HttpPost]
        public IActionResult Create(GetAbsentVM dataVM)
        {
            if (ModelState.IsValid)
            {
                var data = new AbsentVM
                {
                    UserId = dataVM.UserId,
                    InsDate = dataVM.InsAt
                };
                var create = _repo.Create(data);
                if (create > 0)
                {
                    return Ok("Successfully Created");
                }
                return BadRequest("Not Successfully");

            }
            return BadRequest("Not Successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, GetAbsentVM dataVM)
        {
            if (ModelState.IsValid)
            {
                var data = new AbsentVM
                {
                    UpdDate = dataVM.UpdAt
                };
                var update = _repo.Update(data, id);
                if (update > 0)
                {
                    return Ok("Successfully Created");
                }
                return BadRequest("Not Successfully");
            }
            return BadRequest("Not Successfully");
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
                                .OrderByDescending(x => x.LogActivities.Id)
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
