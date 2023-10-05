# Checkout System

This repository contains a simple checkout system for the tech task that supports individual item pricing as well as special pricing for bulk purchases.

## Project Structure

- **CheckoutSystem**: A .NET Class Library containing the core logic for the checkout system.
- **CheckoutSystem.Tests**: A unit test project (using xUnit) to validate the functionality of the checkout system.

## Getting Started

### Prerequisites

- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or any compatible IDE.

### Setup

1. Clone the repository:
   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```bash
   cd path-to-repository/CheckoutSystem
   ```

3. Restore the NuGet packages:
   ```bash
   dotnet restore
   ```

4. Build the solution:
   ```bash
   dotnet build
   ```

### Running Tests

Navigate to the test project directory and run:

```bash
dotnet test
```

## Implementation Details

### Core Logic

The core logic resides in the `CheckoutSystem` project. The main components are:

- **ICheckout Interface**: Defines the contract for the checkout operations.
- **Checkout Class**: Implements the `ICheckout` interface. It handles item scanning and total price calculation based on the provided pricing rules.

### Testing

The `CheckoutSystem.Tests` project contains unit tests for the core logic. It uses the xUnit testing framework.

## Performance

- Scanning an item (`Scan` method) has a time complexity of O(1).
- Calculating the total price (`GetTotalPrice` method) has a time complexity of O(n), where n is the number of unique items in the cart.
