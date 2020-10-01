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
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        AttrEmail attrEmail = new AttrEmail();
        RandomDigit randDig = new RandomDigit();
        SmtpClient client = new SmtpClient();
        BaseURL baseURL = new BaseURL();
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
        [Route("Login")]
        public IActionResult Login(UserVM userVM)
        {
            if (ModelState.IsValid)
            {
                var getData = _context.UserRole.Include("Role").Include("User").Include(x => x.User.Employee).Include(x => x.User.Employee.Department).SingleOrDefault(x => x.User.Email == userVM.Email);
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
                            Email = getData.User.Email,
                            RoleName = getData.Role.Name,
                            DepartmentName = getData.User.Employee.Department.Name,
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
        [Route("Forgot")]
        public async Task<IActionResult> Forgot(ForgotVM forgotVM)
        {
            var getUser = _context.Users.Include("Employee").Where(x => x.Email == forgotVM.Email);
            var cekCount = getUser.Count();
            if (cekCount != 0)
            {
                if (ModelState.IsValid)
                {
                    var getUserId = await getUser.SingleOrDefaultAsync();
                    var code = randDig.GenerateRandom();

                    //var user = await _userManager.FindByEmailAsync(forgotVM.Email);
                    //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    //var callback = Url.Action(nameof(ResetPassword), nameof(AccountController), new { token, email = user.Email }, Request.Scheme);

                    var encode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(forgotVM)));
                    //var link = Url.Action("ResetPassword", "Auth", new { email = forgotVM.Email, encode }, Request.Scheme);
                    //var link =  "<a href='" + Url.Action("ResetPassword", "Auth", new { email = forgotVM.Email, encode }, "http") + "'>Reset Password</a>";
                    var link = baseURL.UsrManage + "reset?token="+encode;

                    var fill = "Hi " + getUserId.Employee.Name + "\n\n"
                              + "Click this link for Reset Password : \n"
                              + "<a href=" + link + ">Reset Password</a>"
                              + "\n\nThank You";

                    MailMessage mm = new MailMessage("donotreply@domain.com", forgotVM.Email, "Forgot Password ", fill);
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
                    else if (!str2.Contains(str1))
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
                        Email = forgotVM.Email,
                        Password = null,
                        Token = encode,
                    };
                    var create = _repo.Update(user, getUserId.Id);
                    if (create > 0)
                    {
                        Sendlog(forgotVM.Email + " send link to email Successfully", forgotVM.Email);
                        return Ok("Please check your email");
                    }
                }
                return BadRequest("Not Successfully");
            }
            return BadRequest("Email Doesn't Exists ");
        }

        [HttpPost]
        [Route("Reset")]
        public async Task<IActionResult> Reset(string token, ForgotVM forgotVM)
        {
            var getToken = _context.Users.Where(x => x.Token == token);
            var tokenCount = getToken.Count();
            if (tokenCount > 0)
            {
                var getdecode = WebEncoders.Base64UrlDecode(token);
                var getString = Encoding.UTF8.GetString(getdecode);
                var getDObj = JsonConvert.DeserializeObject<ForgotVM>(getString);
                var decode = JsonConvert.DeserializeObject<ForgotVM>(Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token)));
                var getUser = _context.Users.Include("Employee").Where(x => x.Email == decode.Email);
                var cekCount = getUser.Count();
                if (cekCount == 1)
                {
                    if (ModelState.IsValid)
                    {
                        var getUserId = await getUser.SingleOrDefaultAsync();
                    
                        var user = new UserVM
                        {
                            Email = decode.Email,
                            Password = Bcrypt.HashPassword(forgotVM.Password),
                            Token = null,
                        };
                        var create = _repo.Update(user,getUserId.Id);
                        if (create > 0)
                        {
                            Sendlog(decode.Email + " Reset Password", forgotVM.Email);
                            return Ok("Reset Password Successfully");
                        }
                        return BadRequest("Reset Password Not Successfully");
                    }
                    return BadRequest("Something wrong");
                }
                return BadRequest("Email Doesn't Exists ");
            }
            return BadRequest("Token Doesn't Exists ");
        }

        private string GetJWT(UserVM dataVM)
        {
            var claims = new List<Claim> {
                            new Claim("Id", dataVM.Id),
                            new Claim("Name", dataVM.Name),
                            new Claim("Email", dataVM.Email),
                            new Claim("RoleName", dataVM.RoleName),
                            new Claim("DepartmentName", dataVM.DepartmentName),
                            new Claim("VerifyCode", dataVM.VerifyCode == null ? "" : dataVM.VerifyCode),
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
                        RoleName = getData.Role.Name,
                        VerifyCode = getData.User.VerifyCode,
                    };
                    Sendlog(userVM.Email + " Verify Code Successfully", userVM.Email);
                    return StatusCode(200, GetJWT(user));
                }
            }
            return BadRequest("Data Not Valid");
        }

        //[HttpPost]
        //[Route("myAksesIboyRegister")]
        //public IActionResult Register(UserVM userVM)
        //{
        //    var getUser = _context.Users.Where(x => x.Email == userVM.Email);
        //    if (getUser.Count() == 0)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var code = randDig.GenerateRandom();
        //            var fill = "Hi " + userVM.Name + "\n\n"
        //                      + "Please verifty Code for this Apps : \n"
        //                      + code
        //                      + "\n\nThank You";

        //            MailMessage mm = new MailMessage("donotreply@domain.com", userVM.Email, "Register Email", fill);
        //            mm.BodyEncoding = UTF8Encoding.UTF8;
        //            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //            string str1 = "gmail.com";
        //            string str2 = attrEmail.mail;

        //            if (str2.Contains(str1))
        //            {
        //                try
        //                {
        //                    client.Port = 587;
        //                    client.Host = "smtp.gmail.com";
        //                    client.EnableSsl = true;
        //                    client.Timeout = 10000;
        //                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                    client.UseDefaultCredentials = false;
        //                    client.Credentials = new NetworkCredential(attrEmail.mail, attrEmail.pass);
        //                    client.Send(mm);
        //                }
        //                catch (Exception ex)
        //                {
        //                    return BadRequest("SMTP Gmail Error " + ex);
        //                }
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    client.Port = 25;
        //                    client.Credentials = new NetworkCredential(attrEmail.mail, attrEmail.pass);
        //                    client.EnableSsl = false;
        //                    client.Send(mm);
        //                }
        //                catch (Exception ex)
        //                {
        //                    return BadRequest("SMTP Email Error " + ex);
        //                }
        //            }

        //            var user = new UserVM
        //            {
        //                Email = userVM.Email,
        //                Password = userVM.Password,
        //                VerifyCode = code,
        //            };
        //            var create = _repo.Create(user);
        //            if (create > 0)
        //            {
        //                var getUserId = getUser.SingleOrDefault();
        //                var checkRole = _context.Roles.SingleOrDefault(x => x.Name == "Employee");
        //                var uRole = new UserRole
        //                {
        //                    UserId = getUserId.Id,
        //                    RoleId = checkRole.Id
        //                };
        //                _context.UserRole.Add(uRole);
        //                var emp = new Employee
        //                {
        //                    UserId = getUserId.Id,
        //                    Name = userVM.Name,
        //                    NIK = userVM.NIK,
        //                    AssignmentSite = userVM.Site,
        //                    Phone = userVM.Phone,
        //                    Address = userVM.Address,
        //                    CreateDate = DateTimeOffset.Now,
        //                    isDelete = false
        //                };
        //                _context.Employees.Add(emp);
        //                _context.SaveChanges();

        //                Sendlog(userVM.Email + " Register Successfully", userVM.Email);
        //                return Ok("Successfully Created");
        //            }
        //            return BadRequest("Register Not Successfully");
        //        }
        //        return BadRequest("Not Successfully");
        //    }
        //    return BadRequest("Email Already Exists ");
        //}

    }
}
