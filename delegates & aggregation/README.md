# C# Delegates and Aggregation Tutorial

This tutorial introduces **C# delegates** and **aggregation**, two powerful concepts in C# programming. 
| Term            | Refined Explanation                                                                                                                   |
| --------------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| **Delegate**    | A type-safe, object-oriented way to **refer to methods**, allowing flexible behavior (like function pointers in C, but safer and OO). |
| **Aggregation** | A **"has-a" relationship** where one class **uses** another without owning its lifecycle — the objects can exist independently.       |

## Understanding Delegates

### What is a Delegate?
A **delegate** is a type-safe function pointer in C#. It allows methods to be passed as parameters, stored in variables, or used as callbacks. Delegates are commonly used for event handling, callbacks, and implementing flexible method invocation.

- **Syntax**: `delegate returnType DelegateName(parameters);`
- A delegate defines a method signature (return type and parameters).
- Any method matching the delegate's signature can be assigned to it.

```csharp
using System;

// Step 1: Define a delegate type
delegate void Greet(string name);

class Program
{
    // Step 2: Create a method that matches the delegate signature
    static void SayHello(string name)
    {
        Console.WriteLine($"Hello, {name}!");
    }

    static void Main()
    {
        // Step 3: Assign the method to the delegate
        Greet greeting = SayHello;

        // Step 4: Call the delegate (it calls the method)
        greeting("Alice");  // Output: Hello, Alice!
    }
}
```

### Why delegates?
✅ Simple Answer:
> Because delegates let you choose the function at runtime, not at compile time.

#### Without delegate

Instead of writing if/else to decide which function to call, you assign the function to a variable (delegate) and call it flexibly.
```csharp
void PrintHello() => Console.WriteLine("Hello");
void PrintBye() => Console.WriteLine("Bye");

void Run(int choice)
{
    if (choice == 1)
        PrintHello();
    else
        PrintBye();
}
```

#### With delegate
```csharp
delegate void Printer();

void PrintHello() => Console.WriteLine("Hello");
void PrintBye() => Console.WriteLine("Bye");

void Run(Printer printer)
{
    printer();  // Calls whatever function is passed (no if/else here)
}

// Usage
if (choice == 1)
    Run(PrintHello);// Output: Hello
else
    Run(PrintBye);  // Output: Bye
```

### Key Features
- **Type Safety**: Delegates ensure the assigned method matches the defined signature.
- **Multicasting**: A single delegate can invoke multiple methods.
- **Flexibility**: Delegates enable decoupling of method calls, useful in event-driven programming.

---

## Understanding Aggregation

### What is Aggregation?
**Aggregation** is a "has-a" relationship where one class contains an instance of another class, but the contained object can exist independently. It represents a loose coupling between objects, unlike composition, where the contained object’s lifecycle depends on the container.
```csharp
class Engine
{
    public void Start() => Console.WriteLine("Engine starting...");
}

class Car
{
    private Engine engine;  // 👉 Car has-a Engine (Aggregation)

    public Car(Engine eng)
    {
        engine = eng;  // 👉 We inject an existing Engine object
    }

    public void StartCar()
    {
        engine.Start();  // 👉 Delegates the starting to Engine
    }
}

class Program
{
    static void Main()
    {
        Engine myEngine = new Engine();  // 👉 Create engine
        Car myCar = new Car(myEngine);   // 👉 Pass engine into car

        myCar.StartCar();  // 👉 Starts the car (via its engine)
    }
}
```
- **Example**: A `University` class may have a collection of `Student` objects. The students exist independently of the university, so this is aggregation.
- **Key Point**: Aggregated objects are typically passed into the container class (e.g., via constructor or property) and can be shared across multiple containers.

---

## Code Examples

### Example 1: Basic Delegate Usage
This example demonstrates a delegate that points to methods performing arithmetic operations.

```csharp
using System;

// Define a delegate with a signature: int (int, int)
delegate int Operation(int x, int y);

class Program
{
    static void Main(string[] args)
    {
        // Assign methods to the delegate
        Operation add = Add;
        Operation subtract = Subtract;

        // Invoke delegate
        Console.WriteLine("Addition: " + add(5, 3));      // Output: Addition: 8
        Console.WriteLine("Subtraction: " + subtract(5, 3)); // Output: Subtraction: 2

        // Multicast delegate
        Operation multiOperation = add + subtract;
        multiOperation(5, 3); // Calls both methods
    }

    static int Add(int a, int b) 
    { 
        Console.WriteLine($"Adding {a} and {b}");
        return a + b; 
    }

    static int Subtract(int a, int b) 
    { 
        Console.WriteLine($"Subtracting {b} from {a}");
        return a - b; 
    }
}
```

### Example 2: Aggregation Example
This example shows a `Department` class aggregating a list of `Employee` objects.

```csharp
using System;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }
    public int Id { get; set; }

    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Employee: {Name}, ID: {Id}");
    }
}

class Department
{
    public string DepartmentName { get; set; }
    private List<Employee> employees; // Aggregation: Department "has-a" list of Employees

    public Department(string name, List<Employee> empList)
    {
        DepartmentName = name;
        employees = empList; // Employees passed in, can exist independently
    }

    public void DisplayEmployees()
    {
        Console.WriteLine($"Department: {DepartmentName}");
        foreach (var emp in employees)
        {
            emp.DisplayInfo();
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create Employee objects
        Employee emp1 = new Employee("Alice", 101);
        Employee emp2 = new Employee("Bob", 102);

        // Create a list of employees
        List<Employee> empList = new List<Employee> { emp1, emp2 };

        // Create Department with aggregated employees
        Department dept = new Department("Engineering", empList);
        dept.DisplayEmployees();

        // Employees can exist independently and be reused
        Department dept2 = new Department("HR", empList);
        dept2.DisplayEmployees();
    }
}
```
### Example 3: Multi cast delegate

```csharp
using System;

namespace DelegatesDemo {
    class Program {
        public void Add(int x, int y) {
            Console.WriteLine("Sum is: " + (x + y));
        }

        public void Subtract(int x, int y) {
            Console.WriteLine("Difference is: " + (x - y));
        }

        public void Multiply(int x, int y) {
            Console.WriteLine("Product is: " + (x * y));
        }

        public void Divide(int x, int y) {
            Console.WriteLine("Quotient is: " + (x / y));
        }
    }

    public delegate void MultiCastDelegate(int a, int b);

    class ClsDelegate {
        static void Main() {
            Program obj1 = new Program();
            MultiCastDelegate objD = new MultiCastDelegate(obj1.Multiply);
            objD += obj1.Add;
            objD += obj1.Subtract;
            objD += obj1.Divide;
            objD(40, 10);
            objD -= obj1.Add;
            objD -= obj1.Divide;
            objD(50, 10);
            Console.ReadLine();
        }
    }
}
```

---

## Practice Exercises

### 🔹 **1. Notification System**

You're building a notification system that can send Email, SMS, or Push messages.

**Tasks:**
- Create a `Notifier` delegate.
- Implement:
  - `SendEmail()`
  - `SendSMS()`
  - `SendPush()`
- Create a `NotifyAll()` method that takes a `Notifier` delegate and invokes it.

> 🔍 **Hint:** Use `+=` to attach multiple functions to the delegate.

---

### 🔹 **2. Custom Sorting**

Create a program that can sort a list of integers using custom logic.

**Tasks:**
- Create a delegate `ComparisonDelegate(int a, int b)`.
- Implement sorting logic for:
  - Ascending
  - Descending
- Create a `SortList(List<int> nums, ComparisonDelegate comp)` that sorts using the passed logic.

> 🔍 **Hint:** Don’t use `List.Sort()` directly. Write your own loop using the delegate.

---

### 🔹 **3. Math Operation Selector**

Build a calculator where user can select the operation (Add, Subtract, Multiply).

**Tasks:**
- Create a `MathOp(int a, int b)` delegate.
- Implement:
  - `Add(a, b)`
  - `Subtract(a, b)`
  - `Multiply(a, b)`
- Pass these into a method `RunOperation(MathOp op)` and print the result.

---

### 🔹 **4. Logger Hook**

You want to add flexible logging to a function (Console, File, etc.).

**Tasks:**
- Create a delegate `LogHandler(string message)`.
- Implement:
  - `LogToConsole()`
  - `LogToFile()` *(just simulate it with a `Console.WriteLine`)*
- Pass the delegate to a method `ProcessData()` that calls the log handler.

---


### 🔸 **5. Library and Books**

Create a library that holds multiple books.

**Tasks:**
- Class `Book`: properties `Title`, `Author`
- Class `Library` has a `List<Book>`
- Methods: `AddBook(Book)`, `ListBooks()`

> 🔍 **Hint:** Books should be able to exist outside the Library.

---

### 🔸 **6. Order and Products**

An order includes multiple products.

**Tasks:**
- Class `Product`: `Name`, `Price`
- Class `Order`: contains `List<Product>`
- Method: `GetTotalPrice()` returns sum of product prices.

---

### 🔸 **7. Student and Courses**

A student takes multiple courses.

**Tasks:**
- Class `Course`: `Name`, `Code`
- Class `Student`: has `List<Course>`
- Method: `Enroll(Course)`, `ShowCourses()`

> 🔍 **Hint:** Courses can exist independently from students.

---

### 🔸 **8. Car and Music System**

A car can optionally have a music system.

**Tasks:**
- Class `MusicSystem` with `PlayMusic()`
- Class `Car`: optionally has a `MusicSystem`
- `Start()` method optionally plays music

---

## 🔄 BONUS — Delegate + Aggregation Combined

### 🔰 Game Event Dispatcher

You’re building a game that calls multiple "listener" methods when a level is completed.

**Tasks:**
- Use a `delegate void GameEventHandler()`
- `Game` class aggregates a list of listeners
- `CompleteLevel()` triggers all handlers (e.g., `SaveGame`, `UpdateUI`, `TrackAnalytics`)

> 🔍 **Hint:** Use multicast delegate (+=) and aggregation of behaviors.


---


> 🧠 Try to use meaningful examples — don't just print "Hello World". Simulate real software behavior.

---
