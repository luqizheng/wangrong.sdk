namespace Wangrong.BatchSubMerchant.Filereader
{
    public interface IImportResult
    {
        string ErrorMessage { get; set; }
        bool IsValidate { get; set; }
        bool ImportSuccess { get; set; }
    }
}