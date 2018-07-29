using AppleUsed.BLL.DTO;
using AppleUsed.BLL.Interfaces;
using AppleUsed.DAL.Entities;
using AppleUsed.DAL.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Services
{
    public class ConversationService : IConversationService, IDisposable
    {
        private AppDbContext _db;

        public ConversationService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<List<ConversationDTO>> GetAllConversationByUserId(string userId)
        {
            List<ConversationDTO> conversations = new List<ConversationDTO>();
            var user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

            if (user != null)
            {
                conversations = await _db.Conversations.Where(x => x.SenderId == user.Id).Select(x => new ConversationDTO
                {
                    ConversationId = x.ConversationId,
                    SenderId = x.SenderId,
                    ReceiverId = x.ReceiverId,
                    Message = x.Message,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt

                }).ToListAsync();
            }

            return conversations;
        }

        public async Task<List<ConversationDTO>> GetConversationByContact(string userId, string contactId)
        {
            List <ConversationDTO> conversations = new List<ConversationDTO>();

            var user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

            conversations = await _db.Conversations.
                                 Where(c => (c.ReceiverId == user.Id && c.SenderId == contactId) || 
                                 (c.ReceiverId == contactId && c.SenderId == user.Id))
                                 .OrderBy(c => c.CreatedAt)
                                 .Select(x => new ConversationDTO
                                 {
                                    ConversationId = x.ConversationId,
                                    SenderId = x.SenderId,
                                    ReceiverId = x.ReceiverId,
                                    Message = x.Message,
                                    Status = x.Status,
                                    CreatedAt = x.CreatedAt

                                 }).ToListAsync();

            return conversations;
        }


        public async Task<ConversationDTO> SaveMessageToConversation(string message, string userId, string contactId)
        {
            Conversation conversation = new Conversation();
            ConversationDTO conversationForReturn = new ConversationDTO();

            if (!String.IsNullOrEmpty(message))
            {
                var user = _db.Users.Where(x => x.Id == userId).FirstOrDefault();

                conversation.SenderId = user.Id;
                conversation.Message = message;
                conversation.ReceiverId = contactId;
                conversation.CreatedAt = DateTime.Now.Date;

                await _db.AddAsync(conversation);
                await _db.SaveChangesAsync();

                conversationForReturn.ConversationId = conversation.ConversationId;
                conversationForReturn.SenderId = conversation.SenderId;
                conversationForReturn.Message = conversation.Message;
                conversationForReturn.ReceiverId = conversation.ReceiverId;
                conversationForReturn.Status = conversation.Status;
                conversationForReturn.CreatedAt = conversation.CreatedAt;
            }
            
            return conversationForReturn;
        }

        public async Task<ConversationDTO> ChangingMessageStatusToDelivered(int conversationId)
        {
            ConversationDTO conversationForReturn = new ConversationDTO();

            if (conversationId > 0)
            {
                var conv = _db.Conversations.FirstOrDefault(c => c.ConversationId == conversationId);
                if (conv != null)
                {
                    conversationForReturn.Status = Conversation.messageStatus.Delivered;
                    _db.Entry(conv).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    conversationForReturn.ConversationId = conv.ConversationId;
                    conversationForReturn.SenderId = conv.SenderId;
                    conversationForReturn.Message = conv.Message;
                    conversationForReturn.ReceiverId = conv.ReceiverId;
                    conversationForReturn.Status = conv.Status;
                    conversationForReturn.CreatedAt = conv.CreatedAt;
                }
            }

            return conversationForReturn;
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
                _db = null;
                disposed = true;
            }
        }
    }
}
