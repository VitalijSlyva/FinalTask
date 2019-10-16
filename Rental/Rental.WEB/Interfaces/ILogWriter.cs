using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rental.WEB.Interfaces
{
    public interface ILogWriter
    {
        void CreateLog(string action, string authorId);
    }
}