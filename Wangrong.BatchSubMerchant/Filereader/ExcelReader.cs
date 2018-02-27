using System;
using System.Collections.Generic;
using System.IO;
using Npoi.Core.HSSF.UserModel;
using Npoi.Core.POIFS.FileSystem;
using Npoi.Core.SS.UserModel;
using Npoi.Core.XSSF.UserModel;

namespace Wangrong.BatchSubMerchant.Filereader
{
    public class ExcelReader : IFileReader<string[]>
    {
        private readonly bool _forExcel2003;

        public ExcelReader(bool forExcel2003)
        {
            _forExcel2003 = forExcel2003;
        }

        public IEnumerable<T> ReadFrom<T>(string file, int skipRows, int? top, Func<string[], T> converTo,
            int sheetIndex = 0)
            where T : IImportResult
        {
            var workbook = GetWorkbook(file);

            var list = new List<T>();
            var sheet = workbook.GetSheetAt(sheetIndex);


            for (var i = 0; i <= sheet.LastRowNum; i++)
            {
                if (i < skipRows)
                    continue;
                var row = sheet.GetRow(i);
                if (IsEmptyRow(row))
                    break;
                var strValues = ToStringValues(row.Cells);
                list.Add(converTo(strValues));
            }
            return list;
        }

        private IWorkbook GetWorkbook(string file)
        {
            var fileStream = File.OpenRead(file);
            var workbook = _forExcel2003
                ? new HSSFWorkbook(new POIFSFileSystem(fileStream))
                : (IWorkbook) new XSSFWorkbook(fileStream);
            return workbook;
        }

        private bool IsEmptyRow(IRow row)
        {
            if (row == null)
                return true;
            var isEmpty = true;
            for (var i = 0; i < 6; i++)
            {
                var cell = row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                isEmpty = isEmpty && string.IsNullOrEmpty(GetCellValue(cell));
            }

            return isEmpty;
        }

        private string[] ToStringValues(IList<ICell> cells)
        {
            var result = new string[cells.Count];
            for (var i = 0; i < cells.Count; i++)
                result[i] = GetCellValue(cells[i]);
            return result;
        }

        private string GetCellValue(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();

                default:
                    return "";
            }
        }
    }
}