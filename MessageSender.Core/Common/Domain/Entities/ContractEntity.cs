using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageSender.Core.Common.Domain.Entities
{
    public class ContractEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Requests { get; set; }
    }
}
