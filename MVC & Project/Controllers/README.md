# URL and Controller
## ***/[Controller]/[ActionName]/[Parameters]***
- ```BooksController``` becomes ```Books``` in the URL
- [ActionName]: The name of the public method (action) in the controller.
- [Parameters]: Could be optional or required that is passed to the action

### Example URL: ***/Books/Details/5***

**Controller:** BooksController
```csharp
public IActionResult Details(int id){
    // what????
}
```

### Example URL: ***/Books/Search?title=C%23&sort=asc***

**Controller:** BooksController
```csharp
public IActionResult Search(string title, string sort){
    // what????
}
```


### Example URL: ***/Books/Details/5?format=pdf***

**Controller:** BooksController
```csharp
public IActionResult Details(int id, string format){
    // what????
}
```

### Example URL: ***/Books*** or ***/Books/Index***

**Controller:** BooksController
```csharp
public IActionResult Index(int? id){
    // what????
}
```


## Path & Query Parameters

### ***/[Controller]/[ActionName]/[PathParams]?[QueryParams]***
Use ```[FromRoute]``` and ```[FromQuery]``` for clarity

### Example URL Revisit: ***/Books/Details/5?format=pdf***

**Controller:** BooksController
```csharp
public IActionResult Details([FromRoute] int id, [FromQuery] string format){
    // what????
}
```

# HTTP Methods Overview

| Method   | Description                                                                 | Idempotent | Safe | Common Use Case                         |
|----------|-----------------------------------------------------------------------------|------------|------|-----------------------------------------|
| `GET`    | Retrieves data from the server (no change in server state).                | ✅         | ✅   | Fetch a book’s details                  |
| `POST`   | Sends data to create a new resource.                                        | ❌         | ❌   | Add a new book                          |
| `PUT`    | Updates an existing resource or creates it if it doesn't exist.            | ✅         | ❌   | Update or create a book                 |
| `DELETE` | Removes a resource from the server.                                         | ✅         | ❌   | Delete a book                           |
| `PATCH`  | Partially updates an existing resource.                                     | ✅\*       | ❌   | Change a book’s price                   |
| `HEAD`   | Same as `GET` but returns only headers, no body.                           | ✅         | ✅   | Check if a book exists (without body)  |
| `OPTIONS`| Returns communication options for the target resource.                     | ✅         | ✅   | Determine allowed methods (CORS, etc.) |

> \* PATCH is *technically* idempotent if designed that way, but not always in practice.

🔄 **Idempotent:** No matter how many times you perform the request, the result on the server is the same.

🛡️ **Safe:** It does not change anything on the server — it's read-only.

## attribute (e.g., [HttpGet], [HttpPost], [HttpPut], [HttpDelete], [HttpPatch], [HttpHead], [HttpOptions])

### Example URL: ***/Books/View/5?format=pdf***

**Controller:** BooksController
```csharp
[HttpGet]
public IActionResult View(int id, [FromQuery] string format){
    // what????
}
```

### Example URL: ***/Books/Create/100***

**Controller:** BooksController
**Body:** Form data or JSON (e.g., Title=C%23&Author=Smith).
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(int id, [FromForm] string Title, [FromForm] string Author){
    // what????
}
```

**Body (form):** Title=C%23 → book.Title="C#"

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(int id, [FromForm] Book book){
    // what????
}
```

> Use [FromBody] when handling JSON payloads

## Want more control in your Controller?? -> attribute routing

### Example URL: ***/Books/View/5/pdf***

**Controller:** BooksController
```csharp
[HttpGet("Books/View/{id}/{format}")]
public IActionResult View(int id, string format)
{
    // what????
}
```

### Example URL: ***/Books/Display/5/pdf***

**Controller:** BooksController
```csharp
[HttpGet("Books/Display/{id}/{format}")]
public IActionResult View(int id, string format)
{
    // what????
}
```

### Example URL: ***/Books/5/Show***

**Controller:** BooksController
```csharp
[HttpGet("Books/{id}/Show")]
public IActionResult View(int id)
{
    // what????
}
```

## Controller-Level Custom Routing
### Example URL: ***/Shop/Display/5/pdf***

```csharp
[HttpGet("Shop/Display/{id}/{format}")]
public IActionResult View(int id, string format)
{
    // what????
}
```

#### Set a route prefix at the controller level
```csharp
[Route("Shop")]
public class BooksController : Controller
{
    [HttpGet("Display/{id}/{format}")]
    public IActionResult View(int id, string format)
    {
        // what????
    }
}
```

## One Action with Multiple Accepted HTTP Methods
### Option 1: Separate Actions for Each HTTP Method
```csharp
[HttpGet("Books/Handle/{id}")]
public IActionResult Handle(int id)
{
    // Fetch book with id
    return View();
}

[HttpPost("Books/Handle/{id}")]
public IActionResult Handle(int id, [FromForm] Book book)
{
    // Save book
    return RedirectToAction("Index");
}
```
### Option 2: Combine GET and POST into One Action
```csharp
[AcceptVerbs("GET", "POST")]
[Route("Books/Handle/{id}")]
public IActionResult Handle(int id, [FromForm] Book book = null)
{
    if (Request.Method == "GET")
    {
        // Fetch book with id
        return View();
    }
    // POST: Save book
    return RedirectToAction("Index");
}
```


## One Action Mapped to Multiple URLs

### Example URL: ***/Books/Display/5*** or ***/Books/Show/5*** or ***/Books/Book/5/Details***

```csharp
[Route("Books")]
public class BooksController : Controller
{
    [HttpGet("Display/{id}")]
    [HttpGet("Show/{id}")]
    [HttpGet("Book/{id}/Details")]
    public IActionResult View(int id){
        // Fetch book with id
        return View();
    }
}
```

### Example URL: ***/Books/View/5*** or ***/Shop/Item/5***

```csharp
public class BooksController : Controller
{
    [HttpGet("Books/View/{id}")]
    [HttpGet("Shop/Item/{id}")]
    public IActionResult GetBook(int id){
        // Fetch book with id
        return View();
    }
}
```

### Example URL: ***GET: /Books/Handle/5*** or ***GET: /Books/Process/5*** or ***POST: /Books/Handle/5*** or ***POST: /Books/Process/5***

```csharp
[Route("Books")]
public class BooksController : Controller
{
    [AcceptVerbs("GET", "POST")]
    [Route("Handle/{id}")]
    [Route("Process/{id}")]
    public IActionResult Manage(int id, [FromForm] Book book = null){
        if (Request.Method == "GET")
        {
            // Fetch book with id
            return View();
        }
        // POST: Save book
        if (ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        return View(book);
    }
}
```


### Example URL: ***/Books/Handle/5*** or ***/Books/Process/5*** or ***/Store/Manage/5*** (GET or POST)

```csharp
[Route("Books")]
public class BooksController : Controller
{
    [AcceptVerbs("GET", "POST")]
    [Route("Handle/{id}")]
    [Route("Process/{id}")]
    [Route("Store/Manage/{id}")]
    public IActionResult Process(int id, [FromForm] Book book = null){
        // Dummy: Handle GET or POST
        if (Request.Method == "GET")
        {
            var bookData = _db.Books.Find(id);
            if (bookData == null) return NotFound();
            return View(bookData);
        }
        if (ModelState.IsValid)
        {
            _db.Books.Add(book);
            _db.SaveChanges();
            return RedirectToAction("List");
        }
        return View(book);
    }
}
```


### Example URL: ***/Books/Remove/5*** or ***/Books/Delete/5*** or ***/Shop/Discard/5***

```csharp
public class BooksController : Controller
{
    [HttpDelete("Books/Remove/{id}")]
    [HttpDelete("Books/Delete/{id}")]
    [HttpDelete("Shop/Discard/{id}")]
    public IActionResult Remove(int id){
        // Dummy: Delete book
        var book = _db.Books.Find(id);
        if (book == null) return NotFound();
        _db.Books.Remove(book);
        _db.SaveChanges();
        return Ok();
    }
}
```


### Example URL: ***Books/Update/5*** or ***/Books/Modify/5*** (POST or PUT)

```csharp
[Route("Books")]
public class BooksController : Controller
{
    [AcceptVerbs("POST", "PUT")]
    [Route("Update/{id}")]
    [Route("Modify/{id}")]
    public IActionResult Update(int id, [FromBody] Book book){
        // Dummy: Update book
        var existing = _db.Books.Find(id);
        if (existing == null) return NotFound();
        existing.Title = book.Title;
        existing.Author = book.Author;
        _db.SaveChanges();
        return Ok();
    }
}
```
