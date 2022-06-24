# SecureTix - SQL Injection Practice Site

## Installation
* Requires dotnet 6+ and a local instance of SQLExpress which can be connected to via `Server=.\\SQLEXPRESS;Database=master;Trusted_Connection=True;`
* Clone the repo
* Navigate to the `source\SecureTixWeb` in a CLI
* Run `dotnet run` from the CLI
* Browse to `https://localhost:7282/reset` and click confirm to build the database

## Resetting Data
If it's the first run or your hacking attempts have completely broken the database, you should navigate to the `/reset` endpoint and confirm that you want to reset the data.

This will rebuild the whole database schema and initial data.

## Challenges
Challenges can be completed in any order, however following the suggested order will allow you to build on data and techniques learned from the previous tasks
#### 1. Find some unlisted events
#### 2. Find the names of all tables and columns
#### 3. Log in as an admin user
#### 4. Get a list of usernames and passwords (bonus if passwords are in plain text)
#### 5. Find a list of users who have tickets to a controversial event
#### 6. Purchase (by paying for) a ticket to a "members only" event
#### 7. "Aquire" a ticket to the sold out "Cats on Ice" event

## Tips
* Go through the steps of purchasing a ticket and viewing the order first, this will give an idea of the features and processes involved
* Keep track of any useful data you retrieve
* Keep a note of working exploits, you may be able to repurpose them later on
* If you exploits are constantly failing
 *  Make sure you're closing any string you may be injecting into
 *  Make sure you're commenting out the rest of the SQL statement after your exploit
 *  Try writing down what you think the query you're injecting into is doing, this will help you work out what payload you need to keep it a valid SQL statement