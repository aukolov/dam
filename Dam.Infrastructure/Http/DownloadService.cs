using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dam.Domain;

namespace Dam.Infrastructure.Http
{
    public class DownloadService : IDownloadService
    {
        public string GetText(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var task = httpClient.GetStringAsync(url);
                if (!Wait(task)) return null;

                var htmlText = task.Result;
                return htmlText;
            }
        }

        public byte[] DownloadBytes(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var task = httpClient.GetByteArrayAsync(url);
                if (!Wait(task)) return null;

                return task.Result;
            }
        }

        private static bool Wait(Task task)
        {
            task.Wait(TimeSpan.FromSeconds(60));
            return task.Status == TaskStatus.RanToCompletion;
        }
    }
}
