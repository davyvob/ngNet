using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Dtos.Users
{
    public class UserResponseDto
    {
        public string Id { get; set; }
        public string SuccesMessage { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
