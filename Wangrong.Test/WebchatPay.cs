using System;
using Wangrong.Sdk;
using Wangrong.Sdk.Wechat;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class WebchatPay
    {
        private WechatSwipeCodePayment Create()
        {
            //微信刷卡支付客户出示二维授权码，由商家进行扫码支付。

            var setting = SettingHelper.Setting();
            var orderDateTime = DateTime.Now;
            var payment = new WechatSwipeCodePayment(
                Guid.NewGuid().ToString("N"),
                setting.MerchantNumber,
                Guid.NewGuid().ToString("n"),
                orderDateTime, "wechantCode", 100)
            {
                ClientIp = "192.168.1.1",
                CommodityName = "测试商品",
                ReturnUrl = "http://127.0.0.1/returnUrl",
                NotifyUrl = SettingHelper.Setting().NotifyUrl
            };
            return payment;
            ;
        }

        [Fact]
        public void OrderPayament()
        {
            var payment = Create();
            var actual = payment.ToDictionary();

            Assert.Equal("17", actual["transId"]);
            Assert.Equal("V1.1", actual["version"]);
            Assert.Equal(payment.OrderNo, actual["orderNo"]);
            Assert.Equal("0113", payment.ProductId);

            Assert.Equal(payment.AutoCode, actual["autoCode"]);
            Assert.Equal(payment.RequestNo, actual["requestNo"]);
            Assert.Equal(payment.ClientIp, actual["clientIp"]);
            Assert.Equal(payment.CommodityName, actual["commodityName"]);
            Assert.Equal(payment.ReturnUrl, actual["returnUrl"]);
            Assert.Equal(payment.NotifyUrl, actual["notifyUrl"]);
            Assert.Equal(payment.ProductId, actual["productId"]);
            Assert.Equal(payment.OrderNo, actual["orderNo"]);

            Assert.Equal(payment.OrderDate.ToString("yyyyMMdd"), actual["orderDate"]);

            Assert.Equal(payment.TransAmt.ToString(), actual["transAmt"]);


            Assert.Equal(16, actual.Count);
        }

        //[Fact]
        //public void OrderPayament_full()
        //{
        //    var orderSearch = Create();
        //    orderSearch.AgentId = "agentId";
        //    orderSearch.SubMchId = "submchid";
        //    orderSearch.StoreId = "storeId";

        //    var actual = orderSearch.ToDictionary();

        //    Assert.Equal(16, actual.Count);
        //}


        [Fact]
        public void OrderPaymentResult()
        {
            var r =
                "autoCode=130263254927466172&clientIp=127.0.0.1&commodityName=tester&merNo=310440300001134&notifyUrl=http://127.0.0.1/notifyurl&orderDate=20170412&orderNo=0bcca7b409844bb5a726e8f76632d768&productId=0113&requestNo=1f4358db95de42dbb0ddc65acf69a230&respCode=0000&respDesc=交易成功&returnUrl=http://127.0.0.1/returnUrl&transAmt=100&transId=17&version=V1.1&signature=oZfmxqyoPNED+RMGzpmmZy0ml7xZPinWTOgMrzAdhv/Tkq64fkmU34Z9HdY8RPdBUrihzRIn/rwSOC5zoO9/M/KRV9crCMrE4d5Ri4LqDdoc2Mc+sEDh3JUpHuihx2lvcFYrLmrvIMPYl6smpFXASAKTAj9fJcDn2xh23Q/y6JUT9EzAmMf7xbQHbjEciKxT3j+ipW7OMQ4uyjIM4BIur3VrSPILf6Sne/iirXrRtHz6dUVGHc9jboux/ILFM0nhN+RYf4k+aNbKzIMfX6CR7MphuCP3+ZCjBgZyyM+95Jma0G2Rc5UvD8d9Gs/9hqVS/XqSoiJk59mZWAhdu3fMQg==";
            var result = new SwipeCodeResult();
            result.FillBy(r);
            Assert.Equal("130263254927466172", result.AutoCode);
            Assert.Equal("127.0.0.1", result.ClientIp);
            Assert.Equal("tester", result.CommodityName);
            Assert.Equal("310440300001134", result.MerNo);
            Assert.Equal("17", result.TransId);
            Assert.Equal("http://127.0.0.1/notifyurl", result.NotifyUrl);
            Assert.Equal("20170412", result.OrderDate.ToString("yyyyMMdd"));
            Assert.Equal("0bcca7b409844bb5a726e8f76632d768", result.OrderNo);
            Assert.Equal("0113", result.ProductId);
            Assert.Equal("1f4358db95de42dbb0ddc65acf69a230", result.RequestNo);
            Assert.Equal("0000", result.RespCode);
            Assert.Equal("交易成功", result.RespDesc);
            Assert.Equal(100, result.TransAmt);
            Assert.Equal("http://127.0.0.1/returnUrl", result.ReturnUrl);
            Assert.Equal("V1.1", result.Version);
            Assert.NotNull(result.Signature);


            //var signature = result.ToDictionary();
            //var actual = SettingHelper.GetSignature(signature);
            Assert.NotEmpty(result.Signature);
        }
    }
}