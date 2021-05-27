using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCAS.DataAccess
{
    public partial class Activity
    {
        public Activity()
        {
            Users = new HashSet<User>();
        }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }


}
