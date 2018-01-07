using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Threading;

namespace URLDownloader
{
    
    class UDdler
    {
        private RqOrder pack;
        public bool isDlFinished=false;
        public bool hasStarted = false;
        
        private int DlTotalPrg;
        private int DlProgress;
        private int StartNum=1;
        private List<String> PicSname = new List<string>();
        WebClient wc;



        public UDdler()
        {
            PicSname.AddRange(new string[] {"png","jpg","bmp","gif"});
            isDlFinished = false;
            hasStarted = false;
            DlTotalPrg = 0;
            DlProgress = 0;
        }

        public UDdler(RqOrder RO)
        {
            PicSname.AddRange(new string[] { "png", "jpg", "bmp", "gif" });
            pack = RO;
            isDlFinished = false;
            hasStarted = false;
            DlTotalPrg= RO.getAllPageNumber();
            DlProgress =0;
            wc = new WebClient();
        }

        public void doDownloadMethod(int method,bool isLowSpeedToDebug)
        {
            //1-AllUrl 2-FinalPN
            hasStarted = true;
            int targetNum=0;
            int SupNum = 0;//跨檔數,限supplement下載模式使用
            switch (method)
            {
                case 1:
                    checkDirectoryExist(pack.getPath() + "\\" + pack.getTitleFileName() + "\\");
                    DlProgress = 0;

                    /*
                     * 附註:請確認好用做檢查目標原尾數的目標資料夾是否存在、該資料夾內部是否存在可轉成INT的檔案名
                     */
                    if(pack.getIssupplement())
                    {

                        String [] targetDLArray = Directory.GetFiles(pack.getPath() + "\\" + pack.getTitleFileName() + "\\");
                        List<String> targetDyList = targetDLArray.ToList();
                        targetDyList.Sort();

                        Console.Out.WriteLine("Bese on supplementMode ,we get the SuoNum : "+ targetDyList[targetDyList.Count - 1]);
                        SupNum = Convert.ToInt16(Path.GetFileName(targetDyList[targetDyList.Count - 1]).Split('.')[0]);
                        targetNum = pack.getAllPageNumber()-1;
                        Console.Out.WriteLine(pack.getFirstPageUrl());
                        wc.DownloadFile(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(StartNum+SupNum) + "." + pack.getTFFileName(2));
                    }
                    else
                    {
                        targetNum = pack.getAllPageNumber()-1   ;
                        Console.Out.WriteLine(pack.getFirstPageUrl());
                        wc.DownloadFile(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + "1" + "." + pack.getTFFileName(2));
                    }



                    StartNum = 0;
                    for (int num = StartNum; num<targetNum; num++ )
                    {
                        //Console.Out.WriteLine("From:" + pack.getURLs(num));
                        //Console.Out.WriteLine("File:" + pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(num) +"."+ pack.getFileName(2, num));
                        DlProgress++;
                       Console.Out.WriteLine("DlProgress" + DlProgress + "/Total:" + DlTotalPrg + "/Num:" + num);
                       

                        if(pack.getIssupplement())
                        {
                            wc.DownloadFile(pack.getURLs(num), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(num + 1+SupNum) + "." + pack.getFileName(2, num));
                            Console.Out.WriteLine(pack.getURLs(num));
                        }
                        else
                        {
                            wc.DownloadFile(pack.getURLs(num), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(num + 1) + "." + pack.getFileName(2, num));
                            Console.Out.WriteLine(pack.getURLs(num));
                        }


                        
                        //Console.Out.WriteLine((decimal)DlProgress / (decimal)DlTotalPrg);
                        //Console.Out.WriteLine("Progress:" + Convert.ToString((100.0*(DlProgress/DlTotalPrg)))); 
                        Thread.Sleep((isLowSpeedToDebug? 5000:1000));
                    }
                    isDlFinished = true;

                    break;
                case 2:
                    checkDirectoryExist(pack.getPath() + "\\" + pack.getTitleFileName() + "\\");
                    DlProgress = 1;

                    if (pack.getIssupplement())
                    {
                        String[] targetDLArray = Directory.GetFiles(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\");
                        List<String> targetDyList = targetDLArray.ToList();
                        targetDyList.Sort();

                        StartNum = Convert.ToInt16(targetDyList[targetDyList.Count - 1])+1;
                        targetNum = StartNum + Convert.ToInt16(pack.getURLs(0));

                        downloadConservalty(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(StartNum) + "." + pack.getTFFileName(2), pack.getTFFileName(2));

                    }
                    else
                    {
                        StartNum = Convert.ToInt16(pack.getTFFileName(1));
                        targetNum = Convert.ToInt16(pack.getURLs(0));

                        downloadConservalty(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(StartNum) + "." + pack.getTFFileName(2), pack.getTFFileName(2));
                    }

                    

                    //
                    String toFormat="";
                    for(int tar=0;tar<pack.getTFFileName(1).Length;tar++)
                    {
                        toFormat += "0";
                    }


                    string WebpageURL = pack.getFirstPageUrl().Substring(0, pack.getFirstPageUrl().LastIndexOf('/')+1);
                    for (int num = StartNum + 1; num <= targetNum; num++)
                    {

                        decimal turndNum = Convert.ToDecimal(num);
                        DlProgress++;
                        Console.Out.WriteLine("DlProgress" + DlProgress + "/Total:" + DlTotalPrg);
                        Console.Out.WriteLine(WebpageURL + turndNum.ToString(toFormat) + "." + pack.getTFFileName(2));
                        Console.Out.WriteLine("using Conservalty Download with subline:"+ pack.getTFFileName(2));
                        downloadConservalty(WebpageURL + turndNum.ToString(toFormat) + "." + pack.getTFFileName(2), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(num) + "." + pack.getTFFileName(2), pack.getTFFileName(2));
                        Thread.Sleep((isLowSpeedToDebug ? 5000 : 1000));
                    }
                    isDlFinished = true;
                    break;
                default:
                    break;
            }
            
        }

        private void downloadConservalty(string address, string filename, string subname)
        {

            List<string> localPSname = PicSname;
            bool needChangeSubName = false;
            Console.Out.WriteLine("LocalPSname is Removeable: "+localPSname.Remove(subname));
            string cuttedSubNamesWebAddress = address.Substring(0,address.LastIndexOf('.')+1);
            Int16 retryTimes = 0;
            bool retrySuccessed = false;
            try
            {
                wc.DownloadFile(cuttedSubNamesWebAddress+ subname, filename);
            }
            catch (WebException we)
            {
                HttpWebResponse errorResponse = we.Response as HttpWebResponse;
                if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    needChangeSubName = true;
                }
            }
            
            while(needChangeSubName&&!(retrySuccessed))
            {

                if(retryTimes<=2)
                {
                    try
                    {
                        Console.Out.WriteLine("retring using subname:" + localPSname[retryTimes]);
                        wc.DownloadFile(cuttedSubNamesWebAddress + localPSname[retryTimes], filename);
                        retrySuccessed = true;
                    }
                    catch (WebException we)
                    {
                        HttpWebResponse errorResponse = we.Response as HttpWebResponse;
                        if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                        {
                            retryTimes++;
                        }
                    }
                    finally
                    {
                        if(retryTimes>2)
                        {
                            needChangeSubName = false;
                        }
                    }
                }
                else
                {
                    needChangeSubName = false;
                }



            }



        }
        public int getTotalPageNum()
        {
            return DlTotalPrg;
        }
        public int getProgressPageNum()
        {
            return DlProgress;
        }


        private void checkDirectoryExist(string path)
        {
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {


            }
        }

    }
}
