using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Services
{
    public class BaseServiceResponse<T>
    {
        public bool IsSucces { get; set; }
        public List<string>? ErrorMessages { get; set; }
        public T? Data { get; set; }

        public BaseServiceResponse()
        {
            ErrorMessages = new List<string>();
        }

    }
}
