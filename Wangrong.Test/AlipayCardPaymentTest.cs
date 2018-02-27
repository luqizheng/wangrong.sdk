using System;
using Wangrong.Sdk;
using Wangrong.Sdk.Alipay;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class AlipayCardPaymentTest
    {
        private AlipaySwipeCodePayment Create()
        {
            //微信刷卡支付客户出示二维授权码，由商家进行扫码支付。
            var orderDateTime = DateTime.Now;
            var setting = SettingHelper.Setting();
            var payment = new AlipaySwipeCodePayment(
                Guid.NewGuid().ToString("N"),
                setting.MerchantNumber,
                Guid.NewGuid().ToString("n"),
                orderDateTime, "wechantCode", 100)
            {
                ClientIp = "192.168.1.1",
                CommodityName = "测试商品",
                ReturnUrl = "http://127.0.0.1/returnUrl",
                NotifyUrl = SettingHelper.Setting().NotifyUrl,

                AgentId = "agent",
                SubMchId = "sunbM"
            };
            return payment;
            ;
        }

        [Fact]
        public void OrderPayament()
        {
            var payment = Create();
            var actual = payment.ToDictionary();
            Assert.Equal("17", payment.TransId);
            Assert.Equal("17", actual["transId"]);

            Assert.Equal("V1.1", actual["version"]);
            Assert.Equal(payment.OrderNo, actual["orderNo"]);

            Assert.Equal("0120", payment.ProductId);
            Assert.Equal("0120", actual["productId"]);

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
            Assert.Equal(payment.AgentId, actual["agentId"]);
            Assert.Equal(payment.SubMchId, actual["subMchId"]);

            Assert.Equal(16, actual.Count);
        }

        [Fact]
        public void PayResult()
        {
            var r =
                "autoCode=285562959623326734&clientIp=127.0.0.1&commodityName=tester&merNo=310440300001134&notifyUrl=http://127.0.0.1/notifyurl&orderDate=20170413&orderNo=9ca39cd4035f4c02b513e44e3dd911eb&productId=0120&requestNo=8ee1bec8dcb34e5c84b5d10268d06790&respCode=0035&respDesc=原交易已撤销&returnUrl=http://127.0.0.1/returnUrl&transAmt=100&transId=17&version=V1.1&signature=gfWpalmKDuGzcyi7J8L7Ww+llmk1G0YNt5fPCoyNax+PwORftvWIiJrgy7QrAfsEPlGMsfodpdIVjvkza5Wj+VXJpJ9UU5XfABzOo/y/GsGe3OSS83OhI1OqD1pXCtC0A34YUGeHdOeA0jieDeT9dvoFqMZJHO6Rue1Wqtt5SLk7uEWZc9T2thvJBjp4Fx5LWOPQg+sRl64KG3i3fU9wFY8NVOw3yNVMSPc8UQ9MF7XScZpgo9zQTpPvP0/rHG0Em6tBxQwoBuKt7/mQYvMcw+42sfyuU9Cfk1BJz1lv4t8sOc+xB3sbwxIBaK1HSWuKm/WWwce4s08WJ2cn1Bf5Ng==";
            var result = new SwipeCodeResult();
            result.FillBy(r);
            Assert.Equal("285562959623326734", result.AutoCode);
            Assert.Equal("127.0.0.1", result.ClientIp);
            Assert.Equal("tester", result.CommodityName);
            Assert.Equal("310440300001134", result.MerNo);
            Assert.Equal("17", result.TransId);
            Assert.Equal("http://127.0.0.1/notifyurl", result.NotifyUrl);
            Assert.Equal("20170413", result.OrderDate.ToString("yyyyMMdd"));
            Assert.Equal("9ca39cd4035f4c02b513e44e3dd911eb", result.OrderNo);
            Assert.Equal("0120", result.ProductId);
            Assert.Equal("8ee1bec8dcb34e5c84b5d10268d06790", result.RequestNo);
            Assert.Equal("0035", result.RespCode);
            Assert.Equal("原交易已撤销", result.RespDesc);
            Assert.Equal(100, result.TransAmt);
            Assert.Equal("http://127.0.0.1/returnUrl", result.ReturnUrl);
            Assert.Equal("V1.1", result.Version);
            Assert.NotNull(result.Signature);


            var signature = result.ToDictionary();
            var actual = SettingHelper.GetSignature(signature);
            Assert.NotEmpty(actual);
        }
    }
}