using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Shared.Events
{
    public class ContactCreated
    {
        public int ContactId { get; set; }
        public int StudentId { get; set; }
        public string Email { get; set; }
    }
}
