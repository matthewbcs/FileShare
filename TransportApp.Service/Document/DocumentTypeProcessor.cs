using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TransportApp.Model.Dto;

namespace TransportApp.Service.Document
{
    
    public class DocumentTypeProcessor
    {
        

        public string GetContentTypeForDownload(EDocumentExtensionType fileExtension)
        {
            switch (fileExtension)
            {
                case EDocumentExtensionType.txt:
                    return "text/plain";
                case EDocumentExtensionType.doc:
                    return "application/msword";
                case EDocumentExtensionType.docx:
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case EDocumentExtensionType.pdf:
                    return "application/pdf";
                case EDocumentExtensionType.xls:
                    return "application/vnd.ms-excel";
                case EDocumentExtensionType.xlsx:
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case EDocumentExtensionType.odt:
                    return "application/vnd.oasis.opendocument.text";
                case EDocumentExtensionType.ppt:
                    return "application/vnd.ms-powerpoint";
                case EDocumentExtensionType.pptx:
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case EDocumentExtensionType.jpg:
                    return "image/jpeg";
                case EDocumentExtensionType.jpeg:
                    return "image/jpeg";
                case EDocumentExtensionType.png:
                    return "image/png";
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileExtension), fileExtension, null);
            }

            return "application/octet-stream";
        }

        public EDocumentExtensionType? GetDocumentType(string docName)
        {
            Regex regex = new Regex(@"\.([a-z]*[A-Z]*)");
            Match match = regex.Match(docName);
            if (match.Success == false)
                return null;

            string type = match.Value.Replace(".", "");


            EDocumentExtensionType t;
            try
            {
                t = (EDocumentExtensionType)Enum.Parse(typeof(EDocumentExtensionType), type.ToLower(), false);
                return t;
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
