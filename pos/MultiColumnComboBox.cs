using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace pos.InheritedCombo
{
    /// <summary>
    /// Summary description for MultiColumnComboBox.
    /// </summary>
    public delegate void AfterSelectEventHandler();

    public class MultiColumnComboBox : System.Windows.Forms.ComboBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private DataRow selectedRow = null;
        private string displayMember = "";
        private string displayValue = "";
        private DataTable dataTable = null;
        private DataRow[] dataRows = null;
        private string[] columnsToDisplay = null;

        public event AfterSelectEventHandler AfterSelectEvent;

        public MultiColumnComboBox(System.ComponentModel.IContainer container)
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            container.Add(this);
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        public MultiColumnComboBox()
        {
            InitializeComponent();
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion Component Designer generated code

        protected override void OnDropDown(System.EventArgs e)
        {
            Form parent = this.FindForm();
            if (this.dataTable != null || this.dataRows != null)
            {
                MultiColumnComboPopup popup = new MultiColumnComboPopup(this.dataTable, ref this.selectedRow, columnsToDisplay);

                popup.AfterRowSelectEvent += new AfterRowSelectEventHandler(MultiColumnComboBox_AfterSelectEvent);
                popup.Location = new Point(parent.Left + this.Left + 8, parent.Top + this.Bottom + this.Height + 8);
                popup.Show();
                if (popup.SelectedRow != null)
                {
                    try
                    {
                        this.selectedRow = popup.SelectedRow;
                        this.displayValue = popup.SelectedRow[this.displayMember].ToString();
                        this.Text = this.displayValue;
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show(e2.Message, "Error");
                    }
                }
                if (AfterSelectEvent != null)
                    AfterSelectEvent();
            }
            base.OnDropDown(e);
        }

        private void MultiColumnComboBox_AfterSelectEvent(object sender, DataRow drow)
        {
            try
            {
                if (drow != null)
                {
                    this.Text = drow[displayMember].ToString();
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(this, exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataRow SelectedRow
        {
            get
            {
                return selectedRow;
            }
        }

        public string DisplayValue
        {
            get
            {
                return displayValue;
            }
        }

        public new string DisplayMember
        {
            set
            {
                displayMember = value;
            }
        }

        public DataTable Table
        {
            set
            {
                dataTable = value;
                if (dataTable == null)
                    return;
                selectedRow = dataTable.NewRow();
            }
        }

        public DataRow[] Rows
        {
            set
            {
                dataRows = value;
            }
        }

        public string[] ColumnsToDisplay
        {
            set
            {
                columnsToDisplay = value;
            }
        }
    }
}