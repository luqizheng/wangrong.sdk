using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public abstract class ResponseBase : TransactionBasicInfo
    {
        protected ResponseBase(string requestNo, string transId, string merNo) : base(transId, merNo, requestNo)
        {
        }

        protected ResponseBase()
        {
        }

        public string RespCode { get; set; }

        public string RespDesc { get; set; }

        public string Signature { get; set; }

        protected override void FillTo(IDictionary<string, string> data)
        {
            this.AddDict(data, s => s.RespCode);
            this.AddDict(data, s => s.RespDesc);
        }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            RespCode = this.GetValueFrom(data, s => s.RespCode);
            RespDesc = this.GetValueFrom(data, s => s.RespDesc);
            Signature = this.GetValueFrom(data, s => s.Signature);
        }
    }
}