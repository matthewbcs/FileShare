using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Model.Entities.Document;
using TransportApp.Respository.DbContext;

namespace TransportApp.Respository.Documents
{
    public interface IDocumentRepository: IBaseRepo
    {
        void AddDocument(Document doc);
        Document Get(int id);
        List<Document> GetDocs(int accessCode);
        bool Delete(int id);
    }

    public class DocumentRepository: BaseRepo, IDocumentRepository
    {
        public void AddDocument(Document doc)
        {
            context.Documents.Add(doc);
        }

        public Document Get(int id)
        {
            return context.Documents.FirstOrDefault(x => x.DocumentId == id);
        }

        public bool Delete(int id)
        {
            Document d = context.Documents.FirstOrDefault(x => x.DocumentId == id);

            if (d != null)
            {
                context.Documents.Remove(d);
                return true;
            }

            return false;


        }

        public List<Document> GetDocs(int accessCode)
        {
            return context.Documents.Where(x => x.AccessCode == accessCode).ToList();
        }
    }
}
