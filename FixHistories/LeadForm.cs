using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



using System.Windows.Forms;
using System.Threading;
using Microsoft.VisualBasic.FileIO;

namespace FixHistories
{
    public partial class LeadForm : Form
    {
        OpenFileDialog OFileDialog;
        SaveFileDialog SFileDialog;
        
        string[] InputFileNames = new string[0];
        ShortHistoryStruct[] SortedHistories;
        int StartRec;
        int EndRec;
        
        int MaxRecs = 1048576;
        string HistoryHeaders = "";
        public struct HistoryStruct : IEquatable<HistoryStruct>
        {
            public string ChangeDate;
            public string Type;
            public string ID;
            public string Name;
            public string Status;
            public string Severity;
            public string Priority;
            public string PlannedFor;
            public string TargetRelease;
            public string CreationDate;
            public string Lastupdatetime;
            public string ResolvedDate;
            public string Summary;
            public string Modifier;
            public string StoryPoints;
            public string FiledAgainst;

            public override bool Equals(object obj)
            {
                throw new NotImplementedException();
            }

            public override int GetHashCode()
            {
                throw new NotImplementedException();
            }

            public static bool operator ==(HistoryStruct left, HistoryStruct right)
            {
                return left.Equals(right);
            }

            public static bool operator !=(HistoryStruct left, HistoryStruct right)
            {
                return !(left == right);
            }

            public bool Equals(HistoryStruct other)
            {
                throw new NotImplementedException();
            }
        };
        public struct ShortHistoryStruct
        {
            public DateTime ChangeDate;
            public DateTime LastModified;
            public string ID;
            public string Contents;
        }
        public ShortHistoryStruct[] Histories;
        public int numHistories;
        public LeadForm()
        {
            InitializeComponent();
        }

        private void btnSelectFiles_Click(object sender, EventArgs e)
        {

            OFileDialog = new OpenFileDialog();
            OFileDialog.Multiselect = true;
            OFileDialog.Title = "Open CSV File";
            OFileDialog.Filter = "CSV files|*.csv";
            OFileDialog.InitialDirectory = tbDirectory.Text;
            OFileDialog.ShowDialog();
            InputFileNames = OFileDialog.FileNames;

            if (InputFileNames.Length < 1)
            {
                MessageBox.Show("Cancelled...");
                return;
            }
            //FileName = InputFileNames[0].Substring(InputFileNames[0].LastIndexOf("\\") + 1,
            //    (InputFileNames[0].Length - InputFileNames[0].LastIndexOf("\\")) - 1);

            tbDirectory.Text = InputFileNames[0].Substring(0, InputFileNames[0].LastIndexOf("\\") + 1);
            //                tbDirectoryName.Text + tbWorkItemsFileName.Text
            lbFileList.Items.Clear();
            for (int i = 0; i < InputFileNames.Length; i++)
                lbFileList.Items.Add(InputFileNames[i]);
            //lbFileList.Items.Add(InputFileNames[i].Substring(InputFileNames[i].LastIndexOf("\\") + 1));
            return;

        }

        private void btnConCatSave_Click(object sender, EventArgs e)
        {
            
            StringBuilder SBR = new StringBuilder();

            
            MaxReturn = 0;
            if (InputFileNames.Length < 2)
            {
                MessageBox.Show("Please select 2 or more files first");
                return;
            }
            if(Histories == null)
            {
                MessageBox.Show("Please load & merge files first");
                return;
            }
            if (Histories.Length < 2)
            {
                MessageBox.Show("Please Load Selected files first");
                return;
            }
            int numRecs = Histories.Length;
            int numBytes = 0;
            SBR.Clear();
            //SBR.AppendLine(HistoryHeaders);
            for (int i=StartRec;i<EndRec;i++)
            {
                SBR.AppendLine(SortedHistories[i].Contents);
            }
            SFileDialog = new SaveFileDialog();
            SFileDialog.Title = "Open CSV File";
            SFileDialog.Filter = "CSV files|*.csv";
            SFileDialog.CheckPathExists = false;
            SFileDialog.InitialDirectory = tbDirectory.Text;
            SFileDialog.ShowDialog();
            if(SFileDialog.FileName == "")
            {
                MessageBox.Show("Cancelled");
                return;
            }
            FileStream OFS = File.OpenWrite(SFileDialog.FileName);
            OFS.SetLength(0);
            OFS.Seek(0, SeekOrigin.Begin);
            StreamWriter OSR = new StreamWriter(OFS);
            
            OSR.Write(SBR.ToString());
            OSR.Flush();
            OFS.Flush(true);
            OSR.Dispose();
            OFS.Close();
            OFS.Dispose();
            numBytes = SBR.Length;

            MessageBox.Show("Wrote " + (EndRec-StartRec) + " Records " + numBytes + " Bytes from " + InputFileNames.Length + " Files.");
        }
        int MaxReturn = 0;

        public ShortHistoryStruct[] DeDuplicateHistory(ShortHistoryStruct[] inH)
        {
            ShortHistoryStruct[] outH = new ShortHistoryStruct[inH.Length];
            int cnt = 0;
            int last = 0;
            last = 0;
            if(inH.Length == 0)
            {
                return outH;
            }
            outH[0] = inH[0];
            for (int i = 1; i < inH.Length; i++)
            {
                if( inH[last].ID != inH[i].ID || inH[last].LastModified != inH[i].LastModified)
                {
                    cnt++;
                    outH[cnt] = inH[i];
                    last = i;
                }
            }
            Array.Resize(ref outH, cnt + 1);
            return outH;
        }
        public ShortHistoryStruct[] MergeHistory( ShortHistoryStruct[] inH )
        {
            if (inH.Length < 2)
                return inH;
            ShortHistoryStruct[] high, low;
            ShortHistoryStruct[] ret = new ShortHistoryStruct[inH.Length];
            int numHigh =  Convert.ToInt32(Math.Floor(Convert.ToDecimal(inH.Length / 2)));
            int numLow = Convert.ToInt32(Math.Floor(Convert.ToDecimal(inH.Length / 2))) + (inH.Length % 2) ;
            high = new ShortHistoryStruct[numHigh];
            low = new ShortHistoryStruct[numLow];
            
            for (int i=0;i<numHigh;i++)
            {
                high[i] = inH[i];
                low[i] = inH[i + numHigh];
            }
            if (numLow > numHigh)
                low[numLow - 1] = inH[inH.Length - 1];
            high = MergeHistory(high);
            low = MergeHistory(low);
            int curr= 0;
            int curh = 0;
            int curl = 0;
            //while(curr < inH.Length)
            //{
            //    if(high[curh].LastModified.CompareTo(low[curl].LastModified) > 0)
            //    {
            //        ret[curr++] = low[curl++];
                    
            //    }
            //    else if(high[curh].LastModified.CompareTo(low[curl].LastModified) == 0)
            //    {
            //        if (high[curh].ID.CompareTo(low[curl].ID) > 0)
            //        {
            //            ret[curr++] = low[curl++];

            //        }
            //        else
            //        {
            //            ret[curr++] = high[curh++];
            //        }
            //    }
            //    else
            //    {
            //        ret[curr++] = high[curh++];
            //    }
            //    if (curh >= numHigh)
            //    {
            //        for(;curl<numLow; curl++)
            //        {
            //            ret[curr++] = low[curl];

            //        }
            //        break;
            //    }
            //    if (curl >= numLow )
            //    {
            //        for (; curh < numHigh; curh++)
            //        {
            //            ret[curr++] = high[curh];

            //        }
            //        break;
            //    }
            //}



            while (curr < inH.Length)
            {
                if (high[curh].ID == null)
                    high[curh].ID = "-1";
                if (low[curl].ID == null)
                    low[curl].ID = "-1";

                if (Convert.ToInt64(high[curh].ID) > Convert.ToInt64(low[curl].ID))
                {
                    ret[curr++] = low[curl++];

                }
                else if (Convert.ToInt64(high[curh].ID) == Convert.ToInt64(low[curl].ID))
                {
                    if (high[curh].LastModified.CompareTo(low[curl].LastModified) > 0)
                    {
                        ret[curr++] = low[curl++];

                    }
                    else
                    {
                        ret[curr++] = high[curh++];
                    }
                }
                else
                {
                    ret[curr++] = high[curh++];
                }
                if (curh >= numHigh)
                {
                    for (; curl < numLow; curl++)
                    {
                        ret[curr++] = low[curl];

                    }
                    break;
                }
                if (curl >= numLow)
                {
                    for (; curh < numHigh; curh++)
                    {
                        ret[curr++] = high[curh];

                    }
                    break;
                }
            }




            if (curr != inH.Length)
            {
                
                MessageBox.Show("Problem");
            }
            if (MaxReturn < curr) MaxReturn = curr;
            
            return ret;
        }
        public DateTime EarliestDate(ShortHistoryStruct[] sh)
        {
            DateTime edate = DateTime.MaxValue;
            for(int i=1;i<sh.Length;i++)
            {
                if (sh[i].ChangeDate.CompareTo(edate) < 0)
                    edate = sh[i].ChangeDate;
            }
            return edate;
        }
        public DateTime LatestDate(ShortHistoryStruct[] sh)
        {
            DateTime edate = DateTime.MinValue;
            for (int i = 0; i < sh.Length; i++)
            {
                if (sh[i].LastModified.CompareTo(edate) > 0)
                    edate = sh[i].LastModified;
            }
            return edate;
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            
            Histories = new ShortHistoryStruct[0];
            string inputline = "";
            TextFieldParser parser;

            int numBytes = 0;
            int numRecs = 0;
            if(InputFileNames.Length < 2)
            {
                MessageBox.Show("Please select 2 or more files first");
                return;
            }

            Thread trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();

            Array.Resize(ref Histories, 100000);
            //formLoadStatus.StartPosition = FormStartPosition.CenterParent;
            //ProjectGlobals.formLoadStatus.Show();
            foreach (string DumpFileName in InputFileNames)
            {
                //FileStream FS = File.OpenRead(DumpFileName);
                //StreamReader sr = new StreamReader(FS);
                TextFieldParser HistoriesDumpParser = new TextFieldParser(DumpFileName);

                HistoriesDumpParser.TextFieldType = FieldType.Delimited;
                HistoriesDumpParser.SetDelimiters(",");
                //while (sr.EndOfStream != true)
                
                while (HistoriesDumpParser.EndOfData != true)
                {
                    // inputline = sr.ReadLine();
                    inputline = HistoriesDumpParser.PeekChars(8000);
                    string[] pline = new string[0];
                    try
                    {
                        pline = HistoriesDumpParser.ReadFields();
                    }
                    catch
                    {
                        Array.Resize(ref pline, 0);
                    }
                    if (pline.Length > 0)
                    {
                        if (numRecs >= 1 && inputline.Length > 12 && inputline.Substring(0, 11).CompareTo("Change Date") != 0) // Strip Headers
                        {

                            //SBR.AppendLine(inputline);
                            if ((numRecs + 1) >= Histories.Length)
                                Array.Resize(ref Histories, Histories.Length + 100000);
                            //Array.Resize(ref Histories, numRecs + 1);
                            if (DateTime.TryParse(inputline.Substring(0, inputline.IndexOf(",", 0)), out DateTime result) == true)
                            {
                                DateTime.TryParse(pline[0], out result);
                                Histories[numRecs].ChangeDate = result;
                                Histories[numRecs].ID = pline[2];
                                DateTime.TryParse(pline[10], out result);
                                Histories[numRecs].LastModified = result;
                                Histories[numRecs].Contents = inputline;
                                numRecs++;
                            }
                            else
                            {
                                Histories[numRecs - 1].Contents += inputline;
                            }

                        }
                        else if (numRecs <= 1)
                        {
                            if (numRecs == 0 && inputline.Substring(0, 11).CompareTo("Change Date") == 0)
                            {
                                HistoryHeaders = inputline;
                            }
                            //Array.Resize(ref Histories, numRecs + 1);
                            if (numRecs != 0)
                            {
                                DateTime.TryParse(pline[0], out DateTime result);
                                Histories[numRecs].ChangeDate = result;
                                Histories[numRecs].ID = pline[2];
                                DateTime.TryParse(pline[10], out result);
                                Histories[numRecs].LastModified = result;
                                Histories[numRecs].Contents = inputline;
                                Histories[numRecs].ID = "0";
                            }
                            //SBR.AppendLine(inputline);
                            Histories[numRecs].Contents = inputline;
                            numRecs++;
                        }
                    }
                }
                //FS.Close();
                HistoriesDumpParser.Close();
            }
            Array.Resize(ref Histories, numRecs);
            SortedHistories = MergeHistory(Histories);
            int sort = SortedHistories.Length;
            SortedHistories = DeDuplicateHistory(SortedHistories);
            MessageBox.Show("Deduplcate from " + sort + " to " + SortedHistories.Length + " (" + Convert.ToDouble(1 - Convert.ToDouble(SortedHistories.Length)/Convert.ToDouble(sort) ).ToString("P") + ")");
            dtpEndDate.Value = LatestDate(SortedHistories);
            dtpStartDate.Value = EarliestDate(SortedHistories);
            StartRec = 0;
            EndRec = SortedHistories.Length;
            trd.Abort();
            lblNumberOfRecords.Text = "Number of Records: " + (EndRec - StartRec) + " Max - " + MaxRecs;
        }



        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {

            if(dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Start Date must be before End Date");
                //dtpStartDate.Value = SortedHistories[StartRec].ChangeDate;
                return;
            }
            StartRec = 0;
          
            for(int i=0; i< SortedHistories.Length-1;i++)
            {
                if(SortedHistories[i].ChangeDate >= dtpStartDate.Value && StartRec == 0)
                {
                    StartRec = i;
                    break;
                }
            }
            lblNumberOfRecords.Text = "Number of Records: " + (EndRec - StartRec) + " Max - " + MaxRecs; 
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            if (dtpStartDate.Value > dtpEndDate.Value)
            {
                MessageBox.Show("Start Date must be before End Date");
                //dtpStartDate.Value = SortedHistories[StartRec].ChangeDate;
                return;
            }

            EndRec = SortedHistories.Length;
            for (int i = 0; i < SortedHistories.Length - 1; i++)
            {

                if (SortedHistories[i].ChangeDate >= (dtpEndDate.Value.AddDays(1)) && StartRec == 0)
                {
                    EndRec = i;
                }
            }
            lblNumberOfRecords.Text = "Number of Records: " + (EndRec - StartRec) + " Max - 1048576";
        }

        private void btnSetStartToMaxRecs_Click(object sender, EventArgs e)
        {

            if((EndRec - StartRec) <= MaxRecs)
            {
                MessageBox.Show("Number of Records " + (EndRec - StartRec) + " is less than Max " + MaxRecs);
                return;
            }
            dtpStartDate.Value = SortedHistories[(EndRec - MaxRecs)].ChangeDate.AddDays(1);
        }
        private void ThreadTask()
        {
            Color clr = btnLoadData.BackColor;

            while (true)
            {
                if (clr == btnLoadData.BackColor)
                    btnLoadData.BackColor = Color.Red;
                else
                    btnLoadData.BackColor = clr;
                Thread.Sleep(1500);
            }
        }

        private void btnCleanSequences_Click(object sender, EventArgs e)
        {

            if (InputFileNames.Length != 1)
            {
                MessageBox.Show("Please select a single file to clean");
                return;
            }

            Thread trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();
            int numBytes = 0;

 



            byte[] inputline = new byte[4096];
            int changes = 0;
            FileStream FS = File.OpenRead(InputFileNames[0]);
            BinaryReader sr = new BinaryReader(FS);
            int xx = sr.Read(inputline, 0, 2);
            

            if (!(xx == 2 && inputline[0] == 255 && inputline[1] == 254))
            {
                SFileDialog = new SaveFileDialog();
                SFileDialog.Title = "Open CSV File";
                SFileDialog.Filter = "CSV files|*.csv";
                SFileDialog.CheckPathExists = false;
                SFileDialog.InitialDirectory = tbDirectory.Text;
                SFileDialog.ShowDialog();
                if (SFileDialog.FileName == "")
                {
                    MessageBox.Show("Cancelled");
                    return;
                }
                FS.Close();
                sr.Close();
                FS = File.OpenRead(InputFileNames[0]);
                sr = new BinaryReader(FS);
                
                FileStream OFS = File.OpenWrite(SFileDialog.FileName);
                OFS.SetLength(0);
                OFS.Seek(0, SeekOrigin.Begin);
                BinaryWriter OSR = new BinaryWriter(OFS);
                //OSR.Write(inputline, 0, 2);
                while (true)
                {

                    int bts = sr.Read(inputline, 0, 4096);
                    //if (inputline[0] != 255 || inputline[1] != 254)
                    if (bts <= 0)
                        break;
                    byte[] obuf = new byte[bts];
                    int numobuf = 0;
                    for (int i = 0; i < bts; i++)
                    {

                        //byte[] lbuf = new byte[5];
                        //if((bts - i) > 6)
                        //{
                        //    for (int ll = 0; ll < 5; ll++)
                        //        lbuf[ll] = inputline[ll+i];
                        //}
                        //if (inputline[i] == 61 && i < (bts - 2) && (inputline[i + 1] == Convert.ToByte(45) || inputline[i + 2] == Convert.ToByte(45)))
                        //  i = i;
                        //if (inputline[i] == Convert.ToByte(61) && i < (bts - 5))
                        //{
                        //    if (inputline[i + 1] == Convert.ToByte(45) || inputline[i + 2] == 45 || inputline[i + 3] == 45 || inputline[i + 4] == 45 || inputline[i + 5] == 45)
                        //        i = i;
                        //}
                       
                        if (inputline[i] == 61 && i < (bts - 2) && inputline[i + 1] == 64)  // =@
                        {
                            changes++; // 16bit encoding?
                            i += 2;
                        }
                        else if (inputline[i] == 44 && i < (bts - 2) && inputline[i + 1] == 45) // ,-
                        {
                            changes++; // 16bit encoding?
                            i += 2;
                        }
                        else
                        {
                            obuf[numobuf] = inputline[i];
                            numobuf++;
                        }
                    }
                    numBytes += numobuf;
                    OSR.Write(obuf, 0, numobuf);
                }

                OSR.Flush();
                OFS.Flush(true);
                OSR.Dispose();
                OFS.Close();
                OFS.Dispose();
                FS.Close();
            }
            else
            {
                SFileDialog = new SaveFileDialog();
                SFileDialog.Title = "Open CSV File";
                SFileDialog.Filter = "CSV files|*.csv";
                SFileDialog.CheckPathExists = false;
                SFileDialog.InitialDirectory = tbDirectory.Text;
                SFileDialog.ShowDialog();
                if (SFileDialog.FileName == "")
                {
                    MessageBox.Show("Cancelled");
                    return;
                }
                FileStream OFS = File.OpenWrite(SFileDialog.FileName);
                OFS.SetLength(0);
                OFS.Seek(0, SeekOrigin.Begin);
                BinaryWriter OSR = new BinaryWriter(OFS);
                OSR.Write(inputline, 0, 2);
                while (true)
                {

                    int bts = sr.Read(inputline, 0, 4096);
                    if (inputline[0] != 255 || inputline[1] != 254)
                        if (bts <= 0)
                            break;
                    byte[] obuf = new byte[bts];
                    int numobuf = 0;
                    for (int i = 0; i < bts; i++)
                    {



                        if (inputline[i] == 61 && i < (bts - 2) && inputline[i + 2] == 64)  // =@
                        {
                            changes++; // 16bit encoding?
                            i += 3;
                        }
                        else if (inputline[i] == 61 && i < (bts - 2) && inputline[i + 2] == 45) // =-
                        {
                            changes++; // 16bit encoding?
                            i += 3;
                        }
                        else
                        {
                            obuf[numobuf] = inputline[i];
                            numobuf++;
                        }
                    }
                    numBytes += numobuf;
                    OSR.Write(obuf, 0, numobuf);
                }

                OSR.Flush();
                OFS.Flush(true);
                OSR.Dispose();
                OFS.Close();
                OFS.Dispose();
                FS.Close();
            }
            trd.Abort();
            lblNumberOfRecords.Text = "Number of Changes: " + changes + " Number of Bytes " + numBytes;
            MessageBox.Show("Wrote Number of Changes: " + changes + " Number of Bytes " + numBytes);
        }

        private void btnDeDuplicate_Click(object sender, EventArgs e)
        {

           
        }

        private void LoadAndConcat_Click(object sender, EventArgs e)
        {
            if (InputFileNames.Length <2)
            {
                MessageBox.Show("Please select multiple files to concatenate");
                return;
            }

            Thread trd = new Thread(new ThreadStart(this.ThreadTask));
            trd.IsBackground = true;
            trd.Start();
            int numBytes = 0;



            SFileDialog = new SaveFileDialog();
            SFileDialog.Title = "Open CSV File";
            SFileDialog.Filter = "CSV files|*.csv";
            SFileDialog.CheckPathExists = false;
            SFileDialog.InitialDirectory = tbDirectory.Text;
            SFileDialog.ShowDialog();
            if (SFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("Cancelled");
                trd.Abort();
                return;
            }
            FileStream OFS = File.OpenWrite(SFileDialog.FileName);
            OFS.SetLength(0);
            OFS.Seek(0, SeekOrigin.Begin);
            BinaryWriter OSR = new BinaryWriter(OFS);

            for (int f = 0; f < InputFileNames.Length; f++)
            {
                byte[] inputline = new byte[4096];
                FileStream FS = File.OpenRead(InputFileNames[f]);
                BinaryReader sr = new BinaryReader(FS);
                //int xx = sr.Read(inputline, 0, 2);
                //if (xx < 2 || inputline[0] != 255 || inputline[1] != 254)
                //{
                //    MessageBox.Show(InputFileNames[f] + " File not 16 Bit UCode File");
                //    FS.Close();
                //    OSR.Dispose();
                //    OFS.Close();
                //    OFS.Dispose();
                //    trd.Abort();
                //    return;
                //}
                //OSR.Write(inputline, 0, 2);
                bool firstline = true;
                while (true)
                {
                    if (firstline == false || f == 0)  // Only need first line header from first file
                    {
                        int bts = sr.Read(inputline, 0, 4096);
                        if (inputline[0] != 255 || inputline[1] != 254)
                            if (bts <= 0)
                                break;
                        numBytes += bts;
                        OSR.Write(inputline, 0, bts);
                    }
                    else
                    {
                        int bts = sr.Read(inputline, 0, 4096);
                        int eol=0;
                        for(int i=3;i<bts;i++)
                        {

                            if (inputline[i-3] == 13 && inputline[i-1] == 10 ) // 16 Bit Format
                            {
                                eol = i;
                                numBytes += bts - eol;
                                OSR.Write(inputline, eol + 1, bts - (eol + 1));
                                break;
                            }
                            else if (inputline[i - 1] == 10 && inputline[i-3] != 13) // 8 Bit Format no leading byte
                            {
                                eol = i-1;
                                numBytes += bts - eol;
                                OSR.Write(inputline, eol, bts - (eol));
                                break;
                            }
                        }
                        firstline = false;
                    }
                }
                FS.Close();

                OSR.Flush();
                OFS.Flush(true);
            }
            OSR.Dispose();
            OFS.Close();
            OFS.Dispose();
            

            trd.Abort();
            lblNumberOfRecords.Text = "Number of Files: " + InputFileNames.Length + " Number of Bytes " + numBytes;
            MessageBox.Show("Wrote Number of Files: " + InputFileNames.Length + " Number of Bytes " + numBytes);
        }

        private void btnAddFiles_Click(object sender, EventArgs e)
        {
            OFileDialog = new OpenFileDialog();
            OFileDialog.Multiselect = true;
            OFileDialog.Title = "Open CSV File";
            OFileDialog.Filter = "CSV files|*.csv";
            OFileDialog.InitialDirectory = tbDirectory.Text;
            OFileDialog.ShowDialog();
            string[] LocalInputFileNames = OFileDialog.FileNames;

            if (LocalInputFileNames.Length < 1)
            {
                MessageBox.Show("Cancelled...");
                return;
            }
            //FileName = InputFileNames[0].Substring(InputFileNames[0].LastIndexOf("\\") + 1,
            //    (InputFileNames[0].Length - InputFileNames[0].LastIndexOf("\\")) - 1);

            tbDirectory.Text = InputFileNames[0].Substring(0, InputFileNames[0].LastIndexOf("\\") + 1);
            //                tbDirectoryName.Text + tbWorkItemsFileName.Text

            int olen = InputFileNames.Length;
            Array.Resize(ref InputFileNames, olen + LocalInputFileNames.Length);
            for (int i = 0; i < LocalInputFileNames.Length; i++)
            {
                lbFileList.Items.Add(LocalInputFileNames[i]);
                InputFileNames[i + olen] = LocalInputFileNames[i];
            }
            return;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (lbFileList.SelectedIndex < 1 )
                return;
            string hstr = "";
            int idx = lbFileList.SelectedIndex;
            hstr = InputFileNames[idx];
            InputFileNames[idx] = InputFileNames[idx-1];
            InputFileNames[idx - 1] = hstr;
            lbFileList.Items[idx] = InputFileNames[idx];
            lbFileList.Items[idx - 1] = InputFileNames[idx - 1];
            lbFileList.SetSelected(idx - 1,true);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (lbFileList.SelectedIndex < 0 || lbFileList.SelectedIndex >= (lbFileList.Items.Count-1))
                return;
            string hstr = "";
            int idx = lbFileList.SelectedIndex;
            hstr = InputFileNames[idx];
            InputFileNames[idx] = InputFileNames[idx + 1];
            InputFileNames[idx + 1] = hstr;
            lbFileList.Items[idx] = InputFileNames[idx];
            lbFileList.Items[idx + 1] = InputFileNames[idx + 1];
            lbFileList.SetSelected(idx + 1, true);
        }
    }
}

