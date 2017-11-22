Feature: AddToRequisition

@Add the job request to requisition
Scenario: Add the job request to requisition
	Given Launch the chrome browser window
	Given Open SVCareers website in the chrome browser
	And Enter the user details in the login form
	Then Login to the SVCareers website with the user details provided
	Then Click on Home tab to get the job requests grid
	Then Select the job request with external resource flag set to one and requesition status is not posted
	Then Click on add to requisition link
	# Job details form
	Then Enter the client code
	Then Select the priority
	Then Enter the billing rate per hour
	Then Enter the duration of the project
	Then Enter if the referral be rewarded
	Then Enter the date initiated
	Then Enter the candidate needed by
	Then Enter requisition type
	Then Enter the count planned
	Then Enter the total number of positions planned
	Then Select the Rationale
	Then Enter the recruiter name
	Then Enter the hiring manager name
	Then Move to job description screen
	# Job description screen
	Then Move to interview management screen
	# Interview management screen
	Then Click on Add interview rounds button
	When Switch focus to interview round popup
	Then Add the interview rounds and submit
	Then Click on Add interviewer button
	When Switch focus to interviewer popup
	Then Search and add the interviewer
	Then Move to requisition summary page
	# Requisition summary page
	Then Save and post the requisition request
	# Requisition history page
	Then Move to requisition history page

@Save the requisition job request without posting it
Scenario: Save the requisition job request without posting it
	Given Launch the chrome browser window
	Given Open SVCareers website in the chrome browser
	And Enter the user details in the login form
	Then Login to the SVCareers website with the user details provided
	Then Click on Home tab to get the job requests grid
	Then Select the job request with external resource flag set to one and requesition status is not posted
	Then Click on add to requisition link
	# Job details form
	Then Enter the client code
	Then Select the priority
	Then Enter the billing rate per hour
	Then Enter the duration of the project
	Then Enter if the referral be rewarded
	Then Enter the date initiated
	Then Enter the candidate needed by
	Then Enter requisition type
	Then Enter the count planned
	Then Enter the total number of positions planned
	Then Select the Rationale
	Then Enter the recruiter name
	Then Enter the hiring manager name
	Then Move to job description screen
	# Job description screen
	Then Move to interview management screen
	# Interview management screen
	Then Click on Add interview rounds button
	When Switch focus to interview round popup
	Then Add the interview rounds and submit
	Then Click on Add interviewer button
	When Switch focus to interviewer popup
	Then Search and add the interviewer
	Then Move to requisition summary page
	# Requisition summary page
	Then Save and unpost the requisition request
	# Requisition history page
	Then Move to requisition history page