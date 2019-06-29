Feature: User Access GitHub APIs
	As a user of GitHub
	I want to Create , Edit , List issues from repo 

	@API
Scenario: User can create issue in Repo using GitHub API
	Given I have made a API request to create issue in GitHub repo with issue title
		  | issueTitle            |
		  | this is a new defect1 |
	Then I see issue created in GitHub with status 'Created'

	@API
Scenario: User can edit issue title in Repo using GitHub API
	Given I have made a API request to edit issue title in GitHub repo 	
		 | updatedTitle          | issueNumber |
		 | this is updated title1 | 1           |
	Then I see updated title assoicated to the issue with status 'OK'

@API
Scenario: User can get list of issues from GitHub Repo sorted by Update date
Given I have made a API request to edit issue title in GitHub repo 	
		 | updatedTitle      | issueNumber |
		 | this is new title | 2           |
And I have made a API request to get list of issues sorted by Update date
		 | sortBy  |
		 | updated |
Then I see list of issues sorted by Update date with status 'OK'







