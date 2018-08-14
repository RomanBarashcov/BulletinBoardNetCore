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
        private readonly AppDbContext _db;

        public ConversationService(AppDbContext context)
        {
            _db = context;
        }

        public async Task<List<ConversationDTO>> GetAllConversationByAdId(int adId)
        {
            List<ConversationDTO> conversations = new List<ConversationDTO>();

            if (adId > 0)
            {
                conversations = await (from c in _db.Conversations 
                                join m in _db.ConversationMessages on
                                c.ConversationId equals m.ConversationId into messageResult
                                where c.AdId == adId
                                select new ConversationDTO
                                {
                                    ConversationId = c.ConversationId,
                                    SellerId = c.SellerId,
                                    SellerName = c.SellerName,
                                    BuyerId = c.BuyerId,
                                    BuyerName = c.BuyerName,
                                    AdId = c.AdId,
                                    Messages = messageResult.ToList()

                                }).ToListAsync();
            }

            return conversations;
        }


        public async Task<List<ConversationDTO>> GetAllConverastionBySenderId(string senderId)
        {
            List<ConversationDTO> conversations = new List<ConversationDTO>();
            
            if(!string.IsNullOrEmpty(senderId))
            {
                List<ConversationMessage> conMessages = new List<ConversationMessage>();

                try
                {
                    conMessages = await _db.ConversationMessages.
                                    Where(c => (c.SenderId == senderId || c.ReceiverId == senderId))
                                    .OrderBy(c => c.CreatedAt)
                                    .Select(x => new ConversationMessage
                                    {
                                        ConversationId = x.ConversationId,
                                        ConversationMessageId = x.ConversationMessageId,
                                        AdId = x.AdId,
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
                    conversations = await (from c in _db.Conversations
                                          join m in conMessages on c.ConversationId equals m.ConversationId into messageResult
                                          select new ConversationDTO
                                          {
                                              ConversationId = c.ConversationId,
                                              AdId = c.AdId,
                                              SellerId = c.SellerId,
                                              SellerName = c.SellerName,
                                              BuyerId = c.BuyerId,
                                              BuyerName = c.BuyerName,
                                              Messages = messageResult.ToList()

                                          }).ToListAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

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
                                    Where(c => (c.ReceiverId == senderId && c.SenderId == contactId && c.AdId == adId) ||
                                    (c.ReceiverId == contactId && c.SenderId == senderId && c.AdId == adId))
                                    .OrderBy(c => c.CreatedAt)
                                    .Select(x => new ConversationMessage
                                    {
                                        ConversationId = x.ConversationId,
                                        ConversationMessageId = x.ConversationMessageId,
                                        AdId = x.AdId,
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
                                          where c.SellerId == senderId && c.BuyerId == contactId || c.BuyerId == senderId && c.SellerId == contactId
                                          join m in conMessages on c.ConversationId equals m.ConversationId into messageResult
                                          select new ConversationDTO
                                          {
                                              ConversationId = c.ConversationId,
                                              AdId = c.AdId,
                                              SellerId = c.SellerId,
                                              SellerName = c.SellerName,
                                              BuyerId = c.BuyerId,
                                              BuyerName = c.BuyerName,
                                              Messages = messageResult.ToList()

                                          }).FirstOrDefaultAsync();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                if (conversation == null)
                {

                    var con = await CreateNewConversation(adId, senderId, contactId);
                    conversation = new ConversationDTO
                    {
                        ConversationId = con.ConversationId,
                        SellerId = con.SellerId,
                        SellerName = con.SellerName,
                        BuyerId = con.BuyerId,
                        BuyerName = con.BuyerId,
                        AdId = con.AdId
                    };

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
                                c.ConversationId equals m.ConversationId into messageResult
                                select new ConversationDTO
                                {
                                    ConversationId = c.ConversationId,
                                    AdId = c.AdId,
                                    SellerId = c.SellerId,
                                    SellerName = c.SellerName,
                                    BuyerId = c.BuyerId,
                                    BuyerName = c.BuyerName,
                                    Messages = messageResult.ToList()

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
                conversation = await CreateNewConversation(adId, userId, contactId);
            }
            else
            {
                conversation = await _db.Conversations.Where(x => x.ConversationId == conversationId).FirstOrDefaultAsync();
            }

            conversationMessage =
            new ConversationMessage
            {
                ConversationId = conversation.ConversationId,
                AdId = adId,
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
                ConversationMessagesId = conversationMessage.ConversationMessageId,
                AdId = conversationMessage.AdId,
                SenderId = userId,
                ReceiverId = contactId,
                Message = message,
                CreatedAt = DateTime.Now
            };

            return conversationMessageForReturn;
        }

        private async Task<Conversation> CreateNewConversation(int adId, string userId, string contactId)
        {
            Conversation conversation = new Conversation();
            List<ConversationMessage> conMessages = new List<ConversationMessage>();

            var sellerUser = await (from a in _db.Ads
                                    where a.AdId == adId
                                    join u in _db.Users on a.ApplicationUser.Id equals u.Id
                                    select new ApplicationUser
                                    {

                                        Id = u.Id,
                                        UserName = u.UserName,
                                        Email = u.Email,
                                        PhoneNumber = u.PhoneNumber

                                    }).FirstOrDefaultAsync();

            var buyerUser = await _db.Users.Where(x => x.Id == contactId).FirstOrDefaultAsync();
            if (sellerUser.Id == buyerUser.Id)
            {
                buyerUser = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            }

            conversation.AdId = adId;
            conversation.SellerId = sellerUser.Id;
            conversation.SellerName = sellerUser.UserName;
            conversation.BuyerId = buyerUser.Id;
            conversation.BuyerName = buyerUser.UserName;

            try
            {
                await _db.Conversations.AddAsync(conversation);
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return conversation;
        }


        public int GetCountNotDeliveredMessageByAdId(int adId)
        {
            var conversationById =  _db.Conversations.Where(x => x.AdId == adId).AsQueryable();
            int notDelivaredMessageCount = 0;

            try
            {
                notDelivaredMessageCount = (from c in conversationById
                                                join m in _db.ConversationMessages.Where(x => x.Status == ConversationMessage.messageStatus.Sent) on c.ConversationId equals m.ConversationId into messageResult
                                                select new Conversation
                                                {
                                                    ConversationId = c.ConversationId,
                                                    AdId = c.AdId,
                                                    Messages = messageResult.ToList()

                                                }).Select(x => x.Messages).Count();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
                    conversationMessagesForReturn.AdId = convMessage.AdId;
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
                disposed = true;
            }
        }
    }
}
