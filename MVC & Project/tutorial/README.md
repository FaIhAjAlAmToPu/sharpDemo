# MVC Pattern Tutorial: Model, View, Controller

This tutorial clarifies the **Model-View-Controller (MVC)** pattern, focusing on the clear boundaries between **Model**, **View**, and **Controller** in C# applications.

## Prerequisites
- Basic knowledge of C# (classes, methods, objects).
- A C# development environment (e.g., Visual Studio, VS Code with .NET SDK, or an online compiler).

---

## What is MVC?

MVC is a design pattern that organizes an application into three components—**Model**, **View**, and **Controller**—to separate concerns, making code modular and easier to maintain. Each component has a specific role, and boundaries ensure they don’t overlap.

---

## Model: Data and Logic

### Role
The **Model** represents the application’s data and business logic. It’s the core that manages data storage, retrieval, and processing, like a database or a set of rules.

### Responsibilities
- Stores data (e.g., patient records, quiz questions, product details).
- Handles business rules (e.g., scheduling logic, scoring quizzes, calculating discounts).
- Interacts with data sources (e.g., databases, external APIs).
- **Does NOT**: Deal with user interface or process user input directly.

### Boundary
The Model is isolated from the UI and user actions. It provides data to the Controller but doesn’t control how it’s displayed or what users do.

---

## Controller: Coordinator

### Role
The **Controller** is the middleman, handling user input, updating the Model, and refreshing the View. It ensures smooth communication between the other components.

### Responsibilities
- Processes user input (e.g., form submissions, button clicks).
- Updates the Model based on input (e.g., booking an appointment, submitting quiz answers).
- Sends data to the View for display.
- **Does NOT**: Contain business logic or render UI elements directly.

### Boundary
The Controller only coordinates. It doesn’t store data long-term or define how data appears in the UI.

---

## View: User Interface

### Role
The **View** is the application’s user interface, displaying data to users and capturing their input.

### Responsibilities
- Renders data in a user-friendly format (e.g., web pages, app screens).
- Shows data provided by the Controller (e.g., appointment details, quiz scores).
- Sends user input to the Controller (e.g., form submissions, button clicks).
- **Does NOT**: Manipulate data or implement business logic.

### Boundary
The View is purely for presentation. It doesn’t access the Model directly or process data.

---

## Boundaries Between Components

- **Model ↔ Controller**: The Model provides data and logic; the Controller requests or updates data. The Model never interacts with the View.
- **Controller ↔ View**: The Controller sends data to the View and receives user input. The View doesn’t access the Model.
- **Model ↔ View**: No direct interaction. The Controller keeps them separate.

---
![alt text](../image-17.png)
## Real-World Examples

### 1. A web app where patients book appointments with doctors

- **Model**:
  - Represents data like patient profiles (name, ID), doctor schedules, and appointment details (date, time).
  - Handles logic, such as checking doctor availability or preventing double-booking.
  - Example: A method ensures appointments are only scheduled during a doctor’s working hours (e.g., 9 AM–5 PM).

- **Controller**:
  - Processes user actions, like submitting a booking form.
  - Retrieves available time slots from the Model and updates it when a patient books an appointment.
  - Passes appointment details to the View for display.
  - Example: When a patient selects a doctor and time, the Controller validates the input and updates the Model.

- **View**:
  - Displays a calendar of available slots or a confirmation page for booked appointments.
  - Captures user input, like selecting a time or submitting patient details.
  - Example: A web page showing a doctor’s schedule with clickable time slots.

### 2.  A platform where students take quizzes and view scores

- **Model**:
  - Stores quiz data (questions, answers, correct options) and student data (scores, attempts).
  - Manages logic, like calculating scores or determining pass/fail status.
  - Example: A method calculates a student’s score based on selected answers.

- **Controller**:
  - Handles user actions, like starting a quiz or submitting answers.
  - Fetches questions from the Model and sends them to the View.
  - Updates the Model with the student’s answers and retrieves the final score.
  - Example: When a student submits a quiz, the Controller processes the answers and requests the score from the Model.

- **View**:
  - Displays quiz questions, answer options, and final scores.
  - Captures user selections (e.g., multiple-choice answers).
  - Example: A web page showing a question with radio buttons and a “Submit” button.

### 3. An online store where users browse products and place orders

- **Model**:
  - Manages product data (name, price, stock) and order details (items, total cost).
  - Implements logic, like applying discounts or checking stock availability.
  - Example: A method applies a 10% discount for orders over $100.

- **Controller**:
  - Processes user actions, like adding items to a cart or checking out.
  - Retrieves product data from the Model and updates it (e.g., reduces stock after purchase).
  - Sends product or cart details to the View.
  - Example: When a user adds a product to their cart, the Controller updates the Model’s cart data.

- **View**:
  - Displays product listings, cart contents, or order confirmations.
  - Captures user actions, like clicking “Add to Cart” or “Checkout.”
  - Example: A product page showing item details and an “Add to Cart” button.

---

## Practice Exercise

Create a simple MVC application for a **Library Management System**:
1. **Model**: Define a `Book` class with properties (e.g., Title, Author, ISBN) and logic to check if a book is available for borrowing.
2. **Controller**: Create a `LibraryController` to handle actions like listing available books or borrowing a book. It should fetch data from the Model and pass it to the View.
3. **View**: Design a simple UI (e.g., console output or web page) to display the book list and allow users to select a book to borrow.
4. **Test Boundaries**:
   - Ensure the Model only handles data and logic (e.g., no UI code).
   - Ensure the Controller only coordinates (e.g., no discount calculations).
   - Ensure the View only displays data and captures input (e.g., no direct Model access).

---