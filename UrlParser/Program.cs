using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// #1
// input = 'http://holidaycheck.com/'  
// output = { protocol: 'http', 'domain': 'holidaycheck.com' } 
// #2
// input = 'https://holidaycheck.com/passions?q=yoga'  
// output = { protocol: 'http', 'domain': 'holidaycheck.com', path: 'passions', query: {q: 'yoga'} }

namespace UrlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> resultDictionary = new Dictionary<string, string>();
            Dictionary<string, object> resultDictionary2 = new Dictionary<string, object>();

            //1
            #region 1
            Console.WriteLine("URL : http://holidaycheck.com/ ");
            resultDictionary = UrlParser("http://holidaycheck.com/");
            foreach (var item in resultDictionary)
            {
                Console.Write(item.Key.ToString() + " : ");
                Console.WriteLine(item.Value.ToString());
            }
            #endregion



            Console.WriteLine();
            Console.WriteLine("-------------------->Tap a key for Advanced Url splitting : ");
            Console.WriteLine("URL : https://holidaycheck.com/passions?q=yoga ");
            Console.ReadKey();
            



            //2
            #region 2
            resultDictionary2 = UrlAdvancedParser("https://holidaycheck.com/passions?q=yoga");
            foreach (var item in resultDictionary2)
            {
                Console.Write(item.Key.ToString() + " : ");
                if (item.Key == "query")
                {
                    //cast the object to dictionary <string, string>
                    var urlQuery = ((KeyValuePair<string, object>)item).Value;
                    Dictionary<string, string> q = urlQuery as Dictionary<string, string>;
                    foreach (var keyValue in q)
                    {
                        Console.Write(keyValue.Key.ToString() + " : ");
                        Console.WriteLine(keyValue.Value.ToString());
                    }

                }
                else
                {
                    Console.WriteLine(item.Value.ToString());
                }
                
            }
            #endregion
            Console.ReadKey();

        }

        //function for case 1 : extracting protocol and domain
        static Dictionary<string, string> UrlParser(string url)
        {
            // declaring a dictionary that will contain the separated strings
            Dictionary<string, string> cuttedStringDictionary = new Dictionary<string, string>();

            string protocol;
            string domain;

            protocol = url.Split(':')[0];
            domain = url.Split(':')[1];
            domain = domain.Replace('/',' ');

            cuttedStringDictionary.Add("protocl", protocol);
            cuttedStringDictionary.Add("domain", domain);

            return cuttedStringDictionary;

        }

        //function for case 2 : extracting protocol, domain ,path and query
        static Dictionary<string, object> UrlAdvancedParser(string url)
        {
            // declaring a dictionary that will contain the separated strings with there keys
            Dictionary<string, object> cuttedStringDictionary = new Dictionary<string, object>();

            // dictionary query
            Dictionary<string, string> queryDictionary = new Dictionary<string, string>();

            string protocol;
            string domain;
            string path;
            string query;


            protocol = url.Split(':')[0];
            domain = url.Split(':')[1];
            //result = //holidaycheck.com/passions?q=yoga'
            //delete the first (//)
            domain = domain.Substring(2);
            //result =   'holidaycheck.com/passions?q=yoga'
            path = domain.Split('/')[1];
            //result = passions?q=yoga'
            domain = domain.Split('/')[0];
            //result = holidaycheck.com

            query = path.Split('?')[1];
            //result = q=yoga'
            path = path.Split('?')[0];
            //result = passions


            queryDictionary.Add(query.Split('=')[0], query.Split('=')[1]);


            cuttedStringDictionary.Add("protocl", protocol);
            cuttedStringDictionary.Add("domain", domain);
            cuttedStringDictionary.Add("path", path);
            cuttedStringDictionary.Add("query", queryDictionary);

            return cuttedStringDictionary;

        }
    }
}




