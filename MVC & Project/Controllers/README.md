## URL and Controller
***/[Controller]/[ActionName]/[Parameters]***
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

## Want more control in your Controller?? -> attribute routing