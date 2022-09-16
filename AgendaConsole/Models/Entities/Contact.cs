namespace AgendaConsole.Models
{
    public class Contact : Register
    {
        public string Name { get; set; }
        public IEnumerable<Phone> Phones { get; set;}

        public override string ToString()
        {
            string obj = $"{Id} - {Name}\n";
            obj += "Phones:\n";
            foreach(var phone in Phones)
            {
                obj += $"{phone}\n";
            }

            return obj;
        }
    }
}
