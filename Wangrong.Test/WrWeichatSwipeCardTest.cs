using System;
using Wangrong.Sdk.Wechat;
using Xunit;

namespace Wangrong
{
    public class WrWeichatSwipeCardTest
    {
        [Fact]
        public void ToDict()
        {
            var guid = Guid.NewGuid().ToString("N");
            var merNo = "ok";
            var payment = new WechatQuickResponseOrder(guid, merNo, "orderNo", DateTime.Now, 10);
            payment.ClientIp = "127.0.0.1";
            payment.CommodityName = "commodityName";
            payment.NotifyUrl = "notifyUrl";
            payment.SubMchId = "submchid";
            payment.StoreId = "storeId";
            payment.SupportCreditCard = false;
            payment.ReturnUrl = "returnurl";
            var actual = payment.ToDictionary();

            Assert.Equal("10", actual["transId"]);
            Assert.Equal("V1.1", actual["version"]);
            Assert.Equal(payment.OrderNo, actual["orderNo"]);

            Assert.Equal("0108", payment.ProductId);
            Assert.Equal(payment.ProductId, actual["productId"]);


            Assert.Equal(payment.RequestNo, actual["requestNo"]);
            Assert.Equal(payment.ClientIp, actual["clientIp"]);
            Assert.Equal(payment.CommodityName, actual["commodityName"]);

            Assert.Equal(payment.NotifyUrl, actual["notifyUrl"]);

            Assert.Equal(payment.OrderNo, actual["orderNo"]);
            Assert.Equal(payment.AgentId ?? "", actual["agentId"]);
            Assert.Equal(payment.SubMchId, actual["subMchId"]);
            Assert.Equal("no_credit", actual["limitPay"]);
            Assert.Equal(payment.OrderDate.ToString("yyyyMMdd"), actual["orderDate"]);
            Assert.Equal(payment.StoreId, actual["storeId"]);
            Assert.Equal(payment.TransAmt.ToString(), actual["transAmt"]);
            Assert.Equal(payment.ReturnUrl, actual["returnUrl"]);
        }
    }
}