using System;

namespace Wangrong.BatchSubMerchant.Filereader
{
    /// <summary>
    ///     ͨ���̳У���չ�����
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class TemplateSetting<TData, T>
        where T : IImportResult
    {
        public Func<TData, T> ConvertFunc { get; set; }
        public IFileReader<TData> FileReader { get; set; }
        public int SkipRows { get; set; }
    }
}