using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QuerySchemas
{
    public class Questions
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string AnswerType { get; set; }
    }
}
