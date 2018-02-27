using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Wangrong.BatchSubMerchant.Filereader;
using Wangrong.Sdk;
using Wangrong.Sdk.Merchants;
using Wangrong.Sdk.Merchants.JsonEntity;
using Wangrong.Sdk.Utils;

namespace Wangrong.BatchSubMerchant
{
    /// <summary>
    /// </summary>
    public class MerchantImportFile : IImportResult
    {
        public static string
            RequestUrl =
                "http://spdbweb.chinacardpos.com/payment-gate-web/gateway/api/backTransReq";

        public string Name { get; set; }

        public string CerfPath { get; set; }

        public string PasswordCerf { get; set; }

        public string AlipaySubMerchant { get; set; }

        public string WechatSubMerchant { get; set; }

        public string MerchantNo { get; set; }

        public string ContractName { get; set; }
        public string ContractPhone { get; set; }
        public string ContractEmail { get; set; }

        public string ContractAddress { get; set; }
        public string WechatBusiness { get; set; }
        public string AlipayBusiness { get; set; }
        public string IdCardNo { get; set; }

        public string DistrictCode { get; set; }
        public string CityCode { get; set; }
        public string ProvinceCode { get; set; }
        public string WechatErrorMessage { get; set; }
        public string AlipayErrorMessage { get; set; }

        public string OurMerchantNo { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsValidate { get; set; }
        public bool ImportSuccess { get; set; }

        public string BuildOurSqlInsert()
        {
            var file = this;
            var str = @"INSERT INTO [dbo].[gateway_merchant]
                    ([Id]
                ,[Name]
                ,[Remarks]
                ,[SignatureId])
            VALUES
                ('" + file.OurMerchantNo + @"'
                ,'" + file.Name + @"'
                ,'" + file.Name + @"'
                ,'84357799-bbbe-4265-974c-759f35d3fc3e')
            GO
";
            str += @"INSERT INTO [dbo].[gateway_protocols]
           ([Id]
           ,[MerchantId]
           ,[Name]
           ,[NotifyUrl]
           ,[Remarks]
           ,[RequestUrl]
           ,[CerficationPath]
           ,[MerchantNumber]
           ,[Password]
           ,[ReturnUrl]
           ,[AgentId]
           ,[MerchantRequestUrl]
           ,[WechatSubMerchant]
           ,[AlipaySubMerchant])
     VALUES
           ('" + Guid.NewGuid().ToString("N") + @"'
            ,'" + file.OurMerchantNo + @"'
            ,'" + file.Name + @"'
            ,'http://123.207.53.108/api/" + file.OurMerchantNo + @"/callback/wangrong'
           ,''
           ,'http://spdbweb.chinacardpos.com/payment-gate-web/gateway/api/backTransReq'
           ,'C:\cef\" + file.OurMerchantNo + @"\wangrong\" + file.MerchantNo + @".pfx'
          ,'" + file.MerchantNo + @"'
          ,'" + file.PasswordCerf + @"'
           ,'http://123.207.53.108/api/" + file.OurMerchantNo + @"/callback/wangrong'
           ,null
           ,null
          ,'" + file.WechatSubMerchant + @"'
           ,'" + file.AlipaySubMerchant + @"')
GO
";
            return str;
        }

        public void Invoke(ILoggerFactory loggerFactory, bool alipay, bool wechat)
        {
            var cerfPath = GetCerfPath();
            var alipayTask = !alipay
                ? Task.FromResult(0)
                : Task.Run(() =>
                {
                    var result = GetSubMerchantAlipay(loggerFactory, cerfPath);
                    if (result.RespCode == "0000")
                        AlipaySubMerchant = result.SubMchId;
                    else
                        AlipayErrorMessage = result.RespCode + "," + result.RespDesc;
                });

            var wechatTask = !wechat
                ? Task.FromResult(0)
                : Task.Run(() =>
                {
                    var result = GetSubMerchantWechat(loggerFactory, cerfPath);
                    if (result.RespCode == "0000")
                        WechatSubMerchant = result.SubMchId;
                    else
                        WechatErrorMessage = result.RespCode + "," + result.RespDesc;
                });

            Task.WaitAll(alipayTask, wechatTask);
        }

        private string GetCerfPath()
        {
            var result = "E:\\证书\\" + MerchantNo + ".pfx";

            if (!Directory.Exists("E:\\证书\\" + OurMerchantNo))
                Directory.CreateDirectory("E:\\证书\\" + OurMerchantNo);

            if (!Directory.Exists("E:\\证书\\" + OurMerchantNo + "\\wangrong"))
                Directory.CreateDirectory("E:\\证书\\" + OurMerchantNo + "\\wangrong");
            if (!File.Exists("E:\\证书\\" + OurMerchantNo + "\\wangrong\\" + MerchantNo + ".pfx"))
                File.Copy(result, "E:\\证书\\" + OurMerchantNo + "\\wangrong\\" + MerchantNo + ".pfx");
            return result;
        }

        public SignInResult GetSubMerchantWechat(ILoggerFactory loggerFactory, string filePath)
        {
            var logger = loggerFactory.CreateLogger<WangrongService>();
            var serviec =
                new WangrongService(logger, RequestUrl,
                    new Signature(filePath, PasswordCerf));
            var wechatPostData = GetBuild(false);
            var wxResult = serviec.SignIn(wechatPostData, CreateContractInfo(),
                CreateAddressInfo());

            return wxResult;
        }

        public SignInResult GetSubMerchantAlipay(ILoggerFactory loggerFactory, string cerfPath)
        {
            var logger = loggerFactory.CreateLogger<WangrongService>();
            var serviec =
                new WangrongService(logger, RequestUrl,
                    new Signature(cerfPath, PasswordCerf));
            var wechatPostData = GetBuild(true);
            var wxResult = serviec.SignIn(wechatPostData, CreateContractInfo(),
                CreateAddressInfo());

            return wxResult;
        }

        public SignIn GetBuild(bool isAlipay)
        {
            var sigin = new SignIn(Guid.NewGuid().ToString("N"), MerchantNo)
            {
                Business = isAlipay ? AlipayBusiness : WechatBusiness,
                SubMechantName = Name, //.Replace("妍丽化妆品(中国)有限公司", ""),
                MerchantRemark = Name, //.Replace("妍丽化妆品(中国)有限公司", ""),
                SubMerchantShortname = Name, //;//.Replace("妍丽化妆品(中国)有限公司", ""),
                ServicePhone = ContractPhone,
                Contact = ContractName,
                ContactPhone = ContractPhone,
                ContactEmail = ContractEmail,
                PayWay = isAlipay ? "ALIPAY" : "WX"
            };
            return sigin;
        }

        public contactInfo CreateContractInfo()
        {
            var contractInfo = new contactInfo
            {
                name = ContractName,
                email = ContractEmail,
                mobile = ContractPhone,
                phone = ContractPhone,
                type = "OTHER",
                id_card_no = IdCardNo
            };

            return contractInfo;
        }

        public address_info CreateAddressInfo()
        {
            var addressINfo = new address_info
            {
                city_code = CityCode,
                district_code = DistrictCode,
                province_code = ProvinceCode,
                address = ContractAddress
            };
            return addressINfo;
        }
    }
}