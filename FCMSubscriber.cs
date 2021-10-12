using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace FCMSubscriber
{
    internal class FCMSubscriber
    {
        /// <summary>
        /// </summary>
        /// <param name="token">single token to register to topic </param>
        /// <param name="topicEnum">
        ///     1=all 2=develop 3=backendTest 4=androidTest 1-send to all user this is for live scenario purpose 2-send
        ///     to beta tester user 3-back end test user 4-android test user
        /// </param>
        /// <param name="googleCredential">Address To the fire base credential JSON file </param>
        /// <returns></returns>
        public static async Task<string> Subscribe(string token, Enum topicEnum, string googleCredential)
        {
            var result = "";
            if (FirebaseApp.DefaultInstance == null)
            {
                var defaultApp = FirebaseApp.Create(new AppOptions
                {
                    //this path should point to desired credential of project.
                    Credential =
                        GoogleCredential.FromFile(googleCredential)
                });
                Console.WriteLine(defaultApp.Name);
            }

            var tokenList = new List<string>();
            tokenList.Add(token);
            var response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
                tokenList, topicEnum.ToString());
            result =
                $"Success Subscribe:{response.SuccessCount} . failure Subscribe: {response.FailureCount} . Error:{response.Errors} \r\n";

            return result;
        }

        /// <summary>
        ///     For Subscribe List Of Token(FCM Tokens) To Specific Topic.
        /// </summary>
        /// <param name="tokenList">list of token for registering into topic </param>
        /// <param name="topicEnum">
        ///     1=all 2=develop 3=backendTest 4=androidTest 1-send to all user this is for live scenario purpose 2-send
        ///     to beta tester user 3-back end test user 4-android test user
        /// </param>
        /// <param name="googleCredential">Address To the firebase credential JSON file </param>
        /// <returns></returns>
        public static async Task<string> Subscribe(List<string> tokenList, Enum topicEnum, string googleCredential)
        {
            var result = "";
            var tokenChunk = new List<string>();
            if (FirebaseApp.DefaultInstance == null)
            {
                var defaultApp = FirebaseApp.Create(new AppOptions
                {
                    //this path should point to desired credential of project.
                    Credential =
                        GoogleCredential.FromFile(googleCredential)
                });
                Console.WriteLine(defaultApp.Name);
            }

            TopicManagementResponse response;
            if (tokenList.Count > 1000)
            {
                var count = 0;

                foreach (var token in tokenList)
                {
                    tokenChunk.Add(token);
                    count++;
                    if (tokenChunk.Count >= 999)
                    {
                        count = 0;

                        response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
                            tokenChunk, topicEnum.ToString());
                        result +=
                            $"Success Subscribe:{response.SuccessCount} . failure Subscribe: {response.FailureCount} . Error:{response.Errors} \r\n";
                        tokenChunk.Clear();
                    }
                }

                response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(tokenChunk,
                    topicEnum.ToString());
                result +=
                    $"Success Subscribe:{response.SuccessCount} . failure Subscribe: {response.FailureCount} . Error:{response.Errors} \r\n";
                tokenChunk.Clear();
            }

            return result;
        }

        /// <summary>
        ///     to fetch and read fcm token directly from DB can use this.
        /// </summary>
        /// <param name="connectionString">connection string to desired database</param>
        /// <param name="tableName">name of the table that FCM Token Are stored to </param>
        /// <param name="FCMColumnName">name of the column that FCM Tokens are stored to </param>
        /// <param name="topicEnum">
        ///     1=all 2=develop 3=backendTest 4=androidTest 1-send to all user this is for live scenario purpose 2-send
        ///     to beta tester user 3-back end test user 4-android test user
        /// </param>
        /// <param name="googleCredential">Address To the fire base credential JSON file </param>
        /// <returns></returns>
        public static async Task<string> Subscribe(string connectionString, string tableName, string FCMColumnName,
            Enum topicEnum
            , string googleCredential)
        {
            var tokenList = new List<string>();


            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command =
                    new SqlCommand($"select {FCMColumnName} from {tableName} where {FCMColumnName} is not null",
                        connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read()) tokenList.Add(reader[0].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (tokenList.Count < 1) return "No DB Record Found!!!";

            if (FirebaseApp.DefaultInstance == null)
            {
                var defaultApp = FirebaseApp.Create(new AppOptions
                {
                    //this path should point to desired credential of project.
                    Credential =
                        GoogleCredential.FromFile(googleCredential)
                });
                Console.WriteLine(defaultApp.Name);
            }

            var tokenChunk = new List<string>();
            var result = "";
            TopicManagementResponse response;

            if (tokenList.Count > 1000)
            {
                var count = 0;

                
                foreach (var token in tokenList)
                {
                    tokenChunk.Add(token);
                    count++;
                    if (tokenChunk.Count >= 999)
                    {
                        count = 0;

                        response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
                            tokenChunk, topicEnum.ToString());
                        result +=
                            $"Success Subscribe:{response.SuccessCount} . failure Subscribe: {response.FailureCount} . Error:{response.Errors} \r\n";
                        tokenChunk.Clear();
                    }
                }

                response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(tokenChunk,
                    topicEnum.ToString());
                result +=
                    $"Success Subscribe:{response.SuccessCount} . failure Subscribe: {response.FailureCount} . Error:{response.Errors} \r\n";
                tokenChunk.Clear();
            }
            else
            {
                response = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(
                    tokenList, topicEnum.ToString());
                result +=
                    $"Success Subscribe:{response.SuccessCount} . failure Subscribe: {response.FailureCount} . Error:{response.Errors} \r\n";
                tokenChunk.Clear();
            }
            return result;
        }

        internal enum TopicName
        {
            All = 1,
            Develop = 2,
            BackendTest = 3,
            AndroidTest = 4
        }
    }
}