using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.BLL.DTO.Log
{
    public class ExceptionLogDTO
    {
        public int Id { get; set; }

        public string ExeptionMessage { get; set; }

        public string ClassName { get; set; }

        public string ActionName { get; set; }

        public string StackTrace { get; set; }

        public DateTime Time { get; set; }
    }
}
