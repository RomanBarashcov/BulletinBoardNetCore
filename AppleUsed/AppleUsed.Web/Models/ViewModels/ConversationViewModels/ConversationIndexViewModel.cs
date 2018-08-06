using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels.ConversationViewModels
{
    public class ConversationIndexViewModel
    {
        public AdDTO Ad { get; set; }
        public ConversationDTO Conversation { get; set; }
    }
}
