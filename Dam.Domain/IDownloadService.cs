namespace Dam.Domain
{
    public interface IDownloadService
    {
        string GetText(string url);
        byte[] DownloadBytes(string url);
    }
}