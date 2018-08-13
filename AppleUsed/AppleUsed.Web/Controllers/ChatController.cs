using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using AppleUsed.Web.Models.ViewModels;
using AppleUsed.Web.Models.ViewModels.ConversationViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly IConversationService _conversationService;
        private readonly IAdService _adService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(IConversationService conversationService, IAdService adService, UserManager<ApplicationUser> userManager)
        {
            _conversationService = conversationService;
            _userManager = userManager;
            _adService = adService;
        }

        //[HttpGet]
        //public ActionResult Index()
        //{
        //    string userId = _userManager.GetUserId(User);
        //    var conv = _conversationService.get(userId);
        //    return View();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetAllConversationsByAdId(int id)
        //{
        //    string userId = _userManager.GetUserId(User);
        //    var conversations = await _conversationService.GetAllConversationByAdId(id);
        //    return PartialView("GetAllConversationsByAdId", conversations);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            ConversationListViewModel model = new ConversationListViewModel();
            var user = await _userManager.GetUserAsync(User);
            model.Conversations = await _conversationService.GetAllConverastionBySenderId(user.Id);
            model.UserId = user.Id;
            return View("Conversations", model);
        }


        [HttpGet]
        public async Task<IActionResult> ConversationByAdIdAndContactId(int adId, string contactId)
        {
            ConversationIndexViewModel model = new ConversationIndexViewModel();
            string userId = _userManager.GetUserId(User);
            model.Conversation = await _conversationService.GetConversationByAdIdAndSenderIdAndContactId(adId, userId, contactId);
            
            ViewBag.SenderId = userId;
            ViewBag.RecivedId = contactId;
            ViewBag.AdId = adId;

            var getAdByIdResult = await _adService.GetAdById(adId);
            if (!getAdByIdResult.Succedeed)
                return View("Conversations", model);

            model.Ad = getAdByIdResult.Property;
            return View("Conversations", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetConversation(int adId, string contactId)
        {
            string senderId = _userManager.GetUserId(User);
            var conversation = await _conversationService.GetConversationByAdIdAndSenderIdAndContactId(adId, senderId, contactId);
            ViewBag.ConversationId = conversation.ConversationId;
            return Json(new { status = "success", data = conversation });
        }

        [HttpPost]
        public async Task<JsonResult> SendMessage(int convsationId, int adId, string message, string contactId)
        {
            string userId = _userManager.GetUserId(User);
            var conversation = await _conversationService.SaveMessageToConversation(convsationId, adId , message, userId, contactId);
            ViewBag.ConversationId = conversation.ConversationId;
            return Json(conversation);
        }

        [HttpPost]
        public JsonResult MessageDelivered(int message_id)
        {
            var convo = _conversationService.ChangingMessageStatusToDelivered(message_id);
            return Json(convo);
        }

        private String getConvoChannel(int user_id, int contact_id)
        {
            if (user_id > contact_id)
            {
                return "private-chat-" + contact_id + "-" + user_id;
            }
            return "private-chat-" + user_id + "-" + contact_id;
        }
    }
}