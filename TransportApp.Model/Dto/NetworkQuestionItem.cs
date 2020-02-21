using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Dto
{
   public class NetworkQuestionItem
    {
        public string QuestionId { get; set; }
        public string Question { get; set; }
        public bool Answer { get; set; }
        public List<string> PossibleNetworks { get; set; }
    }

   public enum ENetwork
   {
       MrLender,
       MrLenderGuest,
       MrLenderHidden,
       MrLenderUnsecure
   }
}
