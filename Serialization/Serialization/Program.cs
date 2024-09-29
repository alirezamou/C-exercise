public class Program
{
    private static void Main(string[] args)
    {

        if (File.Exists("users.bin"))
        {
            using (var fs = File.Open("users.bin", FileMode.Open))
            {
                var users = BinarySerializer.deserialize(fs);
                foreach (var user in users)
                    Console.WriteLine($"{user.FirstName} {user.LastName} {user.age}");
            }
        }
        else
        {
            using (var fs = File.Open("users.bin", FileMode.Create))
            {
                var u1 = new User { FirstName = "john", LastName = "doe", age = 30 };
                var u2 = new User { FirstName = "jane", LastName = "doe", age = 30 };
                var users = new List<User>();
                users.Add(u1);
                users.Add(u2);

                BinarySerializer.serialize(fs, users);
            }
        }

        if (File.Exists("users.xml"))
        {
            using (var fs = File.Open("users.xml", FileMode.Open))
            {
                var users = CustomXmlSerializer.deserialize(fs);
                foreach (var user in users)
                    Console.WriteLine($"{user.FirstName} {user.LastName} {user.age}");
            }
        }
        else
        {
            using (var fs = File.Open("users.xml", FileMode.Create))
            {
                var u1 = new User { FirstName = "john", LastName = "doe", age = 30 };
                var u2 = new User { FirstName = "jane", LastName = "doe", age = 30 };
                var users = new List<User>();
                users.Add(u1);
                users.Add(u2);

                CustomXmlSerializer.serialize(fs, users);
            }
        }
    }
}
