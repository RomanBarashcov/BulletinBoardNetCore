using AppleUsed.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppleUsed.Web.Models.ViewModels
{
    public class ConversationListViewModel
    {
        public List<ConversationDTO> Conversations { get; set; } 
        public string UserId { get; set; }
    }
}
