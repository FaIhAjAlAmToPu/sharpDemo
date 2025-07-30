# One-to-One and One-to-Many Relationships in ASP.NET Core MVC: Bookshop Example

This README explains **one-to-one** and **one-to-many** relationships in an ASP.NET Core MVC web application built in Visual Studio, using a bookshop scenario. It includes definitions, a textual ERD for a one-to-many relationship, C# model classes with Data Annotations for validation (all fields mandatory), and the `ApplicationDbContext` configuration for Entity Framework Core. The scenario involves books, their details, customers, and orders.


## Project Structure in Visual Studio

In a Visual Studio ASP.NET Core MVC project for the bookshop, organize classes as follows:
- **Model Classes**:
  - **Location**: Create a `Models` folder in the project root (e.g., `BookshopApp/Models`).
  - **Purpose**: Define model classes (`Book`, `BookDetail`, `Customer`, `Order`) with data structures and validation rules.
  - **How to Create**: In Solution Explorer, right-click the project > Add > New Folder > Name it `Models`. Then, right-click `Models` > Add > Class for each model (e.g., `Book.cs`, `BookDetail.cs`).
- **ApplicationDbContext**:
  - **Location**: Create a `Data` folder in the project root (e.g., `BookshopApp/Data`).
  - **Purpose**: Define the `ApplicationDbContext` class for Entity Framework Core to manage database interactions and relationships.
  - **How to Create**: Right-click the project > Add > New Folder > Name it `Data`. Right-click `Data` > Add > Class > Name it `ApplicationDbContext.cs`.
- **Setup**:
  - Install NuGet packages: `Microsoft.EntityFrameworkCore.SqlServer` and `Microsoft.EntityFrameworkCore` via NuGet Package Manager.
  - Register `ApplicationDbContext` in `Program.cs` with a connection string (e.g., for SQL Server) in `appsettings.json`.

---

## One-to-One Relationship

### Definition
A one-to-one relationship links one entity to exactly one instance of another entity. In the bookshop system, each `Book` has one `BookDetail` (e.g., with ISBN and publication date), and each `BookDetail` belongs to one `Book`.

### Example
- A `Book` (e.g., ID: 1, Title: “C# Programming”) has one `BookDetail` (e.g., ISBN: “123-4567890123”).
- The `BookDetail` table has a foreign key (`BookId`) linking to the `Book` table.

### Characteristics
- Each `Book` has a unique `BookDetail`.
- The `BookDetail` depends on the `Book` (cannot exist independently).
- Used to separate supplementary book information (e.g., ISBN) from core data (e.g., title).

---

## One-to-Many Relationship

### Definition
A one-to-many relationship links one entity to multiple instances of another entity. In the bookshop system, one `Customer` can have many `Orders`, but each `Order` belongs to one `Customer`.

### Example
- A `Customer` (e.g., “Alice Smith”) can have multiple `Orders` (e.g., for different books).
- Each `Order` references one `Customer` via a foreign key (`CustomerId`).

### Characteristics
- A `Customer` can have zero or more `Orders`.
- An `Order` must link to exactly one `Customer`.
- Common for tracking customer purchase history.

---

## ERD for One-to-Many Relationship

Below is a textual ERD for the one-to-many relationship between `Customer` and `Order`.

```
[Customer] ----(1)-----(many)---- [Order]
  |                                 |
  | PK: CustomerId                 | PK: OrderId
  |    Name                        | FK: CustomerId
  |    Email                       |    BookId
                                   |    OrderDate
```

### Explanation
- **Entities**:
  - `Customer`: Has primary key `CustomerId`, attributes `Name` and `Email`.
  - `Order`: Has primary key `OrderId`, foreign key `CustomerId`, and attributes `BookId` and `OrderDate`.
- **Relationship**: One `Customer` can have many `Orders`, linked via `CustomerId`.
- **Purpose**: Shows how multiple orders can be associated with a single customer.

---

## C# Model Classes with Data Annotations

Create the following model classes in the `Models` folder (`BookshopApp/Models`). All fields are mandatory, enforced with `[Required]` Data Annotations, and relationships are defined using navigation properties and foreign keys.

```csharp
// Models/Book.cs
using System.ComponentModel.DataAnnotations;

namespace BookshopApp.Models
{
    public class Book
    {
        [Key]
        [Required(ErrorMessage = "Book ID is required")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        // Navigation property for one-to-one relationship
        public BookDetail Detail { get; set; }
    }
}

// Models/BookDetail.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookshopApp.Models
{
    public class BookDetail
    {
        [Key]
        [Required(ErrorMessage = "Book ID is required")]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publication date is required")]
        public DateTime PublicationDate { get; set; }

        // Navigation property
        public Book Book { get; set; }
    }
}

// Models/Customer.cs
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookshopApp.Models
{
    public class Customer
    {
        [Key]
        [Required(ErrorMessage = "Customer ID is required")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        // Navigation property for one-to-many relationship
        public List<Order> Orders { get; set; }
    }
}

// Models/Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace BookshopApp.Models
{
    public class Order
    {
        [Key]
        [Required(ErrorMessage = "Order ID is required")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Customer ID is required")]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Book ID is required")]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Order date is required")]
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Book Book { get; set; }
    }
}
```

## What if Many-To-Many
[Book] ----(many)---- [BookCategory] ----(many)---- [Category]
  |                     |                            |
  | PK: BookId         | PK: BookId, CategoryId    | PK: CategoryId
  |    Title           | FK: BookId                |    Name
  |    Author          | FK: CategoryId            |
  |    Price           |

```csharp
// Models/Category.cs
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BookshopApp.Models
{
    public class Category
    {
        [Key]
        [Required(ErrorMessage = "Category ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(50, ErrorMessage = "Category name cannot exceed 50 characters")]
        public string Name { get; set; }

        // Navigation property for many-to-many
        public List<BookCategory> BookCategories { get; set; }
    }
}

// Models/BookCategory.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookshopApp.Models
{
    public class BookCategory
    {
        [Required(ErrorMessage = "Book ID is required")]
        [ForeignKey("Book")]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Category ID is required")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Category Category { get; set; }
    }
}
```
```csharp
// Many-to-many: Book to Category via BookCategory
            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.BookId, bc.CategoryId });

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.BookId)
                .IsRequired();

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.CategoryId)
                .IsRequired();
```



### Explanation
- **Location**: Place these files in the `Models` folder (`BookshopApp/Models`).
- **Validations**: All fields are mandatory with `[Required]`. Additional annotations ensure data integrity:
  - `Book`: `[StringLength]` for `Title` and `Author`, `[Range]` for `Price`.
  - `BookDetail`: `[RegularExpression]` for `ISBN`, ensuring a valid format.
  - `Customer`: `[RegularExpression]` for `CustomerId`, `[EmailAddress]` for `Email`.
  - `Order`: `[Range]` for `OrderId`, `[RegularExpression]` for `CustomerId`.
- **Relationships**:
  - **One-to-One**: `Book` to `BookDetail` via `BookId` (PK and FK in `BookDetail`).
  - **One-to-Many**: `Customer` to `Orders` via `CustomerId` (FK in `Order`).

---

## ApplicationDbContext Configuration

Create the following `ApplicationDbContext` class in the `Data` folder (`BookshopApp/Data`) to configure relationships using Entity Framework Core.

```csharp
// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using BookshopApp.Models;

namespace BookshopApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-one: Book to BookDetail
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Detail)
                .WithOne(d => d.Book)
                .HasForeignKey<BookDetail>(d => d.BookId)
                .IsRequired();

            // One-to-many: Customer to Orders
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            // Many-to-one: Order to Book
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Book)
                .WithMany()
                .HasForeignKey(o => o.BookId)
                .IsRequired();
        }
    }
}
```

### Explanation
- **Location**: Place in the `Data` folder (`BookshopApp/Data/ApplicationDbContext.cs`).
- **DbSets**: Define collections for `Books`, `BookDetails`, `Customers`, and `Orders`.
- **Relationships**:
  - **One-to-One**: `Book` to `BookDetail` configured with `HasOne`/`WithOne`, using `BookId` as the foreign key.
  - **One-to-Many**: `Customer` to `Orders` configured with `HasMany`/`WithOne`, using `CustomerId` as the foreign key.
  - **Many-to-One**: `Order` to `Book` links via `BookId`.
- **IsRequired()**: Ensures foreign keys are mandatory, aligning with Data Annotations.

- **Setup**: Add the connection string in `appsettings.json` and register the context in `Program.cs`
---