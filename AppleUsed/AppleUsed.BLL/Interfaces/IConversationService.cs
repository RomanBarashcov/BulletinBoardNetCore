﻿using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleUsed.BLL.Interfaces
{
    public interface IConversationService
    {
        Task<List<ConversationDTO>> GetAllConversationByAdId(int adId);
        Task<List<ConversationDTO>> GetAllConverastionBySenderId(string senderId);
        Task<ConversationDTO> GetConversationByAdIdAndSenderIdAndContactId(int adId, string userId, string contactId);
        Task<ConversationDTO> GetConversationById(int conversationId);
        Task<ConversationMessageDTO> SaveMessageToConversation(int conversationId, int adId, string message, string userId, string contactId);
        int GetCountNotDeliveredMessageByAdId(int adId);
        Task<ConversationMessageDTO> ChangingMessageStatusToDelivered(int conversationMessageId);
    }
}
