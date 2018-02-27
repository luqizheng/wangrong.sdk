using System;
using Microsoft.Extensions.Logging;
using Wangrong.Sdk;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class WangrongServceTest
    {
        [Fact]
        public void NotifyNoSet()
        {
            var setting = SettingHelper.Setting();
            var logger = new LoggerFactory().CreateLogger<WangrongService>();
            var service = new WangrongService(logger, setting.RequestUrl, setting.SignatureHelper);
            var h5Order = new WrWechatH5Payment(Guid.NewGuid().ToString("N"),
                setting.MerchantNumber, DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Today, 100);
            h5Order.CommodityName = "商户号";
            h5Order.ClientIp = "127.0.0.1";
            h5Order.SubMchId = "subMchId";
            Exception ex = Assert.Throws<ArgumentNullException>(() => service.GetH5PayUrl(h5Order));
            Assert.Equal("NotifyUrl不能为空\r\nParameter name: trans", ex.Message);
            h5Order.NotifyUrl = setting.NotifyUrl;

            h5Order.ClientIp = null;
            ex = Assert.Throws<ArgumentNullException>(() => service.GetH5PayUrl(h5Order));
            Assert.Equal("ClientIp不能为空\r\nParameter name: trans", ex.Message);

            h5Order.ClientIp = "127.0.0.1";

            h5Order.SubMchId = "";
            ex = Assert.Throws<ArgumentNullException>(() => service.GetH5PayUrl(h5Order));
            Assert.Equal("二级商户名SubMchId不能为空\r\nParameter name: trans", ex.Message);
            h5Order.SubMchId = setting.WechatSubMerchant;
        }
    }
}