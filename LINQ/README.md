# Comprehensive LINQ Practice Questions and Solutions

This tutorial provides hands-on practice with LINQ (Language Integrated Query) in C#. It covers array-based, object-based, and database querying scenarios using Entity Framework Core. Each question includes:
- A problem description
- Solutions in **query syntax** (`from`, `where`, etc.) and **method syntax** (e.g., `.Where()`, `.Select()`)
- Expected results
- A brief explanation

Try solving the queries yourself before checking the solutions to build your understanding!

---

## LINQ Functions Reference Table

The following table describes key LINQ functions used in this tutorial, with examples to clarify their behavior. These functions are essential for manipulating and querying data effectively.

| **Function** | **Description** | **Example Input** | **Example Query** | **Output** |
|--------------|-----------------|-------------------|-------------------|------------|
| `Where` | Filters elements based on a condition. | `int[] numbers = { 1, 2, 3, 4, 5 }` | `.Where(n => n > 3)` | `{ 4, 5 }` |
| `Select` | Projects each element into a new form. | `int[] numbers = { 1, 2, 3 }` | `.Select(n => n * 2)` | `{ 2, 4, 6 }` |
| `OrderBy` | Sorts elements in ascending order. | `int[] numbers = { 3, 1, 2 }` | `.OrderBy(n => n)` | `{ 1, 2, 3 }` |
| `OrderByDescending` | Sorts elements in descending order. | `int[] numbers = { 3, 1, 2 }` | `.OrderByDescending(n => n)` | `{ 3, 2, 1 }` |
| `GroupBy` | Groups elements by a key. | `int[] numbers = { 1, 2, 3, 4 }` | `.GroupBy(n => n % 2)` | `{ Key = 1, { 1, 3 } }, { Key = 0, { 2, 4 } }` |
| `SelectMany` | Flattens grouped or nested collections. | `List<List<int>> lists = { {1, 2}, {3, 4} }` | `.SelectMany(list => list)` | `{ 1, 2, 3, 4 }` |
| `Sum` | Calculates the sum of numeric values. | `int[] numbers = { 1, 2, 3 }` | `.Sum()` | `6` |
| `Average` | Computes the average of numeric values. | `int[] numbers = { 2, 4, 6 }` | `.Average()` | `4` |
| `Max` | Finds the maximum value. | `int[] numbers = { 1, 5, 3 }` | `.Max()` | `5` |
| `Min` | Finds the minimum value. | `int[] numbers = { 1, 5, 3 }` | `.Min()` | `1` |
| `Count` | Counts elements in a collection. | `int[] numbers = { 1, 2, 3 }` | `.Count()` | `3` |
| `Distinct` | Removes duplicate elements. | `int[] numbers = { 1, 2, 2, 3 }` | `.Distinct()` | `{ 1, 2, 3 }` |
| `Skip` | Skips a specified number of elements. | `int[] numbers = { 1, 2, 3, 4 }` | `.Skip(2)` | `{ 3, 4 }` |
| `Take` | Takes a specified number of elements. | `int[] numbers = { 1, 2, 3, 4 }` | `.Take(2)` | `{ 1, 2 }` |
| `All` | Checks if all elements satisfy a condition. | `int[] numbers = { 2, 4, 6 }` | `.All(n => n % 2 == 0)` | `true` |
| `Any` | Checks if any element satisfies a condition. | `int[] numbers = { 1, 3, 4 }` | `.Any(n => n % 2 == 0)` | `true` |

**Note on Anonymous Types (`new {}`)**: The `new {}` syntax creates an anonymous type, allowing you to select specific properties or computed values into a new object. For example, `Select(student => new { student.Name, DoubleScore = student.Score * 2 })` creates objects with `Name` and `DoubleScore` properties. This is useful for shaping query results without defining a formal class.

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

**Explanation**: Uses `Where` to filter even numbers (`num % 2 == 0`) greater than 4, then `OrderBy` sorts them in ascending order.

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

**Explanation**: Filters numbers divisible by 3 using `Where` and computes their sum with `Sum`.

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

**Explanation**: Uses `OrderByDescending` to sort numbers in descending order, `Distinct` to remove duplicates, `Skip(2)` to bypass the top two (12, 10), and `First` to get the third (8).

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

**Explanation**: Uses `GroupBy` to group numbers by their value, filters groups with more than one occurrence (`Count() > 1`), and counts the distinct keys.

---

#### 5. Get First Two Numbers Greater Than 5
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

**Explanation**: Filters numbers greater than 5 with `Where` and uses `Take(2)` to select the first two.

---

#### 6. Group Numbers by Even/Odd and Find Maximum
**Question**: Group numbers into even and odd categories, and find the maximum number in each group.

**Query Syntax**:
```csharp
var maxByParity = from num in numbers
                  group num by num % 2 == 0 ? "Even" : "Odd" into g
                  select new { Parity = g.Key, MaxNumber = g.Max() };
```

**Method Syntax**:
```csharp
var maxByParity = numbers.GroupBy(num => num % 2 == 0 ? "Even" : "Odd")
                        .Select(g => new { Parity = g.Key, MaxNumber = g.Max() });
```

**Result**: 
```
{ 
    { Parity = "Even", MaxNumber = 12 }, 
    { Parity = "Odd", MaxNumber = 9 } 
}
```

**Explanation**: Uses `GroupBy` to categorize numbers as "Even" or "Odd" based on `num % 2 == 0`. The `Select` clause creates an anonymous type with the group key (`Parity`) and the maximum value (`Max`) for each group.

---

#### 7. Create Number Ranges and Count Elements
**Question**: Group numbers into ranges (<5, 5-9, ≥10) and count how many numbers fall into each range.

**Query Syntax**:
```csharp
var numberRanges = from num in numbers
                   group num by num < 5 ? "<5" : num < 10 ? "5-9" : "≥10" into g
                   select new { Range = g.Key, Count = g.Count() };
```

**Method Syntax**:
```csharp
var numberRanges = numbers.GroupBy(num => num < 5 ? "<5" : num < 10 ? "5-9" : "≥10")
                         .Select(g => new { Range = g.Key, Count = g.Count() });
```

**Result**: 
```
{ 
    { Range = "<5", Count = 5 }, 
    { Range = "5-9", Count = 6 }, 
    { Range = "≥10", Count = 2 } 
}
```

**Explanation**: Uses `GroupBy` with a conditional expression to categorize numbers into three ranges. The `Select` clause creates an anonymous type with the range name and the count of numbers in each group using `Count`.

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

#### 8. Get Students with Scores Above 90
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

**Explanation**: Uses `Where` to filter students with scores above 90 and `Select` to create an anonymous type with their names and scores.

---

#### 9. Order Students by Name (Ascending)
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

**Explanation**: Uses `OrderBy` to sort by name and `Select` to create an anonymous type with name and major.

---

#### 10. Group Students by Major
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

**Explanation**: Uses `GroupBy` to group students by major. The `Select` clause creates an anonymous type with the major and a collection of names using a nested `Select`.

---

#### 11. Highest Score per Major
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

**Explanation**: Uses `GroupBy` to group by major and `Max` to find the highest score in each group, projected into an anonymous type.

---

#### 12. Students with Above-Average Scores by Major
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

**Explanation**: Uses `GroupBy` to group by major, calculates the average score per group with `Average`, and uses `SelectMany` to flatten the groups and select students with above-average scores.

---

#### 13. Check If All Students Passed
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

**Explanation**: Uses `All` to verify that every student’s score is at least 70.

---

#### 14. Create Initials for Student Names
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

**Explanation**: Uses `Select` to create an anonymous type with the first letter of each student’s name and their score.

---

#### 15. Group Students by Score Range
**Question**: Group students by score ranges (≤80, 81-90, >90) and list their names and majors.

**Query Syntax**:
```csharp
var scoreRanges = from student in students
                  group student by student.Score <= 80 ? "≤80" : student.Score <= 90 ? "81-90" : ">90" into g
                  select new { ScoreRange = g.Key, Students = g.Select(s => new { s.Name, s.Major }) };
```

**Method Syntax**:
```csharp
var scoreRanges = students.GroupBy(student => student.Score <= 80 ? "≤80" : student.Score <= 90 ? "81-90" : ">90")
                         .Select(g => new { ScoreRange = g.Key, Students = g.Select(s => new { s.Name, s.Major }) });
```

**Result**: 
```
{ 
    { ScoreRange = "≤80", Students = { { Name = "Charlie", Major = "Math" } } }, 
    { ScoreRange = "81-90", Students = { { Name = "Alice", Major = "Math" }, { Name = "Emma", Major = "Chemistry" }, { Name = "Frank", Major = "Math" } } }, 
    { ScoreRange = ">90", Students = { { Name = "Bob", Major = "Physics" }, { Name = "David", Major = "Physics" } } } 
}
```

**Explanation**: Uses `GroupBy` to categorize students by score ranges. The `Select` clause creates an anonymous type with the score range and a nested collection of anonymous types containing names and majors.

---

#### 16. Minimum and Maximum Scores by Major
**Question**: For each major, find the minimum and maximum scores and the names of students with those scores.

**Query Syntax**:
```csharp
var minMaxScores = from student in students
                   group student by student.Major into g
                   let minScore = g.Min(s => s.Score)
                   let maxScore = g.Max(s => s.Score)
                   select new
                   {
                       Major = g.Key,
                       MinScore = minScore,
                       MinStudent = g.First(s => s.Score == minScore).Name,
                       MaxScore = maxScore,
                       MaxStudent = g.First(s => s.Score == maxScore).Name
                   };
```

**Method Syntax**:
```csharp
var minMaxScores = students.GroupBy(student => student.Major)
                          .Select(g => new
                          {
                              Major = g.Key,
                              MinScore = g.Min(s => s.Score),
                              MinStudent = g.First(s => s.Score == g.Min(s => s.Score)).Name,
                              MaxScore = g.Max(s => s.Score),
                              MaxStudent = g.First(s => s.Score == g.Max(s => s.Score)).Name
                          });
```

**Result**: 
```
{ 
    { Major = "Math", MinScore = 78, MinStudent = "Charlie", MaxScore = 85, MaxStudent = "Alice" }, 
    { Major = "Physics", MinScore = 92, MinStudent = "Bob", MaxScore = 95, MaxStudent = "David" }, 
    { Major = "Chemistry", MinScore = 88, MinStudent = "Emma", MaxScore = 88, MaxStudent = "Emma" } 
}
```

**Explanation**: Uses `GroupBy` to group by major, `Min` and `Max` to find the extreme scores, and `First` to get the names of students with those scores, all projected into an anonymous type.

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

#### 17. Get Titles of Books Under $20
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

**Explanation**: Uses `Where` to filter books with prices under 20 and `Select` to retrieve their titles.

---

#### 18. Books in Fiction Category Ordered by Price
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

**Explanation**: Uses `Where` to filter "Fiction" books, `OrderBy` to sort by price, and `Select` to create an anonymous type with title and price.

---

#### 19. Average Price per Category
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

**Explanation**: Uses `GroupBy` to group books by category name and `Average` to compute the average price, projected into an anonymous type.

---

#### 20. Most Expensive Book per Category
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

**Explanation**: Uses `GroupBy` to group by category, `Max` to find the highest price, and `SelectMany` to select books matching the maximum price, projected into an anonymous type.

---

#### 21. Books with Prices Above Category Average
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

**Explanation**: Uses `GroupBy` to group by category, `Average` to calculate the average price, and `SelectMany` to select books with prices above the average, projected into an anonymous type.

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

**Explanation**: Uses `Join` to combine categories and books on `CategoryId`, `OrderBy` to sort by category name, and `Select` to create an anonymous type with title and category name.

---

#### 23. Count Books and Total Price by Category
**Question**: For each category, count the number of books and calculate the total price of all books.

**Query Syntax**:
```csharp
var categoryStats = from book in context.Books
                    group book by book.Category.Name into g
                    select new { Category = g.Key, BookCount = g.Count(), TotalPrice = g.Sum(b => b.Price) };
```

**Method Syntax**:
```csharp
var categoryStats = context.Books.GroupBy(book => book.Category.Name)
                                .Select(g => new { Category = g.Key, BookCount = g.Count(), TotalPrice = g.Sum(b => b.Price) });
```

**Result**: 
```
{ 
    { Category = "Fiction", BookCount = 3, TotalPrice = 48.97 }, 
    { Category = "Non-Fiction", BookCount = 1, TotalPrice = 29.99 }, 
    { Category = "Science", BookCount = 1, TotalPrice = 24.99 } 
}
```

**Explanation**: Uses `GroupBy` to group books by category name, and `Select` to create an anonymous type with the category name, count of books (`Count`), and total price (`Sum`).

---

#### 24. Categories with Above-Average Book Counts
**Question**: Find categories with more books than the average number of books per category, including the category name and book count.

**Query Syntax**:
```csharp
var aboveAvgBookCount = from book in context.Books
                        group book by book.Category.Name into g
                        let avgBookCount = context.Books.GroupBy(b => b.Category.Name).Average(g => g.Count())
                        where g.Count() > avgBookCount
                        select new { Category = g.Key, BookCount = g.Count() };
```

**Method Syntax**:
```csharp
var aboveAvgBookCount = context.Books.GroupBy(book => book.Category.Name)
                                    .Where(g => g.Count() > context.Books.GroupBy(b => b.Category.Name).Average(g => g.Count()))
                                    .Select(g => new { Category = g.Key, BookCount = g.Count() });
```

**Result**: 
```
{ 
    { Category = "Fiction", BookCount = 3 } 
}
```

**Explanation**: Uses `GroupBy` to group books by category, calculates the average book count across all categories with `Average`, and selects categories with above-average book counts, projected into an anonymous type. (Average book count = (3 + 1 + 1) / 3 ≈ 1.67, so only Fiction with 3 books qualifies.)