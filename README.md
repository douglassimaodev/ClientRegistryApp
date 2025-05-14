# ClientRegistryApp

ClientRegistryApp is a demonstration project showcasing how to create a CRUD (Create, Read, Update, Delete) application for managing clients. It also includes unit testing to ensure the reliability and correctness of the application.

## Features
- Add, edit, delete, and view client information.
- Unit tests to validate the functionality of the application.

## Technologies Used
- **.NET MAUI**: For building cross-platform applications.
- **C#**: The primary programming language.
- **XAML**: For designing the user interface.
- **Xunit**: For unit testing.
- **NSubstitute**: For mocking dependencies in tests.
- **Fluent Assertions**: For expressive and readable test assertions.
- **MVVM Pattern**: To separate concerns and improve code maintainability.

## Project Structure
The project is organized into the following key folders:
- `src/ClientRegistryApp`: Contains the main application code.
- `src/ClientRegistryApp/Pages`: Includes the XAML pages for the user interface.
- `src/ClientRegistryApp/PageModels`: Contains the page models for the MVVM pattern.
- `src/ClientRegistryApp/Services`: Includes service classes for business logic.
- `tests/ClientRegistryApp.UnitTest`: Contains unit tests for the application.

## Getting Started
1. Clone the repository.
2. Open the solution file `ClientRegistryApp.sln` in Visual Studio.
3. Build and run the application on your desired platform.
4. Run the unit tests to verify functionality.