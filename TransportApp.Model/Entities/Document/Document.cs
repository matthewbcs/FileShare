using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Entities.Document
{
    // Document
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.37.2.0")]
    public class Document
    {
        public int DocumentId { get; set; } // DocumentId (Primary key)
        public string DocumentName { get; set; } // DocumentName (length: 200)
        public byte[] DocumentData { get; set; } // DocumentData
        public string DocumentType { get; set; } // DocumentType (length: 50)
        public System.DateTime DateAddedOnUtc { get; set; } // DateAddedOnUtc
        public System.DateTime DateToDeleteByUtc { get; set; } // DateToDeleteByUtc
        public int AccessCode { get; set; } // AccessCode
    }
}
