﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppleUsed.Web.Controllers
{
    public class ChatController : Controller
    {
        private IConversationService _conversationService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(IConversationService conversationService, UserManager<ApplicationUser> userManager)
        {
            _conversationService = conversationService;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult Index()
        {
            string userId = _userManager.GetUserId(User);
            //var conv = _conversationService.get(userId);
            return View();
        }

        [HttpGet]
        public IActionResult ConversationByAdIdAndContactId(int adId, string contactId)
        {
            string userId = _userManager.GetUserId(User);
            ViewBag.SenderId = userId;
            ViewBag.RecivedId = contactId;
            ViewBag.AdId = adId;
            return View();
        }

        public async Task<JsonResult> GetConversationByAdIdAndSenderId(int adId, string contactId)
        {
            string senderId = _userManager.GetUserId(User);
            var conversation = await _conversationService.GetConversationByAdIdAndSenderIdAndContactId(adId, senderId, contactId);
            ViewBag.ConversationId = conversation.ConversationId;
            return Json(new { status = "success", data = conversation });
        }

        //[HttpGet]
        //public async Task<JsonResult> GetAllConversationsWithByAdId(int adId, string contactId)
        //{
        //    string userId = _userManager.GetUserId(User);
        //    var conversations = await _conversationService.GetAllConversationByAdId(adId, contactId);
        //    return Json(new { status = "success", data = conversations });
        //}

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