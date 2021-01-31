using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FixHistories
{
    public partial class frmLoadStatus : Form
    {
        bool frmLoadStatusCancel = false;
        public frmLoadStatus()
        {
            InitializeComponent();
            frmLoadStatusCancel = false;
        }
        public void UpdateLoadStatus()
        {
            tbLoadStatus.Text = ProjectGlobals.LoadStatus;
            if (ProjectGlobals.LoadStatusPct > 0 && ProjectGlobals.LoadStatusPct <= 1)
                pbStatus.Value = Convert.ToInt16(ProjectGlobals.LoadStatusPct * 100);
            Application.DoEvents();
        }
        public void UpdateLoadStatus(string Status, double Pct)
        {
            tbLoadStatus.Text = Status;
            pbStatus.Value = Convert.ToInt16(Pct * 100);
            Application.DoEvents();
        }

        private void frmLoadStatus_Load(object sender, EventArgs e)
        {

        }
        public bool CancelClicked()
        {
            return frmLoadStatusCancel;
        }
        public bool CancelClicked(bool set)
        {
            bool oldval = frmLoadStatusCancel;
            frmLoadStatusCancel = set;
            return oldval;
        }
        private void btnfrmLoadStausCancel_Click(object sender, EventArgs e)
        {
            CancelClicked(true);
        }
    }
}
