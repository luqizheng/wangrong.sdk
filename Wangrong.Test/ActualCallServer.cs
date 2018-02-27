using System;
using Microsoft.Extensions.Logging;
using Wangrong.Sdk;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class ActualCallServer
    {
        [Fact]
        public void H5支付()
        {
            var setting = SettingHelper.Setting();
            var logger = new LoggerFactory().CreateLogger<WangrongService>();
            var service = new WangrongService(logger, setting.RequestUrl, setting.SignatureHelper);
            var h5Order = new WrWechatH5Payment(Guid.NewGuid().ToString("N"),
                setting.MerchantNumber, DateTime.Now.ToString("yyyyMMddHHmmss"), DateTime.Today, 100);
            h5Order.SubMchId = setting.WechatSubMerchant;
            h5Order.ClientIp = "192.168.1.1";
            h5Order.NotifyUrl = setting.NotifyUrl;
            h5Order.CommodityName = "commoditName";
            var c = service.GetH5PayUrl(h5Order);
        }

        //[Fact]
        //public void OrderPayment_PayToUrl()
        //{
        //    var setting = SettingHelper.Setting();
        //    var orderDateTime = DateTime.Now;
        //    var payment = new WechatSwipeCodeOrder(
        //        Guid.NewGuid().ToString("N"),
        //        setting.MerchantNumber,
        //        Guid.NewGuid().ToString("n"),
        //        orderDateTime, "wechantCode", 100)
        //    {
        //        ClientIp = "192.168.1.1",
        //        CommodityName = "测试商品",
        //        ReturnUrl = "http://127.0.0.1/returnUrl",
        //        NotifyUrl = SettingHelper.Setting().NotifyUrl,

        //    };

        //    payment.AutoCode = "130150687673785255";
        //    payment.SubMchId = "35681554";
        //    var c = new LoggerFactory().CreateLogger<WangrongService>();
        //    var servie = new WangrongService(c, setting.RequestUrl, setting.SignatureHelper);
        //    var result = servie.Pay(payment);
        //    Assert.True(result.RespCode == "0000", result.RespCode + "," + result.RespDesc);
        //}
    }
}