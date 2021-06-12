using System;
using System.IO;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using CsvHelper;
using System.Globalization;
using System.Data;
using System.Threading;
using System.Management;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.IO.Compression;
using CsvHelper.Configuration;

namespace LfUtilities
{
    public static class Util
    {
        private static List<string> defaulDateTimeFormats;
        private static Regex rgxNumber = new Regex(@"^\+?-?\d+(?:[\.,]\d+)?$");
        private static Regex rgxDate = new Regex(@"^[\d\.,\:/\-\s]+$");
        private static Regex rgxInt = new Regex(@"^\+?-?\d+$");

        private static string lastValidDateString = "";
        private static string lastInvalidDateString = "";

        private static string lastValidIntString = "";
        private static string lastInvalidIntString = "";

        private static string lastValidNumString = "";
        private static string lastInvalidNumString = "";

        static Util()
        {
            defaulDateTimeFormats = new List<string>() {
           "yyyy-MM-dd-HH.mm.ss.ffffff"
          ,"yyyy-MM-dd-HH.mm.ss.fffff"
          ,"yyyy-MM-dd-HH.mm.ss.ffff"
          ,"yyyy-MM-dd-HH.mm.ss.fff"
          ,"yyyy-MM-dd-HH.mm.ss.ff"
          ,"yyyy-MM-dd'T'HH:mm:ss.SSS'Z'"
          ,"yyyy-MM-dd'T'HH.mm.ss"
          ,"yyyy-MM-dd-HH.mm.ss"
          ,"yyyy-MM-dd'T'HH.mm"
          ,"yyyy-MM-dd-HH.mm"
          ,"yyyy-MM-dd'T'HH"
          ,"yyyy-MM-dd-HH"
          ,"yyyy-MM-dd"
          ,"yyyy-MM"
          ,"yyyy"
          ,"dd.MM.yyyy"
          ,"yyyy.MM.dd"
          ,"dd/MM/yyyy"
        };
        }

        private static string[] GetDefaulDateTimeFormats()
        {
            return defaulDateTimeFormats.ToArray();
        }

        private static void addDefaulDateTimeFormats(string f)
        {
            defaulDateTimeFormats.Add(f);
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        //  return string for LF
        public static string Eol()
        {
            return "\n";
        }

        //  return string for CR
        public static string Cr()
        {
            return "\r";
        }

        //  return string for CRLF
        public static string CrLf()
        {
            return "\r\n";
        }

        //  return string for TAB
        public static string Tab()
        {
            return "\t";
        }

        //  removes characters in sr on right side of s
        public static string RtrimSet(string s, string sr)
        {
            if ((s == null))
            {
                return null;
            }

            return s.TrimEnd(sr.ToCharArray());
        }

        //  removes characters in sr on left side of s
        public static string LtrimSet(string s, string sr)
        {
            if ((s == null))
            {
                return null;
            }

            return s.TrimStart(sr.ToCharArray());
        }


        // Removes characters in sr on both side of s.
        public static string AlltrimSet(string s, string sr)
        {
            if ((s == null))
            {
                return null;
            }

            return LtrimSet(RtrimSet(s, sr), sr);
        }


        public static string Space(int n)
        {
            return new String(' ', n);
        }

        public static string Right(string s, int n)
        {
            if ((s.Length - n) > 0) return s.Substring(s.Length - n);
            else return s;
        }

        public static string Left(string s, int n)
        {
            if (s.Length > n) return s.Substring(0, n);
            else return s;
        }

        // centers s with length n
        public static string Center(string s, int n)
        {
            string hs, r;
            int pad, hpad;

            if (!(s == null))
            {
                hs = s.Trim();
                pad = (n - hs.Length);
                hpad = pad / 2;
                if ((pad > 0))
                {
                    r = Left(Space(hpad) + hs + Space(pad - hpad), n);
                }
                else
                {
                    r = Left(hs, n);
                }
            }
            else
            {
                r = null;
            }
            return r;
        }

        public static bool IsNumeric(this object obj)
        {
            if (obj == null) return false;

            switch (obj)
            {
                case sbyte _: return true;
                case byte _: return true;
                case short _: return true;
                case ushort _: return true;
                case int _: return true;
                case uint _: return true;
                case long _: return true;
                case ulong _: return true;
                case float _: return true;
                case double _: return true;
                case decimal _: return true;
            }

            string s = Convert.ToString(obj, CultureInfo.InvariantCulture);

            return double.TryParse(s, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out double _);
        }

        public static bool IsInteger(this object obj)
        {
            if (obj == null) return false;

            switch (obj)
            {
                case sbyte _: return true;
                case byte _: return true;
                case short _: return true;
                case ushort _: return true;
                case int _: return true;
                case uint _: return true;
                case long _: return true;
                case ulong _: return true;
                case float _: return false;
                case double _: return false;
                case decimal _: return false;
            }

            string s = Convert.ToString(obj, CultureInfo.InvariantCulture);

            return long.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out long _);
        }

        public static bool IsDBNull(object value)
        {
            if (value == System.DBNull.Value) return true;
            IConvertible convertible = value as IConvertible;
            return convertible != null ? convertible.GetTypeCode() == TypeCode.DBNull : false;
        }

        public static bool IsEmpty(object s)
        {
            var r = false;
            if (s == null)
            {
                r = true;
            }
            else if ((s.GetType() == typeof(string)))
            {
                if (s == null) { r = true; }
                else
                {
                    var ws = s.ToString().Trim();
                    if (ws.Length == 0) r = true;
                }

            }
            else if ((s == null) || IsDBNull(s))
            {
                r = true;
            }
            return r;
        }

        //  Returns True if a string holds a valid numeric value
        public static bool CheckNumeric(string text)
        {

            bool r = false;

            if (IsEmpty(text)) return false;

            if (text.Equals(lastValidNumString)) return true;
            if (text.Equals(lastInvalidNumString)) return false;

            if (rgxNumber.IsMatch(text))
            {
                double w;
                r = double.TryParse(text, out w);
            }
            else
            {
                r = false;
            }

            if (r) lastValidNumString = text;
            else lastInvalidNumString = text;
            return r;
        }

        public static bool CheckInt(string text)
        {

            bool r = false;

            if (text.Equals(lastValidIntString)) return true;
            if (text.Equals(lastInvalidIntString)) return false;

            if (rgxInt.IsMatch(text))
            {
                long w;
                r = long.TryParse(text, out w);
            }
            else
            {
                r = false;
            }
            if (r) lastValidIntString = text;
            else lastInvalidIntString = text;
            return r;
        }

        public static bool CheckDate(string text)
        {
            if (text.Equals(lastValidDateString)) return true;
            if (text.Equals(lastInvalidDateString)) return false;

            if (rgxDate.IsMatch(text))
            {
                DateTime w;
                try
                {
                    w = ConvertoDateTime(text);
                    lastValidDateString = text;
                    return true;
                }
                catch
                {
                    lastInvalidDateString = text;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // count the number of sub-strings
        public static int Numinstr(string source, string search, int start = 0, int end = -1)
        {
            return InstrCount(source.Substring(0, end + 1), search, start, end);
        }

        // count the number of sub-strings
        public static int InstrCount(string source, string search, int start = 0, int end = -1)
        {
            int i, r;
            if ((IsEmpty(source) || IsEmpty(search)))
            {
                r = 0;
            }
            else
            {
                if (end > start)
                {
                    return InstrCount(source.Substring(0, end + 1), search, start);
                }
                if (source.Contains(search))
                {
                    r = 0;
                    i = source.IndexOf(search, start);
                    while (i >= 0)
                    {
                        r++;
                        i = source.IndexOf(search, i + 1);
                    }
                }
                else
                {
                    r = 0;
                }
            }

            return r;
        }
        public static string Translate(string s, string rplset, string trlset)
        {

            if (IsEmpty(s)) return s;
            if (IsEmpty(rplset)) return s;

            var rs =
              from i in Enumerable.Range(0, s.Length)
              let c = s[i]
              let cridx = rplset.IndexOf(c)
              let cnew = (cridx >= 0) ? ((trlset.Length > cridx) ? trlset[cridx] : (char)0) : c
              where cnew > (char)0
              select cnew;

            return new String(rs.ToArray()).ToString();
        }

        public static string Remove(string s, int startpos, int n, bool keeplenflag = false)
        {
            var r = "";
            int lenorig = s.Length;
            string hs = s;
            if (IsEmpty(s))
            {
                r = s;
            }
            else
            {
                hs = Left(hs, startpos) + hs.Substring(startpos + n);
                if (keeplenflag) hs = hs + Space(lenorig - hs.Length);
                r = hs;
            }
            return r;
        }

        public static string Ins(string s, string insert, int startpos)
        {
            var r = "";
            if (IsEmpty(s))
            {
                r = insert;
            }
            else
            {
                if (IsEmpty(insert))
                {
                    r = insert;
                }
                else
                {
                    r = Left(s, startpos + 1) + insert + s.Substring(startpos + 1);
                }
            }
            return r;
        }

        public static string Repeat(string s, int n)
        {
            var r = new StringBuilder();
            if (IsEmpty(s))
            {
                return s;
            }
            for (int i = 0; i < n; i++)
            {
                r.Append(s);
            }
            return r.ToString();
        }

        public static string Lpad(string s, int l, char ps = ' ')
        {
            if (Util.IsEmpty(s))
            {
                s = "";
            }
            return s.PadLeft(l, ps);
        }

        public static string Rpad(string s, int l, char ps = ' ')
        {
            if (Util.IsEmpty(s))
            {
                s = "";
            }
            return s.PadRight(l, ps);
        }

        public static string Fill(string s, int l)
        {
            if (Util.IsEmpty(s))
            {
                s = "";
            }
            return s.PadRight(l);
        }

        public static string ReplaceBlock(string s, int startpos, int endpos, string rpl)
        {
            var r = Left(s, startpos) + rpl + s.Substring(endpos + 1);
            return r;
        }

        public static string RemoveBlock(string s, int startpos, int endpos)
        {
            var r = Left(s, startpos) + s.Substring(endpos + 1);
            return r;
        }


        public static string GetBlock(string s, int startpos, int endpos)
        {
            return GetSubString(s, startpos, endpos);
        }

        public static string GetSubString(string s, int startpos, int endpos = -1)
        {
            var r = "";
            if (startpos < 0)
            {
                r = "";
            }
            else if (endpos < 0)
            {
                r = s.Substring(startpos);
            }
            else if (endpos < startpos)
            {
                r = "";
            }
            else
            {
                r = s.Substring(startpos, endpos - startpos + 1);
            }
            return r;
        }

        public static bool IsEmptyArray<T>(T[] a)
        {
            if (a == null) return true;
            if (a.Length == 0) return true;
            return false;
        }

        public static string RemoveDblspaces(string s)
        {
            string r = "";
            if (IsEmpty(s)) return s;
            var p = " +";
            r = Regex.Replace(s, p, " ", RegexOptions.IgnoreCase);
            return r;
        }

        public static string CompressString(string s)
        {
            string r = "";
            if (IsEmpty(s)) return s;
            var p = @"\s+";
            r = Regex.Replace(s, p, " ", RegexOptions.IgnoreCase);
            return r;
        }

        public static string RemoveEmptyLines(string s)
        {
            var r = Regex.Replace(s, @"^\s*$\r\n", "", RegexOptions.Multiline).TrimEnd();
            r = Regex.Replace(r, @"^\s*$\n", "", RegexOptions.Multiline).TrimEnd();
            return r;
        }

        public static bool IsLike(this string toSearch, string toFind, RegexOptions opt)
        {
            var p = WildcardToRegex(toFind);
            p = "^" + p + "$";
            return new Regex(p, RegexOptions.Singleline | RegexOptions.Multiline | opt).IsMatch(toSearch);
        }

        public static bool IsLike(this string toSearch, string toFind)
        {
            var p = WildcardToRegex(toFind);
            p = "^" + p + "$";
            return new Regex(p, RegexOptions.Singleline | RegexOptions.Multiline).IsMatch(toSearch);
        }

        //  Returns cmdKey and cmdValue of Cmd
        public static void GetCmdInfo(string cmd, ref string cmdkey, ref string cmdvalue)
        {
            GetCmdInfo(cmd, "=", ref cmdkey, ref cmdvalue);
        }

        public static void GetCmdInfo(string cmd, string cmddelim, ref string cmdkey, ref string cmdvalue)
        {
            int pos;
            cmdkey = "";
            cmdvalue = "";
            if (IsEmpty(cmd))
            {
                return;
            }

            pos = cmd.IndexOf(cmddelim);
            if ((pos >= 0))
            {
                cmdkey = cmd.Substring(0, pos).Trim();
                cmdvalue = cmd.Substring(pos + 1).Trim();
            }
        }

        public static string GetCmdKey(string cmd, string cmddelim = "=")
        {
            string cmdkey = "", cmdvalue = "";
            GetCmdInfo(cmd, cmddelim, ref cmdkey, ref cmdvalue);
            if (IsEmpty(cmdkey)) cmdkey = RtrimSet(cmd, cmddelim + " ");
            return cmdkey;
        }

        public static string GetCmdValue(string cmd, string cmddelim = "=")
        {
            string cmdkey = "", cmdvalue = "";
            GetCmdInfo(cmd, cmddelim, ref cmdkey, ref cmdvalue);
            return cmdvalue;
        }


        public static string GetCmdOption(string cmd, string cmddelim = "=")
        {
            return GetCmdValue(cmd, cmddelim);
        }

        //  Returns true if all characters of string are upper case, else false
        public static bool AllUpperCase(string stringToCheck)
        {
            return (String.Compare(stringToCheck, stringToCheck.ToUpper()) == 0);
        }

        //  Returns true if all characters of string are lower case, else false
        public static bool AllLowerCase(string stringToCheck)
        {
            return (String.Compare(stringToCheck, stringToCheck.ToLower()) == 0);
        }

        //  Returns true if all characters of string are upper case, else false
        public static bool FirstUpperCase(string stringToCheck)
        {
            return AllUpperCase(Left(stringToCheck.Trim(), 1));
        }

        //  Returns true if all characters of string are lower case, else false
        public static bool FirstLowerCase(string stringToCheck)
        {
            return AllLowerCase(Left(stringToCheck.Trim(), 1));
        }

        //  Verzeichnis von aktueller Application ermitteln
        public static string GetApplicationFolderName()
        {
            string fnm = "";
            try
            {
                fnm = Assembly.GetEntryAssembly()?.Location;
            }
            catch (Exception ex)
            {
            }


            if (IsEmpty(fnm))
            {
                fnm = Assembly.GetExecutingAssembly().Location;
            }

            if (IsEmpty(fnm))
            {
                return null;
            }
            else
            {
                var fi = new FileInfo(fnm);
                return fi.DirectoryName;
            }
        }

        //  <summary>
        //  Function matches with regular expression
        //  </summary>
        //  <param name="s">input string</param>
        //  <param name="p">regexp pattern</param>
        //  <param name="ignorecaseflag">true if case should be ignored, else false</param>
        //  <param name="multilineflag">^$ matches also end of line</param>
        //  <param name="singlelineflag">dot matches also end of line </param>
        //  <returns>array of matches, first element = matched String of first iteration</returns>
        public static string[] Matches(string s, string p, bool ignorecaseflag = true,
                                       bool multilineflag = true, bool singlelineflag = false)
        {
            int i;
            int k;
            int l;
            Regex r;
            if ((ignorecaseflag
                        && (multilineflag && singlelineflag)))
            {
                r = new Regex(p, (RegexOptions.IgnoreCase
                                | (RegexOptions.Multiline | RegexOptions.Singleline)));
            }
            else if ((ignorecaseflag && multilineflag))
            {
                r = new Regex(p, (RegexOptions.IgnoreCase | RegexOptions.Multiline));
            }
            else if ((ignorecaseflag && singlelineflag))
            {
                r = new Regex(p, (RegexOptions.IgnoreCase | RegexOptions.Singleline));
            }
            else if ((multilineflag && singlelineflag))
            {
                r = new Regex(p, (RegexOptions.Multiline | RegexOptions.Singleline));
            }
            else if (multilineflag)
            {
                r = new Regex(p, RegexOptions.Multiline);
            }
            else if (singlelineflag)
            {
                r = new Regex(p, RegexOptions.Singleline);
            }
            else if (ignorecaseflag)
            {
                r = new Regex(p, RegexOptions.IgnoreCase);
            }
            else
            {
                r = new Regex(p);
            }
            MatchCollection ms = r.Matches(s);
            Match m;
            List<string> ret = new List<string>();
            Group g;
            i = 0;
            if ((ms.Count > 0))
            {
                for (k = 0; (k <= (ms.Count - 1)); k++)
                {
                    m = ms[k];
                    if (m.Success)
                    {
                        if (k == 0)
                        {
                            ret.Add(m.Groups[0].Value);
                            i++;
                        }
                        for (l = 1; (l <= (m.Groups.Count - 1)); l++)
                        {
                            g = m.Groups[l];
                            ret.Add(g.Value);
                            if ((ret[i] != ret[(i - 1)]))
                            {
                                i++;
                            }
                        }
                    }
                }

                if ((i > 0))
                {
                    return ret.ToArray();
                }
                else
                {
                    return null;
                }

            }

            return null;
        }

        //  <summary>
        //  Function replace with regular expression
        //  </summary>
        //  <param name="s">input string</param>
        //  <param name="p">regexp pattern</param>
        //  <param name="r">replacement string</param>
        //  <param name="ignorecaseflag">true if case should be ignored, else false</param>
        //  <returns>string with p replaced by r</returns>
        public static string ReplaceRegExp(string s, string p, string r, bool ignorecaseflag = false)
        {
            if (IsEmpty(s) && IsEmpty(p)) return s;

            if (ignorecaseflag)
            {
                return Regex.Replace(s, p, r, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            }
            else
            {
                return Regex.Replace(s, p, r, RegexOptions.Multiline);
            }
        }

        // apply regex to list
        public static List<string> ReplaceRegExp(List<string> slist, string p, string r, bool ignorecaseflag = false)
        {
            for (int i = 0; i < slist.Count; i++) slist[i] = ReplaceRegExp(slist[i], p, r, ignorecaseflag);
            return slist;
        }

        // apply regex to list
        public static string[] ReplaceRegExp(string[] slist, string p, string r, bool ignorecaseflag = false)
        {
            for (int i = 0; i < slist.Length; i++) slist[i] = ReplaceRegExp(slist[i], p, r, ignorecaseflag);
            return slist;
        }

        public static int MatchPosition(string s, string p, bool ignorecaseflag = false, bool multilineflag = false, bool singlelineflag = false)
        {
            int r = -1;
            Regex rx = MatchRegex(p, ignorecaseflag, multilineflag, singlelineflag);
            Match m = rx.Match(s);
            if (m != null)
            {
                if (m.Success)
                {
                    return m.Index;
                }
            }
            return r;
        }

        public static Regex MatchRegex(string p, bool ignorecaseflag = false,
                         bool multilineflag = true, bool singlelineflag = false)
        {
            Regex r = null;
            if ((ignorecaseflag
                        && (multilineflag && singlelineflag)))
            {
                r = new Regex(p, (RegexOptions.IgnoreCase
                                | (RegexOptions.Multiline | RegexOptions.Singleline)));
            }
            else if ((ignorecaseflag && multilineflag))
            {
                r = new Regex(p, (RegexOptions.IgnoreCase | RegexOptions.Multiline));
            }
            else if ((ignorecaseflag && singlelineflag))
            {
                r = new Regex(p, (RegexOptions.IgnoreCase | RegexOptions.Singleline));
            }
            else if ((multilineflag && singlelineflag))
            {
                r = new Regex(p, (RegexOptions.Multiline | RegexOptions.Singleline));
            }
            else if (multilineflag)
            {
                r = new Regex(p, RegexOptions.Multiline);
            }
            else if (singlelineflag)
            {
                r = new Regex(p, RegexOptions.Singleline);
            }
            else if (ignorecaseflag)
            {
                r = new Regex(p, RegexOptions.IgnoreCase);
            }
            else
            {
                r = new Regex(p);
            }
            return r;
        }

        // '' <summary>
        // '' Function match with regular expression
        // '' </summary>
        // '' <param name="s">input string</param>
        // '' <param name="p">regexp pattern</param>
        // '' <param name="ignorecaseflag">true if case should be ignored, else false</param>
        // '' <param name="multilineflag">^$ matches also end of line not only end of string</param>
        // '' <param name="singlelineflag">dot matches also end of line</param>
        // '' <returns>true if match</returns>

        public static bool Match(string s, string p, bool ignorecaseflag = false,
                                 bool multilineflag = true, bool singlelineflag = false)
        {
            Regex r = MatchRegex(p, ignorecaseflag, multilineflag, singlelineflag);
            return r.IsMatch(s);
        }

        // <summary>
        // Converts pattern with wildcards (*, ?) to a valid regular expression
        // </summary>
        public static string WildcardToRegex(string pattern)
        {
            return Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".");
        }

        public static string GetUniqueFilename(string prf, string tp)
        {
            string ufn;
            object tsd = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            ufn = (prf
                        + (tsd + ("." + tp)));
            return ufn;
        }


        public static bool IsValidFileName(string filename)
        {
            var w = CleanPathName(filename);
            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(w);
            }
            catch (ArgumentException) { }
            catch (System.IO.PathTooLongException) { }
            catch (NotSupportedException) { }
            if (ReferenceEquals(fi, null))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string Filename(string p1, string p2 = "")
        {
            var r = CleanPathName(p1, p2);
            if (!IsValidFileName(r))
            {
                if (File.Exists(r))
                {
                    return new FileInfo(r).FullName;
                }
            }

            if (File.Exists(p2))
            {
                return new FileInfo(p2).FullName;
            }

            if (File.Exists(p1))
            {
                return new FileInfo(p1).FullName;
            }

            if (IsEmpty(p1))
            {
                r = CleanPathName(p2);
            }
            else if (IsEmpty(p2))
            {
                r = CleanPathName(p1);
            }
            else
            {
                r = CleanPathName(p1, p2);
                if (!IsValidFileName(r))
                {
                    r = CleanPathName(p2);
                    if (!IsValidFileName(r))
                    {
                        r = CleanPathName(p1);
                    }
                }
            }
            return r;
        }

        public static bool IsUncPath(String path)
        {
            string root = Path.GetPathRoot(path);

            // Check if roor starts with \\, clearly an UNC
            if (root.StartsWith(@"\\")) return true;

            // Check if drive is a network drive
            try
            {
                DriveInfo drive = new DriveInfo(root);
                if (drive.DriveType == DriveType.Network) return true;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static string CleanPathName(string p1, string p2 = "", string p3 = "", string p4 = "")
        {

            if (p1 == null) p1 = "";
            if (p2 == null) p2 = "";
            if (p3 == null) p3 = "";
            if (p4 == null) p4 = "";

            p1 = cleanText(RtrimSet(p1, "\\/ \r\n\t"));
            StringBuilder r = new StringBuilder(p1);

            string cleanText(String t)
            {
                t = t.Replace(@"/", @"\");
                t = t.Replace(@"/\", @"\");
                t = t.Replace(@"\/", @"\");
                return t;
            }

            void append(String p)
            {
                if (!IsEmpty(p))
                {
                    p = AlltrimSet(p, "\\/ \r\n\t");
                    p = cleanText(p);
                    if (!IsEmpty(p)) r.Append(@"\").Append(p);
                }
            }

            append(p2);
            append(p3);
            append(p4);

            return r.ToString();
        }

        public static string ChFileType(string f, string ty)
        {
            return Util.RtrimSet(Path.ChangeExtension(f, ty), ".");
        }

        // <summary>
        // Dateibezeichnung ff wird in Pfad, Filename und Filetyp aufgespalten (funktioniert auf fuer Namen zu nicht existierenden Dateien).
        // </summary>
        // <param name="ff">Dateibezeichnung, die aufgespalten werden soll (z.B. C:\QB45\QB.EXE)</param>
        // <param name="pfad">Pfad (mit Drive, z.B. C:\QB45)</param>
        // <param name="fina">Dateiname (z.B. QB)</param>
        // <param name="art">Dateityp (z.B. EXE)</param>
        // <remarks></remarks>
        public static void SplitFilePath(string ff, ref string pfad, ref string fina, ref string art)
        {
            art = LtrimSet(Path.GetExtension(ff), ". ");
            pfad = Path.GetDirectoryName(ff);
            fina = Path.GetFileNameWithoutExtension(ff);
        }

        public static MemoryStream GetFileMemoryStream(string p)
        {
            System.IO.Stream st = System.IO.File.Open(p, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            System.IO.MemoryStream ms = null;
            try
            {
                byte[] b = new byte[st.Length - 1];
                st.Read(b, 0, b.Length);
                ms = new System.IO.MemoryStream(b);
                st.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return ms;
        }

        public static List<T> GetGenericList<T>(List<T> l)
        {
            return l;
        }

        // <summary>
        // Returns list with duplicates removed (List or Array of Objects)
        // </summary>
        // <param name="l">input list</param>
        // <param name="ignorecase">true if case should be ignored (only for strings)</param>
        // <param name="trimFlag">true if list elements should be trimmed (only for strings)</param>
        // <returns>unique list</returns>
        public static List<T> GetUniqueList<T>(List<T> l, bool ignorecase = false, bool trimFlag = false)
        {
            List<T> r = new List<T>();

            if (typeof(T) == typeof(string))
            {
                // Keep option flags if T is string
            }
            else if (typeof(T) == typeof(object) && (l.All(x => x is string)))
            {
                // Keep option flags if T is object and all objects are string
            }
            else
            {
                ignorecase = trimFlag = false; // not string: set all options to false
            }


            if (ignorecase || trimFlag)
            {
                var w =
                    from x in l
                    let x1 = (trimFlag) ? x.ToString().Trim() : x.ToString()
                    select x1;

                if (ignorecase) w = w.Distinct(StringComparer.CurrentCultureIgnoreCase);

                r = ((IEnumerable<T>)w).ToList();

            }
            else
            {
                r = l.Distinct().ToList();
            }

            return r;
        }

        public static byte[] GetnBytesFromFile(BinaryReader br, long start, long length, bool reverseFlag = false)
        {
            long wstart;
            long lBytes = br.BaseStream.Length;
            if (reverseFlag)
            {
                //  At end of file
                wstart = (lBytes - (length - start));
            }
            else
            {
                wstart = start;
            }

            if ((wstart < 0))
            {
                wstart = 0;
            }

            if ((lBytes <= (wstart + length)))
            {
                length = (lBytes - wstart);
            }

            br.BaseStream.Position = wstart;
            byte[] fileData = br.ReadBytes((int)length);
            return fileData;
        }

        public static byte[] GetnBytesFromFile(Stream sr, long start, long length, bool reverseFlag = false)
        {
            BinaryReader oBinaryReader = new BinaryReader(sr);
            byte[] fileData = GetnBytesFromFile(oBinaryReader, start, length, reverseFlag);
            oBinaryReader.Close();
            return fileData;
        }

        public static bool ByteArraysAreEqual(byte[] b1, byte[] b2)
        {
            long i = 0;
            if ((b1.Length != b2.Length))
            {
                return false;
            }

            for (i = 0; (i <= (b1.Length - 1)); i++)
            {
                if ((b1[i] != b2[i]))
                {
                    return false;
                }

            }

            return true;
        }

        public static void Swap<T>(ref T s1, ref T s2)
        {
            T w;
            w = s1;
            s1 = s2;
            s2 = w;
        }

        public static bool IsIn<T>(T s, params T[] plist)
        {
            var r = false;
            if (IsEmpty(s)) return r;
            for (int i = 0; i < plist.Length; i++)
            {
                if (s.ToString() == plist[i].ToString())
                {
                    r = true;
                }
            }
            return r;
        }



        //  Gibt Zeile an Position p zurueck
        public static string GetLine(string s, int p)
        {
            int vPosBOL;
            int vPosEOL;
            string vSource;
            var r = "";
            if (p >= s.Length) p = s.Length - 1;
            if ((Util.IsEmpty(s) || (p < 0)))
            {
                p = -1;
                return r;
            }

            vSource = s;
            vPosBOL = GetLineStartPosition(vSource, p);
            vPosEOL = GetLineEndPosition(vSource, p);
            return Util.RtrimSet(Util.GetSubString(vSource, vPosBOL, vPosEOL), Util.CrLf());
        }

        //  Entfernt Zeile in s, zu der Position p gehoert
        public static string RemoveLine(string s, int p)
        {
            int vPosBOL;
            int vPosEOL;
            string vSource;
            var r = s;
            if ((Util.IsEmpty(s) || (p < 0)))
            {
                return r;
            }
            vSource = s;
            vPosBOL = GetLineStartPosition(vSource, p);
            vPosEOL = GetLineEndPosition(vSource, p);
            r = Util.RemoveBlock(s, vPosBOL, vPosEOL);
            return r;
        }

        public static string ReplaceLine(string s, int p, string rpl)
        {
            int vPosBOL;
            int vPosEOL;
            string vSource;
            var r = s;
            if ((Util.IsEmpty(s) || (p < 0)))
            {
                return r;
            }
            vSource = s;
            vPosBOL = GetLineStartPosition(vSource, p);
            vPosEOL = GetLineEndPosition(vSource, p);
            if (vSource.Contains(Util.CrLf()))
            {
                rpl = rpl + Util.CrLf();
            }
            else
            {
                rpl = rpl + Util.Eol();
            }
            vSource = Util.ReplaceBlock(s, vPosBOL, vPosEOL, rpl);
            vSource = Util.RtrimSet(vSource, Util.CrLf());
            return vSource;
        }

        //  Returns position of last character of line (including CR and LF)
        public static int GetLineEndPosition(string s, int p)
        {
            int vposlf;
            int vposcr;
            int vpos = 0;

            if (p >= s.Length)
            {
                p = s.Length - 1;
            }

            vposcr = s.IndexOf('\r', p);
            vposlf = s.IndexOf('\n', p);
            if (vposlf < 0 && vposcr < 0)
            {
                vpos = s.Length - 1;
            }
            else if (vposlf < 0)
            {
                vpos = vposcr;
            }
            else if (vposcr < 0)
            {
                vpos = vposlf;
            }
            else if (vposcr > vposlf)
            {
                vpos = vposlf;
            }
            else if (vposlf >= 0 || vposcr >= 0)
            {
                vpos = Math.Max(vposlf, vposcr);
            }


            return vpos;
        }


        //  Gibt Position von Zeilanfang in s fuer Zeile an Position p zurueck
        //  Erste Position = 1
        public static int GetLineStartPosition(string s, int p)
        {
            int vposlf;
            int vpos = 0;

            if (p >= s.Length)
            {
                p = s.Length - 1;
            }

            if (p == 0)
            {
                vpos = p;
            }
            else
            {
                vposlf = s.LastIndexOf('\n', p);
                if (vposlf == p)
                {
                    vposlf = s.LastIndexOf('\n', p - 1);
                }
                if (vposlf < 0)
                {
                    vpos = 0;
                }
                else
                {
                    vpos = vposlf + 1;
                }
            }

            return vpos;
        }

        public static List<string> GetListOfLinesFromFile(string file, bool removeEmptyLineFlag = true)
        {
            if (file.IsEmpty()) return new List<string>();
            string[] s = File.ReadAllLines(file);
            List<string> r;
            if (removeEmptyLineFlag)
            {
                r = s.Where(x => !LfUtilities.Util.IsEmpty(x)).ToList();
            }
            else
            {
                r = s.ToList();
            }
            return r;
        }

        /// <summary>
        /// Joins list of lines to text
        /// </summary>
        /// <param name="ll">line list</param>
        /// <param name="le">line separator</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string JoinLineList(string[] ll, string le = "")
        {
            if (IsEmpty(le))
                le = Eol();
            return String.Join(le, ll);
        }

        public static string[] GetLineList(string pSource)
        {
            return GetListOfLines(pSource, true).ToArray();
        }

        public static List<string> GetListOfLines(List<string> ll, int startidx, int endidx)
        {
            List<string> r = new List<string>();
            if (endidx < 0 || endidx >= ll.Count) endidx = ll.Count - 1;
            if (startidx > endidx) startidx = endidx;
            for (int i = startidx; i <= endidx; i++) r.Add(ll[i]);
            return r;
        }

        public static List<String> GetListOfLines(string pSource, int startidx, int endidx = -1, bool removeEmptyLineFlag = true)
        {
            var wSrc = "";
            var wstartidx = GetLineStartPosition(pSource, startidx);
            if (endidx < startidx || endidx >= pSource.Length - 1)
            {
                wSrc = pSource.Substring(wstartidx);
            }
            else
            {
                var wendidx = GetLineEndPosition(pSource, endidx);
                wSrc = GetBlock(pSource, wstartidx, wendidx);
            }
            return GetListOfLines(wSrc, removeEmptyLineFlag);
        }

        public static List<string> GetListOfLines(string pSource, bool removeEmptyLineFlag = true)
        {
            List<string> ll;
            string[] wll;
            string crlf = Util.CrLf();
            string[] lf = new string[] { Util.Eol() };
            pSource = Util.AlltrimSet(pSource, crlf);
            if (pSource.Contains(crlf[0]))
            {
                pSource = pSource.Replace(crlf, lf[0]);
                pSource = pSource.Replace(Util.Cr(), lf[0]);
            }

            if (removeEmptyLineFlag)
            {
                wll = pSource.Split(lf, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                wll = pSource.Split(lf, StringSplitOptions.None);
            }

            ll = new List<string>(wll);
            return ll;
        }

        public static String GetTextFromListOfLines(List<string> ll, int startidx, int endidx, string dl = "")
        {
            if (ll.Count == 0) return "";
            StringBuilder r = new StringBuilder(10000);
            if (endidx < 0 || endidx > ll.Count - 1) endidx = ll.Count - 1;
            if (startidx > endidx) startidx = endidx;
            bool first = true;
            for (int i = startidx; i < endidx; i++)
            {
                if (first) r.Append(ll[i]);
                else r.Append(dl).Append(ll[i]);
            }

            return r.ToString();
        }

        public static bool IsDir(string wdir)
        {
            try
            {
                return !Path.HasExtension(wdir);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsFile(string wdir)
        {
            try
            {
                return Path.HasExtension(wdir);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsFullPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || path.IndexOfAny(Path.GetInvalidPathChars()) != -1 || !Path.IsPathRooted(path))
                return false;

            string pathRoot = Path.GetPathRoot(path);
            if (pathRoot.Length <= 2 && pathRoot != "/") // Accepts X:\ and \\UNC\PATH, rejects empty string, \ and X:, but accepts / to support Linux
                return false;

            if (pathRoot[0] != '\\' || pathRoot[1] != '\\')
                return true; // Rooted and not a UNC path

            return pathRoot.Trim('\\').IndexOf('\\') != -1; // A UNC server name without a share name (e.g "\\NAME" or "\\NAME\") is invalid
        }

        public static bool FileExistsFlag(string fpath)
        {
            return File.Exists(fpath);
        }

        //  returns true if file exists
        public static short FileExists(string fpath)
        {
            long lSize;
            short r = -1;
            lSize = -1;
            FileInfo fFile;
            try
            {
                fFile = new FileInfo(fpath);
                if (!fFile.Exists)
                {
                    r = -1;
                }
                else
                {
                    lSize = fFile.Length;
                    if ((lSize == 0))
                    {
                        //  File is zero bytes and exists
                        r = 0;
                    }
                    else if ((lSize > 0))
                    {
                        //  File is not zero bytes and exists
                        r = 1;
                    }

                }
            }
            catch (Exception)
            {
                r = -1;
            }
            return r;
        }

        //  returns true if file exists
        public static short FileExists(string wdir, string fName)
        {
            string fp;
            fp = Filename(wdir, fName);
            return FileExists(fp);
        }

        // <summary>
        // Trennt String in Pfadnamen und Dateinamen auf
        // </summary>
        // <param name="s">Vollstaendiger Dateiname</param>
        // <param name="pfad">Pfadangabe (Leerstring wenn keine Angabe m�glich)</param>
        // <param name="f">Filename</param>
        // <remarks></remarks>
        public static void PathName(string s, ref string pfad, ref string f)
        {
            pfad = Path.GetDirectoryName(s);
            f = Path.GetFileName(s);
        }

        public static void DisposeIfNotNull(IDisposable dispobj)
        {
            if (dispobj != null)
            {
                dispobj.Dispose();
            }

        }

        /*
        enhanced version of join:
        dl: del (,)
        sdl: string delimiter (")
         opt: U= Ucase , T = Trim, L= Lcase, R= Remove Delim, S= LTrim, E= RTrim, X: trim sdelim
        */
        public static string JoinE(string[] fields, string dl = ",", string sdl = "", string opt = "")
        {
            string line = "", w = "";
            fields = Cleantokenlist(fields, dl, sdl, opt);

            if (!Util.IsEmpty(sdl))
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    w = fields[i];
                    if (w.Contains(dl) || w.StartsWith(" ") || w.EndsWith(" "))
                    {
                        w = sdl + w + sdl;
                        fields[i] = w;
                    }
                }
            }
            line = String.Join(dl, fields);
            return line;
        }

        public static string cleanWhiteSpace(string txt)
        {
            var r = Regex.Replace(txt, @"\s+", " ");
            r = AlltrimSet(r, CrLf());
            return r.Trim();
        }

        public static string CleanWord(string w)
        {
            return AlltrimSet(w, " " + CrLf() + ".,;:\t");
        }

        public static string CleanToken(string tk, string dl = " ", string sdl = "", string opt = "")
        {
            string[] tkl = { tk };
            string[] r = Cleantokenlist(tkl, dl, sdl, opt);
            if (r.Length > 0) return r[0];
            else return null;
        }

        /*
        clean token list
        dl: del (,)
        opt: U= Ucase , T = RTrim, L= Lcase, R= Remove Delim, S= LTrim, X: trim sdelim
        */
        private static string[] Cleantokenlist(string[] tkl, string dl, string sdl = "", string opt = "")
        {
            string[] rtkl = tkl;
            string xs = "";

            bool trimflag = false;
            bool ucaseflag = false;
            bool ltrimflag = false;
            bool rtrimflag = false;
            bool lcaseflag = false;
            bool rdflag = false;
            bool tsdflag = false;
            if (!Util.IsEmpty(opt))
            {
                opt = opt.ToUpper();
                trimflag = ((opt.IndexOf("T") + 1) > 0);
                ltrimflag = ((opt.IndexOf("S") + 1) > 0);
                rtrimflag = ((opt.IndexOf("E") + 1) > 0);
                ucaseflag = ((opt.IndexOf("U") + 1) > 0);
                lcaseflag = ((opt.IndexOf("L") + 1) > 0);
                tsdflag = ((opt.IndexOf("X") + 1) > 0);
                rdflag = ((opt.IndexOf("R") + 1) > 0);
            }

            if (tsdflag)
            {
                xs = sdl + " ";
            }
            else
            {
                xs = " ";
            }

            var tlist = xs.ToCharArray();


            for (long i = 0; (i <= (rtkl.Length - 1)); i++)
            {

                if (trimflag)
                {
                    rtkl[i] = rtkl[i].Trim(tlist);
                }

                if (rtrimflag)
                {
                    rtkl[i] = rtkl[i].TrimEnd(tlist);
                }

                if (ltrimflag)
                {
                    rtkl[i] = rtkl[i].TrimStart(tlist);
                }

                if (ucaseflag)
                {
                    rtkl[i] = rtkl[i].ToUpper();
                }

                if (lcaseflag)
                {
                    rtkl[i] = rtkl[i].ToLower();
                }

                if (rdflag)
                {
                    rtkl[i] = rtkl[i].Replace(dl, "");
                }
            }

            return rtkl;
        }

        /*
        enhanced version of split:
        s: string to split
        nel: max no of elements
        dl: del (,)
        sdl: string delimiter (")
        offsetel: the first offsetel are not included in result
        offsetpos: parsing starts at position offsetpos of s
        opt: U= Ucase , T = Rtrim, L= Lcase, S= LTrim
        */
        public static string[] SplitE(string s, ref int nel, string dl = ",", string sdl = "", int offsetel = 0, int offsetpos = 0, string opt = "")
        {
            string[] wtl = null;
            string[] tl = null;
            string token = "";
            var hs = s;
            int ntoken, sp;

            if (IsEmpty(s)) { nel = 0; return tl; }

            if (offsetpos > 0)
            {
                hs = hs.Substring(offsetpos);
            }

            if (dl == " ")
            {
                hs = Util.RemoveDblspaces(hs.Trim());

            }

            if (sdl == "")
            {
                if (nel < 0)
                {
                    wtl = hs.Split(dl.ToCharArray());
                }
                else
                {
                    wtl = hs.Split(dl.ToCharArray(), nel);
                }
            }
            else
            {
                ntoken = 0;
                sp = 0;
                var tokenlist = new List<string>();
                do
                {
                    ntoken++;

                    if ((ntoken == nel) && (nel >= 0))
                    {
                        token = hs.Substring((sp));
                        tokenlist.Add(token);
                    }
                    else
                    {
                        token = GetNextToken(hs, ref sp, dl, sdl);
                        tokenlist.Add(token);
                    }

                } while (sp >= 0 && (ntoken <= nel || nel < 0));
                wtl = tokenlist.ToArray();
            }

            if (offsetel == 0)
            {
                tl = Cleantokenlist(wtl, dl, sdl, opt);
            }
            else
            {
                var tokenlist = new List<string>();
                for (int i = offsetel; i < wtl.Length; i++)
                {
                    tokenlist.Add(wtl[i]);
                }
                tl = Cleantokenlist(tokenlist.ToArray(), dl, sdl, opt);
            }

            nel = tl.Length;
            return tl;
        }


        /*
        Return next position of sdl, 2 consecutive characters like sdl are interpreted as one embedded sdl within the string
        */
        public static int GetEofString(string s, int startpos, string sdl = "")
        {
            var eofs = -1;

            if (IsEmpty(s)) return eofs;
            if (IsEmpty(sdl)) return s.Length - 1;
            if (s.Length == 1) return 1;

            bool[] q = new bool[s.Length];

            for (int i = startpos; i < s.Length; i++)
            {
                if (s.Substring(i, 1) == sdl)
                {
                    q[i] = true;
                }
                else
                {
                    q[i] = false;
                }
            }

            int nq = 0;
            if (q[startpos])
            {
                for (int i = startpos + 1; i < s.Length; i++)
                {
                    if (i == s.Length - 1)
                    {
                        eofs = i;
                    }
                    else
                    {
                        if (q[i])
                        {
                            nq++;
                        }
                        if (q[i] && q[i - 1])
                        {
                            nq--;
                        }
                        if (nq == 1 && !q[i + 1])
                        {
                            eofs = i;
                            break;
                        }
                    }
                }
            }
            else
            {
                eofs = Array.IndexOf(q, true);
                if (eofs < 0)
                {
                    eofs = s.Length - 1;
                }
            }

            return eofs;
        }

        public static void RenameFile(string fpath, string fpathnew)
        {
            if (File.Exists(fpathnew)) File.Delete(fpathnew);
            File.Move(fpath, fpathnew);
        }

        /*
        Return next token in string s starting at startpos
        Return values: token = next token, startpos = new startposition
        dl: delimiter (,), sdl: string delimiter (")
        */
        public static string GetNextToken(string s, ref int startpos, string dl = ",", string sdl = "")
        {

            string token = "";
            if (IsEmpty(s)) return token;

            int pos, endpos;
            string hs, hdl, hsdl, swrk;

            if (IsEmpty(dl))
            {
                hdl = ",";
            }
            else
            {
                hdl = Left(dl, 1);
            }

            if (IsEmpty(sdl))
            {
                hsdl = "";
            }
            else
            {
                hsdl = Left(sdl, 1);
            }

            if (startpos < 0) startpos = 0;
            pos = startpos;
            hs = s;

            if (hs?.Length > pos)
            {
                swrk = hs.Substring(pos, 1);
            }
            else
            {
                swrk = "";
            }

            if (swrk == hsdl)
            {
                endpos = Util.GetEofString(hs, pos, hsdl);
                if ((endpos == 0))
                {
                    token = hs.Substring(pos);
                    endpos = -1;
                }
                else
                {
                    token = Util.GetSubString(hs, pos, endpos);
                    endpos = hs.IndexOf(hdl, endpos);
                    if ((endpos >= 0))
                    {
                        endpos = endpos + 1;
                    }
                    else
                    {
                        endpos = -1;
                    }

                }

            }
            else
            {
                endpos = hs.IndexOf(hdl, pos);
                if ((endpos < 0))
                {
                    token = hs.Substring((pos));
                    endpos = -1;
                }
                else if (endpos == 0)
                {
                    token = "";
                }
                else
                {
                    token = Util.GetSubString(hs, pos, endpos - 1);
                    endpos++;
                }

            }
            startpos = endpos;
            return token;
        }

        public static string GetFileType(string filename)
        {
            return AlltrimSet(Path.GetExtension(filename), ". ");
        }

        public static string GetFileType(FileInfo finfo)
        {
            var ext = "";
            try
            {
                if (finfo.Exists)
                {
                    ext = AlltrimSet(finfo.Extension, ". ");
                }
                else
                {
                    ext = GetFileType(finfo.Name);
                }
            }
            catch
            {
                throw;
            }
            return ext;
        }

        public static int GetDifferenceInDays(this DateTime startDate, DateTime endDate)
        {
            return (int)(endDate.Date - startDate.Date).TotalDays;
        }

        public static string[] GetWorkList(string txt, int minlength = 4, string p = @"[\W]+")
        {
            string[] r = Regex.Split(txt, p).Where(x => x.Length >= minlength).Distinct().ToArray();
            return r;
        }

        public static string GetRegexFirstMatch(string txt, string p, bool triminputflag = true)
        {
            var r = "";
            if (triminputflag) txt = txt?.Trim();
            string[] m = Matches(txt, p, true, false, true);
            if (m != null)
            {
                if (m.Length > 1) return m[1];
            }
            return r;
        }

        public static Dictionary<string, string> CloneDict(Dictionary<string, string> d)
        {
            return new Dictionary<string, string>(d);
        }

        public static Dictionary<string, string> MergeDict(Dictionary<string, string> d1, Dictionary<string, string> d2, bool overwrite = false)
        {
            if (d1 == null) return d2;
            if (d2 == null) return d1;
            var r = CloneDict(d1);
            foreach (string k in d2.Keys)
            {
                if (overwrite && r.ContainsKey(k))
                {
                    r.Remove(k);
                }
                if (!r.ContainsKey(k)) r.Add(k, d2[k]);
            }
            return r;
        }

        public static string GetCleanText(string txt)
        {
            return Regex.Replace(txt.ToUpper().Trim(), @"\s+", " ");
        }

        public static string GetAsciiText(string txt)
        {
            return Regex.Replace(txt, "[^ -~]+", "");
        }

        public static string SetEol2CRLF(string txt)
        {
            if (IsEmpty(txt)) return "";
            var p = "\r?\n";
            return Regex.Replace(txt, p, "\r\n");
        }

        public static string RemoveTrailingSpace(string txt)
        {
            var t = txt.TrimEnd();
            string r, p;
            p = @"\s+\r?\n";
            r = "^\r\n";
            return ReplaceRegExp(t, p, r, true);
        }

        public static string[] ConcatStringArrays(string[] a1, string[] a2)
        {
            var w = a1.ToList();
            w.AddRange(a2);
            return w.ToArray();
        }

        public static string[] Ass2Array(string[] a1, int nelements = 1)
        {
            var a2 = Enumerable.Range(0, nelements).Select(s => "").ToArray();
            return ConcatStringArrays(a1, a2);
        }

        public static List<Dictionary<string, string>> GetCsvRecordDictionary(string f, string dl = ";", char sdl = '\'', Encoding ec = null, List<Dictionary<string, string>> inpdata = null)
        {
            List<Dictionary<string, string>> rlist;

            if (inpdata == null)
            {
                rlist = new List<Dictionary<string, string>>();
            }
            else
            {
                rlist = inpdata;
            }

            if (ec == null) ec = Encoding.Default;

            using (System.IO.TextReader tr = new StreamReader(f))
            {

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = dl,
                    Quote = sdl,
                    Encoding = ec
                };

                using (var csv = new CsvReader(tr, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    string[] clist = csv.HeaderRecord;
                    int nrec = 0;
                    while (csv.Read())
                    {
                        var rec = new Dictionary<string, string>();
                        for (int i = 0; i < clist.Length; i++)
                        {
                            try
                            {
                                var v = csv[i];
                                string k = clist[i].ToUpper().Trim();
                                rec.Add(k, v);

                            }
                            catch (Exception)
                            {
                                break;
                            }

                        }
                        if (rec.Count == clist.Length)
                        {
                            rlist.Add(rec);
                            nrec++;
                        }
                    }

                }

            }

            return rlist;
        }

        private static void getStringType()
        {

        }

        public static bool IsInList(List<string> l, string v, bool caseInsensitiveFlag = true)
        {
            var r = false;
            if (caseInsensitiveFlag)
            {
                v = v.ToUpper().Trim();
                foreach (var s in l)
                {
                    if (v.ToUpper().Trim() == v)
                    {
                        r = true;
                        break;
                    }
                }
            }
            else
            {
                r = l.Contains(v);
            }
            return r;
        }

        // Returns index list of elements in l1 existing in l2
        public static List<int> GetIndexList(List<string> l1, List<string> l2, bool caseInsensitiveFlag = true)
        {
            var r = new List<int>();
            int i = 0;
            foreach (var v1 in l1)
            {
                foreach (var v2 in l2)
                {
                    if (caseInsensitiveFlag)
                    {
                        if (v1.ToUpper().Trim() == v2.ToUpper().Trim())
                        {
                            r.Add(i);
                            break;
                        }
                        else if (v1 == v2)
                        {
                            r.Add(i);
                            break;
                        }
                    }

                }
                i++;
            }

            return r;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="inpdt">Input DataTable</param>
        /// <param name="nCheckRec">Nr of record used to figure out data type</param>
        /// <param name="colFilterList">column filter list for which data type has to be changed</param>
        /// <param name="copyAllColsFlag">if true all columns are copied to target data table, otherwise only those in colFilterList</param>
        /// <returns>sdjusted datatable</returns>
        public static DataTable SetContentDataType(DataTable inpdt, int nCheckRec = 0, List<string> colFilterList = null, bool copyAllColsFlag = false, bool dataAsTextFlag = true)
        {
            // Target fata types:
            //   S: String, I: int, D: double, DT: date, O: Keep Original Type

            var dt = new DataTable("TB");
            if (nCheckRec == 0) nCheckRec = inpdt.Rows.Count;
            else if (nCheckRec > inpdt.Rows.Count) nCheckRec = inpdt.Rows.Count;

            // Get column list
            List<string> colList = inpdt.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToList();

            // List of idx of columns existing in colFilterList
            List<int> cidxlist;

            if (colFilterList == null)
            {
                cidxlist = Enumerable.Range(0, inpdt.Columns.Count).ToList();
            }
            else
            {
                cidxlist = GetIndexList(colList, colFilterList);
            }

            // Datatype of column with index cidxlist[i]
            List<string> newtplist = Enumerable.Repeat(string.Empty, cidxlist.Count).ToList();

            List<string> currentTpList = new List<string>();

            void setType(object v, int i)
            {
                if (newtplist[i] == "O")
                {
                    // no change
                }
                else if (newtplist[i] == "S")
                {
                    // no change
                }
                else
                {
                    var vtxt = v.ToString();
                    if (Util.IsEmpty(vtxt))
                    {
                        // no change
                    }
                    else
                    {
                        //   S: String, I: int, D: double, DT: date, O: Keep Original Type
                        if (newtplist[i] == "")
                        {
                            if (CheckNumeric(vtxt))
                            {
                                if (CheckInt(vtxt))
                                {
                                    newtplist[i] = "I";
                                }
                                else
                                {
                                    newtplist[i] = "D";
                                }
                            }
                            else if (dataAsTextFlag)
                            {
                                newtplist[i] = "S";
                            }
                            else
                            {
                                if (CheckDate(vtxt))
                                {
                                    newtplist[i] = "DT";
                                }
                                else
                                {
                                    newtplist[i] = "S";
                                }
                            }
                        }
                        else if (newtplist[i] == "D")
                        {
                            if (CheckNumeric(vtxt))
                            {
                                // no change
                            }
                            else
                            {
                                newtplist[i] = "S";
                            }
                        }
                        else if (newtplist[i] == "I")
                        {
                            if (CheckInt(vtxt))
                            {
                                // no change
                            }
                            else if (CheckNumeric(vtxt))
                            {
                                newtplist[i] = "D";
                            }
                            else
                            {
                                newtplist[i] = "S";
                            }
                        }
                        else if (newtplist[i] == "DT")
                        {
                            if (dataAsTextFlag)
                            {
                                newtplist[i] = "S";
                            }
                            else if (CheckDate(vtxt))
                            {
                                // no change
                            }
                            else
                            {
                                newtplist[i] = "S";
                            }
                        }
                    }
                }
            }

            for (int r = 0; r < nCheckRec; r++)
            {
                for (int c = 0; c < cidxlist.Count; c++)
                {
                    int cidx = cidxlist[c];
                    if (r == 0) // pre processing if first row
                    {
                        currentTpList.Add(inpdt.Columns[cidx].DataType.Name.ToString().ToUpper());
                        if (currentTpList[c] != "STRING" && currentTpList[c] != "OBJECT") newtplist[c] = "O";
                    }
                    setType(inpdt.Rows[r][cidx], c);
                }
            }

            for (int i = 0; i < newtplist.Count; i++)
            {
                if (newtplist[i].Equals("")) newtplist[i] = "O";
            }

            DataTable inpcopyDT = inpdt.Clone();

            List<int> srccolidx = new List<int>();
            for (int c = 0; c < inpcopyDT.Columns.Count; c++)
            {

                String colname = inpcopyDT.Columns[c].ColumnName;
                int cmapidx = cidxlist.IndexOf(c);

                if (cmapidx < 0)
                {
                    // No found in mapping
                    if (copyAllColsFlag)
                    {
                        // use orinal column
                        dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, inpcopyDT.Columns[c].DataType);
                        srccolidx.Add(c);
                    }
                    else
                    {
                        // skip column
                    }
                }
                else
                {
                    // found in mapping
                    if (newtplist[cmapidx] == "O")
                    {
                        // use original column
                        dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, inpcopyDT.Columns[c].DataType);
                        srccolidx.Add(c);
                    }
                    else
                    {
                        // add column with new data type
                        if (newtplist[cmapidx] == "S") dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, typeof(String));
                        else if (newtplist[cmapidx] == "D") dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, typeof(Double));
                        else if (newtplist[cmapidx] == "I") dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, typeof(int));
                        else if (newtplist[cmapidx] == "DT") dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, typeof(DateTime));
                        else dt.Columns.Add(inpcopyDT.Columns[c].ColumnName, typeof(String));
                        srccolidx.Add(c);
                    }
                }

            }

            int scridx;
            foreach (DataRow inprow in inpdt.Rows)
            {
                var newrow = dt.NewRow();
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    scridx = srccolidx[c];
                    var srcDTP = inpdt.Columns[scridx].DataType;
                    var tgtDTP = dt.Columns[c].DataType;
                    if (IsEmpty(inprow[scridx]))
                    {
                        newrow[c] = DBNull.Value;
                    }
                    else if (srcDTP.Equals(tgtDTP))
                    {
                        newrow[c] = inprow[scridx];
                    }
                    else
                    {
                        setRowValue(inprow, scridx, newrow, c);
                    }
                }
                dt.Rows.Add(newrow);
            }

            inpcopyDT.Dispose();

            dt.TableName = (inpdt.TableName + "_conv").ToLower();

            return dt;
        }

        public static void setRowValue(DataRow srcrow, int srcidx, DataRow tgtrow, int tgtidx)
        {
            var srcDTP = srcrow.Table.Columns[srcidx].DataType;
            var tgtDTP = tgtrow.Table.Columns[tgtidx].DataType;

            if (srcDTP.Equals(tgtDTP))
            {
                tgtrow[tgtidx] = srcrow[srcidx];
            }
            else
            {
                if (tgtDTP.Equals(typeof(System.String)))
                {
                    tgtrow[tgtidx] = srcrow[srcidx].ToString();
                }
                else if (tgtDTP.Equals(typeof(System.Int32)))
                {
                    try
                    {
                        tgtrow[tgtidx] = Int32.Parse(srcrow[srcidx].ToString());
                    }
                    catch (Exception)
                    {
                        Double w = Double.Parse(srcrow[srcidx].ToString());
                        tgtrow[tgtidx] = Math.Round(w, 0);
                    }

                }
                else if (tgtDTP.Equals(typeof(System.Double)))
                {
                    tgtrow[tgtidx] = Double.Parse(srcrow[srcidx].ToString());
                }
                else if (tgtDTP.Equals(typeof(System.DateTime)))
                {
                    tgtrow[tgtidx] = ConvertoDateTime(srcrow[srcidx].ToString());
                }
                else
                {
                    tgtrow[tgtidx] = srcrow[srcidx];
                }
            }
        }

        public static string CleanName(String colnm)
        {
            var r = colnm;
            r = Util.Translate(r, "()[]{}\t\n\r ", "_____________________");
            r = Util.Translate(r, "öüäÖÜÄ", "ouaOUA");
            r = r.Replace("____", "_").Replace("___", "_").Replace("__", "_");
            return r;
        }

        public static DataTable GetCsvDataTable(string f, string dl = ";", char sdl = '\'', Encoding ec = null, DataTable inpdata = null)
        {
            DataTable dt;
            if (inpdata == null)
            {
                dt = new DataTable("Data");
            }
            else
            {
                dt = inpdata;
            }

            if (ec == null) ec = Encoding.Default;

            using (System.IO.TextReader tr = new StreamReader(f))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = dl,
                    Quote = sdl,
                    Encoding = ec
                };

                using (var csv = new CsvReader(tr, config))
                {
                    csv.Read();
                    csv.ReadHeader();
                    string[] clist = csv.HeaderRecord;

                    if (dt.Columns.Count == 0)
                    {
                        for (int i = 0; i < clist.Length; i++)
                        {
                            dt.Columns.Add(new DataColumn(CleanName(clist[i].ToUpper().Trim()), typeof(String)));
                        }
                    }

                    int nrec = 0;
                    List<string> rec = new List<string>();
                    while (csv.Read())
                    {

                        for (int i = 0; i < clist.Length; i++)
                        {
                            try
                            {
                                var v = csv[i];
                                rec.Add(v);

                            }
                            catch (Exception)
                            {
                                break;
                            }

                        }
                        if (rec.Count == clist.Length)
                        {
                            dt.Rows.Add(rec.ToArray());
                            nrec++;
                        }
                        rec.Clear();
                    }

                }

            }

            return dt;
        }

        /// <summary>
        /// Deserialize (binary) dataset
        /// </summary>
        /// <param name="FileName">input file</param>
        /// <returns>dataset</returns>
        public static DataSet DeSerializeDataSet(string FileName)
        {
            DataSet ds = new DataSet();
            ds.RemotingFormat = SerializationFormat.Binary;
            System.Runtime.Serialization.IFormatter myFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream myStream = new FileStream(FileName, FileMode.Open);
            ds = (DataSet)myFormatter.Deserialize(myStream);
            DisposeIfNotNull(myStream);
            return ds;
        }

        /// <summary>
        /// Serialize (binary) dataset
        /// </summary>
        /// <param name="FileName">output file</param>
        /// <param name="ds">dataset</param>
        public static void SerializeDataSet(string FileName, DataSet ds)
        {
            ds.RemotingFormat = SerializationFormat.Binary;
            System.Runtime.Serialization.IFormatter myFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream myStream = new FileStream(FileName, FileMode.Create);
            myFormatter.Serialize(myStream, ds);
            Util.DisposeIfNotNull(myStream);
        }

        public static void DumpDataToXML(DataTable dt, string filename = "")
        {
            if (dt == null)
                return;
            if (IsEmpty(filename))
                filename = dt.TableName.ToString().ToUpper() + ".XML";
            dt.WriteXml(filename);
        }

        public static void DumpDataToXML(DataTable dt, Stream s)
        {
            if (dt == null)
                return;
            dt.WriteXml(s);
        }

        public static void DumpDataToXML(DataView dv, Stream s)
        {
            DumpDataToXML(dv.ToTable(), s);
        }

        public static void DumpDataToXML(DataView dv, string filename = "")
        {
            DumpDataToXML(dv.ToTable(), filename);
        }

        // Change of delimiter from olddl to newdl
        // nel: max no of elements
        // opt: U= Ucase , T = trim, L= Lcase
        public static string ChangeDelim(string line, ref int nel, string sdl, string olddl, string newdl, string opt = "")
        {
            string[] fields;
            fields = SplitE(line, ref nel, olddl, sdl);
            line = JoinE(fields, newdl, sdl, opt);
            return line;
        }


        /// <summary>
        /// Convert Timestamp string to Datetime
        /// </summary>
        /// <param name="datetimeString">Timestamp string</param>
        /// <param name="supportedFormat">format of datetimeString (yyyy: Year, MM: Month, DD: Day, HH: hour (24), mm: minute, ss: second, ffffff: fraction of second</param>
        /// <returns></returns>
        /// <remarks>supportedFormats: array of supportedFormat</remarks>
        public static DateTime ConvertoDateTime(string datetimeString, string supportedFormat = "yyyy-MM-dd-HH.mm.ss.ffffff")
        {
            DateTime result = default(DateTime);
            datetimeString = datetimeString.Trim();
            try
            {
                if (datetimeString.Length == 10)
                    supportedFormat = Util.Left(supportedFormat, 10);
                result = DateTime.ParseExact(datetimeString, supportedFormat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public static DateTime ConvertoDateTime(string datetimeString, string[] supportedFormats)
        {
            if (supportedFormats == null) supportedFormats = GetDefaulDateTimeFormats();
            DateTime result = default(DateTime);
            try
            {
                result = DateTime.ParseExact(datetimeString, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public static DateTime ConvertoDateTimeLoose(string datetimeString)
        {
            DateTime result = default(DateTime);
            try
            {
                result = DateTime.Parse(datetimeString, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// Convert Date string to Datetime
        /// </summary>
        /// <param name="dateString">Date string</param>
        /// <param name="supportedFormat">format of datetimeString (yyyy: Year, MM: Month, DD: Day)</param>
        /// <returns></returns>
        /// <remarks>supportedFormats: array of supportedFormat</remarks>
        public static DateTime ConvertoDate(string dateString, string supportedFormat = "yyyy-MM-dd")
        {
            DateTime result = default(DateTime);
            try
            {
                result = DateTime.ParseExact(dateString, supportedFormat, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public static DateTime ConvertoDate(string dateString, string[] supportedFormats)
        {
            DateTime result = default(DateTime);
            try
            {
                result = DateTime.ParseExact(dateString, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public static bool DirExists(string wdir)
        {
            bool r = false;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(wdir);
                if (dir == null)
                    r = false;
                else
                    r = true;
            }
            catch (Exception ex)
            {
                r = false;
            }
            return r;
        }

        public static long GetFileSize(string fpath)
        {
            FileInfo fi = new FileInfo(fpath);
            long fs = fi.Length;
            return fs;
        }


        public static string DirName(string f)
        {
            string r = "";
            r = Path.GetDirectoryName(f);
            return r;
        }

        public static Version GetDLLVersion(string ddlname)
        {
            Dictionary<string, Version> adict = GetLoadedAssemblyVersionList();
            var k = ddlname.ToLower();
            if (adict.ContainsKey(k))
            {
                return adict[k];
            }
            else
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(ddlname);
                    return a.GetName().Version;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static Version GetCurrentAssemlyVersion()
        {
            var v = Assembly.GetEntryAssembly()?.GetName()?.Version;
            if (v == null)
            {
                Assembly a = Assembly.GetExecutingAssembly();
                v = a.GetName()?.Version;
            }
            return v;
        }

        public static Dictionary<string, Version> GetLoadedAssemblyVersionList()
        {
            var r = new Dictionary<string, Version>();
            Assembly[] alist = Thread.GetDomain().GetAssemblies();
            foreach (var a in alist)
            {
                try
                {
                    var fi = new FileInfo(a.Location);
                    r.Add(fi.Name.ToLower(), a.GetName().Version);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return r;
        }

        public static Assembly GetAssembly(Type type)
        {
            return type?.Assembly;
        }

        public static Assembly GetAssembly(string typename)
        {
            try
            {
                Type t = Type.GetType(typename, true, false);
                return t.Assembly;
            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }

        public static Version GetClassAssemblyVersion(string classname)
        {
            var a = GetAssembly(classname);
            return a?.GetName()?.Version;
        }

        public static void TriggerGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public static string GetFullPath(String path)
        {
            var r = path;

            if (File.Exists(r))
            {
                r = new FileInfo(r).FullName;
            }
            else if (Directory.Exists(r))
            {
                r = new DirectoryInfo(r).FullName;
            }
            else
            {
                r = new FileInfo(r).FullName;
            }
            return r;
        }

        public static string GetUNCPath(string originalPath)
        {
            var fullpath = GetFullPath(originalPath);

            var pos = fullpath.IndexOf("::", StringComparison.Ordinal);
            var r = fullpath;
            if (pos >= 0)
            {
                r = GetUNCPath(fullpath.Substring(0, pos));
                r = r + fullpath.Substring(pos);
            }
            else
            {
                fullpath = fullpath.TrimEnd('\\', '/');
                DirectoryInfo d;
                try
                {
                    d = new DirectoryInfo(fullpath);
                }
                catch (Exception)
                {
                    return null;
                }
                String root = d.Root.FullName.TrimEnd('\\');

                if (!root.StartsWith(@"\\"))
                {
                    using (var managementObject = new ManagementObject())
                    {
                        managementObject.Path = new ManagementPath(String.Format("Win32_LogicalDisk='{0}'", root));
                        if (Convert.ToUInt32(managementObject["DriveType"]) == 4)
                        {
                            root = managementObject["ProviderName"].ToString();
                        }
                        else
                        {
                            root = @"\\" + System.Net.Dns.GetHostName() + "\\" + root.TrimEnd(':') + @"$\";
                            r = Recombine(root, d);
                        }

                    }
                }

            }
            return r.ToLower().Trim();
        }

        public static string Recombine(String root, DirectoryInfo d)
        {
            Stack s = new Stack();
            while (d.Parent != null)
            {
                s.Push(d.Name);
                d = d.Parent;
            }

            while (s.Count > 0)
            {
                root = Path.Combine(root, (String)s.Pop());
            }
            return root;
        }

        public static string GetLocalPath(string sharePath)
        {
            var r = "";
            try
            {
                var regex = new Regex(@"\\\\([^\\]*)\\([^\\]*)(\\.*)?");
                var match = regex.Match(sharePath);
                if (!match.Success) return sharePath;

                var shareHost = match.Groups[1].Value;
                var shareName = match.Groups[2].Value;
                var shareDirs = match.Groups[3].Value;
                ConnectionOptions options = new ConnectionOptions();
                options.Authentication = AuthenticationLevel.PacketPrivacy;
                var scope = new ManagementScope(@"\\" + shareHost + @"\root\cimv2", options);
                var query = new SelectQuery("SELECT * FROM Win32_Share WHERE name = '" + shareName + "'");

                using (var searcher = new ManagementObjectSearcher(scope, query))
                {
                    var result = searcher.Get();
                    foreach (var item in result)
                    {
                        r = item["path"].ToString() + shareDirs;
                        r = CleanPathName(r);
                        r = r.Replace(@":\\", @":\");
                        break;
                    }
                }

                return r;
            }
            catch (Exception)
            {
                return r;
            }
        }



        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }

        //        
        public static bool FileIsOlder(string filename, string filenamecompare)
        {
            bool r = false;
            var fi = new FileInfo(filename);
            var ficomp = new FileInfo(filenamecompare);
            return FileIsOlder(fi, ficomp);
        }

        // Returns true if filecompare is older then file
        // Returns true if file is newer then filecompare
        public static bool FileIsOlder(FileInfo file, FileInfo filecompare)
        {
            bool r = false;

            if (file.Exists && filecompare.Exists)
            {
                return filecompare.LastWriteTimeUtc < file.LastWriteTimeUtc;
            }
            else
            {
                return false;
            }
        }

        public static Encoding GetEncoding(string filename)
        {
            // This is a direct quote from MSDN:  
            // The CurrentEncoding value can be different after the first
            // call to any Read method of StreamReader, since encoding
            // autodetection is not done until the first call to a Read method.

            using (var reader = new StreamReader(filename, Encoding.Default, true))
            {
                if (reader.Peek() >= 0) // you need this!
                    reader.Read();

                return reader.CurrentEncoding;
            }
        }

        public static String ReadAllTextFromFile(string filename)
        {
            return File.ReadAllText(filename, GetEncoding(filename));
        }

    }
}
