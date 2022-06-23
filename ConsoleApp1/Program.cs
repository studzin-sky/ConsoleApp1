
namespace Obiektowe2022Bank 
{
    internal class Program
    {
        public static string userName = "";
        public static string answer = "";
        public static List<BankAccount> bankAccounts = new List<BankAccount>();
        static void Main(string[] args)
        {   
            string enterText = "Witaj, podaj swoje imię \n";
            getUserName(enterText);
            Console.WriteLine("Zapraszamy do naszej aplikacji w której możesz stworzyć subkonta na wydatki, usunąć je, połączyć lub zmienić kwotę");
            //List<BankAccount> bankAccounts = new List<BankAccount>();
            MainMenu(userName);

        }

        static void getUserName(string enterText)
        {
            try
            {
                Console.Write(enterText);
                string name = Console.ReadLine();
                if (!string.IsNullOrEmpty(name))
                {
                    userName = name;
                    Console.WriteLine($"Witaj {name}");
                    return;
                }
                else
                {
                    Console.WriteLine("Trzeba podać swoje imie");
                    getUserName(enterText);
                }
            }
            catch 
            {
                throw;
            }
        }

        static void MainMenu(string userName)
        {
            Console.WriteLine($"{userName} wybierz numer zadania:");
            Console.WriteLine("1 - Utwórz subkonto");
            Console.WriteLine("2 - Usuń subkonto");
            Console.WriteLine("3 - Stwórz nowe konto z dwóch");
            Console.WriteLine("4 - Zmień wartość subkonta");
            Console.WriteLine("5 - Wyświetl subkonta");
            Console.WriteLine("9 - Zakończ Program");
            answer = Console.ReadLine();
            try
            {
                if (answer == "1" || answer == "2" || answer == "3" || answer == "4" || answer == "5")
                {
                    Tasker(answer);
                    return;

                } else if (answer == "9")
                {
                    Console.WriteLine("Do widzenia!");
                    Console.ReadKey();
                } else
                {
                    Console.WriteLine($"{userName} wybrałeś zły numer! Spróbuj jeszcze raz.");
                    MainMenu(userName);
                }
            }
            catch
            {
                throw;
            }
        }

        static void Tasker(string userAnswer)
        {
            BankActions AkcjeBanku = new BankActions();
            switch (userAnswer)
            {   
                case "1":
                    Console.WriteLine("Na co chcesz otworzyć konto?");
                    string accountName = Console.ReadLine();
                    AkcjeBanku.AddAccount(accountName, bankAccounts);
                    break;

                case "2":
                    Console.WriteLine("Chcesz usunąć konto");
                    AkcjeBanku.DeleteAccount(bankAccounts);
                    break;

                case "3":
                    Console.WriteLine("Chcesz utworzyć nowe konto z dwóch innych");
                    AkcjeBanku.SumAccounts(bankAccounts);
                    break;

                case "4":
                    Console.WriteLine("Chcesz zmienić wartość na koncie");
                    AkcjeBanku.ChangeAmmount(bankAccounts);
                    break;

                case "5":
                    AkcjeBanku.ShowAccounts(bankAccounts);
                    break;

                default:
                    Console.WriteLine("Error!");
                    break;
            }
        }

        public class BankAccount
        {
            public string accountName;
            public double amount;
            public BankAccount(string accountName, double amount)
            {
                this.accountName = accountName;
                this.amount = amount;
            }
            public static BankAccount operator +(BankAccount a1, BankAccount a2)
            {
                BankAccount wynik = new BankAccount("", 0);
                wynik.accountName = $"{a1.accountName} + {a2.accountName}";
                wynik.amount = +a1.amount + a2.amount;
                return wynik;
            }
        }


        interface IBankActions
        {
            void AddAccount(string accountName, List<BankAccount> bankAccounts);

            void DeleteAccount(List<BankAccount> bankAccounts);

            void SumAccounts(List<BankAccount> bankAccounts);

            void ChangeAmmount(List<BankAccount> bankAccounts);

            void ShowAccounts(List<BankAccount> bankAccounts);
        }


        public class BankActions: IBankActions
        {
            public void AddAccount(string accountName, List<BankAccount> bankAccounts)
                {
                Console.WriteLine($"Podaj kwotę na {accountName}");
                double amount = Convert.ToDouble(Console.ReadLine());
                BankAccount bankAccount = new BankAccount(accountName, amount);
                bankAccounts.Add(bankAccount);
                Console.WriteLine($"Dodano rachunek na {bankAccount.accountName}");
                MainMenu(userName);
                }

            public void DeleteAccount(List<BankAccount> bankAccounts)
            {
                if (bankAccounts.Count >= 1)
                {
                    for (int i = 0; i < bankAccounts.Count; i++)
                    {
                        Console.WriteLine($"Nr: {i + 1} to {bankAccounts[i].accountName}");
                    }

                    try
                    {
                        Console.WriteLine("Podaj numer konta, które chcesz usunąć");
                        int index = Convert.ToInt32(Console.ReadLine());
                        if (index > 0)
                        {
                            Console.WriteLine($"Usunąłeś budżet na {bankAccounts[index - 1].accountName}");
                            bankAccounts.RemoveAt(index - 1);
                            MainMenu(userName);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Zły numer");
                            DeleteAccount(bankAccounts);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                } 
                else
                {
                    Console.WriteLine("Nie masz żadnego konta! Kliknij by wrócić do Menu i dodać konta.");
                    Console.ReadKey();
                    MainMenu(userName);
                }
            }

            public void SumAccounts(List<BankAccount> bankAccounts)
            {
                BankAccount newAccount;
                if (bankAccounts.Count < 2)
                {
                    Console.WriteLine("Za mało kont do dodania! Kliknij przycisk by wrócić do menu");
                    Console.ReadKey();
                    MainMenu(userName);
                }
                else
                {
                    Console.WriteLine($"{userName}, które konta chcesz dodać?");
                    for (int i = 0; i < bankAccounts.Count; i++)
                    {
                        Console.WriteLine($"Nr: {i + 1} to {bankAccounts[i].accountName}");
                    }
                    try
                    {
                        Console.WriteLine($"{userName}, wybierz numer konta pirwszego które chcesz połączyć?");
                        int indexA = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Teraz wybierz numer drugiego konta");
                        int indexB = Convert.ToInt32(Console.ReadLine());
                        BankAccount AcA = bankAccounts[indexA - 1];
                        BankAccount AcB = bankAccounts[indexB - 1];
                        if (indexA > 0 && indexB > 0)
                        {
                            Console.WriteLine($"Dodałeś konto na {bankAccounts[indexB - 1].accountName} do konta na {bankAccounts[indexA - 1].accountName}");
                            newAccount = AcA + AcB;
                            bankAccounts.Add(newAccount);
                            MainMenu(userName);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Złe numery!");
                            SumAccounts(bankAccounts);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }

            }

            public void ChangeAmmount(List<BankAccount> bankAccounts)
                {
                    Console.WriteLine($"{userName}, w którym koncie chcesz zmienić kwotę?");
                    for (int i = 0; i < bankAccounts.Count; i++)
                    {
                        Console.WriteLine($"Nr: {i + 1} to {bankAccounts[i].accountName}");
                    }
                try
                {
                    Console.WriteLine($"{userName}, wybierz numer konta w którym chcesz zmienić kwotę");
                    int index = Convert.ToInt32(Console.ReadLine());
                    if (index > 0)
                    {
                        try
                        {
                            Console.WriteLine($"Wpisz znak + lub - abyśmy wiedzieli czy chcesz dodać czy odjąć z konta nr {index}.");
                            string sign = Console.ReadLine();
                            Console.WriteLine("Podaj kwotę o którą chcesz zmienić wartość konta");
                            double number = Convert.ToDouble(Console.ReadLine());
                            if (sign == "+" && number > 0)
                            {
                                Console.WriteLine($"Dodajesz {number} do konta nr {index}");
                                bankAccounts[index - 1].amount += number;
                                MainMenu(userName);

                            }
                            else if (sign == "-" && number > 0)
                            {
                                Console.WriteLine($"Odejmujesz {number} od konta nr {index}");
                                bankAccounts[index - 1].amount -= number;
                                MainMenu(userName);
                            }
                            else
                            {
                                Console.WriteLine("Złe dane, spróbuj ponownie.");
                                ChangeAmmount(bankAccounts);
                            }
                        }
                        catch
                        {
                            throw;
                        }

                    }
                    else
                    {
                        Console.WriteLine("Złe numery!");
                        ChangeAmmount(bankAccounts);
                    }
                }
                catch
                {
                    throw;
                }

            }

            public void ShowAccounts(List<BankAccount> bankAccounts)
            {
                Console.WriteLine($"{userName} masz następujące konta:");
                for (int i = 0; i < bankAccounts.Count; i++)
                {
                    Console.WriteLine($"Nr: {i + 1} Na {bankAccounts[i].accountName} z kwotą {bankAccounts[i].amount}");
                }
                Console.WriteLine("\n");
                MainMenu(userName);
            }
        }

    }

}