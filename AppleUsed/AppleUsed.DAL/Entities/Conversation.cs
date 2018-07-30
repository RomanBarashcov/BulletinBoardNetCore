using System;
using System.Collections.Generic;
using System.Text;

namespace AppleUsed.DAL.Entities
{
    public class Conversation
    {
        public int ConversationId { get; set; }
        public int AdId { get; set; }
        public virtual List<ConversationMessage> Messages { get; set; }
    }
}
