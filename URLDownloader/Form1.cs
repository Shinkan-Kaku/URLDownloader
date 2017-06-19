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

namespace URLDownloader
{

    public partial class URLSDownloader : Form
    {
        UDdler Udler;
        //Thread dlListner;
        Thread FBChoiceListner;
        BackgroundWorker background;
        //private BackgroundWorker backgroundworker;
        bool timetoClose;
        public URLSDownloader()
        {
            InitializeComponent();
            Udler = new UDdler();
            timetoClose = false;
            FBChoiceListner = new Thread(threadcheckingFBCChoiced);
            FBChoiceListner.Start();
            background = new BackgroundWorker();
            typeMessageToTtBox("Welcome");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Address_Click(object sender, EventArgs e)
        {
            FBDialog1.ShowDialog();
        }
        private void testDownloading()
        {
            WebClient wc = new WebClient();
            string TargetURL = "";

            string SavingURL = FBDialog1.SelectedPath;
            typeMessageToTtBox(SavingURL);
            Console.WriteLine(SavingURL);
           
            
            //wc.DownloadFile("http://blog.darkthread.net/images/darkthreadbanner.gif","b:\\darkthread.gif");
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void initButton_Click(object sender, EventArgs e)
        {
            switch (ModeCB.SelectedIndex)
            {
                case 0:
                    sendRequestOrder();
                    break;
                case 1:
                    sendExampleRequestOrder();
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

        private void chargeBackgroundWorker()
        {
            background = new BackgroundWorker();
            background.DoWork += BgWReportProgress;
            background.ProgressChanged += updateProgress;
            background.RunWorkerCompleted += ProgressComplete;
            background.WorkerReportsProgress = true;
            background.RunWorkerAsync();
        }
        private void ProgressComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            inteveneUIwithThread("3/Download Request Complete");
            //progressBar1.Visible = false;
        }
        private void updateProgress(object sender, ProgressChangedEventArgs e)
        {
            typeMessageToTtBox(Udler.getProgressPageNum() + " " + Udler.getTotalPageNum());
            progressBar1.Value = e.ProgressPercentage;
        }
        private void BgWReportProgress(object sender, DoWorkEventArgs e)
        {
            while(!Udler.isDlFinished&&!timetoClose)
            {
                decimal result = ((decimal)(Udler.getProgressPageNum()) / (decimal)(Udler.getTotalPageNum())) * (decimal)100.0;
                
                int avgOfprogess = Convert.ToInt32(result);
                background.ReportProgress(avgOfprogess);
                Console.Out.WriteLine(result);
                inteveneUIwithThread("3/"+Convert.ToString(result));
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void sendRequestOrder()
        {
            string v1=TitlettBox.Text;
            string v2 = FPUrlTtBox.Text;
            string v3=FBDialog1.SelectedPath;
            if (dlRuleCBox.Text.Equals("FinalEpNum") )
            {
                RqOrder order = new RqOrder(v1,v2,v3,RqOrder.BASE_ON_FINAL_EPNUM);
                order.addURLs(Convert.ToString(DldataView.Rows[0].Cells[1].FormattedValue));
                chargeBackgroundWorker();
                Udler = new UDdler(order);
                Udler.doDownloadMethod(2);

            }
            else if (dlRuleCBox.SelectedText.Equals("AllURLs"))
            {
                RqOrder order = new RqOrder(v1, v2, v3, RqOrder.BASE_ON_ALL_PAGEURL);
                for (int CrtNum = 0; CrtNum < DldataView.Rows.Count; CrtNum++)
                {
                    order.addURLs(Convert.ToString(DldataView.Rows[CrtNum].Cells[1].FormattedValue));
                }
                chargeBackgroundWorker();
                Udler = new UDdler(order);
                Udler.doDownloadMethod(1);
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
            switch (dlRuleCBox.SelectedIndex)
            {
                case 0:
                    order = new RqOrder(exam[0], exam[1], exam[2], RqOrder.BASE_ON_FINAL_EPNUM);
                    order.addURLs(exam[3]);
                    chargeBackgroundWorker();
                    Udler = new UDdler(order);
                    Udler.doDownloadMethod(2);
                    break;
                case 1:
                    order = new RqOrder(exam[0], exam[1], exam[2], RqOrder.BASE_ON_ALL_PAGEURL);
                    for (int num = 1;num<=Convert.ToInt16(exam[3]);num++ )
                    {
                        order.addURLs("http://w6.loxa.edu.tw/a13302001/00" + Convert.ToString(num) + ".png");
                    }
                    chargeBackgroundWorker();
                    Udler = new UDdler(order);
                    Udler.doDownloadMethod(1);
                        break;
                default:
                    typeMessageToTtBox("ERROR Rising On DownloadRule");
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
               
                if (DlPathCdtCB.Checked && TitleCdtCB.Checked && FPUrlCdtCB.Checked)
                {
                    inteveneUIwithThread("1/1"); 
                }
                else
                {
                    inteveneUIwithThread("1/0"); 
                }

            }
        }
        private void inteveneUIwithThread(string code)
        {
            //格式為Num/String
            //Num 指定要改部位
            //string 用在內容物為布林時，當輸數字，大於0為真
            string[] parsed = code.Split('/');
            bool conver = false;
            switch(Convert.ToInt16(parsed[0]))
            {
                case 0 :
                    conver = Convert.ToInt16(parsed[1]) > 0 ? true : false;
                    var checkCheckBox = new Action(() => DlPathCdtCB.Checked = conver);
                    if(DlPathCdtCB.InvokeRequired)
                    {
                        DlPathCdtCB.Invoke(checkCheckBox);
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
                default:
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
