Feature: Navigation
	In order to browse to the site
	As a user
	I want to use the browser address bar

@GoToCareersPage
Scenario: Browse to SVCareers page
	Given Browser is open
	And Navigation address is entered in the browser address bar
	Then On Enter i should land in the SVCareers login page
