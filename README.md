<b>Introduction</b>

On the basis of the EF model seen in the course, you must design an aircraft price management application for the airline VSFly.

<b>Contraintes</b>

For each flight available in the database, the other partner websites (ebooker / skyscanner type) can buy tickets for their customers through their websites as a front-end using webAPI requests from the BLL of their sites.
For each flight a base price is offered by the airline. Rules exist to maximize the filling of the aircraft and the total gain on all seats. For this there are 2 variables (the filling rate of the plane and the deadline of the flight in relation to the date of purchase of the ticket). The calculation of the sale price must be done on the WebAPI server side and be returned to the partner site on each request. In the database managed by Entity Framework, the sale price of each ticket must be saved.

1.	If the airplane is more than 80% full regardless of the date:<br>
a. sale price = 150% of the base price
2.	If the plane is filled less than 20% less than 2 months before departure:<br>
a. sale price = 80% of the base price
3.	If the plane is filled less than 50% less than 1 month before departure:<br>
a. sale price = 70% of the base price
4. In all other cases:<br>
a. sale price = base price

<b>Delivery</b>

The result consists of 2 Visual Studio solutions:
1)	Partner site<br>
a.	With an MVC presentation layer (.net core) for<br>
i.	List of flights<br>
ii.	Buy tickets on available flights (no change or cancellation possible)

2)	VSAFly's WebAPI<br>
a.	With a webAPI layer<br>
i.	A controller accepting RESTfull requests and returning the data in JSON format<br>
1.	Requests to be processed:<br>
a.	Return all available flights (not full)<br>
b.	Return the sale price of a flight<br>
c.	Buying a ticket on a flight<br>
d.	Return the total sale price of all tickets sold for a flight<br>
e.	Return the average sale price of all tickets sold for a destination (multiple flights possible)<br>
f.	Return the list of all tickets sold for a destination with the first and last name of the travelers and the flight number as well as the sale price of each ticket.<br>
b.	With an EntityFramework core layer to access the database as illustrated in the following figure.<br>


<b>Organisation</b>

Group of 2 students or alone
The 2 solutions must be in a zip file on cyberlearn and uploaded before Sunday June 12, 2022 at 11:59 p.m.
You will present a demo of your project in the course on June 14 2024 for 10 to 15 min in front of the professor only<br>

<b>Evaluation</b>

The final grade will depend on:<br>
1.	Your involvement in the project<br>
2.	The present functionalities<br>
3.	Number of bugs<br>
4.	Quality of the code (LINQ request, correct use of EF and WebAPI)<br>
5.	Answer to the questions asked during the demo<br>
