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

# MVC Data Flow: Diverse Scenarios with Specific Roles


## Ride-Sharing Trip Booking
**Question**: Explain how data is transacted through the MVC layers when a user books a ride by clicking a “Book Now” button, from the action to saving the trip in the database.

**Roles**:
- **Model**: Stores trip details (e.g., pickup location, driver), validates driver availability and distance, saves trip records to the database.
- **Controller**: Handles “Book Now” button requests, retrieves trip data from Model, sends booking confirmation to View.
- **View**: Displays ride booking interface with map and driver options, shows booking status.

**Data Flow**:
1. Controller queries Model for available drivers; View renders booking page (e.g., “Pickup: [Downtown], Drivers: [Driver A, Driver B]”).
2. View captures “Book Now” click (e.g., `{ UserId: "U501", Pickup: "Downtown", DriverId: 7 }`) and sends to Controller via POST.
3. Controller validates booking data, passes to Model.
4. Model checks driver availability, saves trip (e.g., `{ UserId: "U501", DriverId: 7, Pickup: "Downtown", Date: "2025-07-30" }`).
5. Controller sends success (“Ride booked!”) or error (“No drivers available”) to View for display.

---

## Automated Stock Price Update
**Question**: Describe how data flows through the MVC layers when an automated system updates stock prices via a scheduled API call, saving them to the database.

**Roles**:
- **Model**: Stores stock data (e.g., ticker, price), validates API-fetched prices, saves updates to the database.
- **Controller**: Initiates scheduled stock price API calls, retrieves data from Model, updates View with latest prices.
- **View**: Displays stock price dashboard with real-time updates.

**Data Flow**:
1. Controller triggers scheduled API call, requests stock data from Model (e.g., “Ticker: AAPL”).
2. Model fetches price from external API (e.g., `{ Ticker: "AAPL", Price: 150.25 }`).
3. Controller validates API response, passes to Model.
4. Model saves price update (e.g., `{ Ticker: "AAPL", Price: 150.25, Updated: "2025-07-30 11:09" }`).
5. Controller sends updated prices to View, which displays (e.g., “AAPL: $150.25”) or error (“API error”).

---

## Online Auction Bid Placement
**Question**: Explain the data transaction process through the MVC layers when a user places a bid by clicking a “Place Bid” button, with the bid saved in the database.

**Roles**:
- **Model**: Stores auction and bid data, validates bid amount against current highest bid, saves bid records to the database.
- **Controller**: Handles “Place Bid” button requests, retrieves auction data from Model, updates View with bid status.
- **View**: Displays auction details and bidding interface, shows bid confirmation or errors.

**Data Flow**:
1. Controller fetches auction details from Model; View shows auction page (e.g., “Item: Vintage Car, Current Bid: $500”).
2. View captures “Place Bid” click (e.g., `{ UserId: "U602", AuctionId: 8, BidAmount: 550 }`) and sends to Controller via POST.
3. Controller validates bid, passes to Model.
4. Model checks bid exceeds current highest, saves record (e.g., `{ UserId: "U602", AuctionId: 8, BidAmount: 550, Time: "2025-07-30 11:09" }`).
5. Controller sends success (“Bid placed!”) or error (“Bid too low”) to View for display.

---

## URL-Based Blog Comment System
**Question**: Describe how data flows through the MVC layers when a user submits a comment via a URL navigation (e.g., /Posts/Comment/456), saving the comment in the database.

**Roles**:
- **Model**: Stores blog post and comment data, validates comment content (e.g., no profanity), saves comments to the database.
- **Controller**: Handles comment submission requests from URL navigation, retrieves post data from Model, updates View with comment status.
- **View**: Displays blog post and comment submission interface, shows comment confirmation.

**Data Flow**:
1. Controller fetches post details from Model using URL (e.g., `PostId: 456`); View renders comment page.
2. View captures comment data (e.g., `{ UserId: "U703", PostId: 456, Comment: "Great post!" }`) via AJAX.
3. Controller validates comment, passes to Model.
4. Model verifies comment content, saves record (e.g., `{ UserId: "U703", PostId: 456, Comment: "Great post!", Date: "2025-07-30" }`).
5. Controller sends success (“Comment added!”) or error (“Inappropriate content”) to View for display.

---

## Smart Home Device Control
**Question**: Explain how data is transacted through the MVC layers when a smart home device sends a control command (e.g., turn on light) via an API call, with the command saved in the database.

**Roles**:
- **Model**: Stores device and command data (e.g., light status), validates command feasibility, saves command records to the database.
- **Controller**: Processes device control API calls, retrieves device status from Model, updates View with command results.
- **View**: Displays smart home dashboard with device controls and status.

**Data Flow**:
1. Controller receives API call with command (e.g., `{ DeviceId: "D505", Command: "TurnOn" }`).
2. Controller passes command to Model for validation.
3. Model checks command validity, saves record (e.g., `{ DeviceId: "D505", Command: "TurnOn", Timestamp: "2025-07-30 11:09" }`).
4. Controller fetches updated device status from Model.
5. Controller sends status to View, which updates dashboard (e.g., “Light D505: On”) or error (“Invalid command”).

---

## Restaurant Table Reservation System
**Question**: Describe how data flows through the MVC layers when a customer reserves a table at a restaurant via a web form, from submission to saving the reservation in the database.

**Roles**:
- **Model**: Manages restaurant and reservation data, checks table availability, saves records.
- **Controller**: Processes reservation requests, coordinates with Model and View.
- **View**: Displays reservation form and feedback.

**Data Flow**:
1. Controller fetches available time slots from Model; View shows form (e.g., “Date: [calendar], Time: [dropdown with ‘7 PM’]”).
2. View sends form data (e.g., { CustomerId: "C789", Date: "2025-08-01", Time: "19:00", Guests: 4 }) to Controller.
3. Controller validates input, passes to Model.
4. Model checks table availability, saves reservation (e.g., { CustomerId: "C789", Date: "2025-08-01", Time: "19:00", Guests: 4 }).
5. Controller sends success (“Table reserved!”) or error (“No tables available”) to View.
