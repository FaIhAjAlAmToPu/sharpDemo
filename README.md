# 💻 C# Abstraction & Interface – Coding Tasks

Write all your code in a single `.cs` file using a `Main` method for testing. Implement the following tasks step by step.

---

### ✅ Task 1: Basic Abstract Class with Constructor

- Create an abstract class `Animal` with a field `name`, a constructor, and an abstract method `MakeSound()`.
- Create a class `Dog` that inherits from `Animal` and implements `MakeSound()`.
- In `Main()`, create a `Dog` object and call the method.

---

### ✅ Task 2: Multiple Interface

- Create two interfaces `IFlyable` and `ISwimmable` with appropriate method declarations.
- Create a class `Duck` that implements both interfaces.
- In `Main()`, create a `Duck` object and call its methods.

---

### ✅ Task 3: Abstract Class + Interface Combination

- Define an interface `IDriveable` with a method `Drive()`.
- Define an abstract class `Vehicle` with an abstract method `Start()` and a concrete method `FuelUp()`.
- Create a class `Car` that extends `Vehicle` and implements `IDriveable`.
- In `Main()`, test all methods.

---

### ✅ Task 4: Interface extending Interface

- Create an interface `IMachine` with a method `Start()`.
- Create another interface `IAdvancedMachine` that inherits `IMachine` and adds `Stop()`.
- Implement a class `Robot` that implements `IAdvancedMachine` and both methods.
- Test it in `Main()`.

---

### ✅ Task 5: Abstract Class with Child Fields and Constructor

- Create an abstract class `Person` with a protected field `name`, a constructor, and an abstract method `Work()`.
- Create a class `Student` with its own field `studentId`, which inherits from `Person`.
- Implement `Work()` and test the object in `Main()`.

---


### ✅ Task 6: Abstract Class with Concrete and Abstract Method

- Create an abstract class `Report` with a concrete method `Title()` and abstract method `Generate()`.
- Create a class `SalesReport` that overrides `Generate()`.
- In `Main()`, call both methods using a `SalesReport` object.

---

### ✅ Task 8: Abstract Class + Interface + Own Method

- Create an interface `IPlayable` with method `Play()`.
- Create an abstract class `Game` with `CommonRule()` (concrete) and `Start()` (abstract).
- Create a class `Football` that inherits `Game` and implements `IPlayable`.
- Add a method `ExtraTime()` in `Football`.
- In `Main()`, demonstrate all methods.

---

### ✅ Bonus Task 1: Employee Management System

- Create an abstract class `Employee` with:
  - Field: `name`
  - Abstract method `CalculateSalary()`
  - Concrete method `DisplayInfo()`
- Create an interface `IBonusEligible` with method `GetBonus()`.
- Create two classes:
  - `FullTimeEmployee` inherits `Employee`, implements `IBonusEligible`
  - `PartTimeEmployee` only inherits `Employee`
- In `Main()`, use polymorphism to calculate salaries and bonuses.

---

### ✅ Bonus Task 2: File System Simulation

- Create an abstract class `FileItem` with:
  - Fields: `Name`, `Size`
  - Abstract method `Open()`
- Create an interface `ICompressible` with `Compress()`
- Create classes:
  - `TextFile` (inherits `FileItem`)
  - `ZipFile` (inherits `FileItem`, implements `ICompressible`)
- In `Main()`, demonstrate:
  - Opening both files
  - Compressing only `ZipFile` using interface reference

---

### ✅ Bonus Task 3: Smart Home Devices

- Create an abstract class `Device` with:
  - Field: `DeviceName`
  - Abstract method `TurnOn()`
- Create interfaces:
  - `ISchedulable` with method `ScheduleTask(string time)`
  - `IUpdatable` with method `UpdateFirmware()`
- Create classes:
  - `SmartLight` (inherits `Device`, implements `ISchedulable`)
  - `SmartThermostat` (inherits `Device`, implements both interfaces)
- Demonstrate:
  - Calling `ScheduleTask` and `UpdateFirmware` polymorphically
  - Creating a `List<Device>` and looping through it

---

### ✅ Bonus Task 4: University Course Management

- Create an abstract class `Course` with:
  - Fields: `CourseName`, `Credits`
  - Abstract method `PrintSchedule()`
- Create interfaces:
  - `IOnline` with `StartWebSession()`
  - `IInPerson` with `AssignRoom()`
- Create classes:
  - `OnlineCourse` (inherits `Course`, implements `IOnline`)
  - `OfflineCourse` (inherits `Course`, implements `IInPerson`)
- In `Main()`:
  - Create objects of both courses and call all available methods
  - Store both in a `List<Course>` and call `PrintSchedule()` polymorphically

---

### ✅ Bonus Task 5: E-Commerce Checkout System

- Create an abstract class `Product` with:
  - Field: `Price`
  - Abstract method `CalculateTotalPrice()`
- Create interfaces:
  - `IDiscountable` with method `ApplyDiscount()`
  - `IShippable` with method `GetShippingCost()`
- Create classes:
  - `PhysicalProduct` (inherits `Product`, implements `IShippable`)
  - `DigitalProduct` (inherits `Product`, implements `IDiscountable`)
- In `Main()`:
  - Create and use both product types
  - Calculate total with discounts and shipping (polymorphically)

---