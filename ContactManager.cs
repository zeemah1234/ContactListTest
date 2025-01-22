using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;
using System.Text.RegularExpressions; 


namespace ContactListTest;

public class ContactManager : IContactManager
{
    private readonly List<Contact> Contacts;

    public ContactManager()
    {
        Contacts = [];
    }

    public Contact AddContactRequest()
    {
        Contact contact = new();

        Console.Write("Enter contact name: ");
        contact.Name = Console.ReadLine()!;

        Console.Write("Enter phone number: ");
        contact.MobileNumber = Console.ReadLine()!;

        Console.Write("Enter email: ");
        contact.Email = Console.ReadLine();

        int? contactType = Utility.SelectEnum("Select contact type:\nEnter 1 for Family\nEnter 2 for Friend\nEnter 3 for Work\nEnter any special or alphabet character to skip: ", 1, 3);

        if (contactType.HasValue)
        {
            contact.Type = (ContactType)contactType;
        }

        return contact;
    }
    public void AddContact()
    {
        try
        {
            int id = Contacts.Count > 0 ? Contacts.Count + 1 : 1;
            var contactRequest = AddContactRequest();
            ValidateContactName(contactRequest.Name);
            ValidateContactPhoneNumber(contactRequest.MobileNumber);
            bool contactAlreadyExist = IsContactExist(contactRequest.MobileNumber);

            if (contactAlreadyExist)
            {
                ConsoleUtil.WriteLine($"Contact with {contactRequest.MobileNumber} already exist!", ConsoleColor.Red);
                return;
            }

            var contact = new Contact
            {
                Id = id,
                Name = contactRequest.Name,
                MobileNumber = contactRequest.MobileNumber,
                Email = contactRequest.Email,
                Type = contactRequest.Type,
                CreatedAt = DateTime.Today
            };

            Contacts.Add(contact);
            ConsoleUtil.WriteLine($"Contact with name {contact.Name} and id {contact.Id} successfully created!", ConsoleColor.Green);
        }
        catch (Exception ex) 
        { 
            ConsoleUtil.WriteLine($"Exception: {ex.Message}", ConsoleColor.Red);
        }
    }

    public void DeleteContact()
    {
        try
        {
            Console.Write("Enter the ID of contact: ");
            int id = int.Parse(Console.ReadLine()!);
            var contact = Contacts.Find(x => x.Id == id);

            if (contact is null)
            {
                ConsoleUtil.WriteLine("Contact you are trying to delete does not exist!", ConsoleColor.Red);
                return;
            }

            bool isRemoved = Contacts.Remove(contact);

            string result = isRemoved 
                ? "Contact removed successfully!" 
                : "Unable to remove contact!";
                    
            ConsoleUtil.WriteLine(result, ConsoleColor.Green);
        }
        catch (Exception ex) 
        { 
            ConsoleUtil.WriteLine($"An error occurred: {ex.Message}", ConsoleColor.Red); 
        }
    }

    
    public void ListAllContacts()
    {
                
        if (Contacts.Count == 0)
        {
            Console.WriteLine("There is no contact in the record yet!. Add a new contact", ConsoleColor.Cyan);
            return;
        }
        var table = new ConsoleTable("ID", "Name", "Phone", "Email", "Created At", "Modified At");

                    foreach (var contact in Contacts)
                    {
                        table.AddRow(contact.Id, contact.Name, contact.MobileNumber, contact.Email, contact.CreatedAt.ToString("dd MMM, yyyy"), contact.ModifiedAt.HasValue ? contact.ModifiedAt?.ToString("dd MMM, yyyy h:mm:ss") : "N/A");
                    }

                    Console.WriteLine();
                    table.Write(Format.Alternative);
                    Console.WriteLine();
        // ConsoleTables.table = new("ID", "Name", "Phone", "Email", "Created At", "Modified At"); 

        // foreach (var contact in Contacts)
        // {
        //     table.AddRow(contact.ID, contact.Name, contact.MobileNumber, contact.Email, contact.CreatedAt.ToString("dd MMM, yyyy"), contact.ModifiedAt.HasValue ? contact.ModifiedAt?.ToString("dd MMM. yyyy h:mm:ss") : "N/A");
        // }    

        // Console.WriteLine();
        // table.Write(Format.Alternative);
        // Console.WriteLine();
    }

    public void SearchContactById() 
    {
        Console.Write("Enter the ID of the contact: ");
        int id = int.Parse(Console.ReadLine()!);
        var contact = Contacts.Find(x => x.Id == id);

        if (contact is null)
        {
            Console.WriteLine("Contact does not exist!");
            return;
        }
        
        var result = $"""
        =====CONTACT DETAILS=====
        Name: {contact.Name}
        Mobile Number: {contact.MobileNumber}
        Email: {contact.Email ?? "N/A"}
        Alternate Mobile: {contact.AlternateMobileNumber ?? "N/A"}
        Work Number: {contact.WorkNumber ?? "N/A"}
        Contact Type: {contact.Type}
        """;

        Console.WriteLine(result); 
        Console.WriteLine();
    }
    public void SearchContactByPhoneNumber()
    {

        Console.Write("Enter the phone number of the contact: ");
        string mobileNumber = Console.ReadLine()!;
        var contact = Contacts.Find(x => x.MobileNumber == mobileNumber);

        if (contact is null)
        {
            Console.WriteLine("Contact does not exist!");
            return;
        }

        var result = $"""
        =====CONTACT DETAILS=====
        Name: {contact.Name}
        Mobile Number: {contact.MobileNumber}
        Email: {contact.Email ?? "N/A"}
        Alternate Mobile: {contact.AlternateMobileNumber ?? "N/A"}
        Work Number: {contact.WorkNumber ?? "N/A"}
        Contact Type: {contact.Type}
        """;

        Console.WriteLine(result);
    }
    public void UpdateContact()
    {
        try
        {
            Console.Write("Enter the ID of contact: ");
            int id = int.Parse(Console.ReadLine()!);

            var contact = Contacts.Find(x => x.Id == id);

            if (contact is null)
            {
                Console.WriteLine("Contact you are trying to edit does not exist!");
                return;
            }

            bool isRecordUpdated = false;

            Console.Write("Enter contact name: ");
            string name = Console.ReadLine()!;
            
            if (!string.IsNullOrWhiteSpace(name)) 
            {
                ValidateContactName(name);
                contact.Name = name;
                isRecordUpdated = true;  
            }

            Console.Write("Enter phone number: ");
            string mobileNumber = Console.ReadLine()!;

            
            if (!string.IsNullOrWhiteSpace(mobileNumber)) 
            { 
                ValidateContactPhoneNumber(mobileNumber);
                contact.MobileNumber = mobileNumber;
                isRecordUpdated = true;   
            }

            Console.Write("Enter email: ");
            string email = Console.ReadLine()!;

            if(!string.IsNullOrWhiteSpace(email))
            {
                contact.Email = email;
                isRecordUpdated = true;
            } 

            int? updatedContactType = Utility.SelectEnum("Select contact type:\nEnter 1 for Family\nEnter 2 for Friends\nEnter 3 for Work\nEnter any special or alphabet character to skip:  ", 1, 3);

            if(updatedContactType.HasValue)
            {
                contact.Type = (ContactType)updatedContactType;
                isRecordUpdated = true;
            }

            if (isRecordUpdated)
            {
                contact.ModifiedAt = DateTime.Now;
                Console.WriteLine("Contact was updated successfully!");
            
            }
            else
            {
                Console.WriteLine("Contact unchanged");
            }

            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }
    private bool IsContactExist(string phoneNumber)
    {
        return Contacts.Any(c => c.MobileNumber == phoneNumber);
    }
    public bool IsContactExist(int id)
    {
        return Contacts.Any(c => c.Id == id);
    }
    
    
    static void ValidateContactName(string name) 
    {  
        string namePattern = @"^[a-zA-Z@]+(?: [a-zA-Z@]+)*$";

        if(!Regex.IsMatch(name, namePattern))
        {
            throw new Exception("Contact name cannot contain special characters except single white space between each interval of word or character and @ character!");
        }

        if(name.Length < 3)
        {
            throw new Exception("Contact name must be at least three characters.");
        }
    }
    
    static void ValidateContactPhoneNumber(string phoneNumber) 
    { 
        string phoneNumberPattern = @"^\d+$";

        if(!Regex.IsMatch(phoneNumber, phoneNumberPattern))
        {
            throw new Exception("Phone number cannot contain special character(s)");
        }

        if (phoneNumber?.Length < 11 || phoneNumber?.Length > 11)
        {
            throw new Exception("Phone number cannot be less or greater than 11 digits");
        }
    } 
}