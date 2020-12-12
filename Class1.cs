using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace authpr
{
    class Class2
    {
        //public static NHunspell.Hunspell hunspl;
        /*private static string mModelPath = (AppDomain.CurrentDomain.BaseDirectory + "/Models/");
        static private OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer mTokenizer;
        static private OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger mPosTagger;
        static public string[] TokenizeSentence(string sentence)
        {
            if (mTokenizer == null)
            {
                mTokenizer = new OpenNLP.Tools.Tokenize.EnglishMaximumEntropyTokenizer(mModelPath + "EnglishTok.nbin");
            }

            return mTokenizer.Tokenize(sentence);
        }
        static public string[] PosTagTokens(string[] tokens)
        {
            if (mPosTagger == null)
            {
                mPosTagger = new OpenNLP.Tools.PosTagger.EnglishMaximumEntropyPosTagger(mModelPath + "EnglishPOS.nbin", mModelPath + @"\Parser\tagdict");
            }

            return mPosTagger.Tag(tokens);
        }*/
        List<string> FreqTopics = new List<string>();
        List<string> LeastTopics = new List<string>();
        List<string> FreqWords = new List<string>();
        List<string> FavWordsPair = new List<string>();
        List<string> FavStopWrds = new List<string>();
        List<char> FavPunc = new List<char>();
        List<string> FavPOSPair = new List<string>();

        List<string> FreqTopics1 = new List<string>();
        List<string> LeastTopics1 = new List<string>();
        List<string> FreqWords1 = new List<string>();
        List<string> FavWordsPair1 = new List<string>();
        List<string> FavStopWrds1 = new List<string>();
        List<char> FavPunc1 = new List<char>();
        List<string> FavPOSPair1 = new List<string>();

        List<string> FreqTopics2 = new List<string>();
        List<string> LeastTopics2 = new List<string>();
        List<string> FreqWords2 = new List<string>();
        List<string> FavWordsPair2 = new List<string>();
        List<string> FavStopWrds2 = new List<string>();
        List<char> FavPunc2 = new List<char>();
        List<string> FavPOSPair2 = new List<string>();

        List<string> wrds = new List<string>();
        List<string> wrdsPairs = new List<string>();

        double MainPer = 42;
        double MinPer = 35;
        double percentage = 0.0;

        public bool FilePass(List<string> a, bool recalled)
        {
            int half = (int)a.Count() / 2 ; // sentence at half index will be sahred in all partitions
            string para = "";
            string para1 = "";
            string para2 = "";

            for (int h = 0; h < a.Count(); h++)
            {
                para += a[h];
            }
            for (int i = 0; i <= half; i++)
            {
                para1 += a[i];
            }
            for (int j = half; j < a.Count(); j++)
            {
                para2 += a[j];
            }

            FreqTopics.Clear();
            LeastTopics.Clear();
            FreqWords.Clear();
            FavWordsPair.Clear();
            FavStopWrds.Clear();
            FavPunc.Clear();
            FavPOSPair.Clear();

            FreqTopics1.Clear();
            LeastTopics1.Clear();
            FreqWords1.Clear();
            FavWordsPair1.Clear();
            FavStopWrds1.Clear();
            FavPunc1.Clear();
            FavPOSPair1.Clear();

            FreqTopics2.Clear();
            LeastTopics2.Clear();
            FreqWords2.Clear();
            FavWordsPair2.Clear();
            FavStopWrds2.Clear();
            FavPunc2.Clear();
            FavPOSPair2.Clear();


            FavStopWrds = GetFreqStopWrds(para);
            LeastTopics = GetLeastFreqTopics(para);
            FreqTopics = GetMostFreqWordTopics();
            FreqWords = GetMostFreqWords(para);
            FavWordsPair = GetMostFreqWordPairs(a);
            //FavPunc = GetFavNonAlphaNumericChar(para);
            if (!recalled)
            //{
            FavPunc = GetFavNonAlphaNumericChar(para);
            //    FavPOSPair = GetFavPOSPair(a);
            //}
            
            FavStopWrds1 = GetFreqStopWrds(para1);
            LeastTopics1 = GetLeastFreqTopics(para1);
            FreqTopics1 = GetMostFreqWordTopics();
            FreqWords1 = GetMostFreqWords(para1);
            FavWordsPair1 = GetMostFreqWordPairs(a.GetRange(0, half + 1));
            //FavPunc1 = GetFavNonAlphaNumericChar(para1);
            if (!recalled)
            //{
            FavPunc1 = GetFavNonAlphaNumericChar(para1);
            //    FavPOSPair1 = GetFavPOSPair(a.GetRange(0, half + 1));
            //}
           
            FavStopWrds2 = GetFreqStopWrds(para2);
            LeastTopics2 = GetLeastFreqTopics(para2);
            FreqTopics2 = GetMostFreqWordTopics();
            FreqWords2 = GetMostFreqWords(para2);
            FavWordsPair2 = GetMostFreqWordPairs(a.GetRange(half, (a.Count() - half)));
            //FavPunc2 = GetFavNonAlphaNumericChar(para2);
            if (!recalled)
            //{
            FavPunc2 = GetFavNonAlphaNumericChar(para2);
            //    FavPOSPair2 = GetFavPOSPair(a.GetRange(half, (a.Count() - half)));
            //}


            bool Res1 = GetMatchResult(LeastTopics, FreqTopics, FreqWords, FavWordsPair, FavStopWrds, FavPunc, FavPOSPair, LeastTopics1, FreqTopics1, FreqWords1, FavWordsPair1, FavStopWrds1, FavPunc1, FavPOSPair1, recalled, a.Count());
            bool Res2 = GetMatchResult(LeastTopics, FreqTopics, FreqWords, FavWordsPair, FavStopWrds, FavPunc, FavPOSPair, LeastTopics2, FreqTopics2, FreqWords2, FavWordsPair2, FavStopWrds2, FavPunc2, FavPOSPair2, recalled, a.Count());

            if (Res1 && Res2 && a.Count() >= 32/*33/*35*/ && !recalled)
            {
                bool Res3 = FilePass(a.GetRange(0, half + 1), true);
                bool Res4 = FilePass(a.GetRange(half, (a.Count() - half)), true);

                if (Res3 == false && Res4 == false)
                    return true;
                //return !(Res1 & Res2);
                return false;
               // return !(Res3 & Res4);
            }

            if (!recalled)
            {
                if (Res1 == false || Res2 == false)
                    return true;
                //return !(Res1 & Res2);
                return false;
            }
            else
            {
                if (Res1 == true && Res2 == true)
                    return true;
                return false;
            }
            //return !(Res1 & Res2);

        }



        public bool GetMatchResult(List<string> FT, List<string> LT, List<string> FW, List<string> FWP, List<string> STW, List<char> FPunc,List<string> FPOS,
                              List<string> FT1, List<string> LT1, List<string> FW1, List<string> FWP1, List<string> STW1, List<char> FPunc1, List<string> FPOS1, bool half, int sentences)
        {
            
            double total_score = 0.0;
            percentage = 0.0;
            int k = 0;
            for (k = 0; k < LT.Count(); k++)
            {
                try
                {
                    if (LT.Contains(LT1.ElementAt(k)))
                        total_score += 1.5;
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            if (k == 0) { k = 1; }
            percentage = (double)((total_score / k) * 10);

            total_score = 0;
            for (k = 0; k < FT.Count(); k++)
            {
                try
                {
                    if (FT.Contains(FT1.ElementAt(k)))
                        total_score += 1.25;//1.05;
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            if (k == 0) { k = 1; }
            percentage += (double)((total_score / k) * 10);

            total_score = 0;
            for (k = 0; k < FW.Count(); k++)
            {
                try
                {
                    if (FW1.Contains(FW.ElementAt(k)))
                        total_score += 0.45;//0.60;//0.48;
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            if (k == 0) { k = 1; }
            percentage += (double)((total_score / k) * 10);

            total_score = 0;
            for (k = 0; k < FWP.Count(); k++)
            {
                try
                {
                    if (FWP1.Contains(FWP.ElementAt(k)))
                        total_score += 2.1;//1.33;
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            if (k == 0) { k = 1; }
            percentage += (double)((total_score / k) * 10);

            total_score = 0;
            int l = 0;
            if (STW.Count() == 0 && STW1.Count() == 0)
            { total_score += 1.5; k = 1; }
            else
            {
                for (k = 0; k < STW.Count() && l < STW1.Count(); k++, l++)
                {
                    try
                    {
                        if (STW1.Contains(STW.ElementAt(k)))
                            total_score += 1.75;//1.65;//1.5;
                        //if (STW.ElementAt(k) == STW1.ElementAt(l))
                        //    total_score += 2.75;

                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            }
            if (k == 0) { k = 1; }
            percentage += (double)((total_score / k) * 10);

            total_score = 0;
            l = 0;
            for (k = 0; k < 2 ; k++)
                {
                    try
                    {
                        //if (FPunc.Contains(FPunc1.ElementAt(k)))
                        //    total_score += 0.15;
                        if (FPunc.ElementAt(k) == FPunc1.ElementAt(k))
                            total_score += 0.50;

                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            if (k == 0) { k = 1; }
            percentage += (double)((total_score / k) * 10);

            total_score = 0;
            for (k = 0; k < FPOS.Count(); k++)
            {
                try
                {
                    if (FPOS.Contains(FPOS1.ElementAt(k)))
                        total_score += 0.100000;
                    //if (STW.ElementAt(k) == STW1.ElementAt(l))
                    //    total_score += 2.75;

                }
                catch (Exception ex)
                {
                    break;
                }
            }
            if (k == 0) { k = 1; }
            percentage += (double)((total_score / k) * 10);

            if (!half)
            {
                //if (sentences > 35)//25)
                //    MainPer = 30;
                //else
                MainPer = 23;//22;//35;//40;//35;//40;//42;
            }
            else
            {
                MainPer = 46;//46;//50;//55;//25;//30;
                if (sentences >= 23)
                {
                    MainPer = 48.5;
                }

            }
            return (percentage >= MainPer);
                //return true; // true means plagiarised
            
        }

        void adjustThreshold(double percentage, int docLen)
        {
            if (docLen >= 35 && docLen < 50)
                MinPer = 30;
            else if (docLen >= 50 && docLen < 65)
                MinPer = 32.5;
            else
                MinPer = 35;

            if (percentage >= 50)
            {
                MinPer += (int)(percentage - 45);
            }
        }

        List<string> GetFreqStopWrds(string para)
        {
            string pat = @"\b(?:the|of|and|a|to|in|is|it|that|you|for|have|I|not|on|\d+)\b";
            var para1 = Regex.Matches(para, pat, RegexOptions.IgnoreCase).Cast<Match>().Aggregate("", (s, e) => s + " " + e.Value, s => s);
            //string[] LFW = new string[20];
            Dictionary<string, int> chardic = new Dictionary<string, int>();
            List<string> lf = new List<string>();
            string[] str = para1.Split(' ');

            foreach (string aa in str)
            {
                string ab = aa.Trim('.', '?', ';', ',', '"', '!', '(', ')', ' ').ToLower();
                if (chardic.ContainsKey(ab))
                    chardic[ab] += 1;
                else
                    chardic[ab] = 1;
            }
            try
            {
                var sortedDict = from entry in chardic orderby entry.Value descending select entry.Key;
                wrds.Clear();
                wrds = sortedDict.ToList<string>();
                int count = 0;
                for (int i = 0; i < wrds.Count() && count < 15; i++, count++)
                {

                    lf.Add(wrds.ElementAt(i));

                }
                return lf;
                //sortedDict.ToList<string>();

            }
            catch (Exception ex)
            { return lf; }

        }

        List<string> GetFavPOSPair(List<string> str)
        {
            Dictionary<string, int> chardic = new Dictionary<string, int>();
            //string[] str = SentencWindow[currentindex].Split(' ');
            List<string> lf = new List<string>();
            //int cnt = str.Count() - 1;
            //foreach (string s in str)
            //{
            //    if (s == "" || s == " ")
            //        continue;
            //    string[] tokens = TokenizeSentence(s);
            //    string[] tags = PosTagTokens(tokens);
            //    for (int i = 0; i < tags.Count() - 1; i++)
            //    {
            //        string aa = tags[i] + " " + tags[i + 1];
            //        if (chardic.ContainsKey(aa))
            //            chardic[aa] += 1;
            //        else
            //            chardic[aa] = 1;
            //    }
            //}
            //try
            //{
            //    var sortedDict = from entry in chardic orderby entry.Value descending select entry.Key;
            //    wrds.Clear();
            //    //chardic = sortedDict.ToDictionary(x => x, y => y);
            //    wrds = sortedDict.ToList<string>();
            //    int count = 0;
            //    for (int i = 0; i < 3; i++)
            //    {
            //        lf.Add(wrds.ElementAt(i));
            //    }
                
                
            //}
            //catch (Exception ex)
            //{  }
            return lf;
        }

        List<string> GetLeastFreqTopics(string para)
        {
            Regex replacer = new Regex(@"\b(?:the|your|mine|you've|I've|into|being|must|many|more|too|haven't|shouldn't|hasn't|hadn't|wasn't|can't|isn't|couldn't|shalln't|don't|there's|that's|n't|will|would|shall|should|not|nor|no|yes|on|how|very|much|we|I|you|he|my|which|with|-|where|her|him|his|she|they|them|their|is|was|were|are|of|an|also|be|there|do|did|done|so|a|on|this|these|in|from|all|to|that|but|been|as|by|had|has|if|at|may|might|ought|n't|have|who|it|it's|what|and|or|for|„s|how|can|could|if|else|\d+)\b", RegexOptions.IgnoreCase);

            string para1 = replacer.Replace(para, "");
            //string[] LFW = new string[20];
            Dictionary<string, int> chardic = new Dictionary<string, int>();
            List<string> lf = new List<string>();
            string[] str = para1.Split(' ');

            foreach (string aa in str)
            {
                string ab = aa.Trim('.', '?', ';', ',', '"', '!', '(', ')', ' ').ToLower();
                if (chardic.ContainsKey(ab))
                    chardic[ab] += 1;
                else
                    chardic[ab] = 1;
            }
            try
            {
                var sortedDict = from entry in chardic orderby entry.Value ascending select entry.Key;
                wrds.Clear();
                wrds = sortedDict.ToList<string>();
                int count = 0;
                for (int i = 0; i < wrds.Count() && count < 20; i++, count++)
                {

                    lf.Add(wrds.ElementAt(i));

                }
                return lf;
                //sortedDict.ToList<string>();

            }
            catch (Exception ex)
            { return lf; }

        }

        List<string> GetMostFreqWordTopics()
        {
            List<string> lf = new List<string>();
            try
            {

                int count = 0;
                for (int i = wrds.Count() - 1; i >= 0 && count < 20; i--, count++)
                {

                    lf.Add(wrds.ElementAt(i));

                }
                return lf;
                //sortedDict.ToList<string>();

            }
            catch (Exception ex)
            { return lf; }
        }
        List<string> GetMostFreqWords(string para)
        {
            Dictionary<string, int> chardic = new Dictionary<string, int>();
            List<string> lf = new List<string>();
            string[] str = para.Split(' ');

            foreach (string aa in str)
            {
                string ab = aa.Trim('.', '?', ';', ',', '"', '!', '(', ')', ' ').ToLower();
                if (chardic.ContainsKey(ab))
                    chardic[ab] += 1;
                else
                    chardic[ab] = 1;
            }
            try
            {
                var sortedDict = from entry in chardic orderby entry.Value descending select entry.Key;
                wrds.Clear();
                wrds = sortedDict.ToList<string>();
                int count = 0;
                for (int i = 0; i < wrds.Count() && count < 40; i++, count++)
                {

                    lf.Add(wrds.ElementAt(i));

                }
                return lf;
                //sortedDict.ToList<string>();

            }
            catch (Exception ex)
            { return lf; }

        }

         List<char> GetFavNonAlphaNumericChar(string para)
        {
            Dictionary<char, int> chardic = new Dictionary<char, int>();
            List<char> ls = new List<char>();
            var str = new string((from c in para where char.IsSymbol(c) || char.IsPunctuation(c) select c).ToArray());
            if (str != "")
            {
                foreach (char aa in str.ToCharArray())
                {
                    if (aa != '.')
                    {

                        if (chardic.ContainsKey(aa))
                            chardic[aa] += 1;
                        else
                            chardic[aa] = 1;
                        
                    }
                }

                var sortedDict = from entry in chardic orderby entry.Value descending select entry.Key;
                ls = sortedDict.ToList<char>();
                
              }
                      
            return ls;
         }

        List<string> GetMostFreqWordPairs(List<string> para)
        {
            Dictionary<string, int> chardic = new Dictionary<string, int>();
            List<string> lf = new List<string>();
            foreach (string sentence in para)
            {
                string sentence1 = sentence.Trim('.', '?', '!').ToLower();
                string[] tags = sentence1.Split(' ');
                for (int i = 0; i < tags.Count() - 1; i++)
                {
                    string aa = tags[i] + tags[i + 1];
                    if (chardic.ContainsKey(aa))
                        chardic[aa] += 1;
                    else
                        chardic[aa] = 1;
                }
            }
            try
            {
                var sortedDict = from entry in chardic orderby entry.Value descending select entry.Key;
                wrdsPairs.Clear();
                wrdsPairs = sortedDict.ToList<string>();
                int count = 0;
                for (int i = 0; i < wrdsPairs.Count() && count < 30; i++, count++)
                {

                    lf.Add(wrdsPairs.ElementAt(i));

                }
                return lf;
                //sortedDict.ToList<string>();

            }
            catch (Exception ex)
            { return lf; }
        }

    }
}
