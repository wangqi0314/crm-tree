using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Shinfotech.Tools
{
    public class KeywordsFilter
    {
        #region 全局脏词过滤
        public Hashtable FilterWords = new Hashtable();

        public KeywordsFilter()
        {
        }

        public void AddFilterWords(string[] words)
        {
            foreach (string word in words)
            {
                if (!word.Equals(",") && word.Trim().Length>0)
                {
                    AddFilterWord(word); 
                }
            }
        }

        public void AddFilterWord(string word)
        {
            Hashtable h = FilterWords;
            foreach (char c in word.ToUpper())
            {
                if (!h.ContainsKey(c)) h[c] = new Hashtable();
                h = h[c] as Hashtable;
            }
            h[0] = new Hashtable();
        }
        public int Match(string content, int index, out StringBuilder sbWords)
        {
            content = content.ToUpper();
            //alt = new StringBuilder();
            bool filterChar = true;
            Hashtable h = FilterWords;
            int i = index;
            sbWords = new StringBuilder();
            for (; i < content.Length; i++)
            {
                char c = content[i];

                switch (c)
                {
                    case '<':
                        {
                            filterChar = false;
                            break;
                        }
                    case '>':
                        {
                            filterChar = true;
                            break;
                        }
                    case ' ':
                        {
                            break;
                        }
                    default:
                        {
                            if (filterChar)
                            {
                                if (h.ContainsKey(c))
                                {
                                    h = h[c] as Hashtable;
                                    sbWords.Append(c.ToString());
                                }
                                else
                                {
                                    if (!h.ContainsKey(0)) return -1;
                                }
                            }
                            break;
                        }
                }
                if (h.ContainsKey(0)) return i;
            }

            return h.ContainsKey(0) ? i : -1;
        }
        /// <summary>
        /// 系统脏词过滤
        /// </summary>
        /// <param name="strContent">要过滤的原内容</param>
        /// <returns>返回脏词,若为空,则没有脏词</returns>
        public List<string> WordsFilter(string strContent)
        {

            lock (FilterWords)
            {
                //StringBuilder result = new StringBuilder();
                bool filterChar = true;
                List<string> listWords = new List<string>();
                StringBuilder sbWords = new StringBuilder();
                for (int i = 0; i < strContent.Length; i++)
                {
                    char c = strContent[i];
                    switch (c)
                    {
                        case '<':
                            {
                                filterChar = false;
                                break;
                            }
                        case '>':
                            {
                                filterChar = true;
                                break;
                            }
                        default:
                            {
                                if (filterChar)
                                {
                                    int fi = Match(strContent, i, out sbWords);
                                    if (fi != -1 && !listWords.Contains(sbWords.ToString()))
                                    {
                                        listWords.Add(sbWords.ToString());
                                        i = fi;
                                        continue;
                                    }
                                }
                                break;
                            }
                    }
                }
                return listWords;
            }
        }
        #endregion

        #region 过滤关键字公用方法
        /// <summary>
        /// 外部调用过滤关键字
        /// </summary>
        /// <param name="contens">待过滤的内容</param>
        /// <param name="keyword">关键字,号分隔</param>
        /// <returns>存在的关键字</returns>
        public string FilterKeywords(string contens, string keyword)
        {
            var Error = string.Empty;
            if (contens.Length > 0 && keyword.Length > 0)
            {
                contens = contens.TrimEnd(',');
                //将关键词存进哈希表
                AddFilterWords(keyword.Split(','));
                List<string> list = WordsFilter(contens);
                if (list.Count > 0)
                {
                    StringBuilder sb_Filter = new StringBuilder();

                    for (int i = 0; i < list.Count; i++)
                    {
                        sb_Filter.Append(list[i]);
                        sb_Filter.Append(",");
                    }

                    Error = sb_Filter.ToString().Substring(0, sb_Filter.Length - 1);

                }
            }
            return Error;
        }
        #endregion
    }


}

