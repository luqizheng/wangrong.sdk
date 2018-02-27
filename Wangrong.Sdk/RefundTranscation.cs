using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     消费交易完成后，可进行退货交易，可退货订单日期根据银行通道一般有3-6个月，具体根据商户协议确定。
    /// </summary>
    public class RefundTranscation : RevertTransaction
    {
        public RefundTranscation(string requestNo, string merNo, string orderNo,
            string oriOrderNo, int amt, DateTime oriOrderDate,
            string refundReason)
            : base(requestNo, merNo, orderNo, oriOrderNo, oriOrderDate, "02", amt)
        {
            if (amt <= 0)
                throw new ArgumentOutOfRangeException(nameof(amt), "amt必须大于0");
            RefundReason = refundReason;
            TransAmt = amt;
        }

        public string RefundReason { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            RefundReason = this.GetValueFrom(data, s => s.RefundReason);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.RefundReason);
        }
    }
}