using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace vphone.Helper
{
    public class Slug
    {
        public string GetSlug(string str)
        {
            Dictionary<string, string> utf8 = new Dictionary<string, string>
            {
                { "a", "á|à|ả|ã|ạ|ă|ắ|ặ|ằ|ẳ|ẵ|â|ấ|ầ|ẩ|ẫ|ậ|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ặ|Ằ|Ẳ|Ẵ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ" },
                { "d", "đ|Đ" },
                { "e", "é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ" },
                { "i", "í|ì|ỉ|ĩ|ị|Í|Ì|Ỉ|Ĩ|Ị" },
                { "o", "ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ" },
                { "u", "ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự" },
                { "y", "ý|ỳ|ỷ|ỹ|ỵ|Ý|Ỳ|Ỷ|Ỹ|Ỵ" },
            };
            foreach (KeyValuePair<string, string> entry in utf8)
            {
                string ascii = entry.Key;
                string unicode = entry.Value;
                str = Regex.Replace(str, "(" + unicode + ")", ascii, RegexOptions.IgnoreCase);
            }

            str = str.ToLower();
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", "-");
            str = Regex.Replace(str, @"-+", "-");

            return str;
        }
    }
}
