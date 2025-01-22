using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListTest;

public interface IContactManager
{
    void AddContact();

    void SearchContactById();

    void SearchContactByPhoneNumber();

    void ListAllContacts();

    void UpdateContact();

    void DeleteContact();
}
