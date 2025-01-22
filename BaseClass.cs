using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListTest;

    public abstract class BaseClass
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
