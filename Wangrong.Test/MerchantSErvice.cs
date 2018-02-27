using System;
using Microsoft.Extensions.Logging;
using Wangrong.Sdk;
using Wangrong.Sdk.Merchants;
using Wangrong.Sdk.Merchants.JsonEntity;
using Wangrong.Sdk.Utils;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class MerchantSErvice
    {
        [Fact]
        public void 报备()
        {
            var setting = SettingHelper.Setting();
            var c = new LoggerFactory().CreateLogger<WangrongService>();
            var serviec = new WangrongService(c, setting.RequestUrl,
                new Signature(setting.CerficationPath, setting.Password));
            var sigin = new SignIn(Guid.NewGuid().ToString("N"), setting.MerchantNumber);
            sigin.PayWay = "WX";
            sigin.SubMechantName = "深圳市前海科技有限公司";
            sigin.SubMerchantShortname = "前海前台";
            sigin.Contact = "张三";
            sigin.ContactPhone = "15680581617";
            sigin.ContactEmail = "huangxuelian@colotnet.com";
            sigin.MerchantRemark = "深圳市生银万国网络科技有限公司";
            sigin.ServicePhone = "13600368080";
            sigin.Business = "53";

            var contractInfo = new contactInfo();
            contractInfo.name = "张三1";
            contractInfo.email = "13012345678@qq.com";
            contractInfo.mobile = "13012345678";
            contractInfo.phone = "0734-5820311";
            contractInfo.type = "OTHER";
            contractInfo.id_card_no = "430423199001013213";

            var addressINfo = new address_info();
            addressINfo.city_code = "440100";
            addressINfo.district_code = "440103";
            addressINfo.province_code = "440000";
            addressINfo.address = "深圳市软件园8";

            var result = serviec.SignIn(sigin, contractInfo, addressINfo);
            // 2088621879311910
            Assert.True(result.RespCode == "0000", result.RespDesc);

            Assert.True(result.SubMchId == null, result.SubMchId);
        }
    }
}