using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dam.Domain;

namespace Dam.Infrastructure
{
    public class DownloadService : IDownloadService
    {
        public string GetHtmlText()
        {
            var httpClient = new HttpClient();
            var task = httpClient.GetStringAsync("http://www.moa.gov.cy/moa/wdd/wdd.nsf/reservoir_en/reservoir_en");
            task.Wait(TimeSpan.FromSeconds(60));
            if (task.Status != TaskStatus.RanToCompletion)
            {
                return null;
            }

            var htmlText = task.Result;
            return htmlText;
        }

        public void DownloadFile(string url, string destination)
        {
            
        }
    }
}
