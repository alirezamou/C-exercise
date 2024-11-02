using System.IO.Compression;
using System.Text;

public class Program
{
    public static readonly string seperator = "%end%";
    public static readonly byte[] encodedSeperator = Encoding.UTF8.GetBytes("%end%");

    public static void Main(string[] args)
    {
        var filePaths = new List<string>()
        {
            @"c:\temp\file01.txt",
            @"c:\temp\file02.txt",
            @"c:\temp\file03.txt",
            @"c:\temp\file04.txt",
            @"c:\temp\file05.txt",
            @"c:\temp\file06.txt",
            @"c:\temp\file07.txt",
            @"c:\temp\file08.txt",
            @"c:\temp\file09.txt",
            @"c:\temp\file10.txt",
        };
        WriteTenFilesIntoOneCompressedFile(filePaths);
        //ReadAndWriteCompressedFile();
    }

    public static void WriteTenFilesIntoOneCompressedFile(List<string> paths)
    {
        using var compressorStream = new GZipStream(File.Open(@"c:\temp\target.gzip", FileMode.OpenOrCreate), CompressionLevel.SmallestSize, false);
        foreach (var path in paths)
        {
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            fileStream.CopyTo(compressorStream);
            compressorStream.Write(encodedSeperator);
        }

    }

    public static void ReadAndWriteCompressedFile()
    {
        using var compressed = File.Open(@"c:\temp\target.gzip", FileMode.Open);
        using var decompressorStream = new GZipStream(compressed, CompressionMode.Decompress);
        using var origStream = new MemoryStream();

        decompressorStream.CopyTo(origStream);

        var allContent = "";
        origStream.Position = 0;
        var buffer = new byte[1024];
        while (origStream.Read(buffer) > 0)
            allContent += Encoding.UTF8.GetString(buffer);
        var fileContents = allContent.Split("%end%");
        foreach (var file in fileContents)
            Console.WriteLine(file);
    }
}
