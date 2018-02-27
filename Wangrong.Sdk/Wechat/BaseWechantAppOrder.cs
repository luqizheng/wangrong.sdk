using System;
using System.Collections.Generic;

namespace Wangrong.Sdk.Wechat
{
    public abstract class BaseWechantAppOrder : WangrongPayment
    {
        public BaseWechantAppOrder(string requireNo, string merNo, string transId, string productId, string orderNo,
            DateTime orderDate, int transAmt)
            : base(requireNo, merNo, transId, productId, transAmt)
        {
            if (orderDate == DateTime.MinValue)
                throw new ArgumentOutOfRangeException(nameof(orderDate));
            OrderNo = orderNo;
            OrderDate = orderDate;
        }

        public bool SupportCreditCard { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            if (data.ContainsKey("limitPay"))
                SupportCreditCard = data["limitPay"] == "no_credit";
            else
                SupportCreditCard = true;
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            if (!SupportCreditCard)
                data.Add("limitPay", "no_credit");
        }
    }
}