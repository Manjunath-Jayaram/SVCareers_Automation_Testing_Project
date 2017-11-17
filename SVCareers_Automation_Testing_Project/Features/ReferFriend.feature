Feature: ReferFriend
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@Refer an experience candidate
Scenario: Refer an experience candidate
	Given Launch the browser
	Given Open SVCareers websites
	And Type your username and password in the login form
	Then Click on Login form submit button
	Then Select the job you want to refer from the grid
	Then Click on refer a friend link
	Given Switch the focus to the candidate details form popup
	Then Enter candidate firstname
	Then Enter candidate middlename
	Then Enter candidate lastname
	Then Select the gender
	Then Enter the candidate email address
	Then Enter the candidate phone number
	Then Select the candidate type as experienced
	Then Enter the candidate experience
	Then Select the candidates expertise technology
	Then Enter the notice period
	Then Upload the resume of the candidate
	Then Enter the comments
	Then Submit the referal form

@Refer a fresher
Scenario: Refer a fresher
	Given Launch the browser
	Given Open SVCareers websites
	And Type your username and password in the login form
	Then Click on Login form submit button
	Then Select the job you want to refer from the grid
	Then Click on refer a friend link
	Given Switch the focus to the candidate details form popup
	Then Enter candidate firstname
	Then Enter candidate middlename
	Then Enter candidate lastname
	Then Select the gender
	Then Enter the candidate email address
	Then Enter the candidate phone number
	Then Select the candidate type as fresher
	Then Upload the resume of the candidate
	Then Enter the comments
	Then Submit the referal form

@Filter the job requests
Scenario: Filter the job requests based on country selection and keyword search
	Given Launch the browser
	Given Open SVCareers websites
	And Type your username and password in the login form
	Then Click on Login form submit button
	Then Filter the job requests on selecting the respective country
	Then Filter the job requests on keyword search
	Then Click on search button based on filter selection
	Then Select the job you want to refer from the grid
	Then Click on refer a friend link
	Given Switch the focus to the candidate details form popup
	Then Enter candidate firstname
	Then Enter candidate middlename
	Then Enter candidate lastname
	Then Select the gender
	Then Enter the candidate email address
	Then Enter the candidate phone number
	Then Select the candidate type as experienced
	Then Enter the candidate experience
	Then Select the candidates expertise technology
	Then Enter the notice period
	Then Upload the resume of the candidate
	Then Enter the comments
	Then Submit the referal form

