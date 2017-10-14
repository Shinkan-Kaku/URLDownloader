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
        Thread UIUpdayer;
        //BackgroundWorker background;
        //private BackgroundWorker backgroundworker;
        bool timetoClose;
        bool Reseted = false;
        bool UUStandby = true;

        String versionString = "Version "+"B1.65";

        public URLSDownloader()
        {
            InitializeComponent();
            Vlabel.Text = versionString;
            Udler = new UDdler();
            timetoClose = false;
            FBChoiceListner = new Thread(threadcheckingFBCChoiced);
            FBChoiceListner.Start();

            UIUpdayer = new Thread(updateUI);

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
                //background.RunWorkerAsync();
                if (!Reseted)
                {
                    UIUpdayer.Start();
                    
                }
                else
                {
                    UUStandby = false;
                    inteveneUIwithThread("2/true");
                    inteveneUIwithThread("2/0");
                }
            }
            else
            {
                inteveneUIwithThread("3/Download Request Complete");
                inteveneUIwithThread("2/false");
                resetUI();
                Thread.Sleep(3000);
                inteveneUIwithThread("3/You Can Shutdown or Do next Request peacefully");

                //background.CancelAsync();
                //background.Dispose();
            }
            
        }

        //updateThread using this methodGroup
        private void updateUI()
        {
            subUUProgresspack();
        }
        private void subUUProgresspack()
        {
            subUpdateUI();
            chargeBackgroundWorker(false);
            UUStandby = true;
            subUUStandby();
        }
        private void  subUpdateUI()
        {
            int crtPage = 0;
            while (!(Udler.isDlFinished) && !timetoClose)
            {
                if (Udler.getTotalPageNum() > 0 || Udler.getProgressPageNum() > 0)
                {
                    decimal result = ((decimal)(Udler.getProgressPageNum()) / (decimal)(Udler.getTotalPageNum())) * (decimal)100.0;


                    //updateProgrssHere
                    Console.Out.WriteLine("2/" + Convert.ToString(Math.Floor(result)));
                    //inteveneUIwithThread("2/" + Convert.ToString(result));
                    Thread.Sleep(1000);
                    if (result <= 100)
                    {
                        inteveneUIwithThread("2/" + Convert.ToString(Math.Floor(result)));
                    }


                    if (crtPage < Udler.getProgressPageNum())
                    {
                        crtPage = Udler.getProgressPageNum();
                        inteveneUIwithThread("3/" + "Current Progress :Page" + Convert.ToString(crtPage));
                    }
                }

            }
        }
        private void subUUStandby()
        {
            while(UUStandby)
            {
                Thread.Sleep(1000);
            }
            subUUProgresspack();

        }
        //--------------------------------------------------------------------------------------------------------


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //Build & Sent Request methods
        private void sendRequestOrder()
        {
            RqOrder order;
            string v1=TitlettBox.Text;
            string v2 = FPUrlTtBox.Text;
            string v3=FBDialog1.SelectedPath;
            //Thread.Sleep(3000);
            switch (Convert.ToInt16(requestValueFromUI(0)))
            {
                case 0:
                    order = new RqOrder(v1, v2, v3, RqOrder.BASE_ON_FINAL_EPNUM);
                    order.addURLs(Convert.ToString(DldataView.Rows[0].Cells[1].FormattedValue));
                    Udler = new UDdler(order);
                    Udler.doDownloadMethod(2, false);
                    break;
                case 1:
                    order = new RqOrder(v1, v2, v3, RqOrder.BASE_ON_ALL_PAGEURL);
                    for (int CrtNum = 0; CrtNum < DldataView.Rows.Count; CrtNum++)
                    {
                        order.addURLs(Convert.ToString(DldataView.Rows[CrtNum].Cells[1].FormattedValue));
                    }
                    Udler = new UDdler(order);
                    Udler.doDownloadMethod(1, false);
                    break;
                default:
                    inteveneUIwithThread("3/ERROR Rising On DownloadRule");
                    break;
            }


        }
        private void sendExampleRequestOrder()
        {
            string[] exam = { "debugful", "http://w6.loxa.edu.tw/a13302001/000.png","", "3" };
            exam[2] = FBDialog1.SelectedPath;
            RqOrder order;
            
            Console.Out.WriteLine("BackgroundWorker has Charged");
            inteveneUIwithThread("3/BackgroundWorker has Charged");
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
        //-----------------------------------------------------------------------------------------------------

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
               if (requestValueFromUI(1).Equals("FinalEpNum")&&DldataView.Rows.Count>0 )
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
               else if(requestValueFromUI(1).Equals("AllURLs") && DldataView.Rows.Count > 0)
                {
                    //bug
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
                    if(Udler.isDlFinished==false&&Udler.hasStarted)
                    {
                        inteveneUIwithThread("1/0");
                    }
                    else if(Udler.isDlFinished==false&&Udler.hasStarted==false)
                    {
                        inteveneUIwithThread("1/1");
                    }
                    else
                    {
                        inteveneUIwithThread("1/1");
                    }
                    
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
            //0=下載條件確認,1=開始下載按鈕,2=進度條,3=訊息欄,4=資料驗證CB,5=模式選擇CB,6=清空DlDataview
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

                    if (parsed[1].Equals("true") || parsed[1].Equals("false"))
                    {
                        conver = (parsed[1].Equals("true")) ? true : false;
                        var setProbar1Visable = new Action(() => progressBar1.Visible = conver);

                        if(progressBar1.InvokeRequired)
                        {
                            progressBar1.Invoke(setProbar1Visable);
                        }
                        else
                        {
                            progressBar1.Visible = conver;
                        }

                    }
                    else
                    {
                        var setprogress = new Action(() => progressBar1.Value = Convert.ToInt16(parsed[1]));
                        var updateprogrssbar = new Action(() => progressBar1.Update());
                        if (progressBar1.InvokeRequired)
                        {
                            progressBar1.Invoke(setprogress);
                            progressBar1.Invoke(updateprogrssbar);
                        }
                        else
                        {
                            progressBar1.Value = Convert.ToInt16(parsed[1]);
                            progressBar1.Update();
                        }
                    }
                    break;
                case 3:
                        var writeLineTpTtbox = new Action(() => rTBox.Text += "\r\n");
                        var typeMessagesToTtbox = new Action(() => rTBox.Text += parsed[1]);
                        if (rTBox.InvokeRequired)
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
                case 5:

                    var switchModeCB = new Action(() => ModeCB.SelectedIndex = Convert.ToInt16(parsed[1]));

                    if(ModeCB.InvokeRequired)
                    {
                        ModeCB.Invoke(switchModeCB);
                    }
                    else
                    {
                        ModeCB.SelectedIndex = Convert.ToInt16(parsed[1]);
                    }
                    break;
                case 6:
                    var clearDldataView = new Action(() => DldataView.Rows.Clear() );

                    if(DldataView.InvokeRequired)
                    {
                        DldataView.Invoke(clearDldataView);
                    }
                    else
                    {
                        DldataView.Rows.Clear();
                    }

                    break;
                case 7:
                    var cleardlRuleCobBoxText = new Action( () => dlRuleCBox.SelectedText="");

                    if(dlRuleCBox.InvokeRequired)
                    {
                        dlRuleCBox.Invoke(cleardlRuleCobBoxText);
                    }
                    else
                    {
                        dlRuleCBox.SelectedText = "";
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
            else if(ModeCB.Text.Equals("Debug"))
            {
                ModeCB.Enabled = false;
                UIEnableStepA_Debug();
            }
            else
            {
                ModeCB.Enabled = true;
            }
        }
        private void UIEnableStepA_Normal()
        {
            Address.Enabled = true;
            TitlettBox.Enabled = true;
            TitlettBox.Text = "";
            FPUrlTtBox.Enabled = true;
            FPUrlTtBox.Text = "";
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
                    addingPage("NextPageURL");
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
            if(!TitlettBox.Text.Equals("")&&TitlettBox.Text!=null&&!(Regex.IsMatch(TitlettBox.Text, ".*(\\^|\\?|\\*|<|\"|:|>|\\\\|/)+.*")))
            {
                TitleCdtCB.Checked = true;
            }
            else
            {
                if(Regex.IsMatch(TitlettBox.Text, ".*(\\^|\\?|\\*|<|\"|:|>|\\\\|/)+.*"))
                {
                    typeMessageToTtBox("Error : illegal symbols is not allowed!");
                    TitlettBox.Text = "";
                }

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

        private void resetUI()
        {
            inteveneUIwithThread("5/2");
            inteveneUIwithThread("6/1");
            inteveneUIwithThread("7/0");
            //dlRuleCBox.SelectedText = "";
            //TitlettBox.Text = "";
            //FPUrlTtBox.Text = "";
            Udler = new UDdler();
            //UIUpdayer = new Thread(updateUI);
            Reseted = true;

            Console.Out.WriteLine("UI has Reseted");
        }

        private void URLSDownloader_FormClosed(object sender, FormClosedEventArgs e)
        {
            timetoClose = true;
            FBChoiceListner.Abort();
            UIUpdayer.Abort();
            //dlListner.Abort();
        }
    }
}
