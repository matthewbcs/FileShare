using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Model.Dto;
using TransportApp.Model.Entities.Transport;
using TransportApp.Respository.TransportText;
using TransportApp.Service.Helpers;

namespace TransportApp.Service.Transport
{
    public interface ITextTransportService
    {
        ServiceResponse AddTransportText(int accessCode, string value);
    }

    public class TextTransportService : ITextTransportService
    {

       private ITransportTextRepository _transportTextRepository;

       public TextTransportService(ITransportTextRepository transportTextRepository)
       {
           _transportTextRepository = transportTextRepository;
       }

       public ServiceResponse AddTransportText(int accessCode, string value)
        {
            TransportText t = new TransportText();
            t.AccessCode = accessCode;
            t.DateAddedOnUtc = DateTime.Now;
            t.DateToDeleteByUtc = DateTime.Now.AddMinutes(10);
            t.TransportTextDataEncypted = TextEncryptService.Encrypt(value);
            _transportTextRepository.Add(t);
            _transportTextRepository.Submit();
            return new ServiceResponse(){Message = "Transported Successfully",WasSuccess = true};
        }
    }
}
