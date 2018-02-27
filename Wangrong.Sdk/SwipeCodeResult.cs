using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     刷卡支付，扫码
    /// </summary>
    public class SwipeCodeResult : PaymentResult
    {
        public string AutoCode { get; set; }


        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            AutoCode = this.GetValueFrom(data, f => f.AutoCode);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, f => f.AutoCode);
        }
    }
}