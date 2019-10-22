using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ShoeShop.Common
{
    public static class MetaTitleCreator
    {
        private static readonly string[] VietnameseChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static string CreateMetaTitle(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietnameseChar.Length; i++)
            {
                for (int j = 0; j < VietnameseChar[i].Length; j++)
                    str = str.Replace(VietnameseChar[i][j], VietnameseChar[0][i - 1]);
            }

            string result = str.ToLower().Replace(' ', '-');

            return result;
        }
    }
}