using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public class WrWechatAppOrderResult : PaymentResult
    {
        public string formfield { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            formfield = this.GetValueFrom(data, s => s.formfield);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.formfield);
        }
    }
}