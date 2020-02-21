using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Dto
{
   public class MessageDto
    {
        public int MessageId { get; set; }
        public string FileName { get; set; }
        public string MessageData { get; set; }
        public string MessageText { get; set; }
        
        public DateTime AddedOnUtc { get; set; }

        public bool IsDocument => !string.IsNullOrEmpty(MessageData);
    }
}
