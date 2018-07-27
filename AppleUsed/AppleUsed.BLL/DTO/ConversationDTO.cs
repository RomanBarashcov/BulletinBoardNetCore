using AppleUsed.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppleUsed.BLL.DTO
{
    [NotMapped]
    public class ConversationDTO
    {
        public int ConversationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public Conversation.messageStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
