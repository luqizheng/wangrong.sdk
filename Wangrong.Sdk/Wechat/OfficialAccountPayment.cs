using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk.Wechat
{
    /// <summary>
    /// </summary>
    public class OfficialAccountPayment : WangrongPayment
    {
        /// <summary>
        /// </summary>
        /// <param name="requireNo"></param>
        public OfficialAccountPayment(string requireNo, string merNo,
            string orderNo, DateTime orderDate, int amt) : base(requireNo, merNo, "16", "0112", amt)
        {
            if (orderDate == DateTime.MinValue)
                throw new ArgumentOutOfRangeException(nameof(orderDate));
            OrderNo = orderNo;
            OrderDate = orderDate;
        }

        /// <summary>
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);

            this.AddDict(data, s => s.OpenId);
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillTo(data);

            OpenId = this.GetValueFrom(data, s => s.OpenId);
        }
    }
}