using XMLExtractor;

class Program
{
    static void Main(string[] args)
    {
        // Check if the required argument (XML file) is provided
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: Drag and drop a XML file into this program.");
            Console.ReadLine();
            return;
        }

        string xmlFilePath = args[0];  // First argument: Path to the XML file

        // Define the path for the elements.txt file in the same directory as the executable
        string elementsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "elements.txt");

        // Ensure the elements.txt file exists, or create it if it doesn't
        List<string> elementsToFind = EnsureElementsFileExists(elementsFilePath);

        // If the elements list is empty, show an error message
        if (elementsToFind.Count == 0)
        {
            Console.WriteLine("The elements file is empty. Please specify at least one element.");
            Console.ReadLine();
            return;
        }

        // Create an instance of the XmlElementExtractor and process the XML file
        List<List<string>> results = XmlElementExtractor.ProcessXmlFile(xmlFilePath, elementsToFind);

        // Print results
        foreach (var item in results)
        {
            Console.WriteLine($"{item[0]}: {item[1]}");
        }
        Console.ReadLine();
    }

    /// <summary>
    /// Ensures the elements file exists. If it doesn't, it creates the file with default content.
    /// </summary>
    /// <param name="filePath">Path to the elements file</param>
    /// <returns>A list of element paths to find</returns>
    private static List<string> EnsureElementsFileExists(string filePath)
    {
        var elementsToFind = new List<string>();

        // Check if the file exists
        if (!File.Exists(filePath))
        {
            // Create a new file with a default message
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("# Add your XML element paths here, one per line.");
                sw.WriteLine("# Example: ElementName1");
                sw.WriteLine("# Example: ParentElement/ElementName2");
                sw.WriteLine("# Example: GrandparentElement/ParentElement/ElementName3");
            }

            Console.WriteLine($"The file '{filePath}' was not found, so it has been created. Please populate it with the elements you want to find.");
        }
        else
        {
            // Read all lines from the existing file and add them to the list
            elementsToFind.AddRange(File.ReadAllLines(filePath));
        }

        // Remove any comment lines or empty lines
        elementsToFind = elementsToFind.FindAll(line => !string.IsNullOrWhiteSpace(line) && !line.StartsWith('#'));

        return elementsToFind;
    }
}
