public class UploadDetail
{
    public bool IsReady { get; set; }
    public int ContentLength { get; set; }
    public int UploadedLength { get; set; }
    public string FileName { get; set; }
    public string DestinationPath { get; set; }
}