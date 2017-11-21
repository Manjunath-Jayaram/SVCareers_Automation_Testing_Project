Feature: JobRequestReport

@Get all job request report on excel with all filters
Scenario: Get job request report on excel with all filters
	Given Launch the web browser window
	Given Open SVCareers website in the browser
	And Enter the username and password in the login form
	Then Click on Login button to login to SVCareers website
	Then Check if the filter icon is available for the logged in user
	Then Click on filter icon from the job requests grid
	Then Swich the focus to the report list window
	Then Select the location for the job request report
	Then Select the organization for the job request report
	Then Select the client for the job request report
	Then Select the status for the job request report
	Then Select the technology for the job request report
	Then Select the certainty for the job request report
	Then Select the staffing type for the job request report
	Then Select the created by for the job request report
	Then Select the date range 
	Then Select the filter based on NeededBy, CreatedOn, ModifiedOn date range
	Then Generate the excel report of the job requests
	Then Select the all job requests export data option

@Get all external hiring job request report on excel with all filters
Scenario: Get job request report on excel with all filters
	Given Launch the web browser window
	Given Open SVCareers website in the browser
	And Enter the username and password in the login form
	Then Click on Login button to login to SVCareers website
	Then Check if the filter icon is available for the logged in user
	Then Click on filter icon from the job requests grid
	Then Swich the focus to the report list window
	Then Select the location for the job request report
	Then Select the organization for the job request report
	Then Select the client for the job request report
	Then Select the status for the job request report
	Then Select the technology for the job request report
	Then Select the certainty for the job request report
	Then Select the staffing type for the job request report
	Then Select the created by for the job request report
	Then Select the date range 
	Then Select the filter based on NeededBy, CreatedOn, ModifiedOn date range
	Then Generate the excel report of the job requests
	Then Select the external hiring job requests export data option

@Get all action required job request report on excel with all filters
Scenario: Get job request report on excel with all filters
	Given Launch the web browser window
	Given Open SVCareers website in the browser
	And Enter the username and password in the login form
	Then Click on Login button to login to SVCareers website
	Then Check if the filter icon is available for the logged in user
	Then Click on filter icon from the job requests grid
	Then Swich the focus to the report list window
	Then Select the location for the job request report
	Then Select the organization for the job request report
	Then Select the client for the job request report
	Then Select the status for the job request report
	Then Select the technology for the job request report
	Then Select the certainty for the job request report
	Then Select the staffing type for the job request report
	Then Select the created by for the job request report
	Then Select the date range 
	Then Select the filter based on NeededBy, CreatedOn, ModifiedOn date range
	Then Generate the excel report of the job requests
	Then Select the action required job requests export data option

