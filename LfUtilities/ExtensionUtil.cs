using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;

/// <summary>
/// Extension methods with some of the utility functions (mostly string functions)
/// </summary>
namespace LfUtilities
{
    public static class ExtensionUtil
    {
        public static bool IsEmpty(this String s)
        {
            return Util.IsEmpty(s);
        }

        public static string Reverse(this String s)
        {
            return Util.Reverse(s);
        }

        public static string RtrimSet(this String s, string sr)
        {
            return Util.RtrimSet(s, sr);
        }

        public static string LtrimSet(this String s, string sr)
        {
            return Util.LtrimSet(s, sr);
        }

        public static string AlltrimSet(this String s, string sr)
        {
            return Util.AlltrimSet(s, sr);
        }

        public static string Right(this String s, int n)
        {
            return Util.Right(s, n);
        }

        public static string Left(this String s, int n)
        {
            return Util.Left(s, n);
        }

        public static string Center(this String s, int n)
        {
            return Util.Center(s, n);
        }

        public static string Translate(this String s, string rplset, string trlset)
        {
            return Util.Translate(s, rplset, trlset);
        }

        public static string ReplaceBlock(this string s, int startpos, int endpos, string rpl)
        {
            return Util.ReplaceBlock(s, startpos, endpos, rpl);
        }

        public static string RemoveBlock(this string s, int startpos, int endpos)
        {
            return Util.RemoveBlock(s, startpos, endpos);
        }

        public static string GetBlock(this string s, int startpos, int endpos)
        {
            return Util.GetBlock(s, startpos, endpos);
        }

        public static string RemoveDblspaces(this string s)
        {
            return Util.RemoveDblspaces(s);
        }

        public static string Replace2(this string s, string k, string r)
        {
            return Util.Replace2(s, k, r);
        }

        public static string ReplaceRegExp(this string s, string p, string r, bool ignorecaseflag = false)
        {
            return Util.ReplaceRegExp(s, p, r, ignorecaseflag);
        }

        public static List<string> ReplaceRegExp(this List<string> slist, string p, string r, bool ignorecaseflag = false)
        {
            return Util.ReplaceRegExp(slist, p, r, ignorecaseflag);
        }

        public static bool IsIn<T>(this T s, params T[] plist)
        {
            return Util.IsIn(s, plist);
        }

        public static string GetLine(this string s, int p)
        {
            return Util.GetLine(s, p);
        }

        public static string RemoveLine(this string s, int p)
        {
            return Util.RemoveLine(s, p);
        }

        public static string ReplaceLine(this string s, int p, string rpl)
        {
            return Util.ReplaceLine(s, p, rpl);
        }

        public static string GetRegexFirstMatch(this string txt, string p, bool triminputflag = true)
        {
            return Util.GetRegexFirstMatch(txt, p, triminputflag);
        }


        public static string Compress(this string txt)
        {
            return Util.Compress(txt);
        }

        public static string Decompress(this string txt)
        {
            return Util.Decompress(txt);
        }

        public static bool FileIsOlder(this FileInfo fi, FileInfo ficompare)
        {
            return Util.FileIsOlder(fi, ficompare);
        }

        // Return Dictionary with all public properties of an object
        public static Dictionary<string, object> GetPropertiesDictionary(this object obj)
        {
            var r = new Dictionary<string, object>();
            foreach (PropertyInfo pinf in obj.GetType().GetProperties())
            {
                if (pinf.GetIndexParameters().Length == 0) r.Add(pinf.Name, pinf.GetValue(obj));
                else r.Add(pinf.Name, pinf.PropertyType.Name);
            }
            return r;
        }
    }
}
