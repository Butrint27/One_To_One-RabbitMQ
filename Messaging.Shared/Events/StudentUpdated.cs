using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Shared.Events
{
    public class StudentUpdated
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
    }
}
