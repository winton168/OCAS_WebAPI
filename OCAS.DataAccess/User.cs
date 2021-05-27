using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCAS.DataAccess
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserGuid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int ActivityId { get; set; }
        public string Comments { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        public virtual Activity Activity { get; set; }

    }

}
