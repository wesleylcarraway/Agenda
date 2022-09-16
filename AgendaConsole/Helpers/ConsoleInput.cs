namespace AgendaConsole.Helpers
{
    public class ConsoleInput
    {
        public int ReadNumber(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                Console.Write(message);


            string input = Console.ReadLine();
            int result;

            if(String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Enter some value!");
                result = ReadNumber();
            }
            else if(!int.TryParse(input, out result))
            {
                Console.WriteLine("Please enter a valid value!");
                result = ReadNumber();
            }

            return result;
        }

        public string ReadString(string message = null)
        {
            if (!string.IsNullOrEmpty(message))
                Console.Write(message);

            var input = Console.ReadLine();

            if(String.IsNullOrEmpty(input))
            {
                Console.WriteLine("Enter some value!");
                input = ReadString();
            }
            return input;
        }
    }
}
