namespace ContactListTest;

public class Menu
{
    private readonly IContactManager _contactManager;

    public Menu()
    {
        _contactManager = new ContactManager();
    }

    public void ContactMenu()
    {
        bool exit = false;

        while (!exit)
        {
            PrintContactMenu();

            try
            {
                Console.Write("Enter option: ");

                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("Invalid input. please enter a valid number option.");
                    continue;
                }


                switch (option)
                {
                    case 0:
                        exit = true;
                        ConsoleUtil.WriteLine("Exiting application...", ConsoleColor.Yellow);
                        break;

                    case 1:
                        _contactManager.AddContact();
                        break;

                        case 2:
                            _contactManager.SearchContactById();
                            break;

                        case 3:
                            _contactManager.SearchContactByPhoneNumber();
                            break;
                        case 4:
                            _contactManager.UpdateContact();
                            break;

                        case 5:
                            _contactManager.ListAllContacts();
                            break;

                        case 6:
                            _contactManager.DeleteContact();
                            break;

                        default:
                            ConsoleUtil.WriteLine("Invalid operation!", ConsoleColor.Red);
                            break;
                    
                }

            }
            catch (FormatException fe)
            {
                ConsoleUtil.WriteLine($"Invalid operation {fe.Message}", ConsoleColor.Red);
            }

        }
    
    }

    private static void PrintContactMenu()
    {
         Console.WriteLine("Enter 1 to Add new contact");
        Console.WriteLine("Enter 2 to Search contact by ID");
        Console.WriteLine("Enter 3 to Search contact by phone number");
        Console.WriteLine("Enter 4 to Update contact");
        Console.WriteLine("Enter 5 to Display all contacts");
        Console.WriteLine("Enter 6 to Delete contact");
        Console.WriteLine("Enter 0 to Exit");
        Console.WriteLine();
    }

}
