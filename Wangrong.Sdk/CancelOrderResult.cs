using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public class CancelOrderResult : ResponseBase
    {
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public string OrigOrderNo { get; set; }
        public DateTime OrigOrderDate { get; set; }
        public string AgentId { get; set; }

        public string OrderNo { get; set; }

        public DateTime OrderDate { get; set; }

        public int TransAmt { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            AgentId = this.GetValueFrom(data, s => s.AgentId);
            NotifyUrl = this.GetValueFrom(data, s => s.NotifyUrl);
            ReturnUrl = this.GetValueFrom(data, s => s.ReturnUrl);
            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            OrigOrderNo = this.GetValueFrom(data, s => s.OrigOrderNo);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);
            TransAmt = this.GetValueFrom(data, s => s.TransAmt);
            OrigOrderDate = this.GetValueFrom(data, s => s.OrigOrderDate);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.AgentId);
            this.AddDict(data, s => s.NotifyUrl);
            this.AddDict(data, s => s.ReturnUrl);
            this.AddDict(data, s => s.OrderNo);
            this.AddDict(data, s => s.OrigOrderNo);
            this.AddDict(data, s => s.OrderDate);
            this.AddDict(data, s => s.TransAmt);
            this.AddDict(data, s => s.OrigOrderDate);
        }
    }
}