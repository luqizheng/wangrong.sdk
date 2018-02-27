using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    public abstract class RequestBase : TransactionBasicInfo
    {
        protected RequestBase(string requestNo, string merNo, string transId) : base(transId, merNo, requestNo)
        {
        }

        public string AgentId { get; set; }

        public override void FillBy(IDictionary<string, string> data)
        {
            base.FillBy(data);
            AgentId = this.GetValueFrom(data, s => s.AgentId);
        }

        protected override void FillTo(IDictionary<string, string> data)
        {
            this.AddDict(data, s => s.AgentId);
        }
    }
}