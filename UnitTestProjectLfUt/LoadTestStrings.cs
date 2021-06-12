
using LfUtilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProjectLfUt
{
    public class LoadTestStrings
    {

        private static Exception ex;
        private static Dictionary<string, string> textColl;
        private static string textfile = "..\\..\\..\\TestData\\TestText.txt";
        private static string testdir;
        private static string text;
        private static bool debugflag = false;

        public static Exception Ex { get => ex; set => ex = value; }
        public static string Textfile { get => textfile; set => textfile = value; }
        public static string Text { get => text; set => text = value; }
        public static string Testdir { get => testdir; set => testdir = value; }

        private static void addText(string s, string k)
        {
            if (textColl.ContainsKey(k))
            {
                textColl.Remove(k);
            }

            textColl.Add(k, s);
        }

        public static string GetText(string k)
        {
            if ((textColl == null))
            {                
                LoadTestStrings.Load();
            }

            string r = "";
            try
            {
                r = textColl[k];
            }
            catch (Exception ex)
            {
                LoadTestStrings.ex = ex;                
            }

            return r;
        }

        public static void Load()
        {
            string f = textfile;
            string s = "";
            bool textflag = false;
            string[] m;
            string k = "";
            int nit = 0;
            if (!System.IO.File.Exists(f))
            {
                return;
            }
            var wfi = new FileInfo(f);
            Testdir = wfi.DirectoryName;
            textColl = new Dictionary<string, string>();
            Text = File.ReadAllText(f);
            var ll = LfUtilities.Util.GetListOfLinesFromFile(f);

            foreach (string wl in ll)
            {
                nit++;
                if (textflag)
                {
                    if ((wl.Trim().ToUpper() == "#ENDTEXT"))
                    {
                        textflag = false;
                        s = LfUtilities.Util.AlltrimSet(s, (" " + ('\t' + "\r\n")));
                        if ((!Util.IsEmpty(k)
                                    && !Util.IsEmpty(s)))
                        {
                            LoadTestStrings.addText(s, k);
                        }

                        k = null;
                        m = null;
                    }
                    else
                    {
                        s = (s + (wl + "\r\n"));
                    }
                }
                else if (wl.StartsWith("#TEXT"))
                {
                    textflag = true;
                    m = LfUtilities.Util.Matches(wl, "\\#TEXT\\s+ID\\s*\\=\\s*(\\w*)\\s*", false);
                    if (m != null)
                    {
                        if ((m.Length > 1))
                        {
                            k = m[1].Trim().ToUpper();
                            s = "";
                        }

                    }

                }
            }

        }

    }

}
