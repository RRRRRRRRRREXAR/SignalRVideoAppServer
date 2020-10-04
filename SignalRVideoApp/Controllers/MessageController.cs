using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRVideoApp.Models;


namespace SignalRVideoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private static readonly Message[] Messages = new Message[]
        {
            new Message
            {
                text = "text 1",
                author = "Author 1",
                date = new DateTime(2020,09,11,2,2,2)
            },
            new Message
            {
                text = "text 2",
                author = "Author 1",
                date = new DateTime(2020,09,11,2,3,2)
            },
            new Message
            {
                text = "text 3",
                author = "Author 1",
                date = new DateTime(2020,09,11,2,4,2)
            },
            new Message
            {
                text = "text 4",
                author = "Author 2",
                date = new DateTime(2020,09,11,2,5,2)
            }
        };
       
        [HttpGet]
        public IEnumerable<Message> Get()
        {
            return Messages;
        }
        
    }
}
