using ClosedXML.Excel;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProv_Parconsil.Models.Result
{
    public class Helpers
    {
        public static string Esquema = "seg";

        public static int Increment(int i)
        {
            i = i + 1;
            return i;
        }

        public static string CStr(object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch
            {
                return "";
            }
        }

        public static double CDbl(object obj)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static Int32 CInt(object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }

        public static void Cell(IXLCell cell, object value, string comment = "")
        {
            cell.Value = value;
            if (!comment.Equals(""))
                cell.Comment.AddText(comment);
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            cell.Style.Font.FontSize = 12;
            cell.Style.Font.Bold = true;
        }

        public static IXLRange CellRange(IXLRange range, object value, bool isCombine = false)
        {
            if (isCombine)
            {
                range.Merge();
            }

            range.Value = value;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Font.FontSize = 12;
            range.Style.Font.Bold = true;

            return range;
        }

        public static IXLRange CellRangefont(IXLRange range, object value, int tamaño, bool isCombine = false)
        {
            if (isCombine)
            {
                range.Merge();
            }

            range.Value = value;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            range.Style.Font.FontSize = tamaño;
            range.Style.Font.Bold = true;

            return range;
        }


        public static void WriteLog(string path, string methodName, string message)
        {
            path = Path.Combine(path, "Log");
            if (!Directory.Exists(Path.Combine(path, "Log")))
                Directory.CreateDirectory(path);

            string logFilePath = Path.Combine(path, string.Format("Log_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            if (!File.Exists(logFilePath))
                File.Create(logFilePath).Close();

            using (StreamWriter streamWriter = new StreamWriter(logFilePath, true))
            {
                streamWriter.WriteLine(DateTime.Now + " >> [" + methodName + "]: " + message);
                streamWriter.Close();
            }
        }

        public static bool? SafeGetBoolean(SqlDataReader reader, int columnIndex)
        {
            if (!reader.IsDBNull(columnIndex))
                return reader.GetBoolean(columnIndex);

            return null;
        }

    }

}
