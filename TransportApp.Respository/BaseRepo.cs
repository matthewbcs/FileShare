using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransportApp.Model.Dto;
using TransportApp.Respository.DbContext;

namespace TransportApp.Respository
{
    public interface IBaseRepo
    {
        ServiceResponse Submit();
    }

    public class BaseRepo: MyDbContext, IBaseRepo
    {
       public MyDbContext context;

       public BaseRepo()
       {
           context = new MyDbContext();
       }

       public ServiceResponse Submit()
       {
           ServiceResponse response = new ServiceResponse();
           try
           {
               context.SaveChanges();
               response.WasSuccess = true;
           }
           catch (Exception e)
           {
               response.WasSuccess = false;
               response.Message = e.Message;
           }

           return response;
       }
    }
}
