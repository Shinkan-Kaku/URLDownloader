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
        private int DlTotalPrg;
        private int DlProgress;


        public UDdler()
        {
            isDlFinished = false;
            DlTotalPrg = 0;
            DlProgress = 0;
        }

        public UDdler(RqOrder RO)
        {
            pack = RO;
            isDlFinished = false;
            DlTotalPrg= RO.getAllPageNumber();
            DlProgress =0;
        }

        public void doDownloadMethod(int method,bool isLowSpeedToDebug)
        {
            //1-AllUrl 2-FinalPN
            WebClient wc = new WebClient();
            switch(method)
            {
                case 1:
                    checkDirectoryExist(pack.getPath() + "\\" + pack.getTitleFileName() + "\\");
                    DlProgress = 1;
                    Console.Out.WriteLine(pack.getFirstPageUrl());
                    wc.DownloadFile(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + "1" + "." + pack.getTFFileName(2));

                    for (int num = 1;num<pack.getAllPageNumber();num++ )
                    {
                        //Console.Out.WriteLine("From:" + pack.getURLs(num));
                        //Console.Out.WriteLine("File:" + pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(num) +"."+ pack.getFileName(2, num));
                       // wc.DownloadFile("http://blog.darkthread.net/images/darkthreadbanner.gif", "D:\\darkthread.gif");
                        DlProgress++;
                       Console.Out.WriteLine("DlProgress" + DlProgress + "/Total:" + DlTotalPrg);
                       Console.Out.WriteLine(pack.getURLs(num));
                        wc.DownloadFile(pack.getURLs(num), pack.getPath()+"\\"+pack.getTitleFileName() +"\\"+ Convert.ToString(num+1)+"."+pack.getFileName(2,num));
                        //Console.Out.WriteLine((decimal)DlProgress / (decimal)DlTotalPrg);
                        //Console.Out.WriteLine("Progress:" + Convert.ToString((100.0*(DlProgress/DlTotalPrg)))); 
                        Thread.Sleep((isLowSpeedToDebug? 5000:1000));
                    }
                    isDlFinished = true;

                    break;
                case 2:
                    checkDirectoryExist(pack.getPath() + "\\" + pack.getTitleFileName() + "\\");
                    DlProgress = 1;
                    wc.DownloadFile(pack.getFirstPageUrl(), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + "1" + "." + pack.getTFFileName(2));
                    String toFormat="";
                    for(int tar=0;tar<pack.getTFFileName(1).Length;tar++)
                    {
                        toFormat += "0";
                    }

                    int startPage = Convert.ToInt16(pack.getTFFileName(1));
                    int finalPNum = Convert.ToInt16(pack.getURLs(0));
                    string WebpageURL = pack.getFirstPageUrl().Substring(0, pack.getFirstPageUrl().LastIndexOf('/')+1);
                    for (int num = startPage+1; num <=finalPNum; num++)
                    {

                        decimal turndNum = Convert.ToDecimal(num);
                        DlProgress++;
                        Console.Out.WriteLine("DlProgress" + DlProgress + "/Total:" + DlTotalPrg);
                        Console.Out.WriteLine(WebpageURL + turndNum.ToString(toFormat) + "." + pack.getTFFileName(2));
                        wc.DownloadFile(WebpageURL + turndNum.ToString(toFormat) +"."+ pack.getTFFileName(2), pack.getPath() + "\\" + pack.getTitleFileName() + "\\" + Convert.ToString(num) + "." + pack.getTFFileName(2));
                        Thread.Sleep((isLowSpeedToDebug ? 5000 : 1000));
                    }
                    isDlFinished = true;
                    break;
                default:
                    break;
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
