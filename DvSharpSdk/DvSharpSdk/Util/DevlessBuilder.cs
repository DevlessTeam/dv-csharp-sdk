using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DvSharpSdk.Util
{
   public class DevlessBuilder
    {

        public String formUrl(String rootUrl, String serviceName)
        {
            return rootUrl + "/api/v1/service/" + serviceName + "/";
        }

        public String getUrl(String rootUrl, String serviceName, String tableName)
        {
            return rootUrl + "/api/v1/service/" + serviceName + "/db?table=" + tableName;
        }

        public String postUrl(String rootUrl, String serviceName)
        {
            return rootUrl + "/api/v1/service/" + serviceName + "/db";
        }

        public static Boolean checkAuth(String response)
        {

            Boolean booll = false;
            try
            {
                var result = JsonConvert.DeserializeObject(response);
                JObject obj = JObject.Parse(result.ToString());
                string statuscode = obj["status_code"].ToString(); int statusCode = Int32.Parse(statuscode);
                if (statusCode == 625 || statusCode == 609 || statusCode == 619 || statusCode == 636 || statusCode == 614 || statusCode == 1000)
                {
                    booll = true;
                }
                else if (statusCode == 628)
                {
                    booll = false;
                }
                else
                {
                    booll = true;
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return booll;
        }

        public static Boolean checkGetSuccess(String response)
        {
            Boolean booll = false;
            try
            {
                var result = JsonConvert.DeserializeObject(response);
                JObject obj = JObject.Parse(result.ToString());
                string statuscode = obj["status_code"].ToString(); int statusCode = Int32.Parse(statuscode);
                if (statusCode == 634 || statusCode == 604)
                {
                    booll = false;
                }
                else
                {
                    booll = true;
                }

            }
            catch (Exception e)
            {
                throw e;
            }

            return booll;

        }

        public static string GetPayload(String response)
        {
            var result = JsonConvert.DeserializeObject(response);
            JObject obj = JObject.Parse(result.ToString());
            string payload = obj["payload"].ToString();
            if (payload != "")
            {
                var msg = "Payload available";
            }
            else
            {
                var msg = "Payload not available";
            }

            return payload;

        }

        public static void GetResults(String payload)
        {

            JObject obj = JObject.Parse(payload);
            JArray rect = (JArray)obj["results"];
             

        }

        public static IDictionary<String, Object> createPostBody(String table_name, IDictionary<String, Object> fieldMap)
        {
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            ArrayList mainList = new ArrayList();
            Dictionary<String, Object> miniMap = new Dictionary<String, Object>();
            ArrayList fieldList = new ArrayList();
            fieldList.Add(fieldMap);
            miniMap.Add("name", table_name);
            miniMap.Add("field", fieldList);
            mainList.Add(miniMap);
            map.Add("resource", mainList);

            return map;
        }

        public static IDictionary<String, Object> createPatchBody(String table_name, IDictionary<String, Object> dataChange, String id)
        {
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            ArrayList mainList = new ArrayList();
            Dictionary<String, Object> miniMap = new Dictionary<String, Object>();
            ArrayList fieldList = new ArrayList();
            Dictionary<String, Object> fieldMap = new Dictionary<String, Object>();
            fieldMap.Add("where", "id," + id);
            ArrayList dataFields = new ArrayList();
            dataFields.Add(dataChange);
            fieldMap.Add("data", dataFields);
            fieldList.Add(fieldMap);

            miniMap.Add("name", table_name);
            miniMap.Add("params", fieldList);
            mainList.Add(miniMap);
            map.Add("resource", mainList);
            return map;
        }

        public static IDictionary<String, Object> createDeleteBody(String table_name, String id)
        {
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            ArrayList mainList = new ArrayList();
            Dictionary<String, Object> miniMap = new Dictionary<String, Object>();
            ArrayList fieldList = new ArrayList();
            Dictionary<String, Object> fieldMap = new Dictionary<String, Object>();

            fieldMap.Add("delete", "true");
            fieldMap.Add("where", "id,=," + id);
            fieldList.Add(fieldMap);
            miniMap.Add("name", table_name);
            miniMap.Add("params", fieldList);
            mainList.Add(miniMap);
            map.Add("resource", mainList);
            return map;

        }

        public static IDictionary<String, Object> createDeleteAllBody(String serviceName, String tableName)
        {
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            ArrayList mainList = new ArrayList();
            Dictionary<String, Object> miniMap = new Dictionary<String, Object>();
            ArrayList fieldList = new ArrayList();
            Dictionary<String, Object> fieldMap = new Dictionary<String, Object>();

            fieldMap.Add("delete", "true");
            fieldList.Add(fieldMap);
            miniMap.Add("name", tableName);
            miniMap.Add("params", fieldList);
            mainList.Add(miniMap);
            map.Add("resource", mainList);
            return map;
        }

        public static IDictionary<String, Object> signUpBuilder(String userName, String email, String password, String phoneNumber, String firstName, String lastName)
        {
            Dictionary<String, Object> signUp = new Dictionary<String, Object>();
            ArrayList paramss = new ArrayList() { "email", "password", "userName", "phoneNumber", "firstName", "lastName", "null" };

            signUp.Add("params", paramss);
            signUp.Add("jsonrpc", "2.0");
            signUp.Add("method", "devless");
            signUp.Add("id", "1000");

            return signUp;
        }



    }
}
