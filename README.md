# GymStat — Gym Workout Tracker

## 1. Introduction
GymStat is a lightweight desktop application for tracking strength-training workouts.

Main features
- Track exercises and record workout results (sets, reps, weight).
- Browse a predefined exercise catalog with images.
- Create and edit exercise results.
- View exercise history.

UI mockups
- Main window (home): list of exercises with thumbnails.
- Exercise form: view to add a new exercise result (sets with reps and weight).
- History / detail view: per-exercise timeline of previous sessions and sets.

User stories
- "As a user, I want to record sets for an exercise so that I can track my progress over time." 

## 2. Technology Stack
- Language: C# (recommended SDK: .NET 8)
- Framework: .NET 8 (Desktop/WPF)
- UI framework: WPF with XAML
- Package management: NuGet
- Data storage: local JSON files
- Patterns: MVVM (Model-View-ViewModel)

MVVM explanation
- Models: data-centric classes representing domain objects, e.g. `Models/Exercise.cs`, `Models/ExerciseResult.cs`, `Models/Set.cs`.
- ViewModels: UI logic, properties and commands exposed to Views, e.g. files under `ViewModels/` such as `MainViewModel.cs`, `HomeViewModel.cs`, `ExerciseFormViewModel.cs`.
- Views: XAML and code-behind that define the UI, under `Views/` (e.g. `Views/MainWindow.xaml`, `Views/HomeView.xaml`, `Views/ExerciseFormView.xaml`).

## 3. Project Structure
- `App.xaml`, `App.xaml.cs` — application entry
- `Views/` — XAML views and code-behind
- `ViewModels/` — view model classes
- `Models/` — domain models
- `Services/` — data access and business logic helpers
- `Commands/` — `RelayCommand.cs` for ICommand implementation
- `Stores/` — `NavigationStore.cs` for simple navigation/state management
- `Converters/` — value converters used in XAML
- `Images/` — images used in the app
- `exercises.json` — sample exercise catalog used by the app

## 4. Key mechanisms and components
- Navigation: `Stores/NavigationStore.cs` provides a simple navigation/state mechanism between views.
- Commands: `Commands/RelayCommand.cs` implements `ICommand` to bind UI actions to ViewModel logic.
- Data binding: ViewModels expose observable properties. Views bind to them in XAML.
- Value converters: `Converters/` transform bound values for UI display (images, dates, indexes).
- Persistence: `Services/` layer reads/writes JSON files to persist exercises and results locally.

## 5. How to run
Prerequisites
- .NET 8 SDK installed
- Visual Studio 2022/2026

Run steps
1. Clone the repository or open the folder in Visual Studio: `GymStat.sln`.
2. Run (F5) or `dotnet run` from the startup project directory.