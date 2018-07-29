using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class ConversationMessageDTO
    {
        public int ConversationMessagesId { get; set; }
        public int ConversationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public ConversationMessages.messageStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
