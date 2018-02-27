using Wangrong.BatchSubMerchant.Filereader;

namespace Wangrong.BatchSubMerchant
{
    /// <summary>
    /// </summary>
    public class SubMerchantImportSetting : TemplateSetting<string[], MerchantImportFile>
    {
        public SubMerchantImportSetting()
        {
            SkipRows = 1;
            FileReader = new ExcelReader(false);

            ConvertFunc = data =>
            {
                var result = new MerchantImportFile();
                var index = 0;
                result.Name = data[index];
                index++;
                result.MerchantNo = data[index];
                index++;
                result.PasswordCerf = data[index];
                index++;
                result.ContractEmail = data[index];
                index++;
                result.ContractPhone = data[index];
                index++;
                result.ProvinceCode = data[index];
                index++;
                result.CityCode = data[index];
                index++;
                result.DistrictCode = data[index];
                index++;
                result.ContractAddress = data[index];
                index++;
                result.ContractName = data[index];
                index++;
                result.IdCardNo = data[index];
                index++;
                result.AlipayBusiness = data[index];
                index++;
                result.WechatBusiness = data[index];
                index++;
                result.OurMerchantNo = data[index];
                index++;
                if (data.Length > index)
                    result.AlipaySubMerchant = data[index];
                index++;
                if (data.Length > index)
                    result.WechatSubMerchant = data[index];
                return result;
            };
        }
    }
}