using System.Xml.Serialization;

public class CustomXmlSerializer
{
    public static void serialize(FileStream fs,  List<User> users)
    {
        var xmlSerializer = new XmlSerializer(typeof(List<User>));
        xmlSerializer.Serialize(fs, users);
    }
    public static List<User> deserialize(FileStream fs)
    {
        var xmlSerializer = new XmlSerializer(typeof(List<User>));
        return (List<User>)xmlSerializer.Deserialize(fs);
    }
}