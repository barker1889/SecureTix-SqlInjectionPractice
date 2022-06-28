# SecureTix - SQL Injection Practice Site

## Installation
* Requires dotnet 6+ and a local instance of SQLExpress which can be connected to via `Server=.\\SQLEXPRESS;Database=master;Trusted_Connection=True;`
* Clone the repo
* Navigate to the `source\SecureTixWeb` in a CLI
* Run `dotnet run` from the CLI
  * If you have nuget authentication errors, run `dotnet run --interactive` and it will prompt for a login
* Browse to `https://localhost:7282/reset` and click confirm to build the database

## Resetting Data
If it's the first run or your hacking attempts have completely broken the database, you should navigate to the `/reset` endpoint and confirm that you want to reset the data.

This will rebuild the whole database schema and initial data.

## Challenges
Challenges can be completed in any order, however following the suggested order will allow you to build on data and techniques learned from the previous tasks
#### 1. Find some unlisted events (Filter bypass)
#### 2. Find the names of all tables and columns (Union select)
#### 3. Log in as an admin user (Filter bypass+)
#### 4. Oppourtunist - Get a list of usernames and password hashes to sell - passwords are more valuable as plain text (https://crackstation.net/ may be helpful...) 
#### 5. Hacktivist - Expose anyone who has tickets to a "controversial" event
#### 6. Sleight of hand - Purchase (by paying for) a ticket to a "members only" event
#### 7. Thief - "Aquire" a ticket to the sold out "Cats on Ice" event

## Tips
* Go through the steps of purchasing a ticket and viewing the order first, this will give an idea of the features and processes involved
* Be aware of what data is normally returned on a page 
* Keep a note of any useful data you retrieve - tables, columns, id's and other data may be useful later on
* Keep a note of working exploits, you may be able to repurpose them later on
* If you're getting a lot of invalid SQL type errors:
  *  Make sure you're closing any string you may be injecting into
  *  Make sure you're commenting out the rest of the SQL statement after your exploit
  *  Try writing down what you think the query you're injecting into is doing, this will help you work out what payload you need to keep it a valid SQL statement
