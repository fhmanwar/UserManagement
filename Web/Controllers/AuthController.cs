using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AuthController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/auths/")
        };

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/login");
        }

        [Route("forgot")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [Route("reset")]
        public IActionResult ResetPassword(string token)
        {
            if (token != null)
            {
                return View();
            }
            return Redirect("/");
        }

        [Route("verify")]
        public IActionResult Verify()
        {
            return View();
        }

        [Route("notfound")]
        public IActionResult Notfound()
        {
            return View("~/Views/Auth/Notfound.cshtml");
        }
                
        [Route("validate")]
        public IActionResult Validate(UserVM userVM)
        {
            var json = JsonConvert.SerializeObject(userVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (userVM.Name == null)
            {
                HttpResponseMessage result = null;
                if (userVM.VerifyCode != null)
                { // Verify Code
                    result = client.PostAsync("code/", byteContent).Result;
                    SendLogs(userVM.Email + " VerifyCode Successfully", userVM.Email);
                }
                else if (userVM.VerifyCode == null)
                { // Login
                    result = client.PostAsync("login/", byteContent).Result;
                }

                if (result.IsSuccessStatusCode)
                {
                    var data = result.Content.ReadAsStringAsync().Result;
                    if (data != null)
                    {
                        HttpContext.Session.SetString("token", "Bearer " + data);
                        var handler = new JwtSecurityTokenHandler();
                        var tokenS = handler.ReadJwtToken(data);
                        var jwtPayloadSer = JsonConvert.SerializeObject(tokenS.Payload.ToDictionary(x => x.Key, x => x.Value));
                        var jwtPayloadDes = JsonConvert.DeserializeObject(jwtPayloadSer).ToString();
                        var account = JsonConvert.DeserializeObject<UserVM>(jwtPayloadSer);

                        if (!account.VerifyCode.Equals(""))
                        {
                            return Json(new { status = true, msg = "VerifyCode" });
                        }
                        else if (account.RoleName != null)
                        {
                            JsonResult obj = (JsonResult)GetById(account.Id);
                            var objSer = JsonConvert.SerializeObject(obj.Value);
                            var getUserData = JsonConvert.DeserializeObject<GetUserVM>(objSer);
                            if (getUserData.ProfileImages != null)
                            {
                                HttpContext.Session.SetString("img", getUserData.ProfileImages);
                            }
                            //else
                            //{
                            //    HttpContext.Session.SetString("img", "/images/default.png");
                            //}

                            HttpContext.Session.SetString("id", account.Id);
                            HttpContext.Session.SetString("name", account.Name);
                            HttpContext.Session.SetString("email", account.Email);
                            HttpContext.Session.SetString("lvl", account.RoleName);
                            SendLogs(userVM.Email + " Login Successfully", userVM.Email);
                            if (account.RoleName == "Super Admin")
                            {
                                return Json(new { status = true, msg = "Login Successfully !" });
                            }
                            return Json(new { status = true, msg = "Login Successfully !" });
                        }
                        return Json(new { status = false, msg = "You Don't Have Permissions! Please Contact Administrator" });
                    }
                    return Json(new { status = false, msg = result.Content.ReadAsStringAsync().Result });
                }
                return Json(new { status = false, msg = result.Content.ReadAsStringAsync().Result });
            }
            else if (userVM.Name != null)
            { // Register
                var result = client.PostAsync("register/", byteContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    SendLogs(userVM.Email + " Register", userVM.Email);
                    return Json(new { status = true, code = result, msg = "Register Success! " });
                }
                return Json(new { status = false, msg = result.Content.ReadAsStringAsync().Result });
            }
            return Redirect("/login");
        }

        [Route("changePass")]
        public IActionResult ChangePassword(ForgotVM forgotVM)
        {
            var json = JsonConvert.SerializeObject(forgotVM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage result = null;
            if (forgotVM.Token == null)
            {
                result = client.PostAsync("forgot/", byteContent).Result;
                if (result.IsSuccessStatusCode)
                {
                    SendLogs(forgotVM.Email + " Forgot Password", forgotVM.Email);
                    return Json(result);
                }
                var getdata = result.Content.ReadAsStringAsync().Result;
                return Json(new { result, msg = getdata });
            }
            else if (forgotVM.Token != null)
            {
                result = client.PostAsync("reset?token="+ forgotVM.Token, byteContent).Result;
                if (result.IsSuccessStatusCode)
                {

                    SendLogs(HttpContext.Session.GetString("email") + " Forgot Password", HttpContext.Session.GetString("email"));
                    return Json(result);
                }
                var getdata = result.Content.ReadAsStringAsync().Result;
                return Json(new { result, msg = getdata });
            }
            return Redirect("/login");
        }


        readonly HttpClient Userclient = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
        };

        public IActionResult GetById(string Id)
        {
            GetUserVM data = null;
            Userclient.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = Userclient.GetAsync("users/" + Id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<GetUserVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error.");
            }
            return Json(data);
        }

        public IActionResult SendLogs(string response, string mail)
        {
            var log = new LogVM
            {
                Response = response,
                Email = mail,
            };
            var jsonLog = JsonConvert.SerializeObject(log);
            var bufferLog = System.Text.Encoding.UTF8.GetBytes(jsonLog);
            var byteContentLog = new ByteArrayContent(bufferLog);
            byteContentLog.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var sendLog = client.PostAsync("logs/", byteContentLog).Result;
            return Json(sendLog);
        }

    }
}
