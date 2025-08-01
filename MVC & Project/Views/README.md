# Controllers to Views
In ASP.NET Core MVC, **Controllers** handle incoming HTTP requests and can return **Views** to render the UI.
## Basic Controller-to-View Mapping
```csharp
public class BooksController : Controller
{
    public IActionResult Index(){
        return View();
    }
}
```
loads ***Views/Books/Index.cshtml***

```csharp
return View("CreateForm");
```
loads ***Views/Books/CreateForm.cshtml***

```csharp
return View("~/Views/Shared/BookDetails.cshtml");
```
loads ***Views/Shared/BookDetails.cshtml***

## passing data to Views
### Passing a Model
```csharp
return View(book);
```
Passing one Book object to a view and then access it like this:
```html
@model Book

<h2>@Model.Title</h2>
<p>Author: @Model.Author</p>
<p>Price: $@Model.Price.ToString("F2")</p>
```

### ViewData
```csharp
ViewData["publisher"] = "Harry Potter";
return View();
```
Accessing in View:
```html
<p>Price: @ViewData["publisher"]</p>
```
#### ViewData with model
```csharp
ViewData["publisher"] = "Harry Potter";
return View(book);
```
Accessing in View:
```html
@model Book

<h2>@Model.Title</h2>
<p>Author: @Model.Author</p>
<p>Price: $@Model.Price.ToString("F2")</p>
<p>Price: @ViewData["publisher"]</p>
```
### ViewBag
```csharp
ViewBag.Format = "Hardcover";
return View();
```
Accessing in View:
```html
<p>Format: @ViewBag.Format</p>
```
#### Combining ViewBag, ViewData, and Model
```csharp
ViewBag.Format = "Hardcover";
ViewData["publisher"] = "Harry Potter";
return View(book);
```
Accessing in View:
```html
@model Book

<h2>@Model.Title</h2>
<p>Author: @Model.Author</p>
<p>Price: $@Model.Price.ToString("F2")</p>
<p>publisher: @ViewData["publisher"]</p>
<p>Format: @ViewBag.Format</p>
```