using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public abstract class WangrongPayment : RequestBase
    {
        protected WangrongPayment(string requireNo, string merNo,
            string transId, string productId, int transAmt)
            : base(requireNo, merNo, transId)
        {
            TransAmt = transAmt;
            TransId = transId;
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
        }

        public string ProductId { get; protected set; }

        /// <summary>
        ///     交易金额分
        /// </summary>
        public int TransAmt { get; private set; }

        public string CommodityName { get; set; }


        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string SubMchId { get; set; }
        public string StoreId { get; set; }
        public string ClientIp { get; set; }

        public string NotifyUrl { get; set; }

        protected override void FillTo(IDictionary<string, string> data)
        {
            //if (string.IsNullOrEmpty(ClientIp))
            //    throw new PaymentInputParameterException("ClientIp");

            //if (OrderDate == DateTime.MinValue)
            //    throw OrderException.OrderDateException(); 

            //if (string.IsNullOrEmpty(OrderNo))
            //    throw new OrderException("OrderDate").;
            //if (string.IsNullOrEmpty(CommodityName))
            //    throw OrderException.EmptyCommdityName();


            //if (string.IsNullOrEmpty(NotifyUrl))
            //    throw new PaymentInputParameterException("NotifyUrl");

            //if (TransAmt == 0)
            //    throw new PaymentInputParameterException("TransAmt");

            //if (string.IsNullOrEmpty(ReturnUrl))
            //    throw new PaymentInputParameterException("ReturnUrl");


            this.AddDict(data, s => s.CommodityName);
            this.AddDict(data, s => s.ClientIp);
            this.AddDict(data, s => s.ProductId);
            this.AddDict(data, s => s.NotifyUrl);
            this.AddDict(data, s => s.OrderDate);
            this.AddDict(data, s => s.OrderNo);
            this.AddDict(data, s => StoreId);


            this.AddDict(data, s => s.SubMchId);
            this.AddDict(data, s => s.TransAmt);

            base.FillTo(data);
        }

        public override void FillBy(IDictionary<string, string> data)
        {
            //ProductId = this.GetValueFrom(data, s => s.ProductId);
            ClientIp = this.GetValueFrom(data, s => s.ClientIp);
            CommodityName = this.GetValueFrom(data, s => s.CommodityName);
            NotifyUrl = this.GetValueFrom(data, s => s.NotifyUrl);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);
            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            StoreId = this.GetValueFrom(data, s => s.StoreId);


            SubMchId = this.GetValueFrom(data, s => s.SubMchId);
            TransAmt = Convert.ToInt32(this.GetValueFrom(data, s => s.TransAmt));
        }
    }
}