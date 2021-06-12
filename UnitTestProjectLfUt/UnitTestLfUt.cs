using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LfUtilities;
using System.IO;
using System.Collections.Generic;
using UnitTestProjectLfUt;
using System.Data;
using System.Drawing;

namespace XUnitTestProjectLfUt
{
    [TestClass]
    public class UnitTestLfUt
    {

        string testdir;

        public UnitTestLfUt()
        {
            var fi = new FileInfo(@"..\..\..\..\TestData\TestText.txt");
            LoadTestStrings.Textfile = fi.FullName;
            testdir = fi.DirectoryName;
            LoadTestStrings.Load();
        }


        [TestMethod]
        public void TestTranslate()
        {
            var x1 = Util.Translate("", "", "");
            Assert.AreEqual(0, x1.Length);
            var x2 = Util.Translate("abcdefg", "abef", "123");
            Assert.AreEqual("12cd3g", x2);
        }

        [TestMethod]
        public void TestInstrCount()
        {
            var x1 = Util.InstrCount("12121212", "12", 0);
            Assert.AreEqual(4, x1);
            var x2 = Util.InstrCount("12121212", "12", 1);
            Assert.AreEqual(3, x2);
            var x3 = Util.InstrCount("1212122", "12", 1);
            Assert.AreEqual(2, x3);
        }

        [TestMethod]
        public void TestCheckNumeric()
        {
            Assert.IsTrue(Util.CheckNumeric("12121212"));
            Assert.IsTrue(Util.CheckNumeric("12121.212"));
            Assert.IsTrue(Util.CheckNumeric("-12121.212"));
            Assert.IsTrue(Util.CheckNumeric("+12121.212"));
            Assert.IsFalse(Util.CheckNumeric(""));
        }

        [TestMethod]
        public void TestCheckInt()
        {
            Assert.IsTrue(Util.CheckInt("12121212"));
            Assert.IsFalse(Util.CheckInt("12121.212"));
            Assert.IsTrue(Util.CheckInt("-12121"));
            Assert.IsTrue(Util.CheckInt("+12121"));
        }

        [TestMethod]
        public void TestCheckDate()
        {
            return;
            Assert.IsFalse(Util.CheckDate("2019"));
            Assert.IsFalse(Util.CheckDate("201912"));
            Assert.IsFalse(Util.CheckDate("2019-12-31-12"));
            Assert.IsTrue(Util.CheckDate("2019-12-31-12.12.12.123123"));
        }

        [TestMethod]
        public void TestCenter()
        {
            var x1 = Util.Center("1212", 10);
            Assert.IsTrue(x1.Length == 10);
            var x2 = Util.Center("1212", 11);
            Assert.IsTrue(x2.Length == 11);
            Assert.IsTrue(x2.StartsWith("   "));
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            Assert.IsTrue(Util.IsEmpty(" "));
            Assert.IsTrue(Util.IsEmpty(""));
            Assert.IsTrue(Util.IsEmpty(null));
            Assert.IsTrue(Util.IsEmpty((DBNull)null));
        }

        [TestMethod]
        public void TestxTrim()
        {
            Assert.AreEqual("123", Util.AlltrimSet("ab123cd", "abcd"));
            Assert.AreEqual("123cd", Util.LtrimSet("ab123cd", "abcd"));
            Assert.AreEqual("ab123", Util.RtrimSet("ab123cd", "abcd"));
        }

        [TestMethod]
        public void TestxSimple()
        {
            Assert.AreEqual("   ", Util.Space(3));
            Assert.AreEqual("12", Util.Left("12abc", 2));
            Assert.AreEqual("12", Util.Right("abc12", 2));
            Assert.AreEqual("112112", Util.Repeat("112", 2));
            Assert.AreEqual(" 112", Util.Lpad("112", 4));
            Assert.AreEqual("    ", Util.Lpad(null, 4));
            Assert.AreEqual("112 ", Util.Rpad("112", 4));
            Assert.AreEqual("    ", Util.Rpad(null, 4));
            Assert.AreEqual("112Q", Util.Rpad("112", 4, 'Q'));
            Assert.AreEqual("    ", Util.Fill(null, 4));
            Assert.AreEqual(" 1 2 3 4 ", Util.RemoveDblspaces(" 1   2  3 4 "));
            Assert.IsTrue(Util.AllUpperCase("ABC"));
            Assert.IsTrue(Util.AllUpperCase("ÄBC"));
            Assert.IsFalse(Util.AllUpperCase("äBC"));
            Assert.IsFalse(Util.AllLowerCase("äBC"));
            Assert.IsTrue(Util.AllLowerCase("äbc"));
            Assert.IsTrue(Util.FirstUpperCase("Abc"));
            Assert.IsFalse(Util.FirstLowerCase("Abc"));
        }

        [TestMethod]
        public void TestRemove()
        {
            Assert.AreEqual("1234555", Util.Remove("1234abc555", 4, 3));
            Assert.AreEqual("1234555   ", Util.Remove("1234abc555", 4, 3, true));
        }

        [TestMethod]
        public void TestRemoveEmptyLines()
        {
            var txt = "\r\n1234567890\r\nabcdefg\r\n\r\naaaaaaaa";
            var x1 = Util.RemoveEmptyLines(txt);
        }

        [TestMethod]
        public void TestIns()
        {
            Assert.AreEqual("1234abc555", Util.Ins("1234555", "abc", 3));
        }

        [TestMethod]
        public void TestNotInstr()
        {
            Assert.AreEqual("1234abc555", Util.Ins("1234555", "abc", 3));
        }

        [TestMethod]
        public void TestReplaceBlock()
        {
            //                         01234567890
            var x = Util.ReplaceBlock("1234xxx555", 4, 6, "abc");
            Assert.AreEqual("1234abc555", x);
        }

        [TestMethod]
        public void TestRemoveBlock()
        {
            //                        01234567890
            var x = Util.RemoveBlock("1234xxx555", 4, 6);
            Assert.AreEqual("1234555", x);
        }

        [TestMethod]
        public void TestGetSubString()
        {
            //                                        0123456789
            Assert.AreEqual("abc", Util.GetSubString("1234abc555", 4, 6));
            Assert.AreEqual("0", Util.GetSubString("0123456789", 0, 0));
            Assert.AreEqual("1", Util.GetSubString("0123456789", 1, 1));
            Assert.AreEqual("12", Util.GetSubString("0123456789", 1, 2));
        }

        [TestMethod]
        public void TestIsEmptyArray()
        {
            int[] a1 = null;
            Assert.IsTrue(Util.IsEmptyArray(a1));

            int[] a2 = { };
            Assert.IsTrue(Util.IsEmptyArray(a2));

            int[] a3 = { 1, 2 };
            Assert.IsFalse(Util.IsEmptyArray(a3));
        }


        [TestMethod]
        public void TestCmd()
        {
            var cmd = "aaa=bbb";
            var cmdkey = "";
            var cmdvalue = "";
            Util.GetCmdInfo(cmd, "=", ref cmdkey, ref cmdvalue);
            Assert.AreEqual("aaa", cmdkey);
            Assert.AreEqual("bbb", cmdvalue);
        }

        [TestMethod]
        public void TestMatch()
        {
            var teststring = "aaa" + Util.Eol() + "a1212" + Util.Eol() + "XX";
            Assert.IsTrue(Util.Match(teststring, "A1212", true));
            Assert.IsTrue(Util.Match(teststring, "^A1212$", true, true));
            Assert.IsFalse(Util.Match(teststring, "^A1212$", false, true));
            Assert.IsFalse(Util.Match(teststring, "^a1212$", false, false));
        }

        [TestMethod]
        public void TestReplaceRegExp()
        {
            Assert.AreEqual("abXdef", Util.ReplaceRegExp("abc,.*def", @"C\,\.\*", "X", true));
            Assert.AreEqual("abc,.*def", Util.ReplaceRegExp("abc,.*def", @"C\,\.\*", "X", false));
        }

        [TestMethod]
        public void TestFirstLowerCase()
        {
            Assert.IsTrue(Util.FirstLowerCase("abcd"));
            Assert.IsFalse(Util.FirstLowerCase("Abcd"));
            Assert.IsTrue(Util.FirstLowerCase(" abcd"));
        }

        [TestMethod]
        public void TestFirstUpperCase()
        {
            Assert.IsTrue(Util.FirstUpperCase("Acd"));
            Assert.IsFalse(Util.FirstUpperCase("abcd"));
            Assert.IsTrue(Util.FirstUpperCase(" Abcd"));
        }


        [TestMethod]
        public void TestGetFileMemoryStream()
        {
            var f1 = @"..\..\..\..\TestData\testfile.xlsm";
            var f2 = @"..\..\..\..\TestData\testfile2.xlsm";
            if (!File.Exists(f1))
            {
                return;
            }
            if (!File.Exists(f2))
            {
                return;
            }
            var ms1 = Util.GetFileMemoryStream(f1);
            var ms2 = Util.GetFileMemoryStream(f2);
            var x1 = Util.GetnBytesFromFile(ms1, 0, 1000);
            var x2 = Util.GetnBytesFromFile(ms2, 0, 1000);
            Assert.IsTrue(Util.ByteArraysAreEqual(x1, x2));
        }

        [TestMethod]
        public void TestGetUniqueList()
        {
            List<object> lexpected = new List<object>();
            List<object> linput = new List<object>();
            lexpected.Add("12");
            lexpected.Add("13");
            lexpected.Add("14");
            linput.Add("12");
            linput.Add("13");
            linput.Add("14");
            linput.Add("14");
            linput.Add("12");
            var rlist1 = Util.GetUniqueList(linput, true, true);
            Assert.AreEqual(rlist1.Count, lexpected.Count);
            Assert.AreEqual(3, rlist1.Count);

            lexpected.Clear();
            linput.Clear();
            lexpected.Add("12");
            lexpected.Add("13");
            lexpected.Add("aa ");
            linput.Add("12");
            linput.Add("13");
            linput.Add("aa ");
            linput.Add("AA");
            linput.Add("AA");
            var rlist2 = Util.GetUniqueList(linput, true, true);
            Assert.AreEqual(rlist2.Count, lexpected.Count);
            Assert.AreEqual(3, rlist2.Count);
        }

        [TestMethod]
        public void TestIsIn()
        {
            Assert.IsTrue(Util.IsIn("ddd", "111", "222", "333", "ddd"));
            Assert.IsFalse(Util.IsIn("ddd", "111", "222", "333", "444"));
        }

        [TestMethod]
        public void TestGetLineEndPosition()
        {
            //         012345678901
            //         1234567890__
            var txt = "1234567890\r\n";
            var p1 = Util.GetLineEndPosition(txt, 0);
            var p2 = Util.GetLineEndPosition(txt, 1);
            var p3 = Util.GetLineEndPosition(txt, 14);
            var p4 = Util.GetLineEndPosition(txt, 10);
            var p5 = Util.GetLineEndPosition(txt, 11);
            var p6 = Util.GetLineEndPosition(txt, 12);
            Assert.AreEqual(11, p1);
            Assert.AreEqual(p1, p2);
            Assert.AreEqual(p1, p3);
            Assert.AreEqual(p1, p4);
            Assert.AreEqual(p1, p5);
            Assert.AreEqual(p1, p6);
            var txt2 = "1234567890\n";
            var p21 = Util.GetLineEndPosition(txt2, 0);
            var p22 = Util.GetLineEndPosition(txt2, 1);
            var p23 = Util.GetLineEndPosition(txt2, 14);
            var p24 = Util.GetLineEndPosition(txt2, 10);
            Assert.AreEqual(10, p21);
            Assert.AreEqual(p21, p22);
            Assert.AreEqual(p21, p23);
            Assert.AreEqual(p21, p24);
            var txt3 = "1234567890";
            var p31 = Util.GetLineEndPosition(txt3, 0);
            Assert.AreEqual(9, p31);
            //          0 1 23456789012 3 45678901234 5
            var txt4 = "\r\n1234567890\r\n1234567890\r\n";
            var p41 = Util.GetLineEndPosition(txt4, 0);
            var p42 = Util.GetLineEndPosition(txt4, 1);
            var p43 = Util.GetLineEndPosition(txt4, 10);
            var p44 = Util.GetLineEndPosition(txt4, 14);
            Assert.AreEqual(1, p41);
            Assert.AreEqual(1, p42);
            Assert.AreEqual(13, p43);
            Assert.AreEqual(25, p44);

        }

        [TestMethod]
        public void TestGetLineStartPosition()
        {
            //         01234567890 1
            //         1234567890__
            var txt = "1234567890\r\n";
            var p1 = Util.GetLineStartPosition(txt, 0);
            var p2 = Util.GetLineStartPosition(txt, 1);
            var p3 = Util.GetLineStartPosition(txt, 10);
            var p4 = Util.GetLineStartPosition(txt, 11);
            var p5 = Util.GetLineStartPosition(txt, 12);
            var p6 = Util.GetLineStartPosition(txt, 13);
            Assert.AreEqual(0, p1);
            Assert.AreEqual(p1, p2);
            Assert.AreEqual(p1, p3);
            Assert.AreEqual(p1, p4);
            Assert.AreEqual(p4, p5);
            Assert.AreEqual(p5, p6);
            var txt2 = "1234567890\n";
            var p21 = Util.GetLineStartPosition(txt2, 10);
            var p22 = Util.GetLineStartPosition(txt2, 11);
            var p23 = Util.GetLineStartPosition(txt2, 12);
            Assert.AreEqual(0, p21);
            Assert.AreEqual(p21, p22);
            Assert.AreEqual(p22, p23);
            var txt3 = "\r\n1234567890\r\n1234567890\r\n";
            var p31 = Util.GetLineStartPosition(txt3, 0);
            var p32 = Util.GetLineStartPosition(txt3, 1);
            var p33 = Util.GetLineStartPosition(txt3, 2);
            var p34 = Util.GetLineStartPosition(txt3, 3);
            Assert.AreEqual(0, p31);
            Assert.AreEqual(0, p32);
            Assert.AreEqual(2, p33);
            Assert.AreEqual(p33, p34);
            var p35 = Util.GetLineStartPosition(txt3, 11);
            var p36 = Util.GetLineStartPosition(txt3, 12);
            var p37 = Util.GetLineStartPosition(txt3, 13);
            var p38 = Util.GetLineStartPosition(txt3, 14);
            var p39 = Util.GetLineStartPosition(txt3, 15);
            var p3A = Util.GetLineStartPosition(txt3, 16);
            Assert.AreEqual(p33, p35);
            Assert.AreEqual(p33, p36);
            Assert.AreEqual(p33, p37);
            Assert.AreEqual(14, p38);
            Assert.AreEqual(p38, p39);
            Assert.AreEqual(p38, p3A);
        }

        [TestMethod]
        public void TestGetLine()
        {
            //         0 1 23456789012 3 45678901 2 34567
            var txt = "\r\n1234567890\r\nABCDEFG\r\n";
            var x1 = Util.GetLine(txt, 0);
            var x2 = Util.GetLine(txt, 1);
            var x3 = Util.GetLine(txt, 2);
            var x4 = Util.GetLine(txt, 3);
            var x5 = Util.GetLine(txt, 4);
            var x6 = Util.GetLine(txt, 13);
            var x7 = Util.GetLine(txt, 14);
            var x8 = Util.GetLine(txt, 15);
            var x9 = Util.GetLine(txt, 22);
            var xA = Util.GetLine(txt, 30);
            Assert.AreEqual("", x1);
            Assert.AreEqual(x1, x2);
            Assert.AreEqual("1234567890", x3);
            Assert.AreEqual(x3, x4);
            Assert.AreEqual(x3, x5);
            Assert.AreEqual(x3, x6);
            Assert.AreEqual("ABCDEFG", x7);
            Assert.AreEqual(x7, x8);
            Assert.AreEqual(x7, x9);
            Assert.AreEqual(x7, xA);
        }

        [TestMethod]
        public void TestRemoveLine()
        {
            //         01234567890 1 12345678 9 0123456
            var txt = "1234567890\r\nabcdefg\r\naassdd";
            var x1 = Util.RemoveLine(txt, 0);
            var x2 = Util.RemoveLine(txt, 10);
            var x3 = Util.RemoveLine(txt, 11);
            var x4 = Util.RemoveLine(txt, 12);
            var x5 = Util.RemoveLine(txt, 13);
            var x6 = Util.RemoveLine(txt, 19);
            var x7 = Util.RemoveLine(txt, 66);
            Assert.AreEqual("abcdefg\r\naassdd", x1);
            Assert.AreEqual(x1, x2);
            Assert.AreEqual(x1, x3);
            Assert.AreEqual("1234567890\r\naassdd", x4);
            Assert.AreEqual(x4, x5);
            Assert.AreEqual(x4, x6);
            Assert.AreEqual("1234567890\r\nabcdefg\r\n", x7);
        }

        [TestMethod]
        public void TestReplaceLine()
        {
            //         01234567890 1 12345678 9 0123456
            var txt = "1234567890\r\nabcdefg\r\naassdd";
            var x1 = Util.ReplaceLine(txt, 0, "xxyy");
            var x2 = Util.ReplaceLine(txt, 10, "xxyy");
            var x3 = Util.ReplaceLine(txt, 11, "xxyy");
            var x4 = Util.ReplaceLine(txt, 12, "xxyy");
            var x5 = Util.ReplaceLine(txt, 13, "xxyy");
            var x6 = Util.ReplaceLine(txt, 19, "xxyy");
            var x7 = Util.ReplaceLine(txt, 66, "xxyy");
            Assert.AreEqual("xxyy\r\nabcdefg\r\naassdd", x1);
            Assert.AreEqual(x1, x2);
            Assert.AreEqual(x1, x3);
            Assert.AreEqual("1234567890\r\nxxyy\r\naassdd", x4);
            Assert.AreEqual(x4, x5);
            Assert.AreEqual(x4, x6);
            Assert.AreEqual("1234567890\r\nabcdefg\r\nxxyy", x7);
        }

        [TestMethod]
        public void TestIsLike()
        {
            var txt = "abc123";
            Assert.IsTrue(Util.IsLike(txt, "abc*"));
            Assert.IsTrue(Util.IsLike(txt, "a?c*"));
            Assert.IsFalse(Util.IsLike(txt, "ac*"));
            Assert.IsFalse(Util.IsLike(txt, "a??c*"));
            var txt2 = "\r\nabc123\r\nasasasa";
            Assert.IsTrue(Util.IsLike(txt2, "abc*"));
            Assert.IsTrue(Util.IsLike(txt2, "a?c*"));
            Assert.IsFalse(Util.IsLike(txt2, "ac*"));
            Assert.IsFalse(Util.IsLike(txt2, "a??c*"));
        }

        [TestMethod]
        public void TestGetCmdInfo()
        {
            var line = "KEY=VALUE";
            var k = Util.GetCmdKey(line);
            var v = Util.GetCmdValue(line);
            Assert.AreEqual("KEY", k);
            Assert.AreEqual("VALUE", v);
            var line2 = " KEY = VALUE ";
            k = Util.GetCmdKey(line2);
            v = Util.GetCmdValue(line2);
            Assert.AreEqual("KEY", k);
            Assert.AreEqual("VALUE", v);
        }

        [TestMethod]
        public void TestMatches()
        {
            var m = Util.Matches(LoadTestStrings.Text, @"\#ENDTEXT");
            Assert.IsTrue(m.Length > 0);
        }

        [TestMethod]
        public void TestFileFunctions()
        {
            var x = Util.GetUniqueFilename("aa", "txt");
            Assert.IsTrue(Util.IsValidFileName(x));
            var w = Util.ChFileType(x, "").ToLower();
            var fullfilename = Util.Filename(LoadTestStrings.Testdir, x);
            File.WriteAllText(fullfilename, "123456789012345678901234567890\n\r123456789012345678901234567890\n\r123456789012345678901234567890");
            Assert.IsTrue(File.Exists(fullfilename));
            var x2 = Util.GetUniqueFilename("aa", "txt");
            var fullfilename2 = Util.Filename(LoadTestStrings.Testdir, x2);
            Assert.IsFalse(File.Exists(fullfilename2));
            var x3 = Util.ChFileType("abc.txt", "sss");
            Assert.AreEqual("abc.sss", x3);
            var x4 = Util.ChFileType("abc.txt", "");
            Assert.AreEqual("abc", x4);
            string p = "";
            string f = "";
            string t = "";
            Util.SplitFilePath(fullfilename, ref p, ref f, ref t);
            Assert.AreEqual(LoadTestStrings.Testdir.ToLower(), p.ToLower());
            Assert.AreEqual("txt", t.ToLower());
            Assert.AreEqual(w, f.ToLower());
            string p2 = "";
            string f2 = "";
            Util.PathName(fullfilename, ref p2, ref f2);
            Assert.AreEqual(LoadTestStrings.Testdir.ToLower(), p2.ToLower());
            Assert.AreEqual(x.ToLower(), f2.ToLower());
            var x5 = Util.FileExists(LoadTestStrings.Testdir, x);
            Assert.AreEqual(1, x5);
            Assert.IsTrue(Util.FileExistsFlag(fullfilename));
            Assert.IsFalse(Util.FileExistsFlag(fullfilename2));
            Assert.IsTrue(Util.IsDir(LoadTestStrings.Testdir));
            Assert.IsFalse(Util.IsDir(fullfilename));
        }

        [TestMethod]
        public void TestUNCFileFunctions()
        {
            var f1 = LoadTestStrings.Textfile;
            Assert.IsTrue(File.Exists(f1));
            var x1 = Util.GetUNCPath(f1);
            Assert.IsTrue(File.Exists(x1));
            var x2 = Util.GetLocalPath(x1);

            Assert.IsTrue(File.Exists(x2));

            var f2 = @"r:\floristik\ZDSF\flora.db3";
            Assert.IsTrue(File.Exists(f2));
            var x1b = Util.GetUNCPath(f2);
            Assert.IsTrue(File.Exists(x1b));
            var x2b = Util.GetLocalPath(f2);
            Assert.IsTrue(File.Exists(x2b));
            var x3b = Util.GetLocalPath(x1b);
            Assert.IsTrue(File.Exists(x3b));
        }

        [TestMethod]
        public void TestGetListOfLines()
        {
            var ll1 = Util.GetListOfLines(LoadTestStrings.Text, false);
            var ll2 = Util.GetListOfLines(LoadTestStrings.Text, true);
            var ll3 = Util.GetListOfLinesFromFile(LoadTestStrings.Textfile, false);
            var ll4 = Util.GetListOfLinesFromFile(LoadTestStrings.Textfile, true);
            CollectionAssert.AreEqual(ll1, ll3);
            CollectionAssert.AreEqual(ll2, ll4);
            Assert.IsTrue(ll1.Count > 10);
        }

        [TestMethod]
        public void TestJoinE()
        {
            /*
            enhanced version of join:
            dl: del (,)
            sdl: string delimiter (")
            opt: U= Ucase , T = Trim, L= Lcase, R= Remove Delim, S= LTrim, E= RTrim
            public static string JoinE(string[] fields, string dl = ",", string sdl = "", string opt = "")
            */

            string[] fl1 = { "1", "2" };
            var x1 = Util.JoinE(fl1);
            Assert.AreEqual("1,2", x1);

            string[] fl2 = { " 1 ", " 2 " };
            var x2 = Util.JoinE(fl2);
            Assert.AreEqual(" 1 , 2 ", x2);

            fl2 = new string[] { " 1 ", " 2 " };
            var x3 = Util.JoinE(fl2, opt: "T");
            Assert.AreEqual("1,2", x3);

            fl2 = new string[] { " 1 ", " 2 " };
            var x4 = Util.JoinE(fl2, opt: "S");
            Assert.AreEqual("1 ,2 ", x4);

            fl2 = new string[] { " 1 ", " 2 " };
            var x5 = Util.JoinE(fl2, opt: "E");
            Assert.AreEqual(" 1, 2", x5);

            fl2 = new string[] { " 1 ", " 2 " };
            var x6 = Util.JoinE(fl2, dl: ";", sdl: "'", opt: "E");
            Assert.AreEqual("' 1';' 2'", x6);

            fl2 = new string[] { " 1; ", " 2u; " };
            var x7 = Util.JoinE(fl2, dl: ";", sdl: "'", opt: "EUR");
            Assert.AreEqual("' 1';' 2U'", x7);

            fl2 = new string[] { " 1U; ", " 2u; " };
            var x8 = Util.JoinE(fl2, dl: ";", sdl: "'", opt: "EU");
            Assert.AreEqual("' 1U;';' 2U;'", x8);

        }

        [TestMethod]
        public void TestSplitE()
        {
            var s1 = "1,2,3,4";
            int nel = -1;
            string[] fl1 = Util.SplitE(s1, ref nel);
            Assert.AreEqual(fl1.Length, 4);
            nel = 2;
            fl1 = Util.SplitE(s1, ref nel);
            Assert.AreEqual(fl1.Length, 2);
            Assert.AreEqual(fl1[1], "2,3,4");
            fl1 = Util.SplitE(s1, ref nel, offsetpos: 2);
            Assert.AreEqual(fl1[1], "3,4");
            fl1 = Util.SplitE(s1, ref nel, offsetpos: 2, offsetel: 1);
            Assert.AreEqual(fl1[0], "3,4");
            nel = -1;
            var s2 = "'1','2','3','4'";
            fl1 = Util.SplitE(s2, ref nel, sdl: "'", opt: "TRX");
            Assert.IsTrue(fl1.Length == 4);
            var s3 = "',1''','''2''','3','4'";
            nel = -1;
            var fl2 = Util.SplitE(s3, ref nel, sdl: "'", opt: "TRX");
            Assert.IsTrue(fl2.Length == 4);
            CollectionAssert.AreEqual(fl1, fl2);
        }

        [TestMethod]
        public void GetEofString()
        {
            var s1 = "1";
            int sp = 0;
            // public static int GetEofString(string s, ref int startpos, string sdl = "")
            var x1 = Util.GetEofString(s1, sp);
            Assert.AreEqual(0, x1);

            var s2 = "'1'";
            var x2 = Util.GetEofString(s2, sp, "'");
            Assert.AreEqual(2, x2);

            var s3 = "'12'''";
            var x3 = Util.GetEofString(s3, sp, "'");
            Assert.AreEqual(5, x3);

            var s4 = "'12'''''";
            var x4 = Util.GetEofString(s4, sp, "'");
            Assert.AreEqual(7, x4);

            var s5 = "'12''''";
            var x5 = Util.GetEofString(s5, sp, "'");
            Assert.AreEqual(6, x5);

            var s6 = "';1''','''2''','3','4'";
            var x6 = Util.GetEofString(s6, sp, "'");
            Assert.AreEqual(5, x6);
            Assert.AreEqual("';1'''", s6.Substring(0, x6 + 1));

            //        01234567
            var s7 = "';1''''','''2''','3','4'";
            var x7 = Util.GetEofString(s7, sp, "'");
            Assert.AreEqual(7, x7);
            Assert.AreEqual("';1'''''", s7.Substring(0, x7 + 1));

            //        01234567890
            var s8 = "'1';'2';3;4";
            sp = 4;
            var x8 = Util.GetEofString(s8, sp, "'");
            Assert.AreEqual(6, x8);
        }

        [TestMethod]
        public void TestGetNextToken()
        {
            var s1 = "1,2,3,4";
            int sp = 0;
            var tk = Util.GetNextToken(s1, ref sp);
            Assert.AreEqual("1", tk);
            tk = Util.GetNextToken(s1, ref sp);
            Assert.AreEqual("2", tk);
            tk = Util.GetNextToken(s1, ref sp);
            Assert.AreEqual("3", tk);
            tk = Util.GetNextToken(s1, ref sp);
            Assert.AreEqual("4", tk);
            var s2 = "'1';'2';3;4";
            sp = 0;
            tk = Util.GetNextToken(s2, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("'1'", tk);
            tk = Util.GetNextToken(s2, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("'2'", tk);
            tk = Util.GetNextToken(s2, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("3", tk);
            tk = Util.GetNextToken(s2, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("4", tk);
            var s3 = "'1;';'''2';3;4";
            sp = 0;
            tk = Util.GetNextToken(s3, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("'1;'", tk);
            tk = Util.GetNextToken(s3, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("'''2'", tk);
            tk = Util.GetNextToken(s3, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("3", tk);
            tk = Util.GetNextToken(s3, ref sp, dl: ";", sdl: "'");
            Assert.AreEqual("4", tk);

            var s4 = "';1''','''2''','3','4'";
            sp = 0;
            tk = Util.GetNextToken(s4, ref sp, dl: ",", sdl: "'");
            Assert.AreEqual("';1'''", tk);

        }

        [TestMethod]
        public void TestReadCSVDict1()
        {
            var f = Path.Combine(this.testdir, "GENUSFAMILIE.csv");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                f = fi.FullName;
                var data = Util.GetCsvRecordDictionary(f);
                Assert.IsTrue(data.Count > 930);
                var data2 = Util.GetCsvRecordDictionary(f, "\t", '\"', inpdata: data);
                Assert.IsTrue(data.Count > 1860);
            }
        }

        [TestMethod]
        public void TestReadCSVDict2()
        {
            var f = Path.Combine(this.testdir, "observations.csv");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                f = fi.FullName;
                var data = Util.GetCsvRecordDictionary(f, "\t", '\"');
                Assert.IsTrue(data.Count > 1600);
                var data2 = Util.GetCsvRecordDictionary(f, "\t", '\"', inpdata: data);
                Assert.IsTrue(data.Count > 3200);
            }
        }

        [TestMethod]
        public void TestReadCSVDt1()
        {
            var f = Path.Combine(this.testdir, "GENUSFAMILIE.csv");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                f = fi.FullName;
                var data = Util.GetCsvDataTable(f);
                Assert.IsTrue(data.Rows.Count > 930);
                var data2 = Util.GetCsvDataTable(f, "\t", '\"', inpdata: data);
                Assert.IsTrue(data2.Rows.Count > 1860);
            }
        }

        [TestMethod]
        public void TestReadCSVDt2()
        {
            var f = Path.Combine(this.testdir, "observations.csv");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                f = fi.FullName;
                var data = Util.GetCsvDataTable(f, "\t", '\"');
                Assert.IsTrue(data.Rows.Count > 1600);
                var data2 = Util.GetCsvDataTable(f, "\t", '\"', inpdata: data);
                Assert.IsTrue(data2.Rows.Count > 3200);
            }
        }

        [TestMethod]
        public void TestConvertoDate()
        {
            var dt1 = "2010-01-01";
            var ddt1 = Util.ConvertoDateTimeLoose(dt1);

            var dt2 = "2010-01-01-23.11.56.123123";
            var ddt2 = Util.ConvertoDateTime(dt2);

            var dt3 = "2010-01-01";
            var ddt3 = Util.ConvertoDateTime(dt3);

            var dt4 = "2010-01-01-23";
            try
            {
                var ddt4 = Util.ConvertoDateTime(dt4);
            }
            catch (Exception)
            {

            }


            var dt5 = "2010-01";
            try
            {
                var ddt5 = Util.ConvertoDateTime(dt5);
            }
            catch (Exception)
            {

            }


            var dt6 = "2010-01-01-23.11.56";
            try
            {
                var ddt6 = Util.ConvertoDateTime(dt6);
            }
            catch (Exception)
            {

            }


        }

        [TestMethod]
        public void TestSetContentDataType()
        {
            var f = Path.Combine(this.testdir, "test1.csv");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                f = fi.FullName;
                var dt = Util.GetCsvDataTable(f, ",", '\"');
                Assert.IsTrue(dt.Rows.Count > 15);
                Assert.IsTrue(dt.Columns.Count == 5);
                var dt2 = Util.SetContentDataType(dt, 100, null, true);
                Assert.AreEqual(dt.Columns.Count, dt2.Columns.Count);
                Assert.AreEqual(dt.Rows.Count, dt2.Rows.Count);
                Assert.AreEqual(dt2.Columns[2].DataType, typeof(System.Int32));
                Assert.AreEqual(dt2.Columns[3].DataType, typeof(System.Double));
                Assert.AreEqual(dt2.Columns[4].DataType, typeof(System.String));

                var dt3 = Util.SetContentDataType(dt, 5, null, true);
                Assert.AreEqual(dt.Columns.Count, dt3.Columns.Count);
                Assert.AreEqual(dt.Rows.Count, dt3.Rows.Count);
                Assert.AreEqual(dt3.Columns[2].DataType, typeof(System.Int32));
                Assert.AreEqual(dt3.Columns[3].DataType, typeof(System.Int32));
                Assert.AreEqual(dt3.Columns[4].DataType, typeof(System.String));


                var collist = new List<string>() { "FAMILIE", "VAR2" };
                var dt4 = Util.SetContentDataType(dt, 5, collist, true);
                Assert.AreEqual(5, dt4.Columns.Count);
                Assert.AreEqual(dt.Rows.Count, dt4.Rows.Count);
                Assert.AreEqual(dt4.Columns[3].DataType, typeof(System.Int32));

                var dt5 = Util.SetContentDataType(dt, 5, collist);
                Assert.AreEqual(2, dt5.Columns.Count);
                Assert.AreEqual(dt.Rows.Count, dt5.Rows.Count);
                Assert.AreEqual(dt5.Columns[1].DataType, typeof(System.Int32));
            }
        }

        [TestMethod]
        public void TestSetContentDataTypeBig()
        {
            var f = Path.Combine(this.testdir, "130425_St_AUSW_B.csv");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                f = fi.FullName;
                var dt = Util.GetCsvDataTable(f, ";", '\"');
                Assert.IsTrue(dt.Rows.Count > 3000);
                Assert.IsTrue(dt.Columns.Count > 20);
                var dt2 = Util.SetContentDataType(dt, 0, null, true);
            }
        }

        [TestMethod]
        public void TestSetContentDataTypeExcel()
        {
            var f = Path.Combine(this.testdir, "check1.xlsx");
            var fi = new FileInfo(f);
            if (fi.Exists)
            {
                // var ea = new ExcelDataSet(fi.FullName);
                // ea.Load2Dataset();
                // Assert.AreEqual(7, ea.ExcelDB.Tables.Count);

                // var dt = ea.GetDataTable("BEILOPER");
                // var dt2 = Util.SetContentDataType(dt, 0, null, true);
            }
        }

        [TestMethod]
        public void TestGetApplicationFolderName()
        {
            String x = Util.GetApplicationFolderName();
            Assert.IsTrue(x.ToLower().Contains("debug"));
        }

        [TestMethod]
        public void TestExtensions()
        {
            Assert.IsTrue("".IsEmpty());
            Assert.AreEqual("321", "123".Reverse());
            Assert.AreEqual("321", "3210 0".RtrimSet("0 "));
            Assert.AreEqual("3210", "0 03210".LtrimSet("0 "));
            Assert.AreEqual("321", "0 03210 ".AlltrimSet("0 "));
            Assert.AreEqual("32", "3210 ".Left(2));
            Assert.AreEqual("10 ", "3210 ".Right(3));
            Assert.AreEqual("00AAB00BA0 ", "0011200210 ".Translate("12", "AB"));
            Assert.AreEqual("1111", "11  11".ReplaceRegExp(@"\s+", ""));
            var sl = new List<string>() { "11  11", "22  22" };
            var slx = new List<string>() { "1111", "2222" };
            var slc = sl.ReplaceRegExp(@"\s+", "");
            CollectionAssert.AreEqual(slx, slc);
            Assert.IsTrue("1111".IsIn(sl.ToArray()));

            var dirinfo = new DirectoryInfo(testdir);
            var pdict = dirinfo.GetPropertiesDictionary();
            Assert.IsTrue(pdict.Count > 5);
            Assert.AreEqual(pdict["Name"].ToString(), "TestData", ignoreCase:true);
        }

        [TestMethod]
        public void TestStringCompression()
        {
            var txt = LoadTestStrings.Text;
            var tx1 = Util.Compress(txt);
            var tx2 = Util.Decompress(tx1);
            Assert.AreEqual(txt, tx2);
            Assert.AreEqual(tx1, txt.Compress());
            Assert.AreEqual(tx2, tx1.Decompress());
        }

    }
}
