using System;
using Wangrong.Sdk;
using Xunit;

namespace Wangrong
{
    public class RefundUnitTest
    {
        [Fact]
        public void FromReturnData()
        {
            var data =
                @"merNo=800440054111002&notifyUrl=http://123.207.53.108/api/70070001/callback&orderDate=20170715&orderNo=75ac5f5d8d52435eafd19a05390a6d5c&origOrderDate=00010101&origOrderNo=70070001170713211636&requestNo=019bdfc803074100a3315bd7d8be77bf&respCode=0028&respDesc=原交易不存在&returnUrl=http://123.207.53.108/api/70070001/callback&transAmt=0&transId=02&version=V1.1&signature=FWlKorOjQv7CKKcyONJECKp4Cu6t+/eR7gDBf4192ikH6DmaGw5do4P6GtQkA3tNtfGRLV2u9JIgvAPr5DZa53nxZfO1Cj2sdzsdD3qSjWlIIEXkTmzjS4U/puBGs0DEVZrboPGKuysiQ2GBN/W9h/dGW0k9w9CqMkSY9MZOKYceBdrIeUko4cQ2tR6n4cZEJiNzxFkmf0DULpM+rfY+xlVaqunh0bFhe1JK/GL7YTRoJ5CVE2slLwuSKdtAEQpAXrU2Fyd1i7fN41bIcr7tZSB+HD89zAJ5rVsVOvXyetfDnj3nuzAD3+I8oad+fkoHCkm2ixaitC4Og3R+xX+wfA==";

            var ary = data.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);

            var RefundRe = new CancelOrderResult();
            RefundRe.FillBy(data);

            Assert.Equal("800440054111002", RefundRe.MerNo);
            Assert.Equal("http://123.207.53.108/api/70070001/callback", RefundRe.NotifyUrl);
            Assert.Equal("75ac5f5d8d52435eafd19a05390a6d5c", RefundRe.OrderNo);
            Assert.Equal("70070001170713211636", RefundRe.OrigOrderNo);
            Assert.Equal("20170715", RefundRe.OrderDate.ToString("yyyyMMdd"));
            Assert.Equal("00010101", RefundRe.OrigOrderDate.ToString("yyyyMMdd"));
            Assert.Equal(0, RefundRe.TransAmt);
            Assert.Equal("02", RefundRe.TransId);
            Assert.Equal("V1.1", RefundRe.Version);
            Assert.Equal("0028", RefundRe.RespCode);
            Assert.Equal("原交易不存在", RefundRe.RespDesc);
            Assert.Equal(
                "FWlKorOjQv7CKKcyONJECKp4Cu6t+/eR7gDBf4192ikH6DmaGw5do4P6GtQkA3tNtfGRLV2u9JIgvAPr5DZa53nxZfO1Cj2sdzsdD3qSjWlIIEXkTmzjS4U/puBGs0DEVZrboPGKuysiQ2GBN/W9h/dGW0k9w9CqMkSY9MZOKYceBdrIeUko4cQ2tR6n4cZEJiNzxFkmf0DULpM+rfY+xlVaqunh0bFhe1JK/GL7YTRoJ5CVE2slLwuSKdtAEQpAXrU2Fyd1i7fN41bIcr7tZSB+HD89zAJ5rVsVOvXyetfDnj3nuzAD3+I8oad+fkoHCkm2ixaitC4Og3R+xX+wfA==",
                RefundRe.Signature);
        }

        [Fact]
        public void ToDictTest()
        {
            var requireId = Guid.NewGuid().ToString("N");
            var orderId = Guid.NewGuid().ToString("N");
            var oriDate = DateTime.Now;
            var d = new RefundTranscation(requireId,
                "merNo",
                orderId,
                "orderNo", 100, oriDate, "测试");
            d.AgentId = "agentId";
            d.ReturnUrl = "http://1237.0.0.1";

            var actual = d.ToDictionary();
            Assert.Equal(requireId, d.RequestNo);
            Assert.Equal("02", d.TransId);
            Assert.Equal("V1.1", d.Version);
            Assert.Equal(d.TransAmt.ToString(), actual["transAmt"]);
            Assert.Equal(d.Version, actual["version"]);
            Assert.Equal(d.MerNo, actual["merNo"]);
            Assert.Equal(d.TransId, actual["transId"]);
            Assert.Equal(d.AgentId, actual["agentId"]);

            Assert.Equal(d.OrderDate.ToString("yyyyMMdd"), actual["orderDate"]);
            Assert.Equal(d.OrderNo, actual["orderNo"]);
            Assert.Equal(d.OrigOrderNo, actual["origOrderNo"]);
            Assert.Equal(d.OrigOrderDate.ToString("yyyyMMdd"), actual["origOrderDate"]);
            Assert.Equal(d.ReturnUrl, actual["returnUrl"]);
            Assert.Equal(13, actual.Count);
        }

        //[Fact]
        //public void ReundToRemote()
        //{
        //    return;
        //    var requireId = Guid.NewGuid().ToString("N");
        //    var orderId = Guid.NewGuid().ToString("N");
        //    var d = new RefundTranscation(orderId, "orderNo");

        //    d.AgentId = "01";
        //    d.TransAmt = 100;
        //    d.ReturnUrl = "http://1237.0.0.1";
        //    d.OrigOrderDate = DateTimeOffset.Now.DateTime;
        //    d.RefundReason = "测试";
        //    d.NotifyUrl = "http://127.0.0.1";

        //    var c = new LoggerFactory().CreateLogger<WangrongService>();
        //    var servie = new WangrongService(c);
        //    var result = servie.Refund(Guid.NewGuid().ToString("N"), "kjkjkjk", 1, DateTimeOffset.Now,
        //       "refund-test",
        //        SettingHelper.Setting());
        //}
    }
}