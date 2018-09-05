using System;
using System.Web.Http;

namespace WebApi
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("valueGet")]
        public IHttpActionResult Get()
        {
            return Ok("Get Good" + DateTime.UtcNow.ToString());
        }

        [HttpPost]
        [Route("valuePost/{id}")]
        public IHttpActionResult Post(int id)
        {
            return Ok("Post Good");
        }
    }
}