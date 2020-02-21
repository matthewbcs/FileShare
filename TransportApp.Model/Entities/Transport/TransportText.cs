using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Entities.Transport
{
    // TransportText
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.2.0")]
    public class TransportText
    {
        public int TransportTextId { get; set; } // TransportTextId (Primary key)
        public string TransportTextData { get; set; } // TransportTextData
        public string TransportTextDataEncypted { get; set; } // TransportTextDataEncypted
        public System.DateTime DateAddedOnUtc { get; set; } // DateAddedOnUtc
        public System.DateTime DateToDeleteByUtc { get; set; } // DateToDeleteByUtc
        public int AccessCode { get; set; } // AccessCode
    }

}
