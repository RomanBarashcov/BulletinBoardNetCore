using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IConversationService
    {
        Task<List<ConversationDTO>> GetAllConversationByUserId(string userId);
        Task<List<ConversationDTO>> GetConversationByContact(string userId, string contactId);
        Task<ConversationDTO> SaveMessageToConversation(string message, string userId, string contactId);
        Task<ConversationDTO> ChangingMessageStatusToDelivered(int conversationId);
    }
}
