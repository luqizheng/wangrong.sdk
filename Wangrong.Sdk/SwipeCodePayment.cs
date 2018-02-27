using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    /// <summary>
    /// </summary>
    public abstract class SwipeCodePayment : WangrongPayment
    {
        /// <summary>
        /// </summary>
        /// <param name="requestNo"></param>
        /// <param name="orderNo"></param>
        /// <param name="orderDate"></param>
        /// <param name="autoCode"></param>
        /// <param name="transId"></param>
        /// <param name="productId"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public SwipeCodePayment(string requestNo, string merNo, string orderNo, DateTime orderDate,
            string autoCode, string transId, string productId, int amt) :
            base(requestNo, merNo, transId, productId, amt)
        {
            if (orderDate == DateTime.MinValue)
                throw new ArgumentOutOfRangeException(nameof(orderDate));
            OrderNo = orderNo;
            OrderDate = orderDate;
            AutoCode = autoCode;
        }

        /// <summary>
        /// </summary>
        public string AutoCode { get; set; }

        public string ReturnUrl { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.AutoCode);
            this.AddDict(data, s => s.ReturnUrl);
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            AutoCode = this.GetValueFrom(data, s => s.AutoCode);
            ReturnUrl = this.GetValueFrom(data, s => s.ReturnUrl);
        }
    }
}