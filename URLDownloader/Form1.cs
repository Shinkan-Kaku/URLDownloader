using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

delegate void SetTextCallback(string text);
delegate string GetTextCallback();

namespace URLDownloader
{

    public partial class URLSDownloader : Form
    {
        UDdler Udler;
        //Thread dlListner;
        Thread FBChoiceListner;
        Thread Uldlworker;
        BackgroundWorker background;
        //private BackgroundWorker backgroundworker;
        bool timetoClose;
        public URLSDownloader()
        {
            InitializeComponent();
            Udler = new UDdler();
            timetoClose = false;
            FBChoiceListner = new Thread(threadcheckingFBCChoiced);
            //Uldlworker = new Thread(threadingDownloadWork);
            FBChoiceListner.Start();
            //Uldlworker.Start();
            background = new BackgroundWorker();
            background.DoWork += BgWReportProgress;
            background.ProgressChanged += updateProgress;
            background.RunWorkerCompleted += ProgressComplete;
            background.WorkerReportsProgress = true;
            typeMessageToTtBox("Welcome");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Address_Click(object sender, EventArgs e)
        {
            FBDialog1.ShowDialog();
        }


        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void initButton_Click(object sender, EventArgs e)
        {
            switch (ModeCB.SelectedIndex)
            {
                case 0:
                    Uldlworker = new Thread(sendRequestOrder);
                    //sendRequestOrder();
                    chargeBackgroundWorker(true);
                    Uldlworker.Start();
                    break;
                case 1:
                    Uldlworker = new Thread(sendExampleRequestOrder);
                    //sendExampleRequestOrder();
                    chargeBackgroundWorker(true);
                    Uldlworker.Start();
                    break;
                default:
                    typeMessageToTtBox("ERROR Rising on ModeCB");
                    break;
            }

            
        }
        public void typeMessageToTtBox(String s)
        {
            rTBox.Text += "\r\n";
            rTBox.Text += s;
        }

        private void chargeBackgroundWorker(bool switchONOFF)
        {
            if(switchONOFF)
            {
                background.RunWorkerAsync();
            }
            else
            {
                background.CancelAsync();
                background.Dispose();
            }
            
        }
        private void ProgressComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            inteveneUIwithThread("3/Download Request Complete");
            progressBar1.Visible = false;
            inteveneUIwithThread("3/You Can Shutdown or Do next Request peacefully");
        }
        private void updateProgress(object sender, ProgressChangedEventArgs e)
        {
            //typeMessageToTtBox(Udler.getProgressPageNum() + " " + Udler.getTotalPageNum());
            if (Udler.getTotalPageNum() > 0 || Udler.getProgressPageNum() > 0)
            {
                //inteveneUIwithThread("3/" + Udler.getProgressPageNum() + " " + Udler.getTotalPageNum());
            }
                
            progressBar1.Value = e.ProgressPercentage;
        }
        private void BgWReportProgress(object sender, DoWorkEventArgs e)
        {
            int crtPage = 0;
            while (!(Udler.isDlFinished)&&!timetoClose)
            {
                
                if(Udler.getTotalPageNum()>0|| Udler.getProgressPageNum()>0)
                {


                    decimal result = ((decimal)(Udler.getProgressPageNum()) / (decimal)(Udler.getTotalPageNum())) * (decimal)100.0;

                    int avgOfprogess = Convert.ToInt32(result);
                    background.ReportProgress(avgOfprogess);
                    //Console.Out.WriteLine(result);
                    if (crtPage < Udler.getProgressPageNum())
                    {
                        crtPage = Udler.getProgressPageNum();
                        inteveneUIwithThread("3/" +"Current Progress :Page"+ Convert.ToString(crtPage));
                    }
                }

            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void sendRequestOrder()
        {
            RqOrder order;
            string v1=TitlettBox.Text;
            string v2 = FPUrlTtBox.Text;
            string v3=FBDialog1.SelectedPath;
            Thread.Sleep(3000);
            if (dlRuleCBox.Text.Equals("FinalEpNum") )
            {
                order = new RqOrder(v1,v2,v3,RqOrder.BASE_ON_FINAL_EPNUM);
                order.addURLs(Convert.ToString(DldataView.Rows[0].Cells[1].FormattedValue));
                Udler = new UDdler(order);
                Udler.doDownloadMethod(2,false);

            }
            else if (dlRuleCBox.SelectedText.Equals("AllURLs"))
            {
                order = new RqOrder(v1, v2, v3, RqOrder.BASE_ON_ALL_PAGEURL);
                for (int CrtNum = 0; CrtNum < DldataView.Rows.Count; CrtNum++)
                {
                    order.addURLs(Convert.ToString(DldataView.Rows[CrtNum].Cells[1].FormattedValue));
                }
                Udler = new UDdler(order);
                Udler.doDownloadMethod(1,false);
            }
            else
            {
                typeMessageToTtBox("ERROR Rising On sendRequestOrder");
            }

        }
        private void sendExampleRequestOrder()
        {
            string[] exam = { "debugful", "http://w6.loxa.edu.tw/a13302001/000.png","", "3" };
            exam[2] = FBDialog1.SelectedPath;
            RqOrder order;
            
            Console.Out.WriteLine("BackgroundWorker has Charged");
            inteveneUIwithThread("3/BackgroundWorker has Charged");
            Thread.Sleep(5000);
            switch (Convert.ToInt16(requestValueFromUI(0)))
            {
                case 0:
                    order = new RqOrder(exam[0], exam[1], exam[2], RqOrder.BASE_ON_FINAL_EPNUM);
                    order.addURLs(exam[3]);
                    Udler = new UDdler(order);
                    Udler.doDownloadMethod(2,true);
                    Console.Out.WriteLine("Download Request has Send");
                    Thread.Sleep(5000);
                    break;
                case 1:
                    order = new RqOrder(exam[0], exam[1], exam[2], RqOrder.BASE_ON_ALL_PAGEURL);
                    for (int num = 1;num<=Convert.ToInt16(exam[3]);num++ )
                    {
                        order.addURLs("http://w6.loxa.edu.tw/a13302001/00" + Convert.ToString(num) + ".png");
                    }
                    Udler = new UDdler(order);
                    Udler.doDownloadMethod(1,true);
                    Console.Out.WriteLine("Download Request has Send");
                    Thread.Sleep(5000);
                    break;
                default:
                    inteveneUIwithThread("3/ERROR Rising On DownloadRule");
                    break;
            }

        }


        private void threadcheckingFBCChoiced()
        {
            while(!(timetoClose))
            {
                if (string.IsNullOrEmpty(FBDialog1.SelectedPath))
                {
                    inteveneUIwithThread("0/0");
                }
                else
                {
                    inteveneUIwithThread("0/1"); 
                }
               if (requestValueFromUI(1).Equals("FinalEpNum") )
                {
                    if (Regex.IsMatch(Convert.ToString(DldataView.Rows[0].Cells[1].FormattedValue), "^(\\d{1,3})$"))
                    {
                        inteveneUIwithThread("4/1");
                    }
                    else
                    {
                        inteveneUIwithThread("4/0");
                    }

                }
               else if(requestValueFromUI(1).Equals("AllURLs"))
                {
                    if(Regex.IsMatch(Convert.ToString(DldataView.Rows[0].Cells[1].FormattedValue), "http(s?)://(([0-9.\\-A-Za-z]+)/)+\\w+.(jpg|png|gif|bmp)"))
                    {
                        inteveneUIwithThread("4/1");
                    }
                    else
                    {
                        inteveneUIwithThread("4/0");
                    }
                }


                if (DlPathCdtCB.Checked && TitleCdtCB.Checked && FPUrlCdtCB.Checked&& ListvcdtCB.Checked)
                {
                    inteveneUIwithThread("1/1"); 
                }
                else
                {
                    inteveneUIwithThread("1/0"); 
                }

            }
        }
        private void threadingDownloadWork()
        {
            while(!(timetoClose))
            {

            }
        }
        private void inteveneUIwithThread(string code)
        {
            //格式為Num/String
            //Num 指定要改部位
            //string 用在內容物為布林時，當輸數字，大於0為真
            //0=,1=開始下載按鈕,2=進度條,3=訊息欄,4=資料驗證CB
            string[] parsed = code.Split('/');
            bool conver = false;

            switch (Convert.ToInt16(parsed[0]))
            {
                case 0 :
                    conver = Convert.ToInt16(parsed[1]) > 0 ? true : false;
                    var checkDlPathCdtCB = new Action(() => DlPathCdtCB.Checked = conver);
                    if(DlPathCdtCB.InvokeRequired)
                    {
                        DlPathCdtCB.Invoke(checkDlPathCdtCB);
                    }
                    else
                    {
                        DlPathCdtCB.Checked = true;
                    }
                    break;
                case 1 :
                    conver = Convert.ToInt16(parsed[1]) > 0 ? true : false;
                    var setButton = new Action(() => initButton.Enabled = conver);
                    if(initButton.InvokeRequired)
                    {
                        initButton.Invoke(setButton);
                    }
                    else
                    { 
                        initButton.Enabled=conver;
                        Console.Out.WriteLine(conver);
                    }
                    break;
                case 2:
                    var setprogress = new Action(() => progressBar1.Value = Convert.ToInt16(parsed[1]));
                    var updateprogrssbar = new Action(() => progressBar1.Update());
                    if(progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke(setprogress);
                        progressBar1.Invoke(updateprogrssbar);
                    }
                    else
                    {
                        progressBar1.Value = Convert.ToInt16(parsed[1]);
                        progressBar1.Update();
                    }
                    break;
                case 3:
                    var writeLineTpTtbox = new Action(()=>rTBox.Text += "\r\n");
                    var typeMessagesToTtbox = new Action(() => rTBox.Text += parsed[1]);
                    if(rTBox.InvokeRequired)
                    {
                        rTBox.Invoke(writeLineTpTtbox);
                        rTBox.Invoke(typeMessagesToTtbox);
                    }
                    else
                    {
                        typeMessageToTtBox(parsed[1]);
                    }
                    break;
                case 4:
                    conver = Convert.ToInt16(parsed[1]) > 0 ? true : false;
                    var checkListvcdtCB = new Action(() => ListvcdtCB.Checked = conver);
                    if(ListvcdtCB.InvokeRequired)
                    {
                        ListvcdtCB.Invoke(checkListvcdtCB);
                    }
                    else
                    {
                        ListvcdtCB.Checked = conver;
                    }
                    break;

                default:
                    break;




            }
        
        }
        private string requestValueFromUI(Int16 index)
        {
            switch(index)
            {
                case 0:
                    if(dlRuleCBox.InvokeRequired)
                    {
                        string getted="";
                        var tasktoInvoke = new Action(()=>getted = Convert.ToString(dlRuleCBox.SelectedIndex) );
                        dlRuleCBox.Invoke(tasktoInvoke);

                        return getted;
                    }
                    else
                    {
                        return Convert.ToString(dlRuleCBox.SelectedIndex);
                    }
                    
                    break;
                case 1:
                    if (dlRuleCBox.InvokeRequired)
                    {
                        string getted = "";
                        var tasktoInvoke = new Action(() => getted = Convert.ToString(dlRuleCBox.Text));
                        dlRuleCBox.Invoke(tasktoInvoke);

                        return getted;
                    }
                    else
                    {
                        return Convert.ToString(dlRuleCBox.Text);
                    }
                    return "";
                    break;
                default:
                    return "";
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ModeCB.Text.Equals("Normal"))
            {
                ModeCB.Enabled = false;
                UIEnableStepA_Normal();
            }
            else
            {
                ModeCB.Enabled = false;
                UIEnableStepA_Debug();
            }
        }
        private void UIEnableStepA_Normal()
        {
            Address.Enabled = true;
            TitlettBox.Enabled = true;
            FPUrlTtBox.Enabled = true;
            dlRuleCBox.Enabled = true;
            DldataView.Enabled = true;
        }
        private void UIEnableStepA_Debug()
        {
            dlRuleCBox.Enabled = true;
            Address.Enabled = true;
            TitlettBox.Text = "debug.png";
            FPUrlTtBox.Text = "http://w6.loxa.edu.tw/a13302001/000.png";
        }

        private void dlRuleCBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dlRuleCBox.Text.Equals("FinalEpNum"))
            {
                
                if (ModeCB.Text.Equals("Normal"))
                {
                    addingPage("FinalPage");
                }
                else
                {
                    addingPage("3");
                    DldataView.Enabled = false;
                }
                dlRuleCBox.Enabled = false;
            }
            else
            {
                if (ModeCB.Text.Equals("Normal"))
                {
                    BnAddUrlsLog.Enabled = true;
                }
                else
                {
                    addingPage("http://w6.loxa.edu.tw/a13302001/001.png");
                    addingPage("http://w6.loxa.edu.tw/a13302001/002.png");
                    addingPage("http://w6.loxa.edu.tw/a13302001/003.png");
                    DldataView.Enabled = false;

                }
                
                dlRuleCBox.Enabled = false;
            }
        }


        private void TitlettBox_TextChanged(object sender, EventArgs e)
        {
            if(!TitlettBox.Text.Equals("")&&TitlettBox.Text!=null&&(Regex.IsMatch(TitlettBox.Text,"[^\\?*<\":>]")))
            {
                TitleCdtCB.Checked = true;
            }
            else
            {
                TitleCdtCB.Checked = false;
            }
        }

        private void FPUrlTtBox_TextChanged(object sender, EventArgs e)
        {
            if (!FPUrlTtBox.Text.Equals("") && FPUrlTtBox.Text != null && Regex.IsMatch(FPUrlTtBox.Text,"http(s?)://(([0-9.\\-A-Za-z]+)/)+\\w+.(jpg|png|gif|bmp)"))
            {
                FPUrlCdtCB.Checked = true;
            }
            else
            {
                FPUrlCdtCB.Checked = false;
            }
        }

        private void addingURLsLog_Click(object sender, EventArgs e)
        {
            addingPage();
        }
        private void addingPage()
        {
            int LastPageNo = DldataView.RowCount+1;
            DldataView.Rows.Add(LastPageNo, "");
        }
        private void addingPage(string Urlstring)
        {
            int LastPageNo = DldataView.RowCount+1;
            DldataView.Rows.Add(LastPageNo, Urlstring);
        }

        private void URLSDownloader_FormClosed(object sender, FormClosedEventArgs e)
        {
            timetoClose = true;
            FBChoiceListner.Abort();
            //dlListner.Abort();
        }
    }
}
