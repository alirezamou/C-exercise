
#pragma warning disable SYSLIB0011
using System.Runtime.Serialization.Formatters.Binary;

class BinarySerializer
{
    public static void serialize(FileStream fs, List<User> users)
    {
        var serializer = new BinaryFormatter();
        serializer.Serialize(fs, users);
    }
    public static List<User> deserialize(FileStream fs)
    {
        var serializer = new BinaryFormatter();
        return (List<User>)serializer.Deserialize(fs);
    }
}
