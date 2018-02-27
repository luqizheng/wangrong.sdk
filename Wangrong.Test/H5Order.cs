using System;
using Wangrong.Sdk;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class H5Order
    {
        private readonly string requestNo = Guid.NewGuid().ToString("n");
        private readonly string orderNo = Guid.NewGuid().ToString("n");

        private WrWechatH5Payment Create()
        {
            //微信刷卡支付客户出示二维授权码，由商家进行扫码支付。
            var setting = SettingHelper.Setting();
            var orderDateTime = DateTime.Now;
            var payment = new WrWechatH5Payment(
                requestNo,
                setting.MerchantNumber,
                orderNo,
                orderDateTime, 1)
            {
                ClientIp = "192.168.1.1",
                CommodityName = "测试商品",

                NotifyUrl = SettingHelper.Setting().NotifyUrl,

                AgentId = "agentId",
                SubMchId = "SubMchId"
            };
            return payment;
            ;
        }

        [Fact]
        public void 创建()
        {
            var payment = Create();
            payment.SupportCreditCard = false;
            var actual = payment.ToDictionary();

            Assert.Equal("12", actual["transId"]);
            Assert.Equal("V1.1", actual["version"]);
            Assert.Equal(payment.OrderNo, actual["orderNo"]);
            Assert.Equal("0109", payment.ProductId);


            Assert.Equal(payment.RequestNo, actual["requestNo"]);
            Assert.Equal(payment.ClientIp, actual["clientIp"]);
            Assert.Equal(payment.CommodityName, actual["commodityName"]);

            Assert.Equal(payment.NotifyUrl, actual["notifyUrl"]);
            Assert.Equal(payment.ProductId, actual["productId"]);
            Assert.Equal(payment.OrderNo, actual["orderNo"]);
            Assert.Equal(payment.AgentId, actual["agentId"]);
            Assert.Equal(payment.SubMchId, actual["subMchId"]);
            Assert.Equal("no_credit", actual["limitPay"]);
            Assert.Equal(payment.OrderDate.ToString("yyyyMMdd"), actual["orderDate"]);

            Assert.Equal(payment.TransAmt.ToString(), actual["transAmt"]);

            //比文档少signature，
        }
    }
}