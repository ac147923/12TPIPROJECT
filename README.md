# 12TPIPROJECT

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [Troubleshooting](#troubleshooting)
- [Support](#support)
- [Contributing](#contributing)
- [License](#license)
- [Credits](#credits)

---

## Overview

**12TPIPROJECT** is a C# application designed to manage users, teams, coaches, players, and countries, with robust input validation and comprehensive menu options. It supports registration, login, CRUD operations for all entities, and various report generation features.

---

## Features

- User registration and login with username & 4-digit PIN
- Admin and non-admin user roles
- Add, update, and delete:
  - Countries
  - Cities
  - Players
  - Coaches
  - Domestic Teams
  - Player-Team and Coach-Team Bridges
- Comprehensive menu navigation (Main, Admin, Reports, etc.)
- Input validation for all fields (boundaries, formats, duplicates, etc.)
- Multiple levels of reporting (Simple, Advanced, Complex)
- Error handling and user feedback on invalid inputs

---

## Getting Started

### Prerequisites

- [.NET 6.0 SDK or later](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022 or later, or any compatible C# IDE

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/ac147923/12TPIPROJECT.git
   cd 12TPIPROJECT
   ```

2. **Restore dependencies:**
   ```bash
   dotnet restore
   ```

3. **Build the project:**
   ```bash
   dotnet build
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

---

## Usage

- On launch, you will see the Main Menu.
- Register a new user or log in.
- Navigate through menus to manage entities or generate reports.
- Follow on-screen prompts for input.
- All inputs are validated; errors will be shown for invalid entries.

---

## Project Structure

```
12TPIPROJECT/
├── src/                # Source code
├── tests/              # Unit and integration tests
├── README.md
├── LICENSE
└── ...
```

- **src/**: Contains all application code (models, services, menus, validation, etc.)
- **tests/**: Contains test cases for all input scenarios and features

---

## Testing

- The project contains extensive automated and manual test cases for all major input scenarios:
  - Minimum/maximum boundary (valid/invalid)
  - Typical valid/invalid input
  - Edge case handling (empty, whitespace, special characters, etc.)
- To run tests:
  ```bash
  dotnet test
  ```
- For detailed test results, see the [Testing Documentation](./tests/README.md) (if available).

---

## Troubleshooting

**Common Issues:**

- **App doesn't start:**  
  - Ensure you have the correct .NET SDK installed.
  - Run `dotnet restore` and `dotnet build` again.
- **Input not accepted as expected:**  
  - Only use valid input formats as prompted.
  - For usernames: 2-50 characters, letters only.
  - For PINs: 4 numeric digits.
  - For names: No special characters or numbers.
- **Tests fail:**  
  - Check for recent code changes or updates to dependencies.
  - Review the failed test cases in the output for details.

If issues persist, see [Support](#support).

---

## Support

- **GitHub Issues:**  
  Please open an [Issue](https://github.com/ac147923/12TPIPROJECT/issues) for bugs, feature requests, or questions.
- **Discussions:**  
  For general questions or sharing ideas, use the [Discussions](https://github.com/ac147923/12TPIPROJECT/discussions) tab.
- **Email:**  
  For private inquiries, contact the maintainer via GitHub profile.

---

## Contributing

Contributions are welcome!  
To contribute:

1. Fork the repository
2. Create a new branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Create a Pull Request

See [CONTRIBUTING.md](./CONTRIBUTING.md) for more guidelines (if available).

---

## License

This project is licensed under the [MIT License](./LICENSE).

---

## Credits

- Developed by [ac147923](https://github.com/ac147923)
- Uses .NET and C#
- Thanks to contributors and testers
