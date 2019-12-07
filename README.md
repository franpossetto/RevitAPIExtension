### Visual Studio Revit API Extension

#### Snippets Summary

| Category | Shortcut  | <div style="width:250px">Options</div> | Description |
|-----|-----|-----|-----|
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

#### Snippets - Code Samples

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

Use `sl` `pick-objects` to pick one element in Revit. I'm using uiDoc as UIDocument and `Element` as Object Type.
```csharp
IList<Reference> pickObjects = uiDoc.Selection.PickObjects(ObjectType.Element);
```

<br>

Use `sl` `pselection` to pick one element in Revit. I'm using uiDoc as UIDocument and `Element` as Object Type.
```csharp
Selection selection = uiDoc.Selection;
```

