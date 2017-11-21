Feature: CreateJobRequest
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Add new job request
Scenario: Create new job request
	Given Launch the web browser
	Given Open SVCareers website
	And Type username and password in the login form
	Then Click on Login submit button
	Given Check if the user is authroized to create a new job request
	Then Click on Create Job Request link
	Then Switch the focus to the Create new Job Request form popup
	Then Select the organization
	Then Select the Job location
	Then Select client name
	Then Select project name
	Then Select the technology
	Then Select the Role
	Then Select the Job title
	Then Search for Mandatory skills
	Then Switch the focus to the Madatory skills popup
	Then Select the Mandatory skills
	Then Search for Nice to have skills
	Then Switch the focus to the Nice to have skills popup
	Then Select the Nice to have skills
	Then Enter the number of resources required
	Then Select the date by which the candidate is required
	Then Select the billable status
	Then Select the billedTypes
	Then Select if client interview is required
	Then Select the certainity
	Then Enter the max budget
	Then Enter the job description
	Then Enter the billing rate
	Then Select the billing start date
	Then Select the requirement type
	Then Search for account manager
	Then Switch the focus to the Account manager popup
	Then Select the account manager
	Then Search for reporting manager
	Then Switch the focus to the Reporting manager popup
	Then Select the reporting manager
	Then Enter the minimum experience
	Then Enter the preferred experience
	Then Enter the seat ID
	Then Search for any certifications
	Then Switch the focus to the certifications popup
	Then Select the required certification
	Then Enter the work location
	Then Enter the project duration
	Then Select will the resource lead a team Yes or No
	Then Select will the resume be sent to client Yes or No
	Then Select system type
	Then Select configurations
	Then Enter the comments if any for the job request
	Then Submit the job request form
