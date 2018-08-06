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
        public string SellerId { get; set; }
        public string SellerName { get; set; }
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
        
        public List<ConversationMessage> Messages { get; set; }
    }
}
