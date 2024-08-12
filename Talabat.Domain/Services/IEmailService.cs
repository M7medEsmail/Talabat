using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.Services
{
    public interface IEmailService
    {
        void SendEmail(Email email);
    }
}
