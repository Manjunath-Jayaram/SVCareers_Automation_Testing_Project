Feature: Navigation
	In order to browse to the site
	As a user
	I want to use the browser address bar

@Login
Scenario: Login in SVCareers page
	Given SVCareers page is already open
	And Fill the <UserName>, <Password>
	Then Click on Login button
	| UserName          | Password |
	| Manjunath.Jayaram | sv123    | 
