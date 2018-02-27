using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public class PaymentResult : ResponseBase
    {
        internal PaymentResult()
        {
        }

        public string ProductId { get; set; }
        public string AgentId { get; set; }
        public string SubMchId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderNo { get; set; }
        public string ReturnUrl { get; set; }
        public string NotifyUrl { get; set; }
        public int TransAmt { get; set; }
        public string CommodityName { get; set; }
        public string StoreId { get; set; }
        public string ClientIp { get; set; }


        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);


            AgentId = this.GetValueFrom(data, s => s.AgentId);
            SubMchId = this.GetValueFrom(data, s => s.SubMchId);
            OrderDate = this.GetValueFrom(data, s => s.OrderDate);
            OrderNo = this.GetValueFrom(data, s => s.OrderNo);
            ReturnUrl = this.GetValueFrom(data, s => s.ReturnUrl);
            NotifyUrl = this.GetValueFrom(data, s => s.NotifyUrl);
            TransAmt = this.GetValueFrom(data, s => s.TransAmt);
            CommodityName = this.GetValueFrom(data, s => s.CommodityName);
            StoreId = this.GetValueFrom(data, s => s.StoreId);


            ClientIp = this.GetValueFrom(data, s => s.ClientIp);
            ProductId = this.GetValueFrom(data, s => s.ProductId);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.AgentId);
            this.AddDict(data, s => s.SubMchId);
            this.AddDict(data, s => s.OrderDate);
            this.AddDict(data, s => s.OrderNo);
            this.AddDict(data, s => s.ReturnUrl);
            this.AddDict(data, s => s.NotifyUrl);
            this.AddDict(data, s => s.TransAmt);
            this.AddDict(data, s => s.CommodityName);
            this.AddDict(data, s => s.StoreId);


            this.AddDict(data, s => s.ClientIp);
            this.AddDict(data, s => s.ProductId);
        }
    }
}