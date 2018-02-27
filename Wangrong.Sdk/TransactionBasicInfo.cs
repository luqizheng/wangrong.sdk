using System;
using System.Collections.Generic;
using Ornament.PaymentGateway.Helper;

namespace Wangrong.Sdk
{
    /// <summary>
    /// </summary>
    public abstract class TransactionBasicInfo
    {
        protected TransactionBasicInfo()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="transId"></param>
        protected TransactionBasicInfo(string transId, string merNo, string requrestNo)
        {
            if (string.IsNullOrEmpty(transId))
                throw new ArgumentNullException(nameof(transId));
            if (string.IsNullOrEmpty(merNo))
                throw new ArgumentNullException(nameof(merNo));
            if (string.IsNullOrEmpty(requrestNo))
                throw new ArgumentNullException(nameof(requrestNo));
            if (requrestNo.Length > 32)
                throw new ArgumentOutOfRangeException(nameof(requrestNo), "requestNo 不能大于32位");
            //MerchantId = merNo;
            Version = "V1.1";
            TransId = transId;
            MerNo = merNo;
            RequestNo = requrestNo;
        }


        //public string RespCode { get; protected set; }
        //public string RespDesc { get; set; }
        /// <summary>
        /// </summary>
        public string RequestNo { get; protected internal set; }

        /// <summary>
        /// </summary>
        public string Version { get; protected set; }

        /// <summary>
        /// </summary>
        public string MerNo { get; internal set; }

        /// <summary>
        /// </summary>
        public string TransId { get; protected set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, string> ToDictionary()
        {
            var dic = new Dictionary<string, string>();

            this.AddDict(dic, s => s.MerNo);


            this.AddDict(dic, s => s.TransId);
            this.AddDict(dic, s => s.Version);

            this.AddDict(dic, s => s.RequestNo);
            FillTo(dic);

            return dic;
        }


        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        protected abstract void FillTo(IDictionary<string, string> data);

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public void FillBy(string data)
        {
            var dict = new SortedDictionary<string, string>();
            var ary = data.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var keyPareStr in ary)
            {
                var keyPareAry = keyPareStr.Split(new[] {'='}, 2);
                dict.Add(keyPareAry[0], keyPareAry[1]);
            }
            FillBy(dict);
        }

        /// <summary>
        /// </summary>
        /// <param name="data"></param>
        public virtual void FillBy(IDictionary<string, string> data)
        {
            //var resCode = data["respCode"];
            //var resDesc = data["respDesc"];
            ////if (resCode != "0000" && resCode != "P000")
            ////    throw new WangrongResponseException(resCode, resDesc);
            //RespCode = resCode;
            //RespDesc = resDesc;

            TransId = this.GetValueFrom(data, s => s.TransId);

            MerNo = this.GetValueFrom(data, s => s.MerNo);

            RequestNo = this.GetValueFrom(data, s => s.RequestNo);

            Version = this.GetValueFrom(data, s => s.Version);
        }
    }
}