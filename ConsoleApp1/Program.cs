
namespace Obiektowe2022Bank 
{
    internal class Program
    {
        public static string userName = "";
        static void Main(string[] args)
        {
            string enterText = "Witaj, podaj swoje imię \n";
            getUserName(enterText);
            Console.WriteLine("Zapraszamy do naszej aplikacji w której możesz stworzyć subkonta na wydatki, usunąć je i dodać do siebie");
            Console.WriteLine($"{userName} zdecyduj co chcesz zrobić:");
            Console.WriteLine("1 - Utwórz subkonto");
            Console.WriteLine("2 - Usuń subkonto");
            Console.WriteLine("3 - Dodaj subkonta");
            Console.WriteLine("4 - Zmień wartość subkonta");
            Console.ReadKey();


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



    }

}