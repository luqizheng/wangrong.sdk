using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk.Merchants
{
    /// <summary>
    ///     商户报件
    /// </summary>
    public class SignUpResult : ResponseBase
    {
        public string AlipaySubMchId { get; set; }
        public string WxSubMchId { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            this.GetValueFrom(data, s => AlipaySubMchId);
            this.GetValueFrom(data, s => WxSubMchId);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);
            this.GetValueFrom(data, s => AlipaySubMchId);
            this.GetValueFrom(data, s => WxSubMchId);
        }
    }

    public class SignUp : RequestBase
    {
        public SignUp(string requireNo, string merNo) : base(requireNo, merNo, "25")
        {
        }

        public string ParentSpCode { get; set; }
        public string Name { get; set; }
        public string nameAlias { get; set; }
        public string mccValue { get; set; }
        public string legalPerson { get; set; }
        public string idcardNo { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string cityId { get; set; }
        public string registerAddress { get; set; }
        public string regNo { get; set; }
        public string regMoney { get; set; }
        public string regAddress { get; set; }
        public string spIcp { get; set; }
        public string remarks { get; set; }
        public string cardNoCipher { get; set; }
        public string cardName { get; set; }
        public string cardBankNo { get; set; }
        public DateTime expireTime { get; set; }
        public bool isCompay { get; set; }
        public string business { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);


            this.GetValueFrom(data, s => AgentId);
            this.GetValueFrom(data, s => ParentSpCode);
            this.GetValueFrom(data, s => Name);
            this.GetValueFrom(data, s => nameAlias);
            this.GetValueFrom(data, s => mccValue);
            this.GetValueFrom(data, s => legalPerson);
            this.GetValueFrom(data, s => idcardNo);
            this.GetValueFrom(data, s => mobile);
            this.GetValueFrom(data, s => email);
            this.GetValueFrom(data, s => cityId);
            this.GetValueFrom(data, s => registerAddress);
            this.GetValueFrom(data, s => regNo);
            this.GetValueFrom(data, s => regMoney);
            this.GetValueFrom(data, s => regAddress);
            this.GetValueFrom(data, s => spIcp);
            this.GetValueFrom(data, s => remarks);
            this.GetValueFrom(data, s => cardNoCipher);
            this.GetValueFrom(data, s => cardName);
            this.GetValueFrom(data, s => cardBankNo);
            this.GetValueFrom(data, s => expireTime);
            this.GetValueFrom(data, s => isCompay);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            base.FillTo(data);

            this.AddDict(data, s => s.ParentSpCode);
            this.AddDict(data, s => s.Name);
            this.AddDict(data, s => s.nameAlias);
            this.AddDict(data, s => s.mccValue);
            this.AddDict(data, s => s.legalPerson);
            this.AddDict(data, s => s.idcardNo);
            this.AddDict(data, s => s.mobile);
            this.AddDict(data, s => s.email);
            this.AddDict(data, s => s.cityId);
            this.AddDict(data, s => s.registerAddress);
            this.AddDict(data, s => s.regNo);
            this.AddDict(data, s => s.regMoney);
            this.AddDict(data, s => s.regAddress);
            this.AddDict(data, s => s.spIcp);
            this.AddDict(data, s => s.remarks);
            this.AddDict(data, s => s.cardNoCipher);
            this.AddDict(data, s => s.cardName);
            this.AddDict(data, s => s.cardBankNo);
            this.AddDict(data, s => s.expireTime, "yyyy-MM-dd");
            this.AddDict(data, s => s.isCompay);
            this.AddDict(data, s => s.business);
        }
    }
}