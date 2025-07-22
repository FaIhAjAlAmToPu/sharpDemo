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

# Programming Scenarios with OOP Guidance

This README provides five real-world-inspired programming scenarios to practice object-oriented programming (OOP) concepts. Each scenario includes a description and guidance to help you decide when to use abstract classes, interfaces, abstract methods, and polymorphism. Your task is to design and implement a program for each scenario, deducing the appropriate OOP constructs based on the requirements and hints provided.

---

## Scenario 1: Company Payroll System

### Description
You’re developing a payroll system for a company with full-time and part-time employees. Every employee has a name and requires a salary calculation, but the calculation differs: full-time employees earn a fixed monthly salary, while part-time employees are paid based on hours worked. Full-time employees are eligible for a performance-based bonus, but part-time employees are not. The system should display employee information (name and salary) and calculate bonuses for eligible employees, processing a mix of employees efficiently, possibly in a collection.

### Guidance
- **Common Attributes and Behaviors**: All employees share a name and the need to calculate a salary. Consider a structure that defines these commonalities to avoid code duplication.
  - *Hint*: A parent structure with shared attributes and a method to enforce salary calculation (without specifying how) could be useful. Since salary calculation varies by employee type, this method should be defined but left for subclasses to implement.
- **Unique Behaviors**: Only full-time employees receive bonuses. You need a way to ensure that bonus-related functionality is only available to certain employee types.
  - *Hint*: A contract that guarantees a bonus calculation method for specific employees, without affecting others, would work well. This contract should be separate from the core employee structure to allow flexibility.
- **Polymorphism**: The system needs to process different employee types uniformly (e.g., displaying info or calculating salaries) while allowing bonus calculations only for eligible employees.
  - *Hint*: Store employees in a collection of a common type and call shared methods. For bonuses, use a type check or a specific contract reference to access the bonus method only when appropriate.
- **Key Questions**:
  - How can you ensure all employees have a name and a salary calculation method, but the calculation differs by type?
  - How can you restrict bonus eligibility to full-time employees without forcing part-time employees to implement it?
  - How can you process a mix of employees in a loop to display their info or calculate salaries?

---

## Scenario 2: Virtual File Explorer

### Description
You’re building a virtual file explorer that simulates a file system. Files have a name and size, and each can be opened, but the opening process differs (e.g., a text file displays contents, a zip file lists contained files). Some files, like zip files, can be compressed to reduce size, but others, like text files, cannot. The program should allow opening any file and compressing files that support compression, using a consistent approach for compression-capable files.

### Guidance
- **Common Attributes and Behaviors**: All files share a name, size, and the ability to be opened. You need a structure to define these shared properties and behaviors.
  - *Hint*: A base structure with these attributes and a method to enforce opening (without defining the specifics) would help ensure consistency across file types.
- **Unique Behaviors**: Only certain files (e.g., zip files) can be compressed. You need a way to add compression functionality only to those files that support it.
  - *Hint*: A separate contract that defines compression behavior can be applied to specific file types, ensuring that only those types implement it without affecting others.
- **Polymorphism**: The system should open any file type uniformly and compress files that support compression, possibly using a specific reference for compression.
  - *Hint*: Use a collection of the common file type to call the open method on all files. For compression, use a reference to the compression contract to call the compress method only on compatible files.
- **Key Questions**:
  - How can you ensure all files have a name, size, and an open method, with different opening behaviors?
  - How can you restrict compression to specific file types without requiring all files to support it?
  - How can you process a mix of files to open them and compress only those that allow it?

---

## Scenario 3: Smart Home Automation

### Description
You’re designing a smart home system to control devices like lights and thermostats. Each device has a name and can be turned on, but the turn-on process varies (e.g., a light brightens, a thermostat sets a temperature). Some devices, like smart lights, can be scheduled to activate at specific times. Others, like smart thermostats, can both be scheduled and receive firmware updates. The system should control all devices uniformly (e.g., turning them on) and handle scheduling and updating for devices that support those features, possibly in a collection.

### Guidance
- **Common Attributes and Behaviors**: All devices share a name and the ability to be turned on. You need a structure to define these common elements.
  - *Hint*: A parent structure with a name attribute and a method to enforce turning on (with implementation left to subclasses) would ensure consistency.
- **Unique Behaviors**: Scheduling is supported by some devices (e.g., lights and thermostats), while firmware updates are only for specific devices (e.g., thermostats). You need to enforce these behaviors only for certain device types.
  - *Hint*: Separate contracts for scheduling and updating can be applied to devices that support those features. This allows flexibility without forcing all devices to implement unnecessary methods.
- **Polymorphism**: The system should turn on all devices uniformly and call scheduling or updating methods for devices that support them, possibly in a collection.
  - *Hint*: Store devices in a collection of the common device type to call the turn-on method. Use specific contract references to call scheduling or updating methods only on compatible devices.
- **Key Questions**:
  - How can you ensure all devices have a name and a turn-on method, with different turn-on behaviors?
  - How can you enforce scheduling for some devices and firmware updates for others without affecting all devices?
  - How can you process a mix of devices to turn them on and handle scheduling or updates appropriately?

---

## Scenario 4: University Course Registration

### Description
You’re creating a course registration system for a university offering online and in-person courses. Every course has a name and credit value, and each has a schedule to display, though the schedule format differs (e.g., online courses show virtual meeting times, in-person courses show class times and locations). Online courses require starting a web session, while in-person courses need a physical room assigned. The system should manage a collection of courses, display their schedules, and handle specific actions (web sessions or room assignments) based on course type.

### Guidance
- **Common Attributes and Behaviors**: All courses share a name, credit value, and the need to display a schedule. You need a structure to define these commonalities.
  - *Hint*: A base structure with these attributes and a method to enforce schedule display (with implementation details left to subclasses) would promote code reuse.
- **Unique Behaviors**: Online courses need a web session, while in-person courses need room assignments. These behaviors are specific to each course type.
  - *Hint*: Separate contracts for web sessions and room assignments can be applied to the respective course types, ensuring that only those types implement the relevant behavior.
- **Polymorphism**: The system should display schedules for all courses uniformly and handle web sessions or room assignments for specific course types, possibly in a collection.
  - *Hint*: Use a collection of the common course type to call the schedule display method. Use specific contract references to call web session or room assignment methods only on compatible courses.
- **Key Questions**:
  - How can you ensure all courses have a name, credits, and a schedule display method, with different display formats?
  - How can you restrict web sessions to online courses and room assignments to in-person courses?
  - How can you process a mix of courses to display schedules and perform course-specific actions?

---

## Scenario 5: Online Shopping Cart

### Description
You’re building an e-commerce checkout system for a store selling physical and digital products. Each product has a price, but the total cost calculation differs: physical products include shipping costs, while digital products may have discounts. Physical products need a way to calculate shipping costs (e.g., based on weight or destination), while digital products can have promotional discounts applied. The system should handle a mix of products in a shopping cart, calculate their total prices, and account for shipping or discounts as appropriate.

### Guidance
- **Common Attributes and Behaviors**: All products share a price and the need to calculate a total price. You need a structure to define these shared elements.
  - *Hint*: A parent structure with a price attribute and a method to enforce total price calculation (with specifics left to subclasses) would ensure consistency.
- **Unique Behaviors**: Physical products require shipping cost calculations, while digital products can have discounts applied. These behaviors are specific to each product type.
  - *Hint*: Separate contracts for shipping cost calculation and discount application can be applied to the respective product types, allowing flexibility without forcing all products to implement both.
- **Polymorphism**: The system should calculate total prices for all products uniformly and handle shipping or discounts for specific product types, possibly in a collection.
  - *Hint*: Store products in a collection of the common product type to call the total price calculation method. Use specific contract references to call shipping or discount methods only on compatible products.
- **Key Questions**:
  - How can you ensure all products have a price and a total price calculation method, with different calculation logic?
  - How can you restrict shipping costs to physical products and discounts to digital products?
  - How can you process a mix of products to calculate total prices and apply shipping or discounts appropriately?

---

## General Tips for Implementation
- **Abstract Classes**: Use when entities share common attributes and behaviors, but some behaviors need to be defined differently by each type. Abstract classes are ideal for providing a partial implementation and enforcing specific methods for subclasses.
- **Interfaces**: Use when you want to define a contract for specific behaviors that only some classes need to implement. Interfaces are great for adding optional functionality without affecting unrelated classes.
- **Abstract Methods**: Use in abstract classes to enforce that all subclasses provide their own implementation of a method, ensuring consistency while allowing flexibility.
- **Polymorphism**: Use to treat different types uniformly through a common type (e.g., a parent class or interface). Store objects in a collection and call shared methods, or use interface references to access type-specific behaviors.
- **Collections**: Use lists or arrays to store objects of a common type, allowing you to loop through them and call shared methods polymorphically. Check types or use interface references for type-specific actions.
- **Design Questions**:
  - What attributes and methods are shared across all types in a scenario?
  - Which behaviors are unique to specific types, and how can you enforce them without affecting other types?
  - How can you process all objects uniformly while still accessing type-specific functionality?

For each scenario, write a program that implements the described functionality. Use the guidance to decide on the structure (e.g., abstract classes, interfaces) and demonstrate polymorphism by processing objects in a collection or through specific references. Test your program to ensure all required behaviors (e.g., salary/bonus calculations, file opening/compression, device control, course management, product pricing) work as expected.
