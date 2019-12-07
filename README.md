### Visual Studio Revit API Extension

#### Revit API C# Snippets

```csharp
IEnumerable<Category> collector = new FilteredElementCollector(Document)
            .OfCategory(BuiltInCategory.MemberName
            .WhereElementIsNotElementType()
            .ToElements()
            .Cast<Category>();
```
#### Snippets Summary

| Category | Shortcut  | <div style="width:200px">Options</div> | Description |
|-----|-----|-----|-----|
|Collector| `cl`      | `collector`| Constructs a new FilteredElementCollector that will search and filter the set of elements in a document. |
|Collector| `cl`      | `collector-class-types`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-class-instances`| Applies an ElementClassFilter and ElementIsElementTypeFilter to the collector and returns Instances. |
|Collector| `cl`      | `collector-cat-types`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Types. |
|Collector| `cl`      | `collector-cat-instances`| Applies an ElementCategoryFilter  and ElementIsElementTypeFilter to the collector and returns Instances. |
