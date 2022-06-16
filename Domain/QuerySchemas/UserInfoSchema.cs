using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.QuerySchemas
{
    public class UserInfoSchema
    {
        public Guid Id { get; set; }

        public Guid usersId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
