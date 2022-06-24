# Answers
There may be multiple ways of completing each challenge, the answers are just one way of doing this. By following the suggested order, you will be able to expand on data and exploits from previous challenges.

#### 1. Find some unlisted events
When we navigate to the `/events` endpoint, we can see a list of events. There is a search box that allows us to filter by the name of the events that may be happening at the database level.

Instead of searching for an event name we can close the existing string and then enter a clause that will always be true:  `' OR 1=1`

This causes an exception in the application, because the SQL that will run is no longer valid. Since we don't care about any futher filters or ordering we can simply comment out the rest of the statement: `' OR 1=1 --`

Now we see the complete list of events.

#### 2. Find the names of all tables and columns
Since we already know the Event Search input is vulnerable to SQL injection AND it fetches data that gets written to the interface, this is a great place to retrieve some extra data that the query never intended to.

This should be possible with a union select. First let's work out how many parameters we need to fetch by trying `' UNION SELECT NULL` in the search field.

The app helpfully reports an error showing us this is not the correct number of columns. We could next try `' UNION SELECT NULL, NULL --`, `' UNION SELECT NULL, NULL, NULL --` and so on, but let's be cleverer. The event page definitely returns an event name, description, price, sold out indicator - and as we learned from challenge 1 - something that decides if the event is visible or not. It also probably has an ID which we can see if we inspect the "Add To Basket" buttons - so that's at least 6 fields in total, so let's try `' UNION SELECT NULL, NULL, NULL, NULL, NULL, NULL --`.

6 was the magic number, we now see and empty row at the bottom of the list full of our null values. Make a note of this as it may be useful for extracting any data later on.

Now we want to get all the table and column names which we can normally do using `SELECT TABLE_NAME, COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS`. We need to read the table and column name properties into the union select fields - but we must read them into fields that support text. This needs to be trial and error for now, however we can quickly work out that the second and third fields support text, so we can update our union select to `' UNION SELECT NULL, TABLE_NAME, COLUMN_NAME, NULL, NULL, NULL FROM INFORMATION_SCHEMA.COLUMNS --`.

This now shows us all of the tables and columns - which we should make a note of.

#### 3. Log in as an admin user
Navigate to the `/login` page. We can start by trying the classic SQL injection example from challenge 1 in the username field and see what happens: `' OR 1=1 --`

The interface is preventing us from logging in because the statement is not recognised as a valid email - luckily for us, there's no such thing as client side security controls, so we can inspect the element in developer tools and remove the `type="email"` attribute on the input box.

This will cause a null reference error on the password field, so let's also add a password.

We're in, but only as a standard user, looks like the first user in the database isn't an admin. Let's update our exploit so that instead of matching on any user, it matches on any user with admin in their name (after completing challenge 2, we know that the email address is mapped to the Username field - we could also take a few guesses to work this out): `' OR Username LIKE '%admin%' -- `

#### 4. Get a list of usernames and passwords (bonus if passwords are in plain text)
If we have completed challenge 2 then this should be fairly straight forward.

We know that the table we are interested in is `Users` and that the fields we are interested in are called `Username` and `Password`. So let's adjust the union select from challenge 2 to retreive user data instead: `' UNION SELECT NULL, Username, Password, NULL, NULL, NULL FROM Users --`.

The passwords are clearly encrypted, however there didn't appear to be any sort of "salt" field in the users table, indicating that they have only been hashed. We can copy the passwords into a rainbow table lookup - like crackstation.net and see if we can break them.

Looks like most users had basic passwords that appeared in crackstations list. However one user's password was not in the rainbow table - `superuser@securetix.com`

#### 5. Find a list of users who have tickets to a controversial event
Let's find which events might be controversial by listing all events as in challenge 1. Looks like there's one that the attendees don't want us to know about.

Based on the schema we retrieved from challenge 2, it looks like we need to link users to and event via an order. Unfortunately we don't know the id of the controversial event, so we can either try and brute force it or run another query to list it. We can update the union select to retrieve the id and name of each event: `' OR 1=1 UNION SELECT NULL, Name, CAST(Id AS VARCHAR(MAX)), NULL, NULL, NULL FROM Events --` - note the cast from int to varchar, we will receive an error if we try to retrieve the integer Id field as a text field.

It looks like the more secret events have unusually high event id's - lucky we didn't take the brute force approach! 

Ok, so now we know we're interested in orders with an eventid of 863. We will need to join onto the users table to get the usernames of people who have tickets to this event. Normally our SQL would look like:
```
SELECT u.UserName FROM Orders o 
INNER JOIN OrderItems oi ON o.OrderId = oi.OrderItemId
INNER JOIN Users u ON o.UserId = u.UserId 
WHERE oi.EventId = 863
```

We can incorporate this into our union select on the events list using `' UNION SELECT NULL, u.UserName, NULL, NULL, NULL, NULL FROM Orders o INNER JOIN OrderItems oi ON o.OrderId = oi.OrderItemId INNER JOIN Users u ON o.UserId = u.UserId WHERE oi.EventId = 863 --` which will show us any users who attended this event - lucky there's no character limit on the search field!

#### 6. Purchase (by paying for) a ticket to a "members only" event
Although we can see the "members only" event by entering `' OR 1=1 --` we are prevented from adding it to the basket.

One option could be to try and SQL inject into the request that adds the ticket to a basket, however at this point we are dealing with an integer so it's unlikely that SQL injection is possible here.

From our list of tables from challenge 2, we can see there is a table that hold the current basket items, so this is a better target. We can either perform an insert into this table for our current basket or modify an existing basket item, the second option looks easier.

First we need to add any available ticket to our basket. We can then go to the basket screen where the URL reveals our basket id (in this example, we'll say it's basket id=3).

We can determine the ID of the event we want a ticket for either by writing a union select that returns the ID or by examining the markup on the page (after searching for `' OR 1=1 --`) and seeing that there is a hidden field with an id of 521. Under normal circumstances our SQL to update the existing event id for basket 3 would be `UPDATE UserBasketItems SET [EventId] = 521 WHERE [BasketId] = 3`.

To turn this into an SQL injection payload we first need to terminate the select statement that the search screen does. This time we need to both close the quotes and add a semi-colon so that we can write the new update statement. Finally we will need to comment out any leftover SQL, so our payload will look like `'; UPDATE UserBasketItems SET [EventId] = 521 WHERE [BasketId] = 3 --`.

After running this we should be able to see the memebers only event in our basket, which we can now purchase, having bypassed the "add to basket" logic.

#### 7. "Aquire" a ticket to the sold out "Cats on Ice" event
At this point we may be tempted to retry our solution to challenge 6, however the payment button has an extra check to ensure that the event being purchased is not sold out so it won't work.

If we examine the orders tables (returned from challenge 2), we can see that there is a record for each order in the Orders table. There are also records for each item in the OrderItems table which are linked to an order by the OrderId property. This gives us a couple of options, we can either modify a whole order by changing the user id to ours, or we can update the OrderId that an OrderItem is associated with to one of our existing orders.
