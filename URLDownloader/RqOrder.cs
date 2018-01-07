using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLDownloader
{
    class RqOrder
    {
        private String Seris_Title;
        private String Page1URl;
        private String Download_Path;
        private bool ROType;

        private bool isSupplementForSameSeries = false;

        public static bool BASE_ON_ALL_PAGEURL = true;
        public static bool BASE_ON_FINAL_EPNUM = false;
        private  List<String> URLs;
        public RqOrder(String STitle, String P1Url, String DPath,bool dlRule)
        {
            Seris_Title = STitle;
            Page1URl = P1Url;
            Download_Path = DPath;
            ROType = dlRule;
            URLs = new List<String>();
        }
        public RqOrder(String STitle, String P1Url, String DPath, bool dlRule,bool isSupplement)
        {
            isSupplementForSameSeries = isSupplement;
            Seris_Title = STitle;
            Page1URl = P1Url;
            Download_Path = DPath;
            ROType = dlRule;
            URLs = new List<String>();
        }

        public bool getIssupplement()
        {
            return isSupplementForSameSeries;
        }

        public void addURLs(String Url)
        {
            URLs.Add(Url);

        }
        public void addURLs(List<String> SList)
        {
            URLs = SList;
        }
        public String getURLs(int index)
        {
            Console.Out.WriteLine("Output limit is "+URLs.Count+" and Current Request :"+index);
            return URLs[index];
        }
        public String getTitleFileName()
        {
            return Seris_Title;
        }
        public String getFirstPageUrl()
        {
            return Page1URl;
        }
        public String getPath()
        {
            return Download_Path;
        }
        public int getAllPageNumber()
        {
            if (ROType == BASE_ON_ALL_PAGEURL)
            {
                return 1 + URLs.Count;
            }
            else
            {
                Int16 FpageNum = Convert.ToInt16(getTFFileName(1));
                Int16 LastPageNum = Convert.ToInt16(URLs[0]);
                if (FpageNum == 0)
                {
                    return LastPageNum;
                }
                else
                {
                    return LastPageNum - (FpageNum);
                }
                 
                

            }
            
        }
        public string getFileName(int fnameType,int Urlsindex)
        {
            //傳回內容 1-正名,2-副名,3-正+副名
            string[] urlTostrings = URLs[Urlsindex].Split('/');
            string target = urlTostrings[urlTostrings.Length - 1];
            string result="";
            
            switch(fnameType)
            {
                case 1:
                    result = target.Split('.')[0];
                    break;
                case 2:
                    result = target.Split('.')[1];
                    break;
                case 3:
                    result = target;
                    break;
                default:
                    result = target;
                    break;
            }
            return result;
        }
        public string getTFFileName(int fnameType)
        {
            //傳回內容 1-正名,2-副名,3-正+副名
            string[] urlTostrings = Page1URl.Split('/');
            string target = urlTostrings[urlTostrings.Length - 1];
            string result = "";

            switch (fnameType)
            {
                case 1:
                    result = target.Split('.')[0];
                    break;
                case 2:
                    result = target.Split('.')[1];
                    break;
                case 3:
                    result = target;
                    break;
                default:
                    result = target;
                    break;
            }
            return result;
        }

    }
}
