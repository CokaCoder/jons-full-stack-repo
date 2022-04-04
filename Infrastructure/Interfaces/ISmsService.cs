using CSharpFunctionalExtensions;
using Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ISmsService
    {
        public List<Sms> ReceiveSms();

        public Result SendSms(string body, string number);
    }
}
