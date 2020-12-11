To run the Angular Application, please go to the folder LeasePlanCurrencyConverter.Web\CurrencyCalculatorWeb and run the following commands

npm install
ng serve

The application will be running on http://localhost:4200/

To run the api open the solution LeasePlanCurrencyConverter.sln and start the application
When starting the api it will be running on http://localhost:5000/

Possible Improvements Front End:
	Build it with Angular reactive forms, so I can validate the state before performing the call to the api
	Break it into smaller components, eg the dropdown that lists the currencies (on that case I would have to emmit the selected value from 
		that component to the father one, so I know the selected coin)
	Improve how I handle the errors comming from the backend
	Write Angular unit tests
	
Possible Improvements BackEnd:
	Improve unit test coverage
	Use of mediatr library on the api level, so I have a clear separation between my service and my controllers
	Set up Serilog
	Create a wrapper for all the responses, so I end up with a more uniform interface 
	Store coin list on a database
	Setup both the .net and Angular application to start from a docker compose file. 
	