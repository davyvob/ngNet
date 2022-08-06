using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core.Entities
{
    public abstract class BaseClass
    {
        [Required]
        public Guid Id { get; set; }
    }
}
