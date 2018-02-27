using System;
using System.Collections.Generic;
using System.Text;

namespace Wangrong.Sdk
{
    public class WrWechatH5OrdereResult : PaymentResult
    {
        public string payInfo { get; set; }

        protected override void FillTo(IDictionary<string, string> data)
        {
            if (string.IsNullOrEmpty(payInfo))
                data["payInfo"] = Base64Encode(payInfo);
            base.FillTo(data);
        }

        public override void FillBy(IDictionary<string, string> data)
        {
            if (data.ContainsKey("payInfo"))
            {
                var dataPayInfo = data["payInfo"];
                if (!string.IsNullOrEmpty(dataPayInfo))
                    payInfo = Base64Decode(dataPayInfo);
            }

            base.FillBy(data);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}