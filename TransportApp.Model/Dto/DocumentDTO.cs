using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportApp.Model.Dto
{
   public class DocumentDTO
    {
       
        public string DocumentName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContents { get; set; }
        public int DocumentLength { get; set; }
        public string AccessCode { get; set; }
        
    }
   public class DocumentDTONameOnly
   {

       public int DocumentId { get; set; }
       public string DocumentName { get; set; }
  

   }

   public class TransportItem
   {
        public DocumentDTONameOnly Doc { get; set; }
        public TextItem TextItem { get; set; }
        public bool IsDoc => TextItem == null && Doc != null;
   }

   public class TextItem
   {
       public string Value { get; set; }
   }

   public class DownDocumentResponse 
   {
       public byte[] DocumentData { get; set; }
       public string DocumentName { get; set; }
       public string DocumentType { get; set; }
       public EDocumentExtensionType EDocumentType { get; set; }

       public string DocumentNameWithExtension => string.Concat(DocumentName, "." + EDocumentType);
   }

   public enum EDocumentExtensionType
   {
       txt = 1,
       doc = 2,
       docx = 3,
       pdf = 4,
       xls = 5,
       xlsx = 6,
       odt = 7,
       ppt = 8,
       pptx = 9,
       jpg = 10,
       jpeg = 11,
       png = 12

   }
}
