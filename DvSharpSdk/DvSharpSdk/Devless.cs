using DvSharpSdk.Util;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DvSharpSdk
{
    public class Devless
    {
        private String rootUrl, tokenValue, devlessUserToken = "", where = "", orderBy = "id", empty;
        private int size = -1;
        private static String TAG = "DevLess";

        public Devless(String rootUrl, String tokenValue)
        {
            //this.mContext = mContext;
            this.rootUrl = rootUrl; //rootUrl => https://shrouded-shelf-94964.herokuapp.com
            this.tokenValue = tokenValue; // tokenValue=> 961ae615906605e7b4a45846e85b9db6

        }

        //public String getData(String serviceName, String tableName, IGetDataResponse getDataResponse)
        public void getData(String serviceName, String tableName)
        {
            string onioin = "/db?table="; string tablename = tableName;
            string GetRequestUrl = serviceName + onioin + tableName;

            //using RESTSHARP

            var client = new RestClient(new DevlessBuilder().getUrl(rootUrl, serviceName, tableName));
            var request = new RestRequest(Method.GET);
            //request.AddHeader("cache-control", "no-cache");
            request.AddHeader("devless-token", tokenValue);
            //IRestResponse response = client.Execute(request);
            var asynHandler = client.ExecuteAsync(request, requestResponse =>
            {

                try
                {
                    Boolean booll = DevlessBuilder.checkAuth(requestResponse.Content);
                    //Console.WriteLine(booll);
                    if (booll)
                    {
                        Boolean successful = DevlessBuilder.checkGetSuccess(requestResponse.Content);
                        if (successful)
                        {
                            //var Payload = DevLessBuilder.GetPayload(requestResponse.Content);
                            DevlessBuilder.GetResults(DevlessBuilder.GetPayload(requestResponse.Content));

                        }
                        else
                        {
                            string errorMessage = "Error: The ServiceName or TableName doesn't exist";
                            Console.WriteLine(errorMessage);
                        }
                    }

                    else
                    {
                        //requestResponse.userNotAuthenticated(new ErrorMessage("Token expired please log in again"));
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            });
            //var output = response.Content;
            // return response.Content;


        }

        public void postData(String serviceName, String tableName, IDictionary<String, Object> datatoAdd)
        {
            //using RESTSHARP
            var client = new RestClient(new DevlessBuilder().postUrl(rootUrl, serviceName));
            var request = new RestRequest(Method.POST);
            request.AddHeader("devless-token", tokenValue);
            var datatosend = DevlessBuilder.createPostBody(tableName, datatoAdd);
            //short hand
            request.AddJsonBody(datatosend);
            //longhand
            //request.RequestFormat = DataFormat.Json;
            //request.AddBody(datatoAdd);
            //executing the request
            IRestResponse response = client.Execute(request);
            var output = response.Content;
            Console.WriteLine(output);

            // var asynHandler = client.ExecuteAsync(request, response => { Console.WriteLine(response); });

        }

        public void edit(String serviceName, String tableName, IDictionary<String, Object> update, String id)
        {
            var client = new RestClient(new DevlessBuilder().postUrl(rootUrl, serviceName));
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("devless-token", tokenValue);
            var datatosend = DevlessBuilder.createPatchBody(tableName, update, id);
            request.AddJsonBody(datatosend);
            IRestResponse response = client.Execute(request);
            var output = response.Content;
            Console.WriteLine(output);
        }

        public void delete(String serviceName, String tableName, String id)
        {
            var client = new RestClient(new DevlessBuilder().postUrl(rootUrl, serviceName));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("devless-token", tokenValue);
            var datatosend = DevlessBuilder.createDeleteBody(tableName, id);
            request.AddJsonBody(datatosend);
            IRestResponse response = client.Execute(request);
            var output = response.Content;
            Console.WriteLine(output);

        }

        public void deleteAll(String serviceName, String tableName)
        {
            var client = new RestClient(new DevlessBuilder().postUrl(rootUrl, serviceName));
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("devless-token", tokenValue);
            var datatosend = DevlessBuilder.createDeleteAllBody(serviceName, tableName);
            request.AddJsonBody(datatosend);
            IRestResponse response = client.Execute(request);
            var output = response.Content;
            Console.WriteLine(output);



        }
    }
}
