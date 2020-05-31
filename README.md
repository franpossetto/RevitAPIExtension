## Visual Studio Revit API Extension

![Revit API](https://img.shields.io/badge/RevitAPI-2020-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgray.svg)
![.NET](https://img.shields.io/badge/.NET-4.7-green.svg)
![VisualStudio](https://img.shields.io/badge/VisualStudio-2017-purple.svg)


This Extension provides Visual Studio Resources to help .NET Developers to Create different kind of Applications for Autodesk Revit.
Tested on Visual Studio 2017.

### With this extension, you can:
* Create different kind of Revit Plug-ins using Project Templates.
* Create Typical Files using Item Templates.
* Write Code faster using Code Snippets.
* Improve and become faster your Deploy Process. 

<br>

### Project Templates
#### 1. Revit Command Add-in
With this template you can create a Revit Push Button with a basic structure.

        └── RevitAddin
                ├── Properties
                ├── Reference
                    ├── RevitAPI.dll            
                    └── RevitAPIUI.dll
                ├── ExternalApplication.cs      # this file implement the interface IExtenralApplication
                ├── ExternalCommand.cs          # this file implement the interface IExtenralCommand
                ├── Ribbon.cs                   # this class contains methods to create ribbon items in revit.
                └── RevitAddin.addin            # manifest file


#### 2. Revit DB Application Add-in
With this template you can create an add-in and subscribe it to an event. In this template I use `DocumentOpened` event.

        └── RevitAddin
                ├── Properties
                ├── Reference
                    ├── RevitAPI.dll            
                    └── RevitAPIUI.dll
                ├── ExternalDBApplication.cs    # this file implement the interface IExtenralDBApplication
                └── RevitAddin.addin            # manifest file

<br>

### File Templates 
#### 1. External Command
Create a new External Command. I'm using `RevitAddin` as Namespace. `ExtCmd` Class implements IExternalCommand interface.

```csharp
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace RevitAddin
{
    [Transaction(TransactionMode.Manual)]
    public class ExtCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document doc = commandData.Application.ActiveUIDocument.Document;
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                return Autodesk.Revit.UI.Result.Succeeded;
            }
            catch
            {
                message = "Unexpected Exception thrown.";
                return Autodesk.Revit.UI.Result.Failed;
            }

        }
    }
}
```



#### 2. Manifest File
Create a new `Addin Manifest`. In this sample I´m calling an ExternalCommand called `ExtCmd` The manifest includes information used by Revit to load and run the plug-in.

```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<RevitAddIns>
    <AddIn Type="Command">
        <Name>RevitAddin</Name>
        <FullClassName>RevitAddin.ExtCmd</FullClassName>
        <Text>RevitExternalCommand</Text>
        <Description>A new external Command for Revit</Description>
        <VisibilityMode>AlwaysVisible</VisibilityMode>
        <Assembly>RevitAddin.dll</Assembly>
        <AddInId>
            UNIQUE_GUID
        </AddInId>
    </AddIn>
</RevitAddIns>
```

<br>

### Code Snippets
#### 1. Snippets Summary
Use the following shorcuts to use the code snippets that provided this extension: `revit`, `tr`, `cl` `get`, `pi` and `sl`. Some of them  gruops more than one snippet.

| Category | Shortcut  | <div style="width:250px">Options</div> | Description |
|-----|-----|-----|-----|
|Starter| `revit`      | `No Multiple Options`| Add Document, UIDocument, Application and UI Application objects. |
|Transaction| `tr`      | `No Multiple Options`| Instantiates a transaction object. |
|Collector| `cl`      | `collector`| Constructs a new FilteredElementCollector that will search and filter the set of elements in a document. |
|Collector| `cl`      | `collector-class-types`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-class-instances`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Instances. |
|Collector| `cl`      | `collector-cat-types`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-cat-instances`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Instances. |
|Element| `get`      | `No multiple options`| Gets the Element referenced by the input ElementId. |
|Model| `pi`      | `No multiple options`| Return the Project Information of the current project. |
|Selection| `sl`      | `pick-object`| Prompts the user to select one object. |
|Selection| `sl`      | `pick-objects`| Prompts the user to select multiple objects. |
|Selection| `sl`      | `selection`| Retrieve the currently selected Elements in Autodesk Revit. |

<br>

#### 2. Snippets Code Samples

Use `revit` to get Document,UI Document, Application and UI Application objects. 
```csharp
        UIApplication uiApp = commandData.Application;
        UIDocument uiDoc = uiApp.ActiveUIDocument;
        Application app = uiApp.Application;
        Document doc = uiDoc.Document;
```

<br>

Use `tr` to Create a simple transaction. I'm using  `doc` variable as Document.
```csharp
       using (Transaction t = new Transaction(doc, TRANSACTION_NAME))
        {
            t.Start();
            // DO something
            t.Commit();
        }
```

<br>

Use `cl` `collector` to create a collector. I'm using  `doc` variable as Document.
```csharp
FilteredElementCollector collector = new FilteredElementCollector(doc);
```
<br>

Use `cl` `collector-class-first` to get an element from class by property. I'm using  `Level` as Class, `Name` as property and `doc` variable as Document.

```csharp
Level levels = new FilteredElementCollector(doc)
            .OfClass(typeof(Level))
            .Cast<Level>()
            .Where(c => c.Name.Equals(LEVEL_NAME))
            .FirstOrDefault();
```

<br>

Use `cl` `collector-cat-types` to get all Family Types of certain Category. I'm using  `Walls` as Category, `doc` variable as Document and `OST_Walls` as BuiltInCategory Member Name.

```csharp
IEnumerable<Walls> wallsTypes = new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsNotElementType()
            .ToElements()
            .Cast<Walls>();
```

<br>

Use `cl` `collector-cat-instances` to get all elements of certain Category. I'm using  `Walls` as Category, `doc` variable as Document and `OST_Walls` as BuiltInCategory Member Name.

```csharp
IEnumerable<Walls> walls = new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsElementType()
            .ToElements()
            .Cast<Walls>();
```

<br>

Use `get` to get Element referenced by the input ElementId. I'm using  `doc` variable as Document.

```csharp
Element element = doc.GetElement(ELEMENT_ID);
```

<br>

Use `pi` to get Project Information.
```csharp
ProjectInfo pi = commandData.Application.ActiveUIDocument.Document.ProjectInformation;
```
<br>

Use `sl` `pick-object` to pick one element in Revit. I'm using uiDoc as UIDocument and `Element` as Object Type.
```csharp
Reference pickObject = uiDoc.Selection.PickObject(ObjectType.Element);
```
<br>

Use `sl` `pick-objects` to pick elements in Revit. I'm using uiDoc as UIDocument and `Element` as Object Type.
```csharp
IList<Reference> pickObjects = uiDoc.Selection.PickObjects(ObjectType.Element);
```

<br>

Use `sl` `selection` to pick one element in Revit. I'm using uiDoc as UIDocument.
```csharp
Selection selection = uiDoc.Selection;
```

<br>

### Deployment process
Some features were added to the extension in order to become faster the process to mantain every Add-in.

#### 1. Configuration Manager
Create custom configuration for each Revit version. You can have some variations
#### 2. Post-Build Event Command Line
Post built command to copy files in the correct location depending what you need.
