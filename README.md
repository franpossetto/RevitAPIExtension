## Visual Studio Revit API Extension

![.NET](https://img.shields.io/badge/.NET-4.7-green.svg)
![VisualStudio](https://img.shields.io/badge/VisualStudio-2017-purple.svg)
![VisualStudio](https://img.shields.io/badge/VisualStudio-2019-purple.svg)
![Revit API](https://img.shields.io/badge/RevitAPI-2017-blue.svg)
![Revit API](https://img.shields.io/badge/RevitAPI-2018-blue.svg)
![Revit API](https://img.shields.io/badge/RevitAPI-2019-blue.svg)
![Revit API](https://img.shields.io/badge/RevitAPI-2020-blue.svg)

This Extension provides Visual Studio Resources to help .NET Developers to Create different kind of Applications for Autodesk Revit.
Tested on Visual Studio 2017.

### With this extension, you can:
* Create different kind of Revit Plug-ins using Project Templates.
* Create Typical Files using Item Templates.
* Write Code faster using Code Snippets.
* Improve and become faster your Deploy Process. 

<br>

### Project Templates
#### 1. Revit Command
Create a Revit Command with a Push Button in the Ribbon Bar

#### 2. Revit Event DB Application
Create a Revit DB Application associated to DocumentOpened Event Handler

#### 3. Revit Event Application
Create a Revit Application associated to DocumentOpened Event Handler
<br>

### File Templates 
#### 1. Application Manifest
Create a new Application Manifest for your Revit Add-in

#### 2. DBApplication Manifest
Create a new DBApplication Manifest for your Revit Add-in

#### 3. Command Manifest
Create a new Command Manifest for your Revit Add-in

#### 4. External Application
Create a new External Application

#### 5. External Command
Create a new External Command

<br>

### Code Snippets
#### 1. Snippets Summary
Use the following shorcuts to use the code snippets that provided this extension: `revit`, `tr`, `cl` `get`, `pi` and `sl`. Some of them  gruops more than one snippet.

| Category | Shortcut  | <div style="width:250px">Options</div> | Description |
|-----|-----|-----|-----|
|Starter| `revit`      | `Multiple Options`| Add Document, UIDocument, Application and UI Application objects. |
|Starter| `revit`      | `Multiple Options`| Check Revit version. |
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

### Deployment process
Some features were added to the extension in order to become faster the process to mantain every Add-in.

#### 1. Configuration Manager
Create custom configuration for each Revit version. You can have some variations
#### 2. Post-Build Event Command Line
Post built command to copy files in the correct location depending what you need.
