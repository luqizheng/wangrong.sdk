using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public class OrderSearchResult : ResponseBase
    {
        public string OrderId { get; set; }

        /// <summary>
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        ///     上游返回的时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     上游(微信或支付宝)流水号
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        ///     原交易应答码
        /// </summary>
        public string OrigRespCode { get; set; }

        /// <summary>
        ///     原交易应答码描述
        /// </summary>
        public string OrigRespDesc { get; set; }

        /// <summary>
        ///     交易金额，单位分
        /// </summary>
        public int TransAmt { get; set; }

        /// <summary>
        ///     已退金额,单位分
        /// </summary>
        public int RefundAmt { get; set; }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.OrderNo);
            this.AddDict(data, s => s.OrderDate);

            this.AddDict(data, s => s.OrigRespCode);
            this.AddDict(data, s => s.OrigRespDesc);

            this.AddDict(data, s => s.TransAmt);
            this.AddDict(data, s => s.RefundAmt);

            this.AddDict(data, s => s.EndTime, "yyyyMMddHHmmss");
            this.AddDict(data, s => s.TransactionId);
            this.AddDict(data, s => s.OrderId);
        }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);


            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            OrderId = this.GetValueFrom(data, s => s.OrderId);
            RefundAmt = this.GetValueFrom(data, s => s.RefundAmt);
            TransAmt = this.GetValueFrom(data, s => s.TransAmt);
            OrigRespCode = this.GetValueFrom(data, s => s.OrigRespCode);
            OrigRespDesc = this.GetValueFrom(data, s => s.OrigRespDesc);
            TransactionId = this.GetValueFrom(data, s => s.TransactionId);
            EndTime = this.GetValueFrom(data, s => s.EndTime, "yyyyMMddHHmmss");
        }
    }
}