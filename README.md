# Simple Notification System

A C# Console Application that implements a robust notification system using a 3-tier architecture. It provides a menu-driven interface to manage users and send different types of notifications (Email and SMS) to them.

## Architecture

The project follows a 3-tier architecture, separating the application into distinct, maintainable layers:

- **Models**: Defines the core entities like `User`, `Notification` (base class), `EmailNotification`, and `SmsNotification`. Also includes custom domain exceptions in `NotificationExceptions.cs`.
- **Interfaces**: Contains contracts for the system components such as `INotification`, `INotificationSender`, and `IUserRepository` to ensure loose coupling.
- **Repositories**: The data access layer. Handles in-memory storage and retrieval for users (`UserRepository`) and notifications (`NotificationRepository`).
- **Services**: The business logic layer. Includes `UserService` for handling user-related operations (CRUD) and `NotificationService` for validating, processing, and dispatching notifications.
- **Notification Senders**: Concrete implementations for dispatching specific notifications like `EmailNotificationSender` and `SmsNotificationSender`.
- **UI (Console)**: The main entry point (`Program.cs`) providing an interactive, continuous loop menu to interact with the system.

## Features

### 1. User Management (CRUD)
- **Create User**: Add a new user with robust validation ensuring a unique ID, non-empty name, valid email format (e.g., name@gmail.com), and proper phone number formatting.
- **View All Users**: Display a comprehensive list of all registered users utilizing LINQ for clean data presentation.
- **Update User**: Modify the name, email, and phone number of an existing user based on their unique ID.
- **Delete User**: Safely remove a user from the system.

### 2. Notification Management
- **Send Notification**: Select an existing user from the system, choose a notification type (Email or SMS), and input a message to send. The system enforces specific business rules, such as maximum length constraints for SMS and minimum lengths for general messages.
- **View Notifications**: Display a log of all sent notifications, including recipient details, notification type, dispatch status, and the exact date and time it was sent.

### 3. Exception Handling
The application features comprehensive error handling using custom exceptions to manage edge cases gracefully without crashing:
- `DuplicateUserException`: Prevent adding a user with an existing ID.
- `UserNotFoundException`: Handle cases where operations target non-existent users.
- `InvalidNotificationTypeException`: Handle incorrect menu selections for notification types.
- `EmptyMessageException` & `MessageTooShortException`: Validate notification content.
- `SmsTooLongException`: Enforce character limits on SMS messages.
- `InvalidEmailException` & `InvalidPhoneException`: Validate contact information formats.

## Technologies Used
- **C#**
- **.NET** (Console Application)
- **LINQ** (Language Integrated Query) for concise querying and data manipulation

