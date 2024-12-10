TechnologyOne test 

Description:-

 Web user interface convert number into word. The application takes numerical input (e.g., 123.45) and returns the equivalent phrase in words (e.g., ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS).

Features:-
 -Converts numbers into words for both integer and decimal parts.
 -Supports monetary formatting (e.g., dollars and cents).
 -Simple and clean user interface for input and result display.
 -Implements a web server routine for back-end conversion logic.

Technologies Used
 Frontend: HTML, CSS, JavaScript
 Backend: ASP.NET Core
 Database (if applicable): Entity Framework Core (for caching conversion rules)

Getting Started

1.Prerequisites
 -NET SDK 8.0 or later
 -Visual Studio or any IDE supporting ASP.NET Core
 -(Optional) Local SQL Server for caching

2. Installation
 git clone https://github.com/SyahmiShafawi/TechnologyOneTest.git
 
 Branching
  development/test - branching for development
  master - main branch

Usage
 Input
 - Enter a number (e.g., 123.45) into the input field.
 - Click the Convert button.
 Output
 - ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS

Code Structure
 Backend
 - Helper/ApiService.cs: Handles API requests for number conversion.
 - Helper/NumberToWordsConverter.cs: Contains the logic to convert numbers to words.
 - DataModel/NumberCategory.cs : Represents the data model for caching.
Frontend
 - Index.html: HTML page for user interaction.



