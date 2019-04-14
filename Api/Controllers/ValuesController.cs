using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var list = HttpContext.User.Claims.Select(a => $"{a.Type}:{a.Value.ToString()}").ToList();
            list.Add("---------------------------");
//            list.AddRange((await HttpContext.AuthenticateAsync())?.Properties.Items.Select(a => $"{a.Key}:{a.Value}")
//                .ToList());
            
            list.Add(accessToken);
            
            return list.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<string> Get(long id)
        {
            return id.ToString();
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
    }
}
