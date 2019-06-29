# GitHubAPITests

Configurations:
1.	 Add your GitHub user, reponame , Authorization Key  in app.config file
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="GitHubUser" value="yourGitHubUser"/>
		<add key="repoName" value="yourrepoName"/>
		<add key="AuthToken" value="yourAuthToken"/>
	</appSettings>
</configuration>

Tools & Technologies uses
•	Programming Language: C#
•	BDD : Specflow
•	RestSharp Http Client
•	Json Handler: Newtonsoft Json
Enhancements: 
•	Optimize the code in RestHelpers.cs class into smaller reusable methods.
•	Json Response can be deserialized into C# objects so that response values can be used across the project.
