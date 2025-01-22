using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListTest;

    public class Contact : BaseClass
    {
        public string Name { get; set; } = default!;
        public string MobileNumber { get; set; } = default!;
        public string? AlternateMobileNumber { get; set; }
        public string? WorkNumber { get; set; }
        public string? Email { get; set; }
        public ContactType? Type { get; set; }
    }
