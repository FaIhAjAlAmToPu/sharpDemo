# Comprehensive LINQ Practice Questions and Solutions

This README provides a set of LINQ practice questions to help you master LINQ in C#, building on your knowledge of `select`, `from`, and `where`. The questions are divided into three sections: array-based, object-based, and database querying with Entity Framework Core. Each question includes:
- A problem description
- Solutions in **query syntax** (`from`, `where`, etc.) and **method syntax** (e.g., `.Where()`, `.Select()`)
- Expected results
- A brief explanation

Use the datasets below to solve the questions. Try writing the queries yourself before checking the solutions!

---

## Array-Based Questions

### Numbers Array
```csharp
int[] numbers = { 1, 4, 7, 2, 9, 3, 8, 5, 6, 10, 4, 7, 12 };
```

#### 1. Get Even Numbers Greater Than 4 in Ascending Order
**Question**: Retrieve all even numbers from the `numbers` array that are greater than 4, sorted in ascending order.

**Query Syntax**:
```csharp
var evenNumbers = from num in numbers
                  where num % 2 == 0 && num > 4
                  orderby num
                  select num;
```

**Method Syntax**:
```csharp
var evenNumbers = numbers.Where(num => num % 2 == 0 && num > 4)
                        .OrderBy(num => num);
```

**Result**: `{ 6, 8, 10, 12 }`

**Explanation**: Filters numbers that are even (`num % 2 == 0`) and greater than 4, then sorts them in ascending order.

---

#### 2. Sum of Numbers Divisible by 3
**Question**: Calculate the sum of all numbers in the `numbers` array that are divisible by 3.

**Query Syntax**:
```csharp
var sumDivisibleBy3 = (from num in numbers
                       where num % 3 == 0
                       select num).Sum();
```

**Method Syntax**:
```csharp
var sumDivisibleBy3 = numbers.Where(num => num % 3 == 0)
                             .Sum();
```

**Result**: `9 + 3 + 6 + 12 = 30`

**Explanation**: Selects numbers divisible by 3 and computes their sum using `Sum()`.

---

#### 3. Find the Third Highest Number
**Question**: Find the third highest distinct number in the `numbers` array.

**Query Syntax**:
```csharp
var thirdHighest = (from num in numbers
                    orderby num descending
                    select num).Distinct().Skip(2).First();
```

**Method Syntax**:
```csharp
var thirdHighest = numbers.OrderByDescending(num => num)
                         .Distinct()
                         .Skip(2)
                         .First();
```

**Result**: `8`

**Explanation**: Orders numbers in descending order, removes duplicates with `Distinct()`, skips the first two (12, 10), and takes the next (8).

---

#### 4. Count Numbers Appearing More Than Once
**Question**: Count how many numbers in the `numbers` array appear more than once.

**Query Syntax**:
```csharp
var duplicatesCount = (from num in numbers
                       group num by num into g
                       where g.Count() > 1
                       select g.Key).Count();
```

**Method Syntax**:
```csharp
var duplicatesCount = numbers.GroupBy(num => num)
                            .Where(g => g.Count() > 1)
                            .Count();
```

**Result**: `2` (4 and 7 appear twice)

**Explanation**: Groups numbers by their value, filters groups with more than one occurrence, and counts the distinct keys.

---

#### 5. Group Numbers by Range (0-5, 6-10, 11+)
**Question**: Group numbers into ranges (0-5, 6-10, 11 or more) and list the numbers in each range.

**Query Syntax**:
```csharp
var numberRanges = from num in numbers
                   group num by num switch
                   {
                       <= 5 => "0-5",
                       <= 10 => "6-10",
                       _ => "11+"
                   } into g
                   select new { Range = g.Key, Numbers = g };
```

**Method Syntax**:
```csharp
var numberRanges = numbers.GroupBy(num => num switch
                          {
                              <= 5 => "0-5",
                              <= 10 => "6-10",
                              _ => "11+"
                          })
                         .Select(g => new { Range = g.Key, Numbers = g });
```

**Result**: 
```
{ 
    { Range = "0-5", Numbers = { 1, 4, 2, 3, 5, 4 } }, 
    { Range = "6-10", Numbers = { 7, 9, 8, 6, 10, 7 } }, 
    { Range = "11+", Numbers = { 12 } } 
}
```

**Explanation**: Groups numbers by ranges using a switch expression and selects the numbers in each range.

---

#### 6. Check If List Is Sorted Ascending
**Question**: Determine if the `numbers` array is sorted in ascending order.

**Query Syntax**:
```csharp
var isSorted = (from num in numbers
                select num).SequenceEqual(numbers.OrderBy(n => n));
```

**Method Syntax**:
```csharp
var isSorted = numbers.SequenceEqual(numbers.OrderBy(n => n));
```

**Result**: `false`

**Explanation**: Compares the array with its sorted version using `SequenceEqual()`. The array is not sorted (e.g., 4 > 7 is out of order).

---

#### 7. Get First Two Numbers Greater Than 5
**Question**: Retrieve the first two numbers from the `numbers` array that are greater than 5.

**Query Syntax**:
```csharp
var firstTwo = (from num in numbers
                where num > 5
                select num).Take(2);
```

**Method Syntax**:
```csharp
var firstTwo = numbers.Where(num => num > 5)
                     .Take(2);
```

**Result**: `{ 7, 9 }`

**Explanation**: Filters numbers greater than 5 and takes the first two using `Take(2)`.

---

#### 8. Concatenate Numbers as String
**Question**: Create a comma-separated string of all numbers greater than 3 in the `numbers` array.

**Query Syntax**:
```csharp
var numberString = string.Join(", ", from num in numbers
                                     where num > 3
                                     select num);
```

**Method Syntax**:
```csharp
var numberString = string.Join(", ", numbers.Where(num => num > 3));
```

**Result**: `"4, 7, 9, 8, 5, 6, 10, 4, 7, 12"`

**Explanation**: Filters numbers greater than 3 and joins them with commas using `string.Join`.

---

## Object-Based Questions

### Students List
```csharp
class Student
{
    public string Name { get; set; }
    public int Score { get; set; }
    public string Major { get; set; }
}

List<Student> students = new List<Student>
{
    new Student { Name = "Alice", Score = 85, Major = "Math" },
    new Student { Name = "Bob", Score = 92, Major = "Physics" },
    new Student { Name = "Charlie", Score = 78, Major = "Math" },
    new Student { Name = "David", Score = 95, Major = "Physics" },
    new Student { Name = "Emma", Score = 88, Major = "Chemistry" },
    new Student { Name = "Frank", Score = 82, Major = "Math" }
};
```

#### 9. Get Students with Scores Above 90
**Question**: Retrieve the names and scores of students with scores above 90.

**Query Syntax**:
```csharp
var highScorers = from student in students
                  where student.Score > 90
                  select new { student.Name, student.Score };
```

**Method Syntax**:
```csharp
var highScorers = students.Where(student => student.Score > 90)
                         .Select(student => new { student.Name, student.Score });
```

**Result**: 
```
{ 
    { Name = "Bob", Score = 92 }, 
    { Name = "David", Score = 95 } 
}
```

**Explanation**: Filters students with scores above 90 and selects their names and scores.

---

#### 10. Order Students by Name (Ascending)
**Question**: Sort the `students` list by name in ascending order and return their names and majors.

**Query Syntax**:
```csharp
var orderedByName = from student in students
                    orderby student.Name
                    select new { student.Name, student.Major };
```

**Method Syntax**:
```csharp
var orderedByName = students.OrderBy(student => student.Name)
                           .Select(student => new { student.Name, student.Major });
```

**Result**: 
```
{ 
    { Name = "Alice", Major = "Math" }, 
    { Name = "Bob", Major = "Physics" }, 
    { Name = "Charlie", Major = "Math" }, 
    { Name = "David", Major = "Physics" }, 
    { Name = "Emma", Major = "Chemistry" }, 
    { Name = "Frank", Major = "Math" } 
}
```

**Explanation**: Orders students by name and projects their name and major.

---

#### 11. Group Students by Major
**Question**: Group students by their major and list their names in each group.

**Query Syntax**:
```csharp
var majorGroups = from student in students
                  group student by student.Major into g
                  select new { Major = g.Key, Names = g.Select(s => s.Name) };
```

**Method Syntax**:
```csharp
var majorGroups = students.GroupBy(student => student.Major)
                         .Select(g => new { Major = g.Key, Names = g.Select(s => s.Name) });
```

**Result**: 
```
{ 
    { Major = "Math", Names = { "Alice", "Charlie", "Frank" } }, 
    { Major = "Physics", Names = { "Bob", "David" } }, 
    { Major = "Chemistry", Names = { "Emma" } } 
}
```

**Explanation**: Groups students by major and selects the names in each group.

---

#### 12. Highest Score per Major
**Question**: Find the highest score for each major in the `students` list.

**Query Syntax**:
```csharp
var maxScoreByMajor = from student in students
                      group student by student.Major into g
                      select new { Major = g.Key, MaxScore = g.Max(s => s.Score) };
```

**Method Syntax**:
```csharp
var maxScoreByMajor = students.GroupBy(student => student.Major)
                             .Select(g => new { Major = g.Key, MaxScore = g.Max(s => s.Score) });
```

**Result**: 
```
{ 
    { Major = "Math", MaxScore = 85 }, 
    { Major = "Physics", MaxScore = 95 }, 
    { Major = "Chemistry", MaxScore = 88 } 
}
```

**Explanation**: Groups students by major and selects the maximum score for each group.

---

#### 13. Students with Above-Average Scores by Major
**Question**: For each major, list the names of students whose scores are above their major’s average score.

**Query Syntax**:
```csharp
var aboveAvgByMajor = from student in students
                      group student by student.Major into g
                      let avg = g.Average(s => s.Score)
                      from student in g
                      where student.Score > avg
                      select new { student.Major, student.Name, student.Score };
```

**Method Syntax**:
```csharp
var aboveAvgByMajor = students.GroupBy(student => student.Major)
                             .SelectMany(g => g.Where(student => student.Score > g.Average(s => s.Score))
                                               .Select(student => new { student.Major, student.Name, student.Score }));
```

**Result**: 
```
{ 
    { Major = "Math", Name = "Alice", Score = 85 }, 
    { Major = "Physics", Name = "David", Score = 95 }, 
    { Major = "Chemistry", Name = "Emma", Score = 88 } 
}
```

**Explanation**: Groups students by major, calculates the average score per group, and selects students with scores above their group’s average.

---

#### 14. Check If All Students Passed
**Question**: Determine if all students have a score of at least 70.

**Query Syntax**:
```csharp
var allPassed = (from student in students
                 select student.Score).All(score => score >= 70);
```

**Method Syntax**:
```csharp
var allPassed = students.All(student => student.Score >= 70);
```

**Result**: `true`

**Explanation**: Uses `All()` to check if every student’s score is at least 70.

---

#### 15. Create Initials for Student Names
**Question**: Create a list of students’ initials (first letter of their name) and their scores.

**Query Syntax**:
```csharp
var initials = from student in students
               select new { Initial = student.Name[0].ToString(), student.Score };
```

**Method Syntax**:
```csharp
var initials = students.Select(student => new { Initial = student.Name[0].ToString(), student.Score });
```

**Result**: 
```
{ 
    { Initial = "A", Score = 85 }, 
    { Initial = "B", Score = 92 }, 
    { Initial = "C", Score = 78 }, 
    { Initial = "D", Score = 95 }, 
    { Initial = "E", Score = 88 }, 
    { Initial = "F", Score = 82 } 
}
```

**Explanation**: Selects the first character of each student’s name and their score.

---

## Database Querying Questions

### Bookshop Database Schema
The following Entity Framework Core context represents a bookshop database with two related tables: `Books` and `Categories`. The schema includes a one-to-many relationship (one category can have many books).

```csharp
using Microsoft.EntityFrameworkCore;

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}

public class BookshopContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }

    public BookshopContext(DbContextOptions<BookshopContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);
    }
}
```

**Sample Table Data**:

**Categories Table**:
| CategoryId | Name         |
|------------|--------------|
| 1          | Fiction      |
| 2          | Non-Fiction  |
| 3          | Science      |

**Books Table**:
| BookId | Title       | Price  | CategoryId |
|--------|-------------|--------|------------|
| 1      | Novel A     | 19.99  | 1          |
| 2      | History B   | 29.99  | 2          |
| 3      | Novel C     | 15.99  | 1          |
| 4      | Science D   | 24.99  | 3          |
| 5      | Novel E     | 12.99  | 1          |

**Note**: For database queries, assume the context (`BookshopContext`) is initialized, and navigation properties are loaded.

---

#### 16. Get Titles of Books Under $20
**Question**: Retrieve the titles of all books priced under $20.

**Query Syntax**:
```csharp
var cheapBooks = from book in context.Books
                 where book.Price < 20
                 select book.Title;
```

**Method Syntax**:
```csharp
var cheapBooks = context.Books.Where(book => book.Price < 20)
                             .Select(book => book.Title);
```

**Result**: `{ "Novel C", "Novel E" }`

**Explanation**: Filters books with prices less than 20 and selects their titles.

---

#### 17. Books in Fiction Category Ordered by Price
**Question**: List all books in the "Fiction" category, ordered by price in ascending order, including title and price.

**Query Syntax**:
```csharp
var fictionBooks = from book in context.Books
                   where book.Category.Name == "Fiction"
                   orderby book.Price
                   select new { book.Title, book.Price };
```

**Method Syntax**:
```csharp
var fictionBooks = context.Books.Where(book => book.Category.Name == "Fiction")
                               .OrderBy(book => book.Price)
                               .Select(book => new { book.Title, book.Price });
```

**Result**: 
```
{ 
    { Title = "Novel E", Price = 12.99 }, 
    { Title = "Novel C", Price = 15.99 }, 
    { Title = "Novel A", Price = 19.99 } 
}
```

**Explanation**: Filters books in the "Fiction" category, sorts by price, and selects title and price.

---

#### 18. Average Price per Category
**Question**: Calculate the average price of books in each category.

**Query Syntax**:
```csharp
var avgPriceByCategory = from book in context.Books
                         group book by book.Category.Name into g
                         select new { Category = g.Key, AveragePrice = g.Average(b => b.Price) };
```

**Method Syntax**:
```csharp
var avgPriceByCategory = context.Books.GroupBy(book => book.Category.Name)
                                     .Select(g => new { Category = g.Key, AveragePrice = g.Average(b => b.Price) });
```

**Result**: 
```
{ 
    { Category = "Fiction", AveragePrice = 16.3233 }, 
    { Category = "Non-Fiction", AveragePrice = 29.99 }, 
    { Category = "Science", AveragePrice = 24.99 } 
}
```

**Explanation**: Groups books by category name and calculates the average price for each group.

---

#### 19. Most Expensive Book per Category
**Question**: Find the title and price of the most expensive book in each category.

**Query Syntax**:
```csharp
var maxPriceByCategory = from book in context.Books
                         group book by book.Category.Name into g
                         let maxPrice = g.Max(b => b.Price)
                         from book in g
                         where book.Price == maxPrice
                         select new { g.Key, book.Title, book.Price };
```

**Method Syntax**:
```csharp
var maxPriceByCategory = context.Books.GroupBy(book => book.Category.Name)
                                     .SelectMany(g => g.Where(book => book.Price == g.Max(b => b.Price))
                                                       .Select(book => new { g.Key, book.Title, book.Price }));
```

**Result**: 
```
{ 
    { Key = "Fiction", Title = "Novel A", Price = 19.99 }, 
    { Key = "Non-Fiction", Title = "History B", Price = 29.99 }, 
    { Key = "Science", Title = "Science D", Price = 24.99 } 
}
```

**Explanation**: Groups books by category, finds the maximum price per group, and selects the book(s) with that price.

---

#### 20. Books with Prices Above Category Average
**Question**: List books whose prices are above their category’s average price, including title, price, and category name.

**Query Syntax**:
```csharp
var aboveCategoryAvg = from book in context.Books
                       group book by book.Category.Name into g
                       let avgPrice = g.Average(b => b.Price)
                       from book in g
                       where book.Price > avgPrice
                       select new { g.Key, book.Title, book.Price };
```

**Method Syntax**:
```csharp
var aboveCategoryAvg = context.Books.GroupBy(book => book.Category.Name)
                                   .SelectMany(g => g.Where(book => book.Price > g.Average(b => b.Price))
                                                     .Select(book => new { g.Key, book.Title, book.Price }));
```

**Result**: 
```
{ 
    { Key = "Fiction", Title = "Novel A", Price = 19.99 } 
}
```

**Explanation**: Groups books by category, calculates the average price per category, and selects books with prices above their category’s average (Fiction avg ≈ 16.32).

---

#### 21. Count Books per Price Range
**Question**: Group books by price ranges (<$15, $15-$25, >$25) and count the number of books in each range.

**Query Syntax**:
```csharp
var priceRanges = from book in context.Books
                  group book by book.Price switch
                  {
                      < 15 => "Under $15",
                      <= 25 => "$15-$25",
                      _ => "Over $25"
                  } into g
                  select new { Range = g.Key, Count = g.Count() };
```

**Method Syntax**:
```csharp
var priceRanges = context.Books.GroupBy(book => book.Price switch
                               {
                                   < 15 => "Under $15",
                                   <= 25 => "$15-$25",
                                   _ => "Over $25"
                               })
                              .Select(g => new { Range = g.Key, Count = g.Count() });
```

**Result**: 
```
{ 
    { Range = "Under $15", Count = 1 }, 
    { Range = "$15-$25", Count = 3 }, 
    { Range = "Over $25", Count = 1 } 
}
```

**Explanation**: Groups books by price ranges using a switch expression and counts the books in each range.

---

#### 22. Join Categories with Books
**Question**: Join the `Categories` and `Books` tables to list each book’s title and its category name, ordered by category name.

**Query Syntax**:
```csharp
var bookCategories = from category in context.Categories
                     join book in context.Books on category.CategoryId equals book.CategoryId
                     orderby category.Name
                     select new { book.Title, CategoryName = category.Name };
```

**Method Syntax**:
```csharp
var bookCategories = context.Categories.Join(context.Books,
                                            category => category.CategoryId,
                                            book => book.CategoryId,
                                            (category, book) => new { book.Title, CategoryName = category.Name })
                                      .OrderBy(bc => bc.CategoryName);
```

**Result**: 
```
{ 
    { Title = "Novel A", CategoryName = "Fiction" }, 
    { Title = "Novel C", CategoryName = "Fiction" }, 
    { Title = "Novel E", CategoryName = "Fiction" }, 
    { Title = "History B", CategoryName = "Non-Fiction" }, 
    { Title = "Science D", CategoryName = "Science" } 
}
```

**Explanation**: Joins categories and books on `CategoryId`, orders by category name, and selects book title and category name.

---
