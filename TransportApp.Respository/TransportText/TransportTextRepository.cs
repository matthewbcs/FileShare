using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Model.Dto;

namespace TransportApp.Respository.TransportText
{
    public interface ITransportTextRepository: IBaseRepo
    {
        void Add(Model.Entities.Transport.TransportText t);
        List<Model.Entities.Transport.TransportText> Get(int accessCode);
        ServiceResponse Delete(List<Model.Entities.Transport.TransportText> items);
    }

    public class TransportTextRepository: BaseRepo, ITransportTextRepository
    {
        public void Add(Model.Entities.Transport.TransportText t)
        {
            context.TransportTexts.Add(t);
        }

        public List<Model.Entities.Transport.TransportText> Get(int accessCode)
        {
            return context.TransportTexts.Where(x => x.AccessCode == accessCode).ToList();
        }

        public ServiceResponse Delete(List<Model.Entities.Transport.TransportText> items)
        {
            context.TransportTexts.RemoveRange(items);
            Submit();
            return new ServiceResponse(){WasSuccess = true,Message = "Deleted Items"};
        }

    }
}
