using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GitFitDAL;
using GitFitDAL.DataTransferObjects;
using GitFitFacade;


namespace GeneralUGFTestharness_WinForms
{
    public partial class Form1 : Form
    {
        Guid _moveId;
        Guid _userId;

        public Form1()
        {
            InitializeComponent();

            _moveId = "88C423A0-FCCD-4932-B9C0-2CC8A4879FAB".ToGuid();
            _userId = "834D5036-CF60-48A3-BF83-D58F9AD38B53".ToGuid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ProcessManager mgr = new ProcessManager();
            PreemtiveDownLoad ped = mgr.RetrievePreemtiveObject(_userId, _moveId);
        }
    }
}
;