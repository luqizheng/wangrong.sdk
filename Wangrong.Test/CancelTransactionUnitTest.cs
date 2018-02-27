using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Wangrong.Sdk;
using Wangrong.Sdk.Utils;
using Wangrong.Setting;
using Xunit;

namespace Wangrong
{
    public class CancelTransactionUnitTest
    {
        private string GetSignature(IDictionary<string, string> parameters)
        {
            var _gatewayProtocol = SettingHelper.Setting();
            var _signatureHelper = new Signature(_gatewayProtocol.CerficationPath, _gatewayProtocol.Password);

            var sorList = new List<string>(parameters.Keys);
            var result = new SortedDictionary<string, string>();
            foreach (var key in sorList)
                result.Add(key, parameters[key]);
            var sign = _signatureHelper.PrivateSignData(result);
            return sign;
        }

        private bool BytesCompare_Step(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (var i = 0; i < b1.Length; ++i)
                if (b1[i] != b2[i]) return false;
            return true;
        }

        [Fact]
        public void Cancel()
        {
            //var orderId = "";
            //Assert.NotNull(orderId);//输入撤销的订单订单好
            //var requiestNo = Guid.NewGuid().ToString("n");
            //var amt = 1;
            //var service = new WangrongService(SettingHelper.Setting());
            //var request = service.Cancel(requiestNo, Guid.NewGuid().ToString("N"), orderId, amt);
        }

        [Fact]
        public void FillBy()
        {
            var r =
                @"merNo=310440300001134&notifyUrl=http://127.0.0.1/notifyurl&orderDate=20170412&orderNo=90c160db55324b5a88ae94fdc47a6f39&origOrderNo=41d08a7161534c7894930240d877368a&requestNo=5871ae3955ab49e3a7f3580834e92ae4&respCode=0000&respDesc=交易成功&transAmt=100&transId=03&version=V1.1&signature=LEvsirTKNFbGy9uiqsL8/Mjn4L0HKxTEcA27YmKc01pu/7i6bwh2Y6YdYth6Yf1eZwH989PvmAWAmoM983z4UO/a5S9tGUKQth6XCieLRZ7W3Kdlh7Q7j13EKt2/9d0PhN9cUMz6bd/ZoQ7TrjucrMVofI8XLlNd6P6eHH66TKCxPcBkr98RNcHw1i/olXb9nvget84Dvg1uneyufbKcaiLpnLldbmdZAa7jVPk+b+PNk1mShesMtvdjBN4Zj6WVF/HZdu8MprnT+nAiSDhnO+NSfijqg0kfspbOZrkmZeAEZRwJESHdri6i9fGUekC6NVZ7QJcNfc7mQFO2F8FmLg==";
            var reesult = new CancelOrderResult();
            reesult.FillBy(r);

            Assert.Equal("310440300001134", reesult.MerNo);

            Assert.Equal("http://127.0.0.1/notifyurl", reesult.NotifyUrl);

            Assert.Equal("20170412", reesult.OrderDate.ToString("yyyyMMdd"));

            Assert.Equal("90c160db55324b5a88ae94fdc47a6f39", reesult.OrderNo);

            Assert.Equal("41d08a7161534c7894930240d877368a", reesult.OrigOrderNo);

            Assert.Equal("03", reesult.TransId);

            Assert.Equal("5871ae3955ab49e3a7f3580834e92ae4", reesult.RequestNo);

            Assert.Equal("交易成功", reesult.RespDesc);

            Assert.Equal("0000", reesult.RespCode);

            Assert.Equal(100, reesult.TransAmt);

            Assert.Equal("V1.1", reesult.Version);

            Assert.NotNull(reesult.Signature);
        }

        [Fact]
        public void RequestDataTest()
        {
            var requestNo = Guid.NewGuid().ToString("N");
            var setting = SettingHelper.Setting();
            var orderNo = Guid.NewGuid().ToString("N");
            var cal = new WrCancelOrder(requestNo, orderNo, setting.MerchantNumber, "originOrderNo", 1, DateTime.Now)
            {
                AgentId = "agentId",
                ReturnUrl = "http://127.0.0.1/ReturnUrl",
                NotifyUrl = "http://127.0.0.1/NotifyUrl"
            };

            var dict = cal.ToDictionary();
            Assert.Equal(12, dict.Count);
            Assert.Equal("agentId", cal.AgentId);
            Assert.Equal(1, cal.TransAmt);
            Assert.Equal("http://127.0.0.1/ReturnUrl", dict["returnUrl"]);
            Assert.Equal("http://127.0.0.1/NotifyUrl", dict["notifyUrl"]);
        }

        [Fact]
        public void TestCallbakc()
        {
            var setting = SettingHelper.Setting();
            var c = new LoggerFactory().CreateLogger<WangrongService>();
            var ata =
                "respCode=FAIL&orderNo=389ade6343794285b2ea528a5d496bf6&transId=17&orderDate=20170721&respDesc=%E4%BA%8C%E7%BB%B4%E7%A0%81%E5%B7%B2%E8%BF%87%E6%9C%9F%EF%BC%8C%E8%AF%B7%E5%88%B7%E6%96%B0%E5%86%8D%E8%AF%95&transAmt=1&signature=UjkrhTnnEX8ht59WchTeGwRsiETS7V%2BV%2FsIgbV7G%2BniRksFDglK8gH0DRr%2BL6Xo%2BPORMIDK7iTYV9ksj5D50gMJR5AL7poIa8IJtk7CvmdbE1Lzn2fPgvrY4EgERQoSmvesgoQNtO0picSQf5bzt%2FUQbK9gb5wGBjHmL279SRRXA1YdnYK6tZ0SAp%2Fa3efKctM4gidGt14Ki6%2BXj64FAPF7IaCnWTY7leYF4isaXxRD2DZzztikYO3plwJ%2BdTseMZ38dmFyXJAADgcIOB7YdCWTFmcrPruNW0EP67GNWo%2FtANEF%2FAVW%2Fzse9UKefylpE6Lj%2BG0dQdAmd9VOn%2FCCj8w%3D%3D&orderId=200000371269&merNo=800440054111002&productId=0113";
            var s = new WangrongService(c, setting.RequestUrl, setting.SignatureHelper);
            s.Callback(ata);
        }
    }
}