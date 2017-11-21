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
	 
