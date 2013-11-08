using System.Reflection.Emit;
using System.Security.Authentication;
using Feedly.NET;
using Feedly.NET.Exceptions;
using Feedly.NET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Feedly.NET.Services;

namespace FeedlyConsole
{
    class Program
    {

        private static string _oAuthCode = "AQAAqwV7ImkiOiJhMjJjMTNhZS1mN2U2LTQ1MjQtOTU2ZS05N2UyOTJjYWUxNWMiLCJhIjoiRmVlZGx5IHNhbmRib3ggY2xpZW50IiwidiI6InNhbmRib3giLCJ4Ijoic3RhbmRhcmQiLCJ0IjoxLCJwIjoxLCJlIjoxMzg0MzU3NzYzNjQ5fQ";
        private static string _refreshToken = "AQAAUV17InAiOjEsImEiOiJGZWVkbHkgc2FuZGJveCBjbGllbnQiLCJ1IjoiMTE3MTMzMDIxNzg4MTE3MjI0MTM4IiwibiI6ImVWYjlnOTJmRDVhSEh1cDYiLCJ2Ijoic2FuZGJveCIsImkiOiJhMjJjMTNhZS1mN2U2LTQ1MjQtOTU2ZS05N2UyOTJjYWUxNWMifQ";
        private static string _userId = "a22c13ae-f7e6-4524-956e-97e292cae15c";

        static void Main(string[] args)
        {
            bool close = false;
            do
            {
              string selectedTest = WriteTestMenu();
              selectedTest = selectedTest.ToLower();

              ResourceIdsBuilder resourceIdsBuilder = new ResourceIdsBuilder(_userId);
              UrlBuilder _urlBuilder = new UrlBuilder(resourceIdsBuilder);
              FeedlyClient client = new FeedlyClient(_oAuthCode, _urlBuilder);
              
              switch (selectedTest)
              {
                  case "a":
                      TestAuthentication(client);
                      break;
                  case "u":
                      TestUserProfile(client);
                      break;
                  case "p":
                      TestUserPreferences(client);
                      break;
                  case "c":
                      TestCategories(client, resourceIdsBuilder);
                      break;
                  case "s":
                      TestSubscriptions(client, resourceIdsBuilder);
                      break;
                  case "t":
                      TestTopics(client, resourceIdsBuilder);
                      break;
                  case "g":
                      TestTags(client, resourceIdsBuilder);
                      break;
                  case "f":
                      TestFeeds(client, resourceIdsBuilder);
                      break;
                  case "z":
                      close = true;
                      break;
                  case "":
                      RunAll(client, resourceIdsBuilder);
                      break;
              }
                if (!close)
                {
                    Console.WriteLine();
                    Console.WriteLine("Press [Enter] to continue...");
                    Console.ReadLine();                    
                }

            } while (!close);
        }

        private static void TestAuthentication(FeedlyClient client)
        {
            WriteHeader("Testing Authentication");
            LoginClient loginClient = new LoginClient(client.UrlBuilder);
            string urlForOAuthCodeRetrival = loginClient.GetLoginUrl().ToString();

            Console.WriteLine("Please go to the following url and later paste the temporary code in the following line:");
            Console.WriteLine(urlForOAuthCodeRetrival);

            Console.Write("Enter temporary code:");
            string code = Console.ReadLine();

            WriteHeader("Getting Auth Token"); 

            AuthenticationInfo info = loginClient.GetAuthorizationToken(code).Result;
            Console.WriteLine("User Id: "+info.id);
            Console.WriteLine("TokenType: " + info.token_type);
            Console.WriteLine("AuthToken: " + info.access_token);
            string refreshToken = info.refresh_token;

            WriteHeader("Refreshing Auth Token");

            AuthenticationInfo refreshedInfo = loginClient.RefreshAuthorizationToken(refreshToken).Result;

            Console.WriteLine("User Id: " + refreshedInfo.id);
            Console.WriteLine("TokenType: " + refreshedInfo.token_type);
            Console.WriteLine("AuthToken: " + refreshedInfo.access_token);
        }

        private static void TestFeeds(FeedlyClient client, ResourceIdsBuilder resourceIdsBuilder)
        {
            WriteHeader("Testing Feed");
            WriteHeader("Get One Feed");

            Feed result = client.GetFeed("feed/http://feeds.engadget.com/weblogsinc/engadget").Result;

            Console.WriteLine(result.id);
            Console.WriteLine(result.title);
            Console.WriteLine(result.website);

            WriteHeader("Get Multiple Feeds");

            List<string> feeds= new List<string>();
            feeds.Add("feed/http://feeds.engadget.com/weblogsinc/engadget");
            feeds.Add("feed/http://www.engadget.com/rss.xml");
            feeds.Add("feed/http://blog.geocaching.com/feed/");
            feeds.Add("feed/http://www.yatzer.com/feed/index.php");

            List<Feed> list = client.GetFeeds(feeds.ToArray()).Result;

            foreach (var item in list)
            {
                Console.WriteLine(item.id);
                Console.WriteLine(item.title);
                Console.WriteLine(item.website);
                Console.WriteLine(item.topics.Count);
                WriteSeparator();
            }

        }

        private static void TestTags(FeedlyClient client, ResourceIdsBuilder resourceIdsBuilder)
        {
            try
            {
                WriteHeader("Testing Tags");
                WriteHeader("Initial");

                List<Tag> result = client.GetTags().Result;

                foreach (var item in result)
                {
                    Console.WriteLine("{0} = {1}", item.id, item.label);
                }

                if (result.Count(t => !String.IsNullOrWhiteSpace(t.label)) > 0)
                {
                    Tag originalTag = result.OrderBy(t => t.label).First(t => !String.IsNullOrWhiteSpace(t.label));
                    string replaced = originalTag.label.Split('$')[0] + "$" + DateTime.Now.ToShortTimeString();

                    bool updated = client.UpdateTag(originalTag.id, replaced).Result;

                    WriteHeader("updated");

                    List<Tag> result1 = client.GetTags().Result;

                    foreach (var item in result1)
                    {
                        Console.WriteLine("{0} = {1}", item.id, item.label);
                    }


                    bool deleted = client.DeleteTag(originalTag.id).Result;

                    WriteHeader("After Deleting");

                    List<Tag> result2 = client.GetTags().Result;

                    foreach (var item in result2)
                    {
                        Console.WriteLine("{0} = {1}", item.id, item.label);
                    }
                }

            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error happened while performing request:");
                foreach (var exception in ex.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            }

        }

        private static void TestTopics(FeedlyClient client, ResourceIdsBuilder resourceIdsBuilder)
        {
            try {
                WriteHeader("Testing Topics");
                WriteHeader("Initial");

                List<Topic> result = client.GetTopics().Result;

                foreach (var item in result)
                {
                    Console.WriteLine("{0} = {1}|{2}", item.id, item.Label, item.interest);
                }

                Topic newTopic = new Topic()
                {
                    id = resourceIdsBuilder.TopicId("geocaching"),
                    interest = Topic.Interest.MEDIUM
                };

                bool added = client.AddOrUpdateTopic(newTopic).Result;

                WriteHeader("After Adding");

                List<Topic> result1 = client.GetTopics().Result;

                foreach (var item in result1)
                {
                    Console.WriteLine("{0} = {1}|{2}", item.id, item.Label, item.interest);
                }

                bool deleted = client.DeleteTopic(newTopic.id).Result;

                WriteHeader("After Deleting");

                List<Topic> result2 = client.GetTopics().Result;

                foreach (var item in result2)
                {
                    Console.WriteLine("{0} = {1}|{2}", item.id, item.Label, item.interest);
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error happened while performing request:");
                foreach (var exception in ex.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            }

        }

        private static void TestSubscriptions(FeedlyClient client, ResourceIdsBuilder resourceIdsBuilder)
        {
            WriteHeader("Testing Subscriptions");

            List<Subscription> result = client.GetSubscriptions().Result;

            foreach (var item in result)
            {
                Console.WriteLine("{0}\r\n{1}\r\n{2}\r\n{3}", item.id, item.title, item.state,item.updated);
                WriteSeparator();
            }

            WriteSeparator();

            var newSubscription = CreateNewSubscription(resourceIdsBuilder);

            bool added = client.AddOrUpdateSubscription(newSubscription).Result;

            Console.WriteLine("Added feed");
            Console.ReadLine();

            newSubscription.categories.Add(new Category()
            {
                id = resourceIdsBuilder.CATEGORY_MUST_READ,
                label = "must reads"
            });

            bool updated = client.AddOrUpdateSubscription(newSubscription).Result;

            Console.WriteLine("Updated feed");
            Console.ReadLine();

            bool deleted = client.DeleteSubscription(newSubscription.id).Result;

            Console.WriteLine("Deleted feed");
            Console.ReadLine();
        }

        private static Subscription CreateNewSubscription(ResourceIdsBuilder builder)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category()
            {
                id = builder.CategoryId("geocaching"),
                label = "Geocaching"
            });

            string feedId = "feed/http://blog.geocaching.com/feed/";

            Subscription newSubscription = new Subscription()
            {
                id = feedId,
                title = "The Geocaching Blog",
                categories = categories
            };
            return newSubscription;
        }

        private static void TestCategories(FeedlyClient client, ResourceIdsBuilder resourceIdsBuilder)
        {
            WriteHeader("Testing Categories");

            try
            {
                List<Category> result = client.GetCategories().Result;

                foreach (var item in result)
                {
                    Console.WriteLine("{0} = {1}", item.id, item.label);
                }

                WriteSeparator();


                string original = result[0].label;
                string replaced = original.Split('$')[0] + "$"+DateTime.Now.ToShortTimeString();

                bool updated = client.UpdateCategory(result[0].id, replaced).Result;

                Console.WriteLine("Updated");

                WriteSeparator();

                List<Category> result1 = client.GetCategories().Result;

                foreach (var item in result1)
                {
                    Console.WriteLine("{0} = {1}", item.id, item.label);
                }

                WriteSeparator();

                Subscription newSubscription = CreateNewSubscription(resourceIdsBuilder);
                string categoryId = newSubscription.categories[0].id;
                bool newSub = client.AddOrUpdateSubscription(newSubscription).Result;

                List<Category> result2 = client.GetCategories().Result;

                foreach (var item in result2)
                {
                    Console.WriteLine("{0} = {1}", item.id, item.label);
                }

                bool deleted = client.DeleteCategory(categoryId).Result;

                bool deletedSub = client.DeleteSubscription(newSubscription.id).Result;

                List<Category> result3 = client.GetCategories().Result;

                foreach (var item in result3)
                {
                    Console.WriteLine("{0} = {1}", item.id, item.label);
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error happened while performing request:");
                foreach (var exception in ex.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }

            }
        }

        private static void RunAll(FeedlyClient client, ResourceIdsBuilder resourceIdsBuilder)
        {
            TestUserProfile(client);
            Console.WriteLine(); Console.WriteLine();
            TestUserPreferences(client);
            Console.WriteLine(); Console.WriteLine();
            TestCategories(client, resourceIdsBuilder);
            Console.WriteLine(); Console.WriteLine();
            TestSubscriptions(client,resourceIdsBuilder);
            Console.WriteLine(); Console.WriteLine();
            TestTopics(client, resourceIdsBuilder);

        }

        private static void TestUserPreferences(FeedlyClient client)
        {
            try
            {
                WriteHeader("Testing User Preferences");
                Dictionary<string,string> result = client.GetPreferences().Result;

                foreach (var item in result)
                {
                    Console.WriteLine("{0} = {1}", item.Key, item.Value);
                }

                WriteSeparator();

                Dictionary<string, string> result1 = client.UpdatePreference("useEvernote2", "").Result;

                foreach (var item in result1)
                {
                    Console.WriteLine("{0} = {1}", item.Key, item.Value);
                }
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error happened while performing request:");
                foreach (var exception in ex.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private static void WriteSeparator()
        {
            Console.WriteLine("-----------------------------------");
        }

        private static void TestUserProfile(FeedlyClient client)
        {

            WriteHeader("Testing User Profile");

            Console.WriteLine("Getting profile:");
            try
            {
                
            Profile result = client.GetProfile().Result;

            Console.WriteLine(result.givenName);
            Console.WriteLine(result.familyName);

            WriteSeparator();
            Console.WriteLine("Updating profile:");
            result.givenName = "Simone";
            result.twitter = "simonech";
            
            Profile result1 = client.UpdateProfile(result).Result;

            Console.WriteLine(result1.givenName);
            Console.WriteLine(result1.familyName);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Error happened while performing request:");
                foreach (var exception in ex.InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }

            }
        }

        private static void WriteHeader(string header)
        {
            WriteSeparator();
            Console.WriteLine(header);
            WriteSeparator();
        }

        private static string WriteTestMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the testing console for Feedly.NET");
            Console.WriteLine("Please choose the feature of Feedly.NET you want to test:");
            Console.WriteLine();
            Console.WriteLine(" - [A]uthentication");
            Console.WriteLine(" - [U]ser profile");
            Console.WriteLine(" - user [P]references");
            Console.WriteLine(" - [C]ategories");
            Console.WriteLine(" - [S]ubscriptions");
            Console.WriteLine(" - [T]opics");
            Console.WriteLine(" - ta[G]s");
            Console.WriteLine(" - [F]eeds");
            Console.WriteLine();
            Console.WriteLine("Select the option by pressing the relevant key, or [enter] to test all");

            return Console.ReadLine();
        }
    }
}
