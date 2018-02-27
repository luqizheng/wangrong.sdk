using System;

namespace Wangrong.Sdk
{
    public class WangrongResponseException : Exception
    {
        public WangrongResponseException(string code, string desc)
        {
            Code = code;
            Desc = desc;
        }

        public string Code { get; }
        public string Desc { get; }

        public override string Message => Desc + "(code=" + Code + ")";
    }
}