using System;
using System.Configuration;
using System.Text;
using System.Data;

namespace Shinfotech.Tools
{
    /// <summary>
    /// Assistant ��ժҪ˵����
    /// </summary>
    public sealed class Assistant
    {

        #region

        /// <summary>
        /// ���ַ���������õ����涨�������ַ���.
        /// </summary>
        /// <param name="allChar"></param>
        /// <param name="CodeCount"></param>
        /// <returns></returns>
        private string GetRandomCode(string allChar, int CodeCount)
        {
            //string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"; 
            string[] allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < CodeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(allCharArray.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }

                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }

        #endregion

        #region �������ָ�������ַ�������Сд��ĸ���ֻ�ϣ�
        /// <summary>
        /// ����Դ
        /// </summary>
        private static char[] constant =   
       {   
        '0','1','2','3','4','5','6','7','8','9',   
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',   
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'   
        };
        /// <summary>
        /// �������ָ�������ַ�������
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
        #endregion �������ָ�������ַ�������Сд��ĸ���ֻ�ϣ�
    }
}
