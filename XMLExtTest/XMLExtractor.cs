using System.Xml;

namespace XMLExtractor
{
    public static class XmlElementExtractor
    {
        /// <summary>
        /// Entry method to process the XML file.
        /// </summary>
        /// <param name="filePath">Path to the XML file</param>
        /// <param name="elementsToFind">List of element paths to find in the XML</param>
        public static List<List<string>> ProcessXmlFile(string filePath, List<string> elementsToFind)
        {
            if (elementsToFind == null || elementsToFind.Count == 0)
            {
                Console.WriteLine("The elements to find list must contain at least one item.");
                return [];
            }

            XmlDocument xmlDoc = new();
            var results = new List<List<string>>();

            try
            {
                xmlDoc.Load(filePath);
            }
            catch (XmlException e)
            {
                Console.WriteLine($"Error parsing the XML file: {e.Message}");
                return results;
            }

            XmlNode? root = xmlDoc.DocumentElement;
            if (root != null)
            {
                results = TraverseXml(root, "", elementsToFind);
            }

            return results;
        }

        /// <summary>
        /// Recursively traverses the XML tree to find matching elements.
        /// </summary>
        /// <param name="element">The current XML element</param>
        /// <param name="parentPath">The path of parent elements</param>
        /// <param name="elementsToFind">List of element paths to find in the XML</param>
        /// <returns>A bidimensional list containing element names and their values</returns>
        private static List<List<string>> TraverseXml(XmlNode element, string parentPath, List<string> elementsToFind)
        {
            var matchedElements = new List<List<string>>();

            // Check if the node is an element node
            if (element.NodeType == XmlNodeType.Element)
            {
                string tagName = StripNamespace(element.Name);

                // Update the parent path to include the current element
                string newParentPath = string.IsNullOrEmpty(parentPath) ? tagName : $"{parentPath}/{tagName}";

                // Check if the current path matches any pattern in elementsToFind
                if (MatchesPath(newParentPath, elementsToFind))
                {
                    matchedElements.Add([tagName, element.InnerText.Trim()]);
                }

                // Recursively process child nodes
                foreach (XmlNode child in element.ChildNodes)
                {
                    var childResults = TraverseXml(child, newParentPath, elementsToFind);
                    matchedElements.AddRange(childResults); // Merge child results into the main list
                }
            }

            return matchedElements;
        }

        /// <summary>
        /// Checks if the given element path matches any pattern in the elementsToFind list.
        /// </summary>
        /// <param name="elementPath">The path of the current element</param>
        /// <param name="elementsToFind">List of element paths to find in the XML</param>
        /// <returns>True if a match is found, otherwise False</returns>
        private static bool MatchesPath(string elementPath, List<string> elementsToFind)
        {
            return elementsToFind.Any(elementPath.Contains);
        }

        /// <summary>
        /// Strips the namespace from the element's tag name if present.
        /// </summary>
        /// <param name="tag">The tag name with namespace</param>
        /// <returns>The tag name without namespace</returns>
        private static string StripNamespace(string tag)
        {
            int index = tag.IndexOf('}');
            return index != -1 ? tag.Substring(index + 1) : tag;
        }
    }
}