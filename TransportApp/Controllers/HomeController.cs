using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransportApp.Model.Dto;
using TransportApp.Model.ViewModel;
using TransportApp.Model.ViewModel.NetworkWizard;
using TransportApp.Service.Document;
using TransportApp.Service.Transport;
using TransportApp.WebHelpers;

namespace TransportApp.Controllers
{
    public class HomeController : Controller
    {

        private IDocumentService _documentService;
        private ITextTransportService _textTransportService;

        public HomeController(IDocumentService documentService, ITextTransportService textTransportService)
        {
            _documentService = documentService;
            _textTransportService = textTransportService;
        }


        public ActionResult Index()
        {
            TransportViewModel model = new TransportViewModel();
            model.Messages = new List<MessageDto>();
            return View(model);
        }

        public ActionResult NetworkWizard()
        {
            NetworkWizardViewModel model = new NetworkWizardViewModel();
            List<NetworkQuestionItem> t = new List<NetworkQuestionItem>();
            NetworkQuestionItem q1 = new NetworkQuestionItem();
            q1.Question = "Does need network access?";
            q1.PossibleNetworks = new List<string>
            {
                ENetwork.MrLender.ToString(), ENetwork.MrLenderHidden.ToString(), ENetwork.MrLenderGuest.ToString()
            };
            t.Add(q1);

            NetworkQuestionItem q2 = new NetworkQuestionItem();
            q2.Question = "Is the device Un-secure?";
            q2.PossibleNetworks = new List<string> {ENetwork.MrLenderUnsecure.ToString() };
            t.Add(q2);

            NetworkQuestionItem q3 = new NetworkQuestionItem();
            q3.Question = "Do you have to reset the password frequently?";
            q3.PossibleNetworks = new List<string> {ENetwork.MrLenderUnsecure.ToString() };
            t.Add(q3);
            model.QuestionsItems = t;



            return View(model);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        // access docs
        [HttpPost]
        public ActionResult Login(int userAccessCode)
        {

            List<TransportItem> docs = _documentService.GetItemsForAccessCode(userAccessCode);

            return Json(docs);
        }

        [HttpPost]
        public ActionResult Transport(int userAccessCode, string Text)
        {
            ServiceResponse response = _textTransportService.AddTransportText(userAccessCode, Text);

            return Json(response);
        }
        [HttpGet]
        public virtual FileResult DownloadDoc(int documentId)
        {

            DownDocumentResponse downLoadDocumentResponse = _documentService.GetDocumentWithData(documentId);
            if (downLoadDocumentResponse.DocumentData == null)
                return null;

            var document = new FileContentResult(downLoadDocumentResponse.DocumentData,
                downLoadDocumentResponse.DocumentType)
            {
                FileDownloadName = downLoadDocumentResponse.DocumentName
            };

            // return document;
            return document;

        }

        [HttpPost]
        public virtual ActionResult UploadDocument(int AccessCode)
        {
            DocumentDTO uploadedFile = GetSingleUploadedFile();

            ServiceResponse serviceResponse = _documentService.AddDocument(uploadedFile.DocumentName, uploadedFile.FileContents, uploadedFile.ContentType,AccessCode);


            return Json(serviceResponse);

        }

        [HttpPost]
        public virtual FileResult UploadCsv(string FileName)
        {
            DocumentDTO uploadedFile = GetSingleUploadedFile();

            //ServiceResponse serviceResponse = _documentService.AddDocument(uploadedFile.DocumentName, uploadedFile.FileContents, uploadedFile.ContentType, AccessCode);

            WebClient req = new WebClient();
            byte[] data = req.DownloadData(Server.MapPath(@"C:\Personal Projects\CsvXmlReader\Output"));

            var document = new FileContentResult(data,
               "xml")
            {
                FileDownloadName = "test"
            };

            // return document;
            return document;
        

        }

        #region helpers

        public DocumentDTO GetSingleUploadedFile()
        {
            PostedFile file = new PostedFile(Request.Files[0]);
            DocumentDTO doc = new DocumentDTO()
            {
                ContentType = Request.Files[0]?.ContentType,
                DocumentLength = file.ContentLength,
                DocumentName = file.FileName,
                FileContents = file.FileContents

            };
            return doc;
        }

        #endregion

    }
}