namespace Dam.Domain
{
    public interface IDownloadService
    {
        string GetHtmlText();
        void DownloadFile(string url, string destination);
    }
}