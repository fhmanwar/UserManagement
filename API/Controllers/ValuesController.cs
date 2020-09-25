using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost]
        [Route("Test")]
        public IActionResult Test(ForgotVM forgotVM)
        {
            var txt = "Test";
            byte[] encodedBytes = Encoding.Unicode.GetBytes(txt);
            string encodedTxt = Convert.ToBase64String(encodedBytes);

            byte[] decodedBytes = Convert.FromBase64String(Convert.ToBase64String(Encoding.Unicode.GetBytes(txt)));
            string decodedTxt = Encoding.UTF8.GetString(decodedBytes);
            string decodedTxt2 = Encoding.Unicode.GetString(decodedBytes);

            var obj = new
            {
                Id = "202020",
                Name = "Joni bro",
                NIK = 212321,
            };
            var encode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(forgotVM)));
            var getdecode = WebEncoders.Base64UrlDecode(encode);
            var getString = Encoding.UTF8.GetString(getdecode);
            var decode = JsonConvert.DeserializeObject(getString);
            return Ok(new { 
                encByte = encodedBytes,  
                encTxt = encodedTxt,
                decByte = decodedBytes,
                dectxt = decodedTxt,
                dectxt2 = decodedTxt2,
                tesEnc = encode,
                tesDec = decode,
            });
        }
    }
}
