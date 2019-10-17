using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public string List()
        {

            return "";
        }
        [HttpPost]
        public string Login(string nome,string senha)
        {

            return "";
        }

    }
}
