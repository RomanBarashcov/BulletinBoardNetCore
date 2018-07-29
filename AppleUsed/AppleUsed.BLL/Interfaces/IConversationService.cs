using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IConversationService
    {
        Task<List<ConversationDTO>> GetAllConversationByAdId(int adId);
        Task<ConversationDTO> GetConversationByAdIdAndSenderId(int adId, string userId);
        Task<ConversationDTO> GetConversationById(int conversationId);
        Task<ConversationMessageDTO> SaveMessageToConversation(int conversationId, int adId, string message, string userId, string contactId);
        Task<ConversationMessageDTO> ChangingMessageStatusToDelivered(int conversationMessageId);
    }
}
