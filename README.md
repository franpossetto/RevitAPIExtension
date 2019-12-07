### Visual Studio Revit API Extension

#### Revit API C# Snippets - Code Samples

Use `cl` `collector` to create a collector. I'm using  `doc` variable as Document.
```csharp
FilteredElementCollector collector = new FilteredElementCollector(doc);
```
<br>
Use `cl` `collector-class-first` to get an element from class by property. I'm using  `Level` as Class, `Name` as property and `doc` variable as Document.
Level levels = new FilteredElementCollector(doc)
            .OfClass(typeof(Level))
            .Cast<Level>()
            .Where(c => c.Name.Equals("LEVEL_NAME")
            .FirstOrDefault();
<br>
Use `cl` `collector-cat-instances` to get all elements of certain Category. I'm using  `Walls` as Category, `doc` variable as Document and `OST_Walls` as BuiltInCategory Member Name.
```csharp
IEnumerable<Walls> walls = new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsNotElementType()
            .ToElements()
            .Cast<Walls>();
```
<br>

#### Snippets Summary

| Category | Shortcut  | <div style="width:250px">Options</div> | Description |
|-----|-----|-----|-----|
|Collector| `cl`      | `collector`| Constructs a new FilteredElementCollector that will search and filter the set of elements in a document. |
|Collector| `cl`      | `collector-class-types`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-class-instances`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Instances. |
|Collector| `cl`      | `collector-cat-types`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-cat-instances`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Instances. |
