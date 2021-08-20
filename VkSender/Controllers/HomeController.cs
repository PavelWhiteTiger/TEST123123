using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkSender.Models;
using VkSender.Services;

namespace VkSender.Controllers
{
    [Route("[controller]/[action]")]
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IAccountWrapper _account;

        public MessageController(ILogger<MessageController> logger, IAccountWrapper account)
        {
            _logger = logger;
            _account = account;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(MessageRequest request)
        {
            _account.PostMessage(request.LinkToPerson, request.Message);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
