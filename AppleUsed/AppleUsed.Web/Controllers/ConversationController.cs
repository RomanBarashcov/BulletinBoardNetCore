using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ConversationController : Controller
    {
        private IConversationService _conversationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ConversationController(IConversationService conversationService, UserManager<ApplicationUser> userManager)
        {
            _conversationService = conversationService;
            _userManager = userManager;
        }

        [HttpGet]
        public JsonResult ByAdId(int adId, string contactId)
        {
            string userId = _userManager.GetUserId(User);
            var conversations = _conversationService.GetConversationByAdIdAndSenderIdAndContactId(adId, userId, contactId);
            return Json(new { status = "success", data = conversations });
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                _conversationService = null;
                disposed = true;
            }
        }
    }
}