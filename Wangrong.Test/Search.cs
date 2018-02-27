using System;
using Microsoft.Extensions.Logging;
using Wangrong.Sdk;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class Search
    {
        [Fact]
        public void Result()
        {
            var r =
                @"endTime=20170927134235&merNo=310440300017201&orderDate=20170927&orderId=100054437752&orderNo=70070002170927134234&origRespCode=P000&origRespDesc=交易处理中&refundAmt=0&requestNo=7007003070070002170927134234&respCode=0000&respDesc=交易成功&transAmt=100&transId=04&version=V1.1&signature=ZROYnGS+3CHNaDdKXprRLGFluEQvRu2Y98lyYjj5d2aqlrpx5EoPomOcKUMZKMIA4bT9SzfEeZJvTKRCUME/mYm/Tj8jPixil/fzcAikVQv+9tA9jGuj6+jh2N4VpSW5tR7qOASlxAiEG2mo3z85NvmDZm8h0VMUCnYYRUQ824kgP/fCdt8Kd+5Lw7tk5zLS/vHBKwKEwhRW5jneGDUD4sIgaxpfX/4WwJYTb9RgdaHDlKBbbVvcJjyft4C1FkubmAkYgyiLyFakaZEwW43g4lF/t5In9ZxE8Ka0kw+olaKYSLsjqM3DKjM67sx9eUoqxabYVVcvRXWuo/r23aEtmw==";
            var result = new OrderSearchResult();
            result.FillBy(r);
            Assert.Equal("20170927134235", result.EndTime.ToString("yyyyMMddHHmmss"));
            Assert.Equal("100054437752", result.OrderId);
            Assert.Equal("20170927", result.OrderDate.ToString("yyyyMMdd"));
            Assert.Equal("70070002170927134234", result.OrderNo);
            Assert.Equal("310440300017201", result.MerNo);
            Assert.Equal("04", result.TransId);
            Assert.Equal(0, result.RefundAmt);
            Assert.Equal("P000", result.OrigRespCode);
            Assert.Equal("7007003070070002170927134234", result.RequestNo);
            Assert.Equal("0000", result.RespCode);
            Assert.Equal("交易处理中", result.OrigRespDesc);
            Assert.Equal("交易成功", result.RespDesc);
            Assert.Equal(100, result.TransAmt);
            Assert.Equal("V1.1", result.Version);
            Assert.NotNull(result.Signature);


            var signature = result.ToDictionary();
            //var actual = SettingHelper.GetSignature(signature);
            Assert.NotEmpty(signature);
        }

        [Fact]
        public void 获取订单()
        {
            return;

            var c = new LoggerFactory().CreateLogger<WangrongService>();
            var setting = SettingHelper.Setting();

            var servie = new WangrongService(c, setting.RequestUrl, setting.SignatureHelper);
            var serchOrder = new OrderSearch(Guid.NewGuid().ToString("N"), setting.MerchantNumber,
                "743a4784bb3c48f3bdd836310330d060",
                DateTime.Now)
            {
                AgentId = null
            };
            var result = servie.Search(serchOrder);
        }
    }
}