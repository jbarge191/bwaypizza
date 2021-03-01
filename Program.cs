using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace BrightPizza
{
    class Program
    {
        static void Main(string[] args)
        {
            //Try obtaining web request and deserializing JSON array into an object called PizzaToppingsJson
            List<PizzaToppingsJson> toppingsObj = null;
            try
            {
                //Note: I most likely would have used the HttpClient Class, but I wanted to stay within the request of the assessment.
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.brightway.com/CodeTests/pizzas.json");
                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                string responseText;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }
                toppingsObj = JsonConvert.DeserializeObject<List<PizzaToppingsJson>>(responseText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(2);
            }
            //Exit application if object is null.
            if (toppingsObj == null)
            {
                Console.WriteLine($"JSON string to deserialize was null, exiting application.");
                Environment.Exit(2);
            }

            var result = CompareToppings(toppingsObj);
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine($"The [{string.Join(",", item.Toppings)}] combination has been ordered {item.Count} times.");
                }
                //Solution ends here.
                Console.WriteLine("Press any key to close the application.");
                Console.ReadKey();
            }
        }


        public static List<PizzaToppings> CompareToppings(List<PizzaToppingsJson> pizzaToppings)
        {
            var result = pizzaToppings.GroupBy(a => a.Toppings, new ToppingsComparer())
                                      .Select(f => new PizzaToppings() 
                                        { Toppings = f.Key, Count = f.Count() 
                                        })
                                      .OrderByDescending(f => f.Count)
                                      .Take(20)
                                      .ToList();
            return result;
        }
       
    }
}
