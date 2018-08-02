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
                List<ConversationMessage> conMessages = new List<ConversationMessage>();

                try
                {
                    conMessages = await _db.ConversationMessages.
                                    Where(c => (c.ReceiverId == senderId && c.SenderId == contactId) ||
                                    (c.ReceiverId == contactId && c.SenderId == senderId))
                                    .OrderBy(c => c.CreatedAt)
                                    .Select(x => new ConversationMessage
                                    {
                                        ConversationId = x.ConversationId,
                                        SenderId = x.SenderId,
                                        ReceiverId = x.ReceiverId,
                                        Message = x.Message,
                                        Status = x.Status,
                                        CreatedAt = x.CreatedAt

                                    }).ToListAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    conversation = await (from c in _db.Conversations
                                          join m in conMessages on c.ConversationId equals m.ConversationId into messageResult
                                          select new ConversationDTO
                                          {
                                              ConversationId = c.ConversationId,
                                              AdId = c.AdId,
                                              Messages = messageResult.ToList()

                                          }).FirstOrDefaultAsync();

                }
                catch (Exception ex)
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
            ConversationMessage conversationMessage = new ConversationMessage();
            ConversationMessageDTO conversationMessageForReturn = new ConversationMessageDTO();

            if (String.IsNullOrEmpty(message))
                return conversationMessageForReturn;

            if (conversationId == 0)
            {

                List<ConversationMessage> conMessages = new List<ConversationMessage>();

                try
                {
                    conMessages = await _db.ConversationMessages.
                                    Where(c => (c.ReceiverId == userId && c.SenderId == contactId) ||
                                    (c.ReceiverId == contactId && c.SenderId == userId))
                                    .OrderBy(c => c.CreatedAt)
                                    .Select(x => new ConversationMessage
                                    {
                                        ConversationId = x.ConversationId,
                                        SenderId = x.SenderId,
                                        ReceiverId = x.ReceiverId,
                                        Message = x.Message,
                                        Status = x.Status,
                                        CreatedAt = x.CreatedAt

                                    }).ToListAsync();

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                try
                {
                    conversation = await (from c in _db.Conversations
                                          join m in conMessages on c.ConversationId equals m.ConversationId into messageResult
                                          select new Conversation
                                          {
                                              ConversationId = c.ConversationId,
                                              AdId = c.AdId,
                                              Messages = messageResult.ToList()

                                          }).FirstOrDefaultAsync();

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (conversation == null || conversation.ConversationId == 0)
                {
                    conversation = new Conversation();
                    conversation.AdId = adId;

                    await _db.Conversations.AddAsync(conversation);
                    await _db.SaveChangesAsync();
                }
            }

            conversationMessage =
            new ConversationMessage
            {
                ConversationId = conversation.ConversationId,
                SenderId = userId,
                ReceiverId = contactId,
                Message = message,
                CreatedAt = DateTime.Now
            };

            await _db.ConversationMessages.AddAsync(conversationMessage);
            await _db.SaveChangesAsync();


            conversationMessageForReturn =
            new ConversationMessageDTO
            {
                ConversationId = conversationMessage.ConversationId,
                SenderId = userId,
                ReceiverId = contactId,
                Message = message,
                CreatedAt = DateTime.Now
            };

            return conversationMessageForReturn;
        }


        public int GetCountNotDeliveredMessageByAdId(int adId)
        {
            var conversationById =  _db.Conversations.Where(x => x.AdId == adId);

            int notDelivaredMessageCount = (from c in conversationById
                                            join m in _db.ConversationMessages.Where(x=>x.Status == ConversationMessage.messageStatus.Sent) on c.ConversationId equals m.ConversationId into messageResult
                                                            select new Conversation
                                                            {
                                                                ConversationId = c.ConversationId,
                                                                AdId = c.AdId,
                                                                Messages = messageResult.ToList()

                                                            }).Select(x=>x.Messages).Count();

            return notDelivaredMessageCount;
        }

        public async Task<ConversationMessageDTO> ChangingMessageStatusToDelivered(int conversationMessageId)
        {
            ConversationMessageDTO conversationMessagesForReturn = new ConversationMessageDTO();

            if (conversationMessageId > 0)
            {
                var convMessage = await _db.ConversationMessages.Where(x => x.ConversationMessageId == conversationMessageId).FirstOrDefaultAsync();

                if (convMessage != null)
                {
                    convMessage.Status = ConversationMessage.messageStatus.Delivered;
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
