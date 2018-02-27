using System.Collections.Generic;
using Wangrong.Sdk.Utils;

namespace Wangrong.Setting
{
    public static class SettingHelper
    {
        public static Setting Setting()
        {
            return new Setting("wr")
            {
                CerficationPath = @"F:\Ornament\Ornament.PaymentGateway\src\Wangrong.Test\Merchant\310440300035594.pfx",
                Password = "713437212614",
                MerchantNumber = "310440300035594",
                WechatSubMerchant = "38543828",
                AlipaySubMerchant = "2088721636035210",
                //AgentId = "100003",
                MerchantRequestUrl = "http://121.201.32.197:9080/payment-gate-web/merchant/api/addMerchant",
                RequestUrl =
                    "http://spdbweb.chinacardpos.com/payment-gate-web/gateway/api/backTransReq", // "http://121.201.111.67:9080/payment-gate-web/gateway/api/backTransReq",
                NotifyUrl = "http://127.0.0.1/notifyurl",
                ReturnUrl = "http://127.0.0.1/returnUrl"
            };
        }

        public static string GetSignature(IDictionary<string, string> parameters)
        {
            var gatewayProtocol = Setting();
            var signatureHelper = new Signature(gatewayProtocol.CerficationPath, gatewayProtocol.Password);

            var sorList = new List<string>(parameters.Keys);
            sorList.Sort();
            var result = new SortedDictionary<string, string>();
            foreach (var key in sorList)
                result.Add(key, parameters[key]);
            var sign = signatureHelper.PrivateSignData(result);
            return sign;
        }

        public static bool BytesCompare_Step(byte[] b1, byte[] b2)
        {
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (var i = 0; i < b1.Length; ++i)
                if (b1[i] != b2[i]) return false;
            return true;
        }
    }
}