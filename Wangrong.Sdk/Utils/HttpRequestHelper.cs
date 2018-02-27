using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Wangrong.Sdk.Utils
{
    public class HttpRequestHelper
    {
        protected readonly ILogger _logger;


        private readonly string _url;

        public HttpRequestHelper(string url, ILogger logger)
        {
            _url = url;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public static string GetSignStr(IDictionary<string, string> dic)
        {
            var keyList = dic.Keys.ToArray();
            Array.Sort(keyList, string.CompareOrdinal);

            var sigSource = new List<string>();
            foreach (var key in keyList)
            {
                var item = dic[key]?.Trim();
                if (!string.IsNullOrEmpty(item))
                    sigSource.Add(key + "=" + item);
            }
            var result = string.Join("&", sigSource);
            return result;
        }

        public static Dictionary<string, string> CoverToResult(string resultMsg)
        {
            var results = resultMsg.Split('&');
            string[] temp;
            var resultItem = new Dictionary<string, string>();
            foreach (var item in results)
            {
                temp = item.Split('=');
                if (temp.Length == 2)
                    resultItem.Add(temp[0], temp[1]);
                else
                    resultItem.Add(temp[0], "");
            }
            return resultItem;
        }

        private void LogPostData(string url, HttpContent content)
        {
            content.ReadAsStreamAsync().ContinueWith(task =>
            {
                using (var stream = new StreamReader(task.Result))
                {
                    var r = stream.ReadToEnd();
                    _logger.LogInformation("post 数据 ：url:{0} data:{1}", url, r);
                }
            });
        }

        public T Post<T>(RequestBase trans, Signature signature)
            where T : ResponseBase, new()
        {
            var postData = trans.ToDictionary();
            postData.Add("signature", signature.PrivateSignData(postData));

            var client = new HttpClient();
            client.Timeout = new TimeSpan(0, 1, 0);
            var contentStr = new FormUrlEncodedContent(postData);

            var result = new T();

            try
            {
                LogPostData(_url, contentStr);
                var content = client.PostAsync(_url, contentStr, CancellationToken.None);
                var resp = content.Result;
                var code = resp.StatusCode;
                var strResult = resp.Content.ReadAsStringAsync().Result;
                _logger.LogInformation("浦发银行返回数据:{0}", strResult);
                if (code == HttpStatusCode.NotFound)
                {
                    _logger.LogError("支付通道出现问题,status-code:{0},content:{1}", strResult, code);
                    //var c = ChannelException.CannotAccessChannel("银行");
                    result.RespCode = "9994";
                    result.RespDesc = "支付通道无法访问";
                    return result;
                }

                if (string.IsNullOrEmpty(strResult))
                {
                    //var c = ChannelException.CannotAccessChannel("银行");
                    _logger.LogError("支付通道出现问题,status-code:{0},content:{1}", strResult, code);
                    result.RespCode = "9997";
                    result.RespDesc = "交易结果未知（应当发起交易查询）";
                    return result;
                }

                result.FillBy(strResult);
                return result;
            }
            catch (AggregateException ex)
            {
                foreach (var inner in ex.InnerExceptions)
                {
                    _logger.LogError(new EventId(9998), "对接万融现异常", inner);
                    var cancelExcepton = inner as TaskCanceledException;
                    if (cancelExcepton != null)
                    {
                        //var c = ChannelException.UnknownStatus(cancelExcepton);
                        result.RespCode = "9997";
                        result.RespDesc = "交易结果未知（应当发起交易查询）";
                        break;
                    }
                    var request = inner as HttpRequestException;
                    if (request != null)
                    {
                        //var c = ChannelException.CannotAccessChannel("银行");
                        result.RespCode = "9994";
                        result.RespDesc = "支付通道无法访问";

                        break;
                    }
                }
            }
            return result;
        }

        private void fillByUnknowState<T>(T response, TaskCanceledException cancelException)
            where T : ResponseBase, new()
        {
            response.RespCode = "9997";
            response.RespDesc = "交易结果未知（应当发起交易查询）";
        }
    }
}