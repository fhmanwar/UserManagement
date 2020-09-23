using Bcrypt = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using API.Context;
using API.Models;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using API.Repository.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        AttrEmail attrEmail = new AttrEmail();
        RandomDigit randDig = new RandomDigit();
        SmtpClient client = new SmtpClient();
        readonly MyContext _context;
        public IConfiguration _configuration;
        UserRepository _repo;

        public AuthsController(MyContext myContext, IConfiguration config, UserRepository repo)
        {
            _context = myContext;
            _configuration = config;
            _repo = repo;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserVM userVM)
        {
            var getUser = _context.Users.Where(x => x.Email == userVM.Email);
            if (getUser.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    var code = randDig.GenerateRandom();
                    var fill = "Hi " + userVM.Name + "\n\n"
                              + "Please verifty Code for this Apps : \n"
                              + code
                              + "\n\nThank You";

                    MailMessage mm = new MailMessage("donotreply@domain.com", userVM.Email, "Register Email", fill);
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    string str1 = "gmail.com";
                    string str2 = attrEmail.mail;

                    if (str2.Contains(str1))
                    {
                        try
                        {
                            client.Port = 587;
                            client.Host = "smtp.gmail.com";
                            client.EnableSsl = true;
                            client.Timeout = 10000;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Credentials = new NetworkCredential(attrEmail.mail, attrEmail.pass);
                            client.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest("SMTP Gmail Error " + ex);
                        }
                    }
                    else
                    {
                        try
                        {
                            client.Port = 25;
                            client.Credentials = new NetworkCredential(attrEmail.mail, attrEmail.pass);
                            client.EnableSsl = false;
                            client.Send(mm);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest("SMTP Email Error " + ex);
                        }
                    }

                    var user = new UserVM
                    {
                        Email = userVM.Email,
                        Password = userVM.Password,
                        VerifyCode = code,
                    };
                    var create = _repo.Create(user);
                    if (create > 0)
                    {
                        var getUserId = getUser.SingleOrDefault();
                        var checkRole = _context.Roles.SingleOrDefault(x => x.Name == "Employee");
                        var uRole = new UserRole
                        {
                            UserId = getUserId.Id,
                            RoleId = checkRole.Id
                        };
                        _context.UserRole.Add(uRole);
                        var emp = new Employee
                        {
                            UserId = getUserId.Id,
                            Name = userVM.Name,
                            NIK = userVM.NIK,
                            AssignmentSite = userVM.Site,
                            Phone = userVM.Phone,
                            Address = userVM.Address,
                            CreateDate = DateTimeOffset.Now,
                            isDelete = false
                        };
                        _context.Employees.Add(emp);
                        _context.SaveChanges();

                        Sendlog(userVM.Email + " Register Successfully", userVM.Email);
                        return Ok("Successfully Created");
                    }
                    return BadRequest("Register Not Successfully");
                }
                return BadRequest("Not Successfully");
            }
            return BadRequest("Email Already Exists ");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var getData = _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).SingleOrDefault(x => x.User.Email == userVM.Email);
                if (getData == null)
                {
                    return NotFound("Email Not Found");
                }
                else if (userVM.Password == null || userVM.Password.Equals(""))
                {
                    return BadRequest("Password must filled");
                }
                else if (!Bcrypt.Verify(userVM.Password, getData.User.Password))
                {
                    return BadRequest("Password is Wrong");
                }
                else
                {
                    if (getData != null)
                    {
                        var user = new UserVM()
                        {
                            Id = getData.User.Id,
                            Name = getData.User.Employee.Name,
                            NIK = getData.User.Employee.NIK,
                            Site = getData.User.Employee.AssignmentSite,
                            Email = getData.User.Email,
                            Password = getData.User.Password,
                            Phone = getData.User.Employee.Phone,
                            Address = getData.User.Employee.Address,
                            RoleID = getData.Role.Id,
                            RoleName = getData.Role.Name,
                            VerifyCode = getData.User.VerifyCode,
                        };
                        Sendlog(userVM.Email + " Login Successfully", userVM.Email);
                        return Ok(GetJWT(user));
                    }
                    return BadRequest("Invalid credentials");
                }
            }
            return BadRequest("Data Not Valid");
        }

        [HttpPost]
        [Route("code")]
        public IActionResult VerifyCode(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var getData = _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).SingleOrDefault(x => x.User.Email == userVM.Email);
                if (getData == null)
                {
                    return NotFound();
                }
                else if (userVM.VerifyCode != getData.User.VerifyCode)
                {
                    return BadRequest("Your Code is Wrong");
                }
                else
                {
                    getData.User.VerifyCode = null;
                    _context.SaveChanges();
                    var user = new UserVM()
                    {
                        Id = getData.User.Id,
                        Name = getData.User.Employee.Name,
                        Email = getData.User.Email,
                        Password = getData.User.Password,
                        Phone = getData.User.Employee.Phone,
                        RoleID = getData.Role.Id,
                        RoleName = getData.Role.Name,
                        VerifyCode = getData.User.VerifyCode,
                    };
                    Sendlog(userVM.Email + " Verify Code Successfully", userVM.Email);
                    return StatusCode(200, GetJWT(user));
                }
            }
            return BadRequest("Data Not Valid");
        }

        private string GetJWT(UserVM userVM)
        {
            var claims = new List<Claim> {
                            new Claim("Id", userVM.Id),
                            new Claim("RoleName", userVM.RoleName),
                            new Claim("VerifyCode", userVM.VerifyCode == null ? "" : userVM.VerifyCode),
                        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(1),
                            signingCredentials: signIn
                        );
            return new JwtSecurityTokenHandler().WriteToken(token);
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
}
