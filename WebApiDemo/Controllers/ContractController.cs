using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    //Bazı apilerde sonuç json formatından farklı olabilir. Bu durumda default olarak veriler json formatında gelir
    //İnputFormatter ve OutputFormetter ile input değerini de istediğimiz formatta alabilir 
    //output değerini de istediğimiz formatta gönderebiliriz.

    [Route("api/{contrroller}")]
    public class ContractController : Controller
    {
        [HttpGet("")]
        [Authorize(Roles ="Admin")]
        public List<ContractModel> Get()
        {
            return new List<ContractModel>
            {
                new ContractModel{ Id = 1, FirstName="Fatih", LastName="Aksöz"}
            };
        }
    }
}