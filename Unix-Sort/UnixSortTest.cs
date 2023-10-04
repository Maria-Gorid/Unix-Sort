using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Unix_Sort;

public class UnixSortTest
{
    [Test]
    public void SimpleTest()
    {
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        Program.Main(new []{"file1.txt", "file2.txt", "file3.txt"});
        String expected = "1441\r\n2131\r\n2325\r\n321123\r\n4454\r\n546465\r\n565\r\n651\r\n778\r\n9665\r\n99888\r\n";
        Assert.That(sw.ToString(), Is.EqualTo(expected));
    }    
    
    [Test]
    public void NumericTest()
    {
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        Program.Main(new []{"file1.txt", "file2.txt", "file3.txt", "-n"});
        String expected = "565\r\n651\r\n778\r\n1441\r\n2131\r\n2325\r\n4454\r\n9665\r\n99888\r\n321123\r\n546465\r\n";
        Assert.That(sw.ToString(), Is.EqualTo(expected));
    }    
    
    [Test]
    public void FileOutputTest()
    {
        StringWriter sw = new StringWriter();
        Console.SetOut(sw);
        Program.Main(new []{"file1.txt", "file2.txt", "file3.txt", "-o=output.txt"});
        String expected = "1441\r\n2131\r\n2325\r\n321123\r\n4454\r\n546465\r\n565\r\n651\r\n778\r\n9665\r\n99888\r\n";
        string[] strings = File.ReadAllLines("output.txt");
        Assert.That(String.Join("\r\n", strings) + "\r\n", Is.EqualTo(expected));
    }

    [Test]
    public void FileNotFoundTest()
    {
        Assert.Throws<FileNotFoundException>(() =>
        {
            Program.SortFiles(new List<string>() {"file4.txt"}, new Dictionary<string, string>());
        });
    }
    
    
}