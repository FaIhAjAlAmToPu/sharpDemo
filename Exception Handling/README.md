# C# Exception Handling: Simple Guide for Students

Errors, like dividing by zero or opening a missing file, can crash your program. Exception handling helps you catch and fix these problems so your program keeps running smoothly.

---

## What Is Exception Handling?

Exception handling lets your program deal with errors (called "exceptions") without crashing. In C#, we use `try`, `catch`, `finally`, and `throw` to manage errors. 

```csharp
try
{
    // Put code here that might cause an error
    // Example: dividing numbers or reading a file
}
catch (Exception ex)
{
    // This runs if an error happens
    // "ex" holds details about the error
    // Example: show a message like "Oops, something went wrong!"
}
finally
{
    // This runs no matter what (error or no error)
    // Example: close a file or clean up resources
}

// You can also create your own error with "throw"
throw new Exception("Something bad happened!");
```

**Key Ideas**:
- **try**: Holds code that might cause an error (like dividing by zero).
- **catch**: Runs when an error happens. You can catch specific errors (like `DivideByZeroException`) or all errors with `Exception`.
- **finally**: Runs every time, whether there’s an error or not. Great for cleaning up (like closing files).
- **throw**: Lets you create your own error when something’s wrong (like invalid input).
- **Tips**:
  - Always catch specific errors (like `FormatException`) before the general `Exception`.
  - Use `finally` to clean up things like files or connections.
  - Show helpful messages to users when errors happen.
  - Don’t leave `catch` blocks empty—it hides problems!

---

Here’s how they work:

```csharp
try
{
    // Code that might cause an error
    int x = 10, y = 0;
    Console.WriteLine(x / y); // This could crash!
}
catch (DivideByZeroException)
{
    // Runs if an error happens
    Console.WriteLine("Can't divide by zero!");
}
finally
{
    // Runs no matter what
    Console.WriteLine("Done trying!");
}
```

**Key Parts**:
- **try**: Put code here that might cause an error, like dividing numbers or reading files.
- **catch**: Runs when an error happens. It can catch specific errors (like `DivideByZeroException`) or all errors with `Exception`.
- **finally**: Runs every time, whether there’s an error or not. Great for cleaning up (like closing files).
- **throw**: Lets you create your own error, like when someone enters bad data.

**Pro Tips for Students**:
- Catch specific errors (like `DivideByZeroException`) before general `Exception`.
- Use `finally` to clean up things like files.
- Show clear error messages, like "Please try again!" instead of "Error."
- Don’t leave `catch` blocks empty—it hides problems!

---

## More Examples

### Example 1: Handling Invalid Input
```csharp
try
{
    Console.Write("Enter a number: ");
    int num = int.Parse(Console.ReadLine());
    Console.WriteLine($"You entered: {num}");
}
catch (FormatException)
{
    Console.WriteLine("That’s not a number! Try again.");
}
```
**What It Does**: If the user types "abc" instead of a number, the program shows a friendly message instead of crashing.

**Pro Tip**: Always check user input—it’s a common source of errors!

### Example 2: Throwing Your Own Error
```csharp
try
{
    int age = -5;
    if (age < 0) throw new ArgumentException("Age can’t be negative!");
    Console.WriteLine($"Age: {age}");
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
```
**What It Does**: If the age is negative, the program creates an error with a custom message.

**Pro Tip**: Use `throw` to enforce rules, like valid ages or numbers.

### Example 3: Custom Error
```csharp
public class NoMoneyException : Exception
{
    public NoMoneyException(string message) : base(message) { }
}

try
{
    int balance = 0;
    if (balance <= 0) throw new NoMoneyException("No money left!");
}
catch (NoMoneyException ex)
{
    Console.WriteLine(ex.Message);
}
```
**What It Does**: Creates a custom error for when a bank account has no money.

**Pro Tip**: Make custom errors for specific problems in your program.

---

## Practice Problems (Easy to Hard)

Try these problems to practice exception handling. Start with the easy ones and move to the harder ones as you get better.

### Easy Problems
1. **Check User Input**  
   Write a program that asks for a number and converts it to an integer. If the user enters something like "hello", show "Please enter a valid number!"  
   *Hint*: Use `int.Parse` in a `try` block and catch `FormatException`.

2. **Divide Numbers**  
   Ask the user for two numbers and divide the first by the second. If they enter zero for the second number, show "Can’t divide by zero!"  
   *Hint*: Do the division in a `try` block and catch `DivideByZeroException`.

### Medium Problems
3. **Safe Array Number**  
   Use an array `{10, 20, 30}`. Ask the user for an index and show the number at that index. If the index is wrong (like 5), show "Invalid index!"  
   *Hint*: Catch `IndexOutOfRangeException` in a `try-catch` block.

4. **Check Temperature**  
   Write a method that takes a temperature (in Celsius). If it’s below -273.15 or above 1000, throw an error saying "Invalid temperature!" Catch it in `Main` and show the message.  
   *Hint*: Use `throw new ArgumentException` and catch it in `Main`.

### Hard Problem
5. **Custom Error for Shopping**  
   Create a custom error called `OutOfStockException`. Write a `Shop` class with a `BuyItem` method that takes a quantity. If the quantity is zero or negative, throw `OutOfStockException`. Catch it in `Main` and show the error message.  
   *Hint*: Make `OutOfStockException` inherit from `Exception`. Use `throw` and catch it in `Main`.

---

## For Students: Keep Practicing!

- **Test Your Code**: Try bad inputs (like "abc" for a number or 0 for division) to see if your program handles them well.
- **Use Pro Tips**: Follow the tips in each section to write better code.
- **Ask Questions**: If you’re stuck, ask why an error happens or how to fix it.
- **Real-World Use**: Exception handling is used in apps, games, and websites to avoid crashes when users do something unexpected.

This guide and problems will help you learn how to handle errors in C# step-by-step. Try each problem and test your code to make sure it works!