using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     撤销订单
    /// </summary>
    public abstract class RevertTransaction : RequestBase
    {
        protected RevertTransaction(string requestNo,
            string merNo, string orderNo, string oriOrderNo, DateTime origOrderDate, string transId, int amt)
            : base(requestNo, merNo, transId)
        {
            if (amt <= 0)
                throw new ArgumentOutOfRangeException(nameof(amt), "amt必须大于0");
            TransAmt = amt;

            OrigOrderNo = oriOrderNo ?? throw new ArgumentNullException(nameof(oriOrderNo));
            OrderDate = DateTime.Today;
            OrigOrderDate = origOrderDate;
            OrderNo = orderNo;
        }


        /// <summary>
        ///     撤销订单号
        /// </summary>
        public string OrderNo { get; private set; }

        /// <summary>
        ///     商户订单号
        /// </summary>
        public string OrigOrderNo { get; private set; }

        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public DateTime OrigOrderDate { get; private set; }
        public int TransAmt { get; internal set; }
        public DateTime OrderDate { get; private set; }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.OrderNo);
            this.AddDict(data, s => s.OrigOrderNo);
            this.AddDict(data, s => s.NotifyUrl);
            this.AddDict(data, s => s.ReturnUrl);
            this.AddDict(data, s => s.TransAmt);

            this.AddDict(data, s => s.OrderDate);
            this.AddDict(data, s => s.OrigOrderDate);
        }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            TransAmt = this.GetValueFrom(data, s => s.TransAmt);
            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            ReturnUrl = this.GetValueFrom(data, s => s.ReturnUrl);
            NotifyUrl = this.GetValueFrom(data, s => s.NotifyUrl);


            OrderDate = this.GetValueFrom(data, s => s.OrderDate);
            OrigOrderNo = this.GetValueFrom(data, s => OrigOrderNo);

            OrigOrderDate = this.GetValueFrom(data, s => s.OrigOrderDate);
        }
    }
}