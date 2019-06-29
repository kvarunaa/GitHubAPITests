using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using GitHubAPITests.RestHelpers;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace GitHubAPITests.StepDefinitions
{


    
    [Binding]
    public sealed class User_Access_GitHub_APIs_Steps
    {
       
        private readonly ScenarioContext context;
        private string gitHubUser;
        private string repoName;
        private string authToken;
        private string requestType;
        private string issueTitle;
        private string issueNumber;
        private string sortOrder;
        private IRestResponse apiResponse;
        private JObject jsonObjects;
        private JArray jsonArray;

        public User_Access_GitHub_APIs_Steps(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

       public RestAPIHelpers restAPIHelpers = new RestAPIHelpers();

        [Given(@"I have made a API request to create issue in GitHub repo with issue title")]
        public void GivenIHaveMadeAAPIRequestToCreateIssueInGitHubRepoWithIssueTitle(Table table)
        {
            issueTitle = table.Rows[0]["issueTitle"];
            apiResponse = restAPIHelpers.CreateIssue(issueTitle);
        }


        [Given(@"I have made a API request to edit issue title in GitHub repo")]
        public void GivenIHaveMadeAAPIRequestToEditIssueTitleInGitHubRepo(Table table)
        {           
            issueTitle = table.Rows[0]["updatedTitle"];
            issueNumber = table.Rows[0]["issueNumber"];           
            apiResponse = restAPIHelpers.UpdateIssue(issueTitle, issueNumber);

        }

        [Given(@"I have made a API request to get list of issues sorted by Update date")]
        public void GivenIHaveMadeAAPIRequestToGetListOfIssuesSortedByUpdateDate(Table table)
        {
            sortOrder = table.Rows[0]["sortBy"];
            apiResponse = restAPIHelpers.GetIssues(sortOrder);
        }


        [Then(@"I see issue created in GitHub with status '(.*)'")]
        public void ThenISeeIssueCreatedInGitHubWithStatus(string status)
        {
            jsonObjects = restAPIHelpers.GetJsonObjects(apiResponse);
            Assert.AreEqual(status, apiResponse.StatusCode.ToString(),"Issue Not created in GitHub");
            Assert.AreEqual("open", jsonObjects["state"] , "Expected issue state OPEN dosen't match");
            Assert.AreEqual(issueTitle, jsonObjects["title"] , "Expected issue title dosen't match");
        }

        [Then(@"I see updated title assoicated to the issue with status '(.*)'")]
        public void ThenISeeUpdatedTitleAssoicatedToTheIssueWithStatus(string status)
        {
            jsonObjects = restAPIHelpers.GetJsonObjects(apiResponse);
            Assert.AreEqual(status, apiResponse.StatusCode.ToString(), "Issue Not updated in GitHub");
            Assert.AreEqual("open", jsonObjects["state"], "Expected issue state OPEN dosen't match");
            Assert.AreEqual(issueTitle, jsonObjects["title"], "Updated issue title dosen't match");
        }

        [Then(@"I see list of issues sorted by Update date with status '(.*)'")]
        public void ThenISeeListOfIssuesSortedByUpdateDateWithStatus(string status)
        {
            jsonArray = restAPIHelpers.GetJsonArray(apiResponse);
            Assert.AreEqual(status, apiResponse.StatusCode.ToString(), "Issue Not returned from GitHub repo");
            Assert.AreEqual(issueNumber, jsonArray.First["number"].ToString(), "Issues Not sorted by last udpated");
            
        }




    }
}
