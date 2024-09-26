# XML Element Extractor

This project provides a C# command-line tool to extract specific elements and their values from an XML file. The elements to be extracted are listed in an `elements.txt` file that resides in the same directory as the executable. The tool traverses the XML structure, finds the elements defined in `elements.txt`, and prints their names and values to the console.

## Features
- Extracts specific XML elements based on user-defined paths.
- Automatically creates an `elements.txt` file if it doesn't exist.
- Allows nested element searching with customizable element paths.
- Simple and easy-to-use command-line interface.

## Prerequisites
- .NET SDK installed on your machine. You can download it from [here](https://dotnet.microsoft.com/download).

## Setup & Installation

### 1. Clone the repository
Clone this repository to your local machine:
```bash
git clone https://github.com/FabrizioTavares/XMLExtractor.git
cd XMLExtractor
```
### 2. Build the project

Build the project using the .NET CLI:

```bash
dotnet build
```
### 3. Run the program

To run the program, you need an XML file that you want to parse. Use the following command to run the program:
```bash
dotnet run <path_to_xml_file>
```
Example:

```bash
dotnet run C:\path\to\your\file.xml
```
The program will automatically create an elements.txt file in the same directory as the executable if it doesn't exist. You can populate this file with the element paths you want to extract from the XML.

## The elements.txt File

The elements.txt file is used to specify the XML elements or element paths you want to extract. Each element or path must be on a new line.
Example elements.txt:

```txt
# Add your XML element paths here, one per line.
# Example: ElementName1
# Example: ParentElement/ElementName2
Grandparent/Parent/ChildElement
ElementName
```
The paths can include nested elements using a forward slash (/) to separate parent-child relationships.
Lines starting with # are treated as comments and ignored.

## If elements.txt is Missing

If the elements.txt file is missing, the program will create it automatically with the following content:

```txt
# Add your XML element paths here, one per line.
# Example: ElementName1
# Example: ParentElement/ElementName2
```
How It Works

  The program reads the XML file specified in the command line argument.
  It looks for an elements.txt file in the same directory as the executable. If the file is not present, it creates one.
  The program traverses the XML tree and extracts the elements whose paths match the paths specified in the elements.txt file.
  The results are printed to the console in the format: ElementName: Value.

Example Output

For the following XML:

```xml
<Root>
  <Parent>
    <ChildElement>12345</ChildElement>
  </Parent>
  <ElementName>MyValue</ElementName>
</Root>
```
With this elements.txt:
```txt
Parent/ChildElement
ElementName
```
The output will be:

```txt
ChildElement: 12345
ElementName: MyValue
```

### Error Handling

  If no elements.txt file is found, it is created automatically with a default template.
  If the elements.txt file is empty or contains no valid element paths, the program will notify the user and exit.
  If the XML file cannot be read or parsed (e.g., due to invalid syntax), an error message will be displayed.

## License

This project is licensed under the MIT License.

## Contributions

Feel free to fork this project, submit issues, or create pull requests to improve the functionality of the tool.

## Authors

Fabrizio
