# Controllers to Views
In ASP.NET Core MVC, **Controllers** handle HTTP requests and return **Views** to show the user interface.

## Basic Controller-to-View Mapping
```csharp
public class BooksController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```
Loads ***Views/Books/Index.cshtml***

```csharp
return View("CreateForm");
```
Loads ***Views/Books/CreateForm.cshtml***

## Passing Data to Views
### Passing a Model
```csharp
return View(book);
```
Pass a Book object to the view:
```html
@model Book

<h2>@Model.Title</h2>
<p>Author: @Model.Author</p>
<p>Price: $@Model.Price</p>
```

### ViewData
```csharp
ViewData["Publisher"] = "Harry Potter";
return View();
```
Access in view:
```html
<p>Publisher: @ViewData["Publisher"]</p>
```

### ViewBag
```csharp
ViewBag.Format = "Hardcover";
return View();
```
Access in view:
```html
<p>Format: @ViewBag.Format</p>
```

## Form Submission and Handling
### Example: Creating a New Book
#### View (Create.cshtml)
A simple form to add a book:
```html
@model Book

<h2>Add a Book</h2>

<form asp-action="Create" method="post">
    <div>
        <label>Title</label>
        <input asp-for="Title" />
    </div>
    <div>
        <label>Author</label>
        <input asp-for="Author" />
    </div>
    <div>
        <label>Price</label>
        <input asp-for="Price" type="number" />
    </div>
    <button type="submit">Add Book</button>
</form>
```

#### Controller (BooksController.cs)
Handles showing the form and saving the book:
```csharp
public class BooksController : Controller
{
    // Show the form
    public IActionResult Create()
    {
        return View();
    }

    // Handle form submission
    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (ModelState.IsValid)
        {
            // Save book to database
            return RedirectToAction("Index");
        }
        // Show form again if there are errors
        return View(book);
    }
}
```


#### Model (Book.cs)
Defines the book with simple rules:
```csharp
public class Book
{
    public int Id { get; set; } // Primary key for the database
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
}
```

#### Updated Controller (BooksController.cs)
```csharp
public class BooksController : Controller
{
    private readonly AppDbContext _context; // Database context

    // Constructor to inject the database context
    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    // Show the form
    public IActionResult Create()
    {
        return View();
    }

    // Handle form submission
    [HttpPost]
    public IActionResult Create(Book book)
    {
        if (ModelState.IsValid)
        {
            // Save book to database
            _context.Books.Add(book); // Add book to the Books table
            _context.SaveChanges();   // Save changes to the database
            return RedirectToAction("Index");
        }
        // Show form again if there are errors
        return View(book);
    }
}
```

### Explanation
1. **Form in View**: The form lets users enter book details. It sends data to the `Create` action when submitted.
2. **GET Action**: The `Create` action shows the empty form.
3. **POST Action**: The `Create` action with `[HttpPost]` processes the form. If the data is valid, it saves the book and redirects to `Index`. If not, it shows the form again.
4. **Model**: The `Book` class holds the book’s data (title, author, price).

This is a simple example for beginners to understand form submission in ASP.NET Core MVC.