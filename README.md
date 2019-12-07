### Visual Studio Revit API Extension

#### Revit API C# Snippets - Code Samples

##### `cl` - `collector-cat-types` with "Walls" as Category, "doc" as Document and "OST_Walls" as Member name of BuiltInCategory. 
```csharp
IEnumerable<Walls> collector = new FilteredElementCollector(doc)
            .OfCategory(BuiltInCategory.OST_Walls)
            .WhereElementIsNotElementType()
            .ToElements()
            .Cast<Walls>();
```
#### Snippets Summary

| Category | Shortcut  | <div style="width:250px">Options</div> | Description |
|-----|-----|-----|-----|
|Collector| `cl`      | `collector`| Constructs a new FilteredElementCollector that will search and filter the set of elements in a document. |
|Collector| `cl`      | `collector-class-types`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-class-instances`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Instances. |
|Collector| `cl`      | `collector-cat-types`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-cat-instances`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Instances. |
