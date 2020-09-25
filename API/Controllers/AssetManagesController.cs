using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using API.Context;
using API.Repository.Data;
using API.Services;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AssetManagesController : ControllerBase
    {
        AttrEmail attrEmail = new AttrEmail();
        RandomDigit randDig = new RandomDigit();
        SmtpClient client = new SmtpClient();
        BaseURL baseURL = new BaseURL();
        readonly MyContext _context;
        public IConfiguration _configuration;
        UserRepository _repo;
        AssetManageRepository _repoAsset;

        public AssetManagesController(MyContext myContext, IConfiguration config, UserRepository repo, AssetManageRepository repoAsset)
        {
            _context = myContext;
            _configuration = config;
            _repo = repo;
            _repoAsset = repoAsset;
        }

        [HttpGet]
        public async Task<IEnumerable<AssetVM>> GetAll()
        {
            return await _repoAsset.getAll();
        }

        [HttpGet("{id}")]
        public AssetVM GetId(string id)
        {
            return _repoAsset.getID(id);
        }

        [HttpPost]
        [Route("Forgot")]
        public async Task<IActionResult> Forgot(AssetForgotVM assetForgotVM)
        {
            var getUser = _context.Users.Include("Employee").Where(x => x.Email == assetForgotVM.Email);
            var cekCount = getUser.Count();
            if (cekCount != 0)
            {
                if (ModelState.IsValid)
                {
                    var getUserId = await getUser.SingleOrDefaultAsync();
                    var code = randDig.GenerateRandom();

                    var encode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(assetForgotVM)));
                    var link = baseURL.assetM + "reset?token=" + encode;

                    var fill = "Hi " + getUserId.Employee.Name + "\n\n"
                              + "Click this link for Reset Password : \n"
                              + "<a href=" + link + ">Reset Password</a>"
                              + "\n\nThank You";

                    MailMessage mm = new MailMessage("donotreply@domain.com", assetForgotVM.Email, "Forgot Password ", fill);
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
                        Email = assetForgotVM.Email,
                        Password = null,
                        Token = encode,
                    };
                    var create = _repo.Update(user, getUserId.Id);
                    if (create > 0)
                    {
                        Sendlog(assetForgotVM.Email + " send link to email Successfully", assetForgotVM.Email);
                        return Ok("Please check your email");
                    }
                }
                return BadRequest("Not Successfully");
            }
            return BadRequest("Email Doesn't Exists ");
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
