using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Model.Dto;
using TransportApp.Model.Entities.Transport;
using TransportApp.Respository.Documents;
using TransportApp.Respository.TransportText;
using TransportApp.Service.Helpers;

namespace TransportApp.Service.Document
{
    public interface IDocumentService
    {
        ServiceResponse AddDocument(string docName, byte[] docData, string uploadedFileContentType, int accessCode);
        List<DocumentDTONameOnly> GetDocumentsForAccessCode(int accessCode);
        DownDocumentResponse GetDocumentWithData(int documentId);
        List<TransportItem> GetItemsForAccessCode(int accessCode);
    }

    public class DocumentService : IDocumentService
    {
       private IDocumentRepository _iDocumentRepository;
       private ITransportTextRepository _transportTextRepository;
    

       public DocumentService(IDocumentRepository idDocumentRepository, ITransportTextRepository transportTextRepository)
       {
           this._iDocumentRepository = idDocumentRepository;
           _transportTextRepository = transportTextRepository;
       }

       public ServiceResponse AddDocument(string docName, byte[] docData, string uploadedFileContentType,
           int accessCode)
       {
            Model.Entities.Document.Document doc = new Model.Entities.Document.Document()
            {
                DateAddedOnUtc = DateTime.Now,
                DateToDeleteByUtc = DateTime.Now.AddMinutes(10),
                DocumentName = docName,
                DocumentData = docData,
                AccessCode = accessCode,
                DocumentType = uploadedFileContentType
            };
            _iDocumentRepository.AddDocument(doc);
            ServiceResponse submitResponse = _iDocumentRepository.Submit();
            if (submitResponse.WasSuccess)
            {
                submitResponse.Message = "Uploaded Successfully";
                
            }

            return submitResponse;
            
       }

       public List<DocumentDTONameOnly> GetDocumentsForAccessCode(int accessCode)
       {
           List<Model.Entities.Document.Document> documents = _iDocumentRepository.GetDocs(accessCode);

           List<DocumentDTONameOnly> docsToReturn = new List<DocumentDTONameOnly>();
           foreach (var d in documents)
           {
               DocumentDTONameOnly doc = new DocumentDTONameOnly()
               {
                   DocumentName = d.DocumentName,
                   DocumentId = d.DocumentId
                   //FileContents = d.DocumentData,

               };
               docsToReturn.Add(doc);
           }

           return docsToReturn;
       }

       public List<TransportItem> GetItemsForAccessCode(int accessCode)
       {
           List<Model.Entities.Document.Document> documents = _iDocumentRepository.GetDocs(accessCode);
           List<TransportText> transportTexts = _transportTextRepository.Get(accessCode);

          
           

           List<TextItem> transportTextItems = new List<TextItem>();

           foreach (var t in transportTexts)
           {
               // should a have already been deleted so don't pull out again
               if(DateTime.Now > t.DateToDeleteByUtc)
                   continue;

               TextItem ti = new TextItem();
               ti.Value = TextEncryptService.Decrypt(t.TransportTextDataEncypted);
               transportTextItems.Add(ti);

           }

           List<DocumentDTONameOnly> docsToReturn = new List<DocumentDTONameOnly>();
           foreach (var d in documents)
           {
               // should a have already been deleted so don't pull out again
               if (DateTime.Now > d.DateToDeleteByUtc)
                   continue;

                DocumentDTONameOnly doc = new DocumentDTONameOnly()
               {
                   DocumentName = d.DocumentName,
                   DocumentId = d.DocumentId
                   //FileContents = d.DocumentData,

               };
               docsToReturn.Add(doc);
           }
           ///// here was are combining any docs and text items into 1 list
           List<TransportItem> items = new List<TransportItem>();
           foreach (var d in docsToReturn)
           {
               TransportItem item = new TransportItem();
               item.Doc = d;

               items.Add(item);
           }

           foreach (var tii in transportTextItems)
           {
               TransportItem item = new TransportItem();
               item.TextItem = tii;
               items.Add(item);
           }

           if (transportTexts.Count > 0)
           {
               _transportTextRepository.Delete(transportTexts); // want to delete straight away - this does submit changes
           }

            return items;
       }

        public DownDocumentResponse GetDocumentWithData(int documentId)
       {
           Model.Entities.Document.Document document = _iDocumentRepository.Get(documentId);


           if(document == null || DateTime.Now > document.DateToDeleteByUtc)
               return new DownDocumentResponse(){};

           // delete doc after download

           DownDocumentResponse doc = new DownDocumentResponse()
           {
               DocumentName = document.DocumentName,
               DocumentData = document.DocumentData,
               DocumentType = document.DocumentType
               
           };


           // delete doc after download
           bool wasDocDeleted = _iDocumentRepository.Delete(documentId);

           if (wasDocDeleted)
               _iDocumentRepository.Submit();


           return doc;
       }
   }
}
