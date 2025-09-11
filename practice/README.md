# C# Practice Questions README

This document outlines solutions to a set of C# practice questions covering various programming concepts, including data annotations, LINQ, Razor views, custom exceptions, delegates, aggregation, model relationships, and ASP.NET Core controller actions. Each question is designed to test specific skills and is presented with its solution and explanation.

## Table of Contents
1. [Model with Data Annotations (Student Profile)](#1-model-with-data-annotations-student-profile)
2. [LINQ Query (Music Streaming Playlist)](#2-linq-query-music-streaming-playlist)
3. [Displaying Data in a Razor View (Travel Agency)](#3-displaying-data-in-a-razor-view-travel-agency)
4. [Custom Exception (Fitness Tracking App)](#4-custom-exception-fitness-tracking-app)
5. [Multicasting Delegates (E-commerce Notifications)](#5-multicasting-delegates-e-commerce-notifications)
6. [Aggregation with List (Bookstore Inventory)](#6-aggregation-with-list-bookstore-inventory)
7. [Relation Handling in Models (Hospital Management)](#7-relation-handling-in-models-hospital-management)
8. [Displaying Data with ViewBag and Foreach Loop (Restaurant Menu)](#8-displaying-data-with-viewbag-and-foreach-loop-restaurant-menu)
9. [Configuring Controller Actions with HTTP Methods and Routes (Pet Adoption)](#9-configuring-controller-actions-with-http-methods-and-routes-pet-adoption)
10. [Explain how data is transacted through the MVC layers (Ride Sharing)](#10-when-a-user-books-a-ride-by-clicking-a-book-now-button-from-the-action-to-saving-the-trip-in-the-database)



---

## 1. Model with Data Annotations (Student Profile)
**Task**: Create a `Student` class with properties `Id`, `Name`, `Email`, and `Age`, using `System.ComponentModel.DataAnnotations` to enforce validation (all required, valid email, age 18–30).

**Solution**:
```csharp
using System.ComponentModel.DataAnnotations;

public class Student
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Age is required")]
    [Range(18, 30, ErrorMessage = "Age must be between 18 and 30")]
    public int Age { get; set; }
}
```

**Explanation**:
- `[Required]` ensures non-null values with custom error messages.
- `[EmailAddress]` validates email format.
- `[Range(18, 30)]` restricts `Age` to 18–30 inclusive.

---

## 2. Model with Data Annotations and Custom Validation (Event Management)

**Task**: Create an `Event` class with properties `EventName`, `EventDate`, `RegistrationDeadline` and `LastUpdated`. Enforce the following validations:

* `EventName`: Required, 5–50 characters
* `EventDate`: Required, must be **today or later**
* `RegistrationDeadline`: Required, must be **before EventDate**
* `LastUpdated`: Required, must be **today or earlier** (cannot be a future update timestamp)

Additionally, implement **three separate methods** to display both dates in different formats, and **one method** to display the number of days remaining until the event:

| Method Name               | Format / Description                | Example Output                     |
| ------------------------- | ----------------------------------- | ---------------------------------- |
| `PrintDates_DDMMYYYY()`   | dd-MM-yyyy                          | 31-12-2025                         |
| `PrintDates_MMMMddyyyy()` | MMMM dd, yyyy                       | December 31, 2025                  |
| `PrintDates_YYYYMMDD()`   | yyyy/MM/dd                          | 2025/12/31                         |
| `PrintDaysRemaining()`    | Number of days left until the event | Days remaining until the event: 10 |
| `PrintLastUpdated()`      | LastUpdated date and time           | Last updated on 31-12-2025 14:30   |


**Solution**:


**Way-1:With data annotation and custom validation**
```csharp
public class Event : IValidatableObject
{
    [Required, MinLength(5), MaxLength(50)]
    public string EventName { get; set; }

    [Required]
    public DateTime EventDate { get; set; }

    [Required]
    public DateTime RegistrationDeadline { get; set; }

    [Required]
    public DateTime LastUpdated { get; set; }

    // Implement all validations here
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EventDate.Date < DateTime.Today)
            yield return new ValidationResult("Event Date cannot be in the past", new[] { nameof(EventDate) });

        if (RegistrationDeadline.Date >= EventDate.Date)
            yield return new ValidationResult("Registration Deadline must be before Event Date", new[] { nameof(RegistrationDeadline) });

        if (LastUpdated > DateTime.Now)
            yield return new ValidationResult("LastUpdated cannot be a future date/time", new[] { nameof(LastUpdated) });
    }

    // Method 1: dd-MM-yyyy
    public void PrintDates_DDMMYYYY()
    {
        Console.WriteLine("Event Date: " + EventDate.ToString("dd-MM-yyyy"));
        Console.WriteLine("Registration Deadline: " + RegistrationDeadline.ToString("dd-MM-yyyy"));
    }

    // Method 2: MMMM dd, yyyy
    public void PrintDates_MMMMddyyyy()
    {
        Console.WriteLine("Event Date: " + EventDate.ToString("MMMM dd, yyyy"));
        Console.WriteLine("Registration Deadline: " + RegistrationDeadline.ToString("MMMM dd, yyyy"));
    }

    // Method 3: yyyy/MM/dd
    public void PrintDates_YYYYMMDD()
    {
        Console.WriteLine("Event Date: " + EventDate.ToString("yyyy/MM/dd"));
        Console.WriteLine("Registration Deadline: " + RegistrationDeadline.ToString("yyyy/MM/dd"));
    }

    // Method 4: Days remaining until event
    public void PrintDaysRemaining()
    {
        int daysRemaining = (EventDate.Date - DateTime.Today).Days;
        Console.WriteLine($"Days remaining until the event: {daysRemaining} day(s)");
    }

    // Method 5: Print LastUpdated with date and time
    public void PrintLastUpdated()
    {
        Console.WriteLine("Last updated on: " + LastUpdated.ToString("dd-MM-yyyy HH:mm"));
    }
}

```


**Way-2: Constructor Validation**
```csharp
public class Event
{
    public string EventName { get; private set; }
    public DateTime EventDate { get; private set; }
    public DateTime RegistrationDeadline { get; private set; }
    public DateTime LastUpdated { get; private set; }

    // Constructor enforces all validations
    public Event(string eventName, DateTime eventDate, DateTime registrationDeadline, DateTime lastUpdated)
    {
        if (string.IsNullOrWhiteSpace(eventName) || eventName.Length < 5 || eventName.Length > 50)
            throw new ArgumentException("Event Name must be 5-50 characters");

        if (eventDate.Date < DateTime.Today)
            throw new ArgumentException("Event Date cannot be in the past");

        if (registrationDeadline.Date >= eventDate.Date)
            throw new ArgumentException("Registration Deadline must be before Event Date");

        if (lastUpdated > DateTime.Now)
            throw new ArgumentException("LastUpdated cannot be a future date/time");

        EventName = eventName;
        EventDate = eventDate;
        RegistrationDeadline = registrationDeadline;
        LastUpdated = lastUpdated;
    }

    // Display methods same as Way-1
    public void PrintDates_DDMMYYYY() => Console.WriteLine($"Event Date: {EventDate:dd-MM-yyyy}, Registration Deadline: {RegistrationDeadline:dd-MM-yyyy}");
    public void PrintDates_MMMMddyyyy() => Console.WriteLine($"Event Date: {EventDate:MMMM dd, yyyy}, Registration Deadline: {RegistrationDeadline:MMMM dd, yyyy}");
    public void PrintDates_YYYYMMDD() => Console.WriteLine($"Event Date: {EventDate:yyyy/MM/dd}, Registration Deadline: {RegistrationDeadline:yyyy/MM/dd}");
    public void PrintDaysRemaining() => Console.WriteLine($"Days remaining until event: {(EventDate.Date - DateTime.Today).Days} day(s)");
    public void PrintLastUpdated() => Console.WriteLine($"Last updated on: {LastUpdated:dd-MM-yyyy HH:mm}");
}
```
**Explanation**:

* `[Required]` ensures **EventName, EventDate, and RegistrationDeadline** are provided.
* `[MinLength]` and `[MaxLength]` restrict **EventName length** to 5–50 characters.
* `ValidateEventDate` ensures **EventDate is today or later**.
* `ValidateRegistrationDeadline` ensures **registration ends before the event date**.
* Three methods display dates in **different formats**: `"dd-MM-yyyy"`, `"MMMM dd, yyyy"`, `"yyyy/MM/dd"`.
* `PrintDaysRemaining()` calculates and prints **days left until the event**.
* `PrintLastUpdated()` displays **LastUpdated date and time** in `dd-MM-yyyy HH:mm` format.

---

## 3. LINQ Query (Music Streaming Playlist)
**Task**: Implement `FilterAndSortSongs` to filter songs with `Duration > 180` seconds and sort by `Title` ascending.
Task: Complete the FilterAndSortSongs method to use LINQ to:
- Accept a List<Song> parameter.
- Return the result as a List<Song>.

```csharp
using System.Collections.Generic;


public class Song
{
    public string Title { get; set; }
    public int Duration { get; set; }
}


public class SongManager
{
    public List<Song> FilterAndSortSongs(List<Song> songs)
    {
        // Complete the LINQ query
    }
}


// For reference (not to be written by students)
public class Program
{
    public static void Main()
    {
        List<Song> songs = new List<Song>
        {
            new Song { Title = "Song A", Duration = 200 },
            new Song { Title = "Song B", Duration = 150 },
            new Song { Title = "Song C", Duration = 190 }
        };
        SongManager manager = new SongManager();
        List<Song> filteredSongs = manager.FilterAndSortSongs(songs);
    }
}

```
**Solution**:
```csharp
using System.Collections.Generic;
using System.Linq;

public class Song
{
    public string Title { get; set; }
    public int Duration { get; set; }
}

public class SongManager
{
    public List<Song> FilterAndSortSongs(List<Song> songs)
    {
        return songs
            .Where(s => s.Duration > 180)
            .OrderBy(s => s.Title)
            .ToList();
    }
}
```

**Explanation**:
- `Where` filters songs with `Duration > 180`.
- `OrderBy` sorts by `Title` ascending.
- `ToList()` returns the filtered and sorted list.

---

## 4. Displaying Data in a Razor View (Travel Agency)

**Scenario:** A travel agency website displays tour details on a web page. The controller passes a Tour object to the view using return View(tour);.
**Task:** Complete the Razor view to:
- Specify the model type.
- Display Destination and Price with labels.
- Show "Tour not found" if the model is null.

```csharp
// For reference (not to be written by students)
public class Tour
{
    public string Destination { get; set; }
    public decimal Price { get; set; }
}
```

**Solution**:
```razor
@model Tour
@if (Model == null)
{
    <p>Tour not found</p>
}
else
{
    <h2>@Model.Destination</h2>
    <p>Price: $@Model.Price</p>
}
```

**Explanation**:
- `@model Tour` sets the model type.
- `if-else` checks for null model, displaying "Tour not found" or `Destination` and `Price`.

---

## 5. Custom Exception (Fitness Tracking App)
**Task**: Define `InvalidCaloriesException` and `ValidateCalories` to throw it if calories are not between 50 and 1000.

**Solution**:
```csharp
public class InvalidCaloriesException : Exception
{
    public InvalidCaloriesException(string message) : base(message)
    {
    }
}

public void ValidateCalories(int calories)
{
    if (calories < 50 || calories > 1000)
    {
        throw new InvalidCaloriesException("Calories must be between 50 and 1000.");
    }
}
```

**Explanation**:
- `InvalidCaloriesException` inherits from `Exception` with a message constructor.
- `ValidateCalories` checks calorie range and throws the exception if invalid.

---

## 6. Multicasting Delegates (E-commerce Notifications)
**Task**: Define a delegate `OrderNotification` and use it to invoke `SendEmail` and `LogOrder` in `TriggerNotifications`.

**Solution**:
```csharp
public delegate void OrderNotification(string message);

public void SendEmail(string message)
{
    Console.WriteLine($"Email: {message}");
}

public void LogOrder(string message)
{
    Console.WriteLine($"Log: {message}");
}

public void TriggerNotifications(string message)
{
    OrderNotification notification = SendEmail;
    notification += LogOrder;
    notification?.Invoke(message);
}
```

**Explanation**:
- `OrderNotification` is a delegate for methods with a `string` parameter.
- `SendEmail` and `LogOrder` are added to the delegate using `+=`.
- `?.Invoke` safely calls all attached methods.

---

## 7. Aggregation with List (Bookstore Inventory)
A bookstore application manages book inventories. Each bookstore contains a list of books in its stock, representing an aggregation relationship. The book data is provided when the bookstore is initialized, as the books are inherent to the library.

**Task:**

a. Define the Book class with properties Id, Title, and Price (must be positive). Use data annotations to ensure Title is required and Price is positive.

b. Complete the BookStore class to:
- Define a List<Book> property to represent the aggregation.
- Implement a constructor that accepts a List<Book> parameter to initialize the Books property.
- Implement the GetAvailableBooks method to use LINQ to filter books with Price < 50 from the aggregated list and return the filtered list.

c. Write a Main method to:
- Create a List<Book> with 3 sample books (with varying prices).
- Create a BookStore instance, passing the book list to the constructor.

**Solution**:
```csharp
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be positive")]
    public double Price { get; set; }
}

public class BookStore
{
    public List<Book> Books { get; set; }

    public BookStore(List<Book> books)
    {
        Books = books;
    }

    public List<Book> GetAvailableBooks()
    {
        return Books
            .Where(b => b.Price < 50)
            .ToList();
    }
}

public class Program
{
    public static void Main()
    {
        List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Book A", Price = 45.99 },
            new Book { Id = 2, Title = "Book B", Price = 60.00 },
            new Book { Id = 3, Title = "Book C", Price = 30.50 }
        };
        BookStore store = new BookStore(books);
        List<Book> availableBooks = store.GetAvailableBooks();
    }
}
```

**Explanation**:
- `Book` uses `[Required]` and `[Range]` for validation.
- `BookStore` has a `List<Book>` property, initialized via constructor.
- `GetAvailableBooks` filters books with `Price < 50`.
- `Main` creates sample books and tests the functionality.

---

## 8. One-to-Many Relation Handling in Models with EF Core
**Task**: Define `Department` and `Doctor` classes with a one-to-many relationship using data annotations.

**Solution**:
```csharp
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Department
{
    [Key]
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    public List<Doctor> Doctors { get; set; }
}

public class Doctor
{
    [Key]
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Department is required")]
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    public Department Department { get; set; }
}
```

**Explanation**:
* `Department` has `Id` (primary key), `Name`, and `Doctors` (navigation property) representing the **one-to-many relationship**.
* `Doctor` has `Id`, `Name`, `DepartmentId` (foreign key), and `Department` (navigation property).
* `[ForeignKey]` explicitly tells EF Core which property is the foreign key for the navigation property.
* EF Core uses this information to **generate proper database tables, columns, and constraints**.

---

## 9. One-to-Many Relation Handling in Models with just C# Classes
**Task**: Define `Department` and `Doctor` classes with a one-to-many relationship.

**Solution**:
```csharp
public class Department
{

    public int Id { get; set; }

    public string Name { get; set; }

}

public class Doctor
{

    public int Id { get; set; }

    public string Name { get; set; }

    public int DepartmentId { get; set; }

}
```

**Explanation**:
* `Department` has `Id` and `Name`.
* `Doctor` has `Id`, `Name`, and `DepartmentId` to indicate which department it belongs to.
* No `[ForeignKey]` or navigation property is required, because **relationships are just conceptual** in memory.
* This version is **suitable for in-memory C# objects** or **non-database scenarios**.

---


## 10. Many-to-Many Relation Handling in Models with just C# Classes

**Scenario:**
You are designing a simple **School Attendance System** using only **C# classes (no database)**. The system has the following entities:

1. **Student**

   * Attributes: `StudentID` (int), `Name` (string), `Grade` (string)
   * Functions: `DisplayStudentDetails()`

2. **ClassSession**

   * Attributes: `SessionID` (int), `Subject` (string), `Date` (DateTime)
   * Functions: `DisplaySessionDetails()`

3. **AttendanceMapping**

   * Attributes: `MappingID` (int), `StudentID` (int), `SessionID` (int), `Status` (string)
   * Functions: `MarkAttendance()`, `DisplayMappingDetails()`

**Task:**

* Write the **C# class fragments** with **attributes and functions**.
**Solution**:
```csharp
public class Student
{
    public int StudentID { get; set; }
    public string Name { get; set; }
    public string Grade { get; set; }

    // Function to display student details
    public void DisplayStudentDetails()
    {
        Console.WriteLine($"StudentID: {StudentID}, Name: {Name}, Grade: {Grade}");
    }
}

public class ClassSession
{
    public int SessionID { get; set; }
    public string Subject { get; set; }
    public DateTime Date { get; set; }

    // Function to display session details
    public void DisplaySessionDetails()
    {
        Console.WriteLine($"SessionID: {SessionID}, Subject: {Subject}, Date: {Date.ToShortDateString()}");
    }
}

public class AttendanceMapping
{
    public int MappingID { get; set; }
    public int StudentID { get; set; }   // Conceptual link to Student
    public int SessionID { get; set; }   // Conceptual link to ClassSession
    public string Status { get; set; }

    // Function to mark attendance
    public void MarkAttendance(string status)
    {
        Status = status;
        Console.WriteLine($"Attendance marked as: {Status}");
    }

    // Function to display mapping details
    public void DisplayMappingDetails()
    {
        Console.WriteLine($"MappingID: {MappingID}, StudentID: {StudentID}, SessionID: {SessionID}, Status: {Status}");
    }
}
```

**Explanation:**

* `Student` has `StudentID`, `Name`, and `Grade`.
* `ClassSession` has `SessionID`, `Subject`, and `Date`.
* `AttendanceMapping` has `MappingID`, `StudentID`, `SessionID`, and `Status` to indicate which student attended which session.
* Functions like `DisplayStudentDetails()` or `MarkAttendance()` are included for **logic and display**, but **relationships are just conceptual**.
* This version is **suitable for in-memory C# objects** or **non-database scenarios**.

---

## 11. Many-to-Many Relation Handling in Models with EF Core

**Scenario:**
You are designing a **School Attendance System** using **EF Core**, which will map your classes to a database. The system has the following entities:

1. **Student**

   * Attributes: `StudentID` (int), `Name` (string), `Grade` (string)
   * Functions: `DisplayStudentDetails()`

2. **ClassSession**

   * Attributes: `SessionID` (int), `Subject` (string), `Date` (DateTime)
   * Functions: `DisplaySessionDetails()`

3. **AttendanceMapping**

   * Attributes: `MappingID` (int), `StudentID` (int), `SessionID` (int), `Status` (string)
   * Functions: `MarkAttendance()`, `DisplayMappingDetails()`

**Task:**

* Write the **EF Core model fragments** including:

  * Primary keys
  * Foreign keys (`StudentID`, `SessionID`)
  * Navigation properties for relationships
  * Functions (`DisplayStudentDetails()`, `DisplaySessionDetails()`, `MarkAttendance()`, `DisplayMappingDetails()`)
**Solution**:
```csharp
public class Student
{
    [Key]
    public int StudentID { get; set; }
    public string Name { get; set; }
    public string Grade { get; set; }

    // Navigation property: one student can have many attendance mappings
    public List<AttendanceMapping> Attendances { get; set; }

    // Function to display student details
    public void DisplayStudentDetails()
    {
        Console.WriteLine($"StudentID: {StudentID}, Name: {Name}, Grade: {Grade}");
    }
}

public class ClassSession
{
    [Key]
    public int SessionID { get; set; }
    public string Subject { get; set; }
    public DateTime Date { get; set; }

    // Navigation property: one session can have many attendance mappings
    public List<AttendanceMapping> Attendances { get; set; }

    // Function to display session details
    public void DisplaySessionDetails()
    {
        Console.WriteLine($"SessionID: {SessionID}, Subject: {Subject}, Date: {Date.ToShortDateString()}");
    }
}

public class AttendanceMapping
{
    [Key]
    public int MappingID { get; set; }

    [ForeignKey("Student")]
    public int StudentID { get; set; }
    public Student Student { get; set; }   // Navigation property

    [ForeignKey("ClassSession")]
    public int SessionID { get; set; }
    public ClassSession Session { get; set; }   // Navigation property

    public string Status { get; set; }

    // Function to mark attendance
    public void MarkAttendance(string status)
    {
        Status = status;
        Console.WriteLine($"Attendance marked as: {Status}");
    }

    // Function to display mapping details
    public void DisplayMappingDetails()
    {
        Console.WriteLine($"MappingID: {MappingID}, StudentID: {StudentID}, SessionID: {SessionID}, Status: {Status}");
    }
}

```

**Explanation:**

* `Student` has `StudentID`, `Name`, `Grade`, and a **navigation property** `Attendances`.
* `ClassSession` has `SessionID`, `Subject`, `Date`, and a **navigation property** `Attendances`.
* `AttendanceMapping` has `MappingID`, `StudentID`, `SessionID`, `Status`, and **foreign keys** with **navigation properties** to `Student` and `ClassSession`.
* Functions like `DisplayStudentDetails()` or `MarkAttendance()` can still exist for **C# logic**, but EF Core **ignores them for database mapping**.
* This version is **suitable for database scenarios** with **EF Core relationships**.

---



## 12. Displaying Data with ViewBag and Foreach Loop (Restaurant Menu)
**Scenario:** A restaurant website displays its menu items on a web page. The controller uses ViewBag to pass a list of categories to the view, which displays menu items using a foreach loop.

**Task:**
Complete the controller action to populate ViewBag.Categories with a list of 3 sample categories (e.g., "Appetizers", "Main Course", "Desserts") and pass a List<MenuItem> to the view.

Complete the Razor view to:
- Specify the model type as List<MenuItem>.
- Display ViewBag.Categories in an \<h2\> tag.
- Use a foreach loop to display each MenuItem's Name and Price.

```csharp
// Controller (RestaurantController.cs)
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class RestaurantController : Controller
{
    public IActionResult Menu()
    {
        ViewBag.Categories = new List<string> { "Appetizers", "Main Course", "Desserts" };
        var menuItems = new List<MenuItem>
        {
            new MenuItem { Name = "Spring Rolls", Price = 5.99m },
            new MenuItem { Name = "Grilled Salmon", Price = 15.99m },
            new MenuItem { Name = "Cheesecake", Price = 7.99m }
        };
        return View(menuItems);
    }
}
```
**Solution**:

```razor
// Razor view (Restaurant/Menu.cshtml)
@model List<MenuItem>
<h2>Categories: @string.Join(", ", ViewBag.Categories)</h2>
<ul>
    @foreach (var item in Model)
    {
        <li>@item.Name - $@item.Price</li>
    }
</ul>
```

**Explanation**:
- Controller sets `ViewBag.Categories` and passes a `List<MenuItem>`.
- View uses `@model List<MenuItem>`, displays categories, and iterates over items with `foreach`.

---

## 13. Configuring Controller Actions with HTTP Methods and Routes (Pet Adoption)
**Scenario:** A pet adoption system manages pet profiles and adoption requests. The system exposes endpoints to retrieve, create, update, and delete pet data, using various HTTP methods and URL patterns. Requests may include path parameters, query parameters, and form data.

**Task:**
Complete the PetController class with action methods for the following endpoints, handling different parameter bindings:
- GET /pet/list?species={species}: Retrieves pets by species.
- POST /pet/add: Creates a new pet profile (accepts form data with fields 'name' (string), 'species' (string), 'age' (int)).
- PUT /pet/update/{id}: Updates an existing pet by ID (also accepts updated form data with fields 'name' (string), 'species' (string), 'age' (int)).
- DELETE /pet/remove/{id}: Deletes a pet by ID.
- GET /pet/details?id={id}: Retrieves pet details via GET; also supports POST /pet/details with form data 'formId'.
Use HttpGet, HttpPost, HttpPut, and HttpDelete attributes to map the routes. Return a simple string response for each action incorporating the parameters (e.g., Content($"Pets retrieved for species: {species}"), Content($"Pet added: {name}, {species}, {age}")).

**Solution**:
```csharp
using Microsoft.AspNetCore.Mvc;

public class PetController : Controller
{
    [HttpGet("pet/list")]
    public IActionResult GetPetsBySpecies([FromQuery] string species)
    {
        return Content($"Pets retrieved for species: {species}");
    }

    [HttpPost("pet/add")]
    public IActionResult AddPet([FromForm] string name, [FromForm] string species, [FromForm] int age)
    {
        return Content($"Pet added: {name}, {species}, {age}");
    }

    [HttpPut("pet/update/{id}")]
    public IActionResult UpdatePet([FromRoute] int id, [FromForm] string name, [FromForm] string species, [FromForm] int age)
    {
        return Content($"Pet updated: ID {id}, {name}, {species}, {age}");
    }

    [HttpDelete("pet/remove/{id}")]
    public IActionResult RemovePet([FromRoute] int id)
    {
        return Content($"Pet removed: {id}");
    }

    [HttpGet("pet/details")]
    [HttpPost("pet/details")]
    public IActionResult GetPetDetails([FromQuery] int? id = null, [FromForm] int? formId = null)
    {
        int? petId = id ?? formId;
        if (petId.HasValue)
        {
            return Content($"Pet details retrieved for ID: {petId.Value}");
        }
        return Content("Pet details retrieved");
    }
}
```

**Explanation**:
- Actions use `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, and `[HttpDelete]` to map routes.
- Parameters are bound using `[FromQuery]` for query strings, `[FromForm]` for form data, and `[FromRoute]` for path parameters.
- For `GetPetDetails`, it handles both GET (query 'id') and POST (form 'id'), combining them for a unified response.
- Supports path parameters (e.g., {id}), query parameters (e.g., ?species={species}), and form bodies (e.g., for POST/PUT with name, species, age).

---
## 14. Endpoint vs Attribute Routing
**Scenario:** You are building an MVC-based library management system. The system has the following endpoints:

1. /books/all → Returns a list of all books in the library. (BooksController has ListAll action)
2. /books/{bookId} → Returns details of a specific book by its unique ID. (BooksController has Details action)

**Task:** Configure routing for both endpoints using:
- Endpoint routing in Program.cs / Startup.cs
- Attribute routing in the controller
**Solution**:
**Endpoint Routing**
```csharp
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        pattern: "books/all",
        defaults: new { controller = "Books", action = "ListAll" });

    endpoints.MapControllerRoute(
        pattern: "books/{bookId}",
        defaults: new { controller = "Books", action = "Details" });
});
```
**Attribute Routing**
```csharp
public class BooksController : Controller
{
    [HttpGet("books/all")]
    public IActionResult ListAll(){
        ...
    }

    [HttpGet("books/{bookId}")]
    public IActionResult Details(int bookId){
        ...
    }
}
```
## 15. when a user books a ride by clicking a “Book Now” button, from the action to saving the trip in the database
1. **Inside End User (clicks a button in View)**: The user sees a "Book Now" button on the ride booking page, clicks it, and submits their ride details like pickup location and driver preference.
2. **End User → Controller**: The click sends the ride details (e.g., User ID, pickup location, driver ID) from the webpage to the RideController's Book method.
3. **Controller**: The Controller receives the details, checks that all required information (e.g., User ID, pickup location, driver ID) is present.
4. **Controller → Model**: The Controller passes the ride details to the Model.
5. **Model**: The Model checks that the ride details are complete (e.g., User ID is there, pickup location isn’t blank).
6. **Model → Database**: The Model sends a query to the Database.
7. **Database**: The Database looks up the driver’s status.
8. **Database → Model**: The Database sends back whether the driver is available or busy.
9. **Model**: The Model confirms if the driver is free.
10. **Model → Database**: The Model sends queries to the Database.
11. **Database**: The Database saves the trip and updates the driver’s status to busy.
12. **Database → Model**: The Database sends back the result.
13. **Model**: The Model notes if the trip was saved with a booking ID or if it failed (e.g., "Driver not available").
14. **Model → Controller**: The Model sends this result back to the Controller.
15. **Controller**: The Controller decides if it should show a "success" or "error" message.
16. **Controller → View**: The Controller passes the result (success or error message) to the View.
17. **View**: The View turns the result into a message, like "Ride booked!" or "Driver not available," and displays it.
18. **View → End User**: The View shows the message on the page for the user to see.


---

# Class Practices
## 16. When an admin generates a sales report in a business dashboard, explain how the MVC data flow works from selecting a date range and clicking “Generate Report” to retrieving data from the database and displaying the report in charts and tables.

## 17. Develop action methods for a Student Management System using ASP.NET Core MVC:
(a) Design action methods only for the following endpoints. Use correct HTTP methods, route templates, and parameter binding attributes:  
- `GET /students/2025?grade=A` → Retrieves all students admitted in 2025, optionally filtered by grade (year from route, grade from query).  
- `POST /students/add?scholarship=true` → Adds a new student with fields submitted via form (FullName, RollNumber, Email). Query parameter scholarship indicates whether the student has a scholarship.  

(b) Develop a Razor View given this action:
```csharp
public IActionResult AllStudents(){
    var students = new List<Student>{
        new Student { Id = 1, FullName = "David", RollNumber = "R101", Email = "david@example.com" },
        new Student { Id = 2, FullName = "Eva", RollNumber = "R102", Email = "eva@example.com" },
        new Student { Id = 3, FullName = "Frank", RollNumber = "R103", Email = "frank@example.com" }
    };
    return View(students);
}

```
Task: Display these students in a table with columns: Id, FullName, RollNumber, and Email.

## 18. Query employee data using LINQ:

Employee: has EmployeeId, Name, Department, Salary.
- Given: a list named employees.
- Task: Write a LINQ query to select Name and Salary of IT employees, ordered by Salary descending.