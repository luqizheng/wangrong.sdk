using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    /// <summary>
    ///     微信刷卡支付
    /// </summary>
    public abstract class BaseQuickResponseOrder : WangrongPayment
    {
        protected BaseQuickResponseOrder(string requireNo,
            string merNo, string orderNo, DateTime orderDateTime, int transAmt, string transId, string productId)
            : base(requireNo, merNo, transId, productId, transAmt)
        {
            OrderDate = orderDateTime;
            OrderNo = orderNo;
        }


        public bool SupportCreditCard { get; set; }
        public string ReturnUrl { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            if (data.ContainsKey("limitPay"))
                SupportCreditCard = data["limitPay"] == "no_credit";
            else
                SupportCreditCard = true;

            ReturnUrl = this.GetValueFrom(data, s => s.ReturnUrl);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            if (!SupportCreditCard)
                data.Add("limitPay", "no_credit");

            this.AddDict(data, s => s.ReturnUrl);
        }
    }
}