using System.IO.Compression;
using System.Text;

public class Program
{
    const int FILE_LENGTH = 8;
    const string TARGET_ZIPPED_FILE_PATH = @"c:\temp\target.gzip";

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
        Compress(filePaths);
        Decompress();
    }

    public static void Compress(List<string> paths)
    {
        if (paths.Count == 0)
        {
            Console.WriteLine("NO File provided");
            return;
        }

        using var compressorStream = new GZipStream(File.Open(TARGET_ZIPPED_FILE_PATH, FileMode.OpenOrCreate), CompressionLevel.SmallestSize, false);

        foreach (var path in paths)
        {
            if (!File.Exists(path)) continue;

            using var file = File.Open(path, FileMode.Open);
            byte[] fileLengthBuffer = new byte[FILE_LENGTH];
            var fileLength = (int)file.Length;
            var encodedFileLength = Encoding.UTF8.GetBytes(fileLength.ToString());
            encodedFileLength.CopyTo(fileLengthBuffer, 0);
            compressorStream.Write(fileLengthBuffer, 0, FILE_LENGTH);
            file.CopyTo(compressorStream);
        }
    }

    public static void Decompress()
    {
        var targetFilePath = TARGET_ZIPPED_FILE_PATH;
        if (!File.Exists(targetFilePath))
        {
            Console.WriteLine("No zipped file provided");
            return;
        }

        using var compressed = File.Open(targetFilePath, FileMode.Open);
        using var decompressorStream = new GZipStream(compressed, CompressionMode.Decompress);

        byte[] buffer;
        byte[] fileLengthBuffer = new byte[FILE_LENGTH];
        int fileLength = 0;
        int readBytes = 0;
        int fileIndex = 1;

        while (true)
        {
            if (!decompressorStream.CanRead) break;

            readBytes = decompressorStream.Read(fileLengthBuffer, 0, FILE_LENGTH);
            if (readBytes <= 0) break;
            if (!int.TryParse(Encoding.UTF8.GetString(fileLengthBuffer), out fileLength)) break;

            buffer = new byte[fileLength];
            readBytes = decompressorStream.Read(buffer, 0, fileLength);

            if (readBytes <= 0) break;

            var filePath = $"c:\\temp\\output-{fileIndex}.txt";
            var fileExists = File.Exists(filePath);

            using (var fileToWrite = File.Open(filePath, fileExists ? FileMode.Truncate : FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileToWrite.Write(buffer, 0, fileLength);
            }

            fileIndex++;
        }
    }
}
