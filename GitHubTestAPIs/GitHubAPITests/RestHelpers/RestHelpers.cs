using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Fluent;
using RestSharp;

namespace GitHubAPITests.RestHelpers
{
    public class RestAPIHelpers
    {
        //client
        //request
        //baseUrl
        public RestClient _restClient;
        public RestRequest _restRequest;
        string gitHubUser;
        string repoName;
        string authToken;
        public string _baseUrl = "https://api.github.com/";
        private IRestResponse response = null;
       // private object jsonUtil;
        JSONUtils jsonUtil = new JSONUtils();

        /// <summary>
        /// Constructor
        /// </summary>
        public RestAPIHelpers()
        {
            _restClient = new RestClient(_baseUrl);
             gitHubUser = ConfigurationManager.AppSettings["GitHubUser"];
            repoName = ConfigurationManager.AppSettings["repoName"];
            authToken = ConfigurationManager.AppSettings["authToken"];

        }

                    
        /// <summary>
        /// Create Issue in GitHub repo
        /// </summary>
           public IRestResponse CreateIssue(string issueTitle)
        {
            try
            {
               StringBuilder endPoint = new StringBuilder("/repos/" + gitHubUser + "/" + repoName + "/issues");
              _restRequest = new RestRequest(endPoint.ToString(), Method.POST);
              _restRequest.Parameters.Clear();
              _restRequest.AddHeader("Accept", "application/json");
              _restRequest.AddHeader("Authorization", "token "+ authToken);
              JObject body = jsonUtil.GetJSONObjectFromFile("createIssue");
              body["title"] = issueTitle;
                _restRequest.RequestFormat = DataFormat.Json;
                _restRequest.AddParameter("Application/Json", body, ParameterType.RequestBody);
                response = _restClient.Execute(_restRequest);
                if (response.StatusCode.ToString() == "Created")
                {
                    return response;
                }
               
            }
            catch (Exception e)
            {
                Log.Info(e.ToString());                               
            }
            return null;
        }


        /// <summary>
        /// Update Issue in GItHub repo
        /// </summary>
            public IRestResponse UpdateIssue(string issueTitle, string issueNumber)
            {
            try
            {
                StringBuilder endPoint = new StringBuilder("/repos/" + gitHubUser + "/" + repoName + "/issues/" + issueNumber);
                _restRequest = new RestRequest(endPoint.ToString(), Method.PATCH);
                _restRequest.Parameters.Clear();
                _restRequest.AddHeader("Accept", "application/json");
                _restRequest.AddHeader("Authorization", "token " + authToken);
                JObject body = jsonUtil.GetJSONObjectFromFile("EditIssue");
                body["title"] = issueTitle;
                _restRequest.RequestFormat = DataFormat.Json;
                _restRequest.AddParameter("Application/Json", body, ParameterType.RequestBody);
                response = _restClient.Execute(_restRequest);
                if (response.StatusCode.ToString() == "OK")
                {
                    return response;
                }

            }
            catch (Exception e)
            {
                Log.Info(e.ToString());
            }
            return null;
        }

        /// <summary>
        /// Get Issues from GItHub repo sort by last updated
        /// </summary>
        public IRestResponse GetIssues(string sortBy)
        {
            try
            {
                _restRequest.Parameters.Clear();
                StringBuilder endPoint = new StringBuilder("/repos/" + gitHubUser + "/" + repoName + "/issues?sort=" + sortBy);
                _restRequest = new RestRequest(endPoint.ToString(), Method.GET);
                _restRequest.Resource = endPoint.ToString();
                _restRequest.AddHeader("Accept", "application/json");
               _restRequest.AddHeader("Authorization", "token " + authToken);
               _restRequest.RequestFormat = DataFormat.Json;
                response = _restClient.Execute(_restRequest);
                if (response.StatusCode.ToString() == "OK")
                {
                    return response;
                }

            }
            catch (Exception e)
            {
                Log.Info(e.ToString());
            }
            return null;
        }

        public JObject GetJsonObjects(IRestResponse response)
        {
            JObject objects = null;
            if (response != null)
            {
                var content = response.Content; // raw content as string
                objects = JObject.Parse(content);
               
            }
            return objects;


        }

        public JArray GetJsonArray(IRestResponse response)
        {
            JArray objects = null;
            if (response != null)
            {
                var content = response.Content; // raw content as string
                objects = JArray.Parse(content);

            }
            return objects;


        }
    }

}
