using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace _2GisAPI
{
    class Program
    {
        static void Main(string[] args)
        {


            string time = $"&offset_date={DateTime.Now.Year}-{DateTime.Now.Month.ToString("0#")}-{DateTime.Now.Day}T00:00:00.000000%2B07:00";
            string urlA = "https://public-api.reviews.2gis.com/2.0/branches/70000001029417608/reviews?limit=10";
            string urlB = "&is_advertiser=false&fields=meta.providers,meta.branch_rating,meta.branch_reviews_count,meta.total_count,reviews.hiding_reason,reviews.is_verified&without_my_first_review=false&rated=true&sort_by=date_edited&key=37c04fe6-a560-4549-b459-02309cf643ad&locale=ru_RU";


            /*
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlA + time + urlB);
            HttpWebResponse ans = (HttpWebResponse)req.GetResponse();
            */
            string ans = "";
            string url = urlA + urlB;
            bool next = true;
            List<Comment> list = new List<Comment>();

            do
            {

                ans = Ans(url);
                var res = JsonConvert.DeserializeObject<JToken>(ans);
                var tempo = (JObject)JsonConvert.DeserializeObject<JToken>(res["meta"].ToString());
                next = tempo.ContainsKey("next_link");
                if (next)
                {
                    Console.WriteLine(tempo["next_link"]);
                    url = tempo["next_link"].ToString();
                }

                var run = res["reviews"].First;

                while (run != null)
                {
                    list.Add(new Comment(run["text"].Value<string>(), run["rating"].Value<double>(), run["likes_count"].Value<int>()));

                    run = run.Next;
                }

            } while (next);

        }


        static async Task<string> Temp(string rout)
        {
            string ans = "";
            HttpClient client = new HttpClient();

            try
            {
                HttpResponseMessage resp = await client.GetAsync(rout);

                resp.EnsureSuccessStatusCode();
                string body = await resp.Content.ReadAsStringAsync();
                return body;

                /* using (var res = await resp.Content.ReadAsStreamAsync())
                 {
                     return (string)
                 }*/
            }
            catch (Exception e)
            {

                Console.WriteLine("АААААААА АШИБКА СТОП НОЛЬ НОЛЬ АААААААААААААААА");
            }


            return ans;
        }

        static string Ans(string rout)
        {
            string ans = "";
            WebRequest req = WebRequest.Create(rout);
            WebResponse wb = req.GetResponse();

            using (Stream stream = wb.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        ans += line;
                    }
                }
            }

            wb.Close();

            return ans;
        }


    }


    class Comment
    {
        string text;
        double rating;
        int likes_count;

        public Comment(string text, double rating, int likes)
        {
            this.text = text;
            this.rating = rating;
            likes_count = likes;
        }

    }

}

