using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public class OrderSearch : RequestBase
    {
        public OrderSearch(string requireNo, string orderNo, string merNo, DateTime orderDateTime)
            : base(requireNo, merNo, "04")

        {
            if (orderDateTime == DateTime.MinValue)
                throw new ArgumentOutOfRangeException(nameof(orderDateTime));
            OrderNo = orderNo ?? throw new ArgumentNullException(nameof(orderNo));
            OrderDate = orderDateTime;
        }


        public string OrderNo { get; private set; }

        public DateTime OrderDate { get; private set; }


        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.OrderNo);
            this.AddDict(data, s => s.OrderDate);
        }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillTo(data);
            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);
        }
    }
}