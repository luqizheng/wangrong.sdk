using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk.Merchants
{
    /// <summary>
    /// </summary>
    public class SignInResult : ResponseBase
    {
        public string PayWay { get; set; }

        public string SubMchId { get; set; }
        public string SubMechantName { get; set; }
        public string Business { get; set; }
        public string SubMerchantShortname { get; set; }
        public string Contact { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string MerchantRemark { get; set; }
        public string ServicePhone { get; set; }

        public string contactInfo { get; set; }
        public string addressInfo { get; set; }
        public string bankCardInfo { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            PayWay = this.GetValueFrom(data, s => s.PayWay);


            SubMechantName = this.GetValueFrom(data, s => s.SubMechantName);
            Business = this.GetValueFrom(data, s => s.Business);
            SubMerchantShortname = this.GetValueFrom(data, s => s.SubMerchantShortname);
            Contact = this.GetValueFrom(data, s => s.Contact);
            ContactPhone = this.GetValueFrom(data, s => s.ContactPhone);
            ContactEmail = this.GetValueFrom(data, s => s.ContactEmail);
            MerchantRemark = this.GetValueFrom(data, s => s.MerchantRemark);
            ServicePhone = this.GetValueFrom(data, s => s.ServicePhone);
            SubMchId = this.GetValueFrom(data, s => s.SubMchId);
            contactInfo = this.GetValueFrom(data, s => s.contactInfo);
            addressInfo = this.GetValueFrom(data, s => s.addressInfo);
            bankCardInfo = this.GetValueFrom(data, s => s.bankCardInfo);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.AddDict(data, s => s.PayWay);

            this.AddDict(data, s => s.SubMechantName);
            this.AddDict(data, s => s.Business);
            this.AddDict(data, s => s.SubMerchantShortname);
            this.AddDict(data, s => s.Contact);
            this.AddDict(data, s => s.ContactPhone);
            this.AddDict(data, s => s.ContactEmail);
            this.AddDict(data, s => s.MerchantRemark);
            this.AddDict(data, s => s.ServicePhone);
            this.AddDict(data, s => s.SubMchId);
            this.AddDict(data, s => s.contactInfo);

            this.AddDict(data, s => s.addressInfo);
            this.AddDict(data, s => s.bankCardInfo);
        }
    }
}