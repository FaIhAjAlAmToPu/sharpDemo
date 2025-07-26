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

Instead of writing if/else to decide which function to call, you assign the function to a variable (delegate) and call it flexibly.


### Key Features
- **Type Safety**: Delegates ensure the assigned method matches the defined signature.
- **Multicasting**: A single delegate can invoke multiple methods.
- **Flexibility**: Delegates enable decoupling of method calls, useful in event-driven programming.

---

## Understanding Aggregation

### What is Aggregation?
**Aggregation** is a "has-a" relationship where one class contains an instance of another class, but the contained object can exist independently. It represents a loose coupling between objects, unlike composition, where the contained object’s lifecycle depends on the container.

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

---

## Practice Exercises

### Exercise 1: Delegate for String Manipulation
1. Create a delegate called `StringTransformer` that takes a string and returns a string.
2. Write two methods: one to convert a string to uppercase and another to reverse a string.
3. Assign these methods to the delegate and test them.
4. Combine both methods into a multicast delegate and observe the output.

### Exercise 2: Aggregation with Library and Books
1. Create a `Book` class with properties like `Title` and `Author`.
2. Create a `Library` class that aggregates a list of `Book` objects.
3. Implement a method in `Library` to display all book details.
4. Test by creating multiple books and adding them to a library. Then, reuse the same books in another library to demonstrate aggregation.

### Exercise 3: Combining Delegates and Aggregation
1. Create a `Calculator` class that aggregates a list of `Operation` delegates (like in Example 1).
2. Add a method to the `Calculator` class to execute all operations on two input numbers.
3. Test by adding multiple operations (e.g., add, subtract, multiply) and running them.

---
