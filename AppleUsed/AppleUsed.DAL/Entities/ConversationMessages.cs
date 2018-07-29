using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class ConversationMessages
    {
        public ConversationMessages()
        {
            Status = messageStatus.Sent;
        }

        public enum messageStatus
        {
            Sent,
            Delivered
        }

        public int ConversationMessagesId { get; set; }
        public int ConversationId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public messageStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
