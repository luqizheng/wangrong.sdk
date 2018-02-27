using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk.Wechat
{
    public class QuickResponseOrderResult : PaymentResult
    {
        /// <summaryCodeUrl
        /// </summary>
        public string CodeUrl { get; set; }

        /// <summary>
        /// </summary>
        public string ImgUrl { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            CodeUrl = this.GetValueFrom(data, s => s.CodeUrl);
            ImgUrl = this.GetValueFrom(data, s => s.ImgUrl);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.CodeUrl);
            this.AddDict(data, s => s.ImgUrl);
        }
    }
}