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

        public async Task<List<ConversationDTO>> GetAllConversationByAdId(int adId)
        {
            List<ConversationDTO> conversations = new List<ConversationDTO>();

            if (adId > 0)
            {
                conversations = await (from c in _db.Conversations where c.AdId == adId
                                 join m in _db.ConversationMessages on
                                 c.ConversationId equals m.ConversationId
                                 select new ConversationDTO
                                 {
                                     ConversationId = c.ConversationId,
                                     AdId = c.AdId,
                                     Messages = c.Messages

                                 }).ToListAsync();
            }

            return conversations;
        }

        public async Task<ConversationDTO> GetConversationByAdIdAndSenderIdAndContactId(int adId, string senderId, string contactId)
        {
            ConversationDTO conversation = new ConversationDTO();

            if (adId > 0 && !String.IsNullOrEmpty(senderId))
            {
                try
                {
                    conversation = await (from c in _db.Conversations
                                          where c.AdId == adId
                                          join m in _db.ConversationMessages on
                                          c.ConversationId equals m.ConversationId
                                          where m.SenderId == senderId && m.ReceiverId == contactId
                                          select new ConversationDTO
                                          {
                                              ConversationId = c.ConversationId,
                                              AdId = c.AdId,
                                              Messages = c.Messages

                                          }).FirstOrDefaultAsync();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return conversation;
        }

        public async Task<ConversationDTO> GetConversationById(int conversationId)
        {
            ConversationDTO conversation = new ConversationDTO();

            if(conversationId > 0)
            {
                conversation =  await (from c in _db.Conversations
                                where c.ConversationId == conversationId
                                join m in _db.ConversationMessages on
                                c.ConversationId equals m.ConversationId
                                select new ConversationDTO
                                {
                                    ConversationId = c.ConversationId,
                                    AdId = c.AdId,
                                    Messages = c.Messages

                                }).FirstOrDefaultAsync();
            }
           
            return conversation;
        }


        public async Task<ConversationMessageDTO> SaveMessageToConversation(int conversationId, int adId, string message, string userId, string contactId)
        {
            Conversation conversation = new Conversation();
            ConversationMessages conversationMessages = new ConversationMessages();
            ConversationMessageDTO conversationMessageForReturn = new ConversationMessageDTO();

            if(conversationId == 0)
            {

                var conMessages =   await _db.ConversationMessages.
                                    Where(c => (c.ReceiverId == userId && c.SenderId == contactId) ||
                                    (c.ReceiverId == contactId && c.SenderId == userId))
                                    .OrderBy(c => c.CreatedAt)
                                    .Select(x => new ConversationMessages
                                    {
                                        ConversationId = x.ConversationId,
                                        SenderId = x.SenderId,
                                        ReceiverId = x.ReceiverId,
                                        Message = x.Message,
                                        Status = x.Status,
                                        CreatedAt = x.CreatedAt

                                    }).ToListAsync();

                conversation =  await (from c in _db.Conversations
                                join m in conMessages on c.ConversationId equals m.ConversationId into messageResult
                                select new Conversation
                                {
                                    ConversationId = c.ConversationId,
                                    AdId = c.AdId, 
                                    Messages = messageResult.ToList()

                                }).FirstOrDefaultAsync();

                if (conversation.ConversationId == 0)
                {
                    conversation.AdId = adId;

                    await _db.Conversations.AddAsync(conversation);
                    await _db.SaveChangesAsync();
                }
            }

            conversationMessages =
            new ConversationMessages
            {
                ConversationId = conversation.ConversationId,
                SenderId = userId,
                ReceiverId = contactId,
                Message = message,
                CreatedAt = DateTime.Now
            };

            await _db.ConversationMessages.AddAsync(conversationMessages);
            await _db.SaveChangesAsync();
            
            return conversationMessageForReturn;
        }

        public async Task<ConversationMessageDTO> ChangingMessageStatusToDelivered(int conversationMessageId)
        {
            ConversationMessageDTO conversationMessagesForReturn = new ConversationMessageDTO();

            if (conversationMessageId > 0)
            {
                var convMessage = await _db.ConversationMessages.Where(x => x.ConversationMessagesId == conversationMessageId).FirstOrDefaultAsync();

                if (convMessage != null)
                {
                    convMessage.Status = ConversationMessages.messageStatus.Delivered;
                    _db.Entry(convMessage).State = EntityState.Modified;
                    await _db.SaveChangesAsync();

                    conversationMessagesForReturn.ConversationId = convMessage.ConversationId;
                    conversationMessagesForReturn.SenderId = convMessage.SenderId;
                    conversationMessagesForReturn.Message = convMessage.Message;
                    conversationMessagesForReturn.ReceiverId = convMessage.ReceiverId;
                    conversationMessagesForReturn.Status = convMessage.Status;
                    conversationMessagesForReturn.CreatedAt = convMessage.CreatedAt;
                }
            }

            return conversationMessagesForReturn;
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
