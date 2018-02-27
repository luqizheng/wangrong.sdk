using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk.Callback
{
    public class CallbackData : ResponseBase
    {
        public CallbackData()
        {
        }

        public CallbackData(string requestNo, string merNo, string transId) : base(requestNo, transId, merNo)
        {
        }

        public string AgentId { get; set; }
        public string OrderNo { get; set; }
        public int TransAmt { get; set; }
        public DateTime OrderDate { get; set; }
        public string TransactionId { get; set; }
        public string BankType { get; set; }
        public DateTime TimeEnd { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            AgentId = this.GetValueFrom(data, s => s.AgentId);
            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            TransAmt = this.GetValueFrom(data, s => s.TransAmt);
            BankType = this.GetValueFrom(data, s => s.BankType);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);
            TransactionId = this.GetValueFrom(data, s => s.TransactionId);
            TimeEnd = this.GetValueFrom(data, s => s.TimeEnd, "yyyyMMddHHmmss");
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.AgentId);
            this.AddDict(data, s => s.TransAmt);
            this.AddDict(data, s => s.OrderDate);
            this.AddDict(data, s => s.OrderNo);

            this.AddDict(data, s => s.TransactionId);
            this.AddDict(data, s => s.BankType);
        }
    }
}