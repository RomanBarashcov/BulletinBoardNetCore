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
        public int AdId { get; set; }
        public List<ConversationMessages> Messages { get; set; }
    }
}
