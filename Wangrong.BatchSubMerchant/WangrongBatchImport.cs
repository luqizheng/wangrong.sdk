using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Npoi.Core.SS.UserModel;
using Wangrong.BatchSubMerchant.Filereader;
using Wangrong.BatchSubMerchant.FileWriter;
using Wangrong.Sdk;

namespace Wangrong.BatchSubMerchant
{
    public class WangrongBatchImport
    {
        private readonly IEnumerable<Func<MerchantImportFile, string>> getValue = new Func<MerchantImportFile, string>[]
        {
            s => s.Name,
            s => s.MerchantNo,
            s => s.PasswordCerf,
            s => s.ContractEmail,
            s => s.ContractPhone,
            s => s.ProvinceCode,
            s => s.CityCode,
            s => s.DistrictCode,
            s => s.ContractAddress,
            s => s.ContractName,
            s => s.IdCardNo,
            s => s.AlipayBusiness,
            s => s.WechatBusiness,
            s => s.OurMerchantNo,
            s => s.AlipaySubMerchant,
            s => s.WechatSubMerchant,
            s => s.AlipayErrorMessage,
            s => s.WechatErrorMessage
        };

        private readonly IList<string> Titles = new List<string>
        {
            "公司名称",
            "浦发商户号",
            "证书密码",
            "邮件",
            "电话",
            "省编码",
            "城市编码",
            "区域编码 ",
            "地址",
            "联络人姓名",
            "身份证号",
            "支付宝经营号 ",
            "微信经营范围",
            "万国商户号",
            "AliPay商户号",
            "微信商户号",
            "Alipay结果",
            "微信申请结果"
        };

        public void BuildSql(string file, string sql)
        {
            var setting = new SubMerchantImportSetting();
            var reader = new ExcelReader(false);
            var result = reader.ReadFrom(file, setting.SkipRows, null, setting.ConvertFunc, 0);

            var sb = new StringBuilder();
            foreach (var item in result)
                sb.Append(item.BuildOurSqlInsert());
            using (var writer = new StreamWriter(File.OpenWrite(sql)))
            {
                writer.Write(sb.ToString());
            }
        }

        public IEnumerable<MerchantImportFile> Import(string file, bool alipay = true, bool wechat = true)
        {
            var setting = new SubMerchantImportSetting();
            var c = new LoggerFactory().CreateLogger<WangrongService>();
            var reader = new ExcelReader(false);
            var result = reader.ReadFrom(file, setting.SkipRows, null, setting.ConvertFunc, 0);
            foreach (var item in result)
                item.Invoke(new LoggerFactory(), alipay, wechat);
            return result;
        }

        public void Write(IEnumerable<MerchantImportFile> merchant, string file)
        {
            Action<IRow, int, MerchantImportFile> setting = (row, index, imortData) =>
            {
                var cellIndex = 0;
                foreach (var getValFunc in getValue)
                {
                    var cell = row.CreateCell(cellIndex);

                    AddCell(cell, imortData, getValFunc);

                    cellIndex++;
                }
            };

            Action<IRow> func = row =>
            {
                var cellIndex = 0;
                foreach (var title in Titles)
                {
                    var cell = row.CreateCell(cellIndex);

                    cell.SetCellValue(title);

                    cellIndex++;
                }
            };

            var writer = new ExcelFileWriter<MerchantImportFile>(false);
            writer.Write(file, func, setting, merchant);
        }

        private void AddCell(ICell cell, MerchantImportFile data,
            Func<MerchantImportFile, string> getValue)
        {
            var val = getValue(data);
            cell.SetCellValue(val);
        }
    }
}