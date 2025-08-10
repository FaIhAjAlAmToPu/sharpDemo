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

## 2. LINQ Query (Music Streaming Playlist)
**Task**: Implement `FilterAndSortSongs` to filter songs with `Duration > 180` seconds and sort by `Title` ascending.

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

## 3. Displaying Data in a Razor View (Travel Agency)
**Task**: Create a Razor view to display a `Tour` object's `Destination` and `Price`, or "Tour not found" if the model is null.

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

## 4. Custom Exception (Fitness Tracking App)
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

## 5. Multicasting Delegates (E-commerce Notifications)
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

## 6. Aggregation with List (Bookstore Inventory)
**Task**: Create `Book` and `BookStore` classes, with a constructor to initialize a `List<Book>` and a method to filter books with `Price < 50`. Include a sample `Main` method.

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

## 7. Relation Handling in Models (Hospital Management)
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
- `Department` has `Id` (primary key), `Name`, and `Doctors` (navigation property).
- `Doctor` has `Id`, `Name`, `DepartmentId` (foreign key), and `Department` (navigation property).
- `[ForeignKey]` configures the one-to-many relationship.

---

## 8. Displaying Data with ViewBag and Foreach Loop (Restaurant Menu)
**Task**: Create a controller to populate `ViewBag.Categories` and pass a `List<MenuItem>`. Create a Razor view to display categories and menu items.

**Solution**:
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

## 9. Configuring Controller Actions with HTTP Methods and Routes (Pet Adoption)
**Task**: Create `PetController` with actions for GET, POST, PUT, and DELETE endpoints.

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
    public IActionResult AddPet()
    {
        return Content("Pet added");
    }

    [HttpPut("pet/update/{id}")]
    public IActionResult UpdatePet(int id)
    {
        return Content($"Pet updated: {id}");
    }

    [HttpDelete("pet/remove/{id}")]
    public IActionResult RemovePet(int id)
    {
        return Content($"Pet removed: {id}");
    }

    [HttpGet("pet/details")]
    [HttpPost("pet/details")]
    public IActionResult GetPetDetails()
    {
        return Content("Pet details retrieved");
    }
}
```

**Explanation**:
- Actions use `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, and `[HttpDelete]` to map routes.
- Supports query parameters (`species`), route parameters (`id`), and multiple HTTP methods (`GET/POST` for details).

---

## 10. when a user books a ride by clicking a “Book Now” button, from the action to saving the trip in the database
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

## Notes
- These are standalone practice questions to demonstrate C# and ASP.NET Core concepts.
- Each solution is self-contained and can be tested individually.
- Use an IDE like Visual Studio to create and test the code snippets.
- For Razor views, ensure an ASP.NET Core project is set up with MVC configured.

This README serves as a reference for understanding and revisiting the solutions to these practice questions. Let me know if you need further clarification or additional practice questions!
