using System;
using System.Collections.Generic;

namespace Wangrong.BatchSubMerchant.Filereader
{
    public interface IFileReader<TData>
    {
        IEnumerable<T> ReadFrom<T>(string file, int skipRows, int? top,
            Func<TData, T> convertFunc, int sheetIndex = 0)
            where T : IImportResult;
    }
}