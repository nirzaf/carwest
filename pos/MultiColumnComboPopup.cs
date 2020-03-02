using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace pos
{
    public delegate void AfterRowSelectEventHandler(object sender, DataRow SelectedRow);
    /// <summary>
    /// Summary description for MultiColumnComboPopup.
    /// </summary>
    public class MultiColumnComboPopup : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private DataRow selectedRow = null;
        private System.Windows.Forms.ListView lstvMyView;
        private DataRow[] inputRows = null;
        private DataTable dataTable = null;
        private int mCols = 0;
        private int mRows = 0;
        private string[] columnsToDisplay = null;
        private string[][] data = null;

        public event AfterRowSelectEventHandler AfterRowSelectEvent;


        public MultiColumnComboPopup()
        {
            InitializeComponent();
            mCols = 4;
            mRows = 0;
            InitializeGridProperties();
        }

        public MultiColumnComboPopup(DataRow[] drows)
        {
            InitializeComponent();
            this.inputRows = drows;
            SetDataRows(inputRows);
            this.dataTable = inputRows[0].Table;
        }

        public MultiColumnComboPopup(DataTable dtable, ref DataRow selRow, string[] colsToDisplay)
        {
            InitializeComponent();
            this.dataTable = dtable;
            this.selectedRow = selRow;
            this.columnsToDisplay = colsToDisplay;
            if (columnsToDisplay != null)
                SetDataTable(dataTable, columnsToDisplay);
            else
                SetDataTable(dataTable);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstvMyView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lstvMyView
            // 
            this.lstvMyView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstvMyView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstvMyView.FullRowSelect = true;
            this.lstvMyView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstvMyView.Location = new System.Drawing.Point(0, 0);
            this.lstvMyView.MultiSelect = false;
            this.lstvMyView.Name = "lstvMyView";
            this.lstvMyView.Size = new System.Drawing.Size(459, 226);
            this.lstvMyView.TabIndex = 0;
            this.lstvMyView.UseCompatibleStateImageBehavior = false;
            this.lstvMyView.View = System.Windows.Forms.View.Details;
            this.lstvMyView.DoubleClick += new System.EventHandler(this.lstvMyView_DoubleClick);
            this.lstvMyView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstvMyView_KeyDown);
            // 
            // MultiColumnComboPopup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(459, 226);
            this.ControlBox = false;
            this.Controls.Add(this.lstvMyView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MultiColumnComboPopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Deactivate += new System.EventHandler(this.MultiColumnComboPopup_Deactivate);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MultiColumnComboPopup_KeyDown);
            this.Leave += new System.EventHandler(this.MultiColumnComboPopup_Leave);
            this.ResumeLayout(false);

        }
        #endregion

        #region Functions
        private void InitializeGridProperties()
        {
            lstvMyView.Items.Clear();
            lstvMyView.Columns.Clear();

            //Add Data Headers
            for (int i = 0; i < mCols; i++)
            {
                lstvMyView.Columns.Add(columnsToDisplay[i], -1, HorizontalAlignment.Left);
            }

            //Add Empty Values
            for (int i = 0; i < mRows; i++)
            {
                ListViewItem item = new ListViewItem("");
                lstvMyView.Items.Add(item);
           
                for (int j = 0; j < mCols; j++)
                {
                    item.SubItems.Add(" ");
                }
            }

        }

        public void SetCellValue(int Row, int Column, string ItemValue)
        {
            if (IsValidRowAndColumn(Row, Column))
            {
                if (Column == 0)
                {
                    lstvMyView.Items[Row].Text = ItemValue;
                }
                else
                {
                    lstvMyView.Items[Row].SubItems[Column].Text = ItemValue;
                }
            }
        }

        public string GetCellValue(int Row, int Column)
        {
            if (IsValidRowAndColumn(Row, Column))
            {
                if (Column == 0)
                {
                    return lstvMyView.Items[Row].Text;
                }
                else
                {
                    return lstvMyView.Items[Row].SubItems[Column].Text;
                }
            }
            else
            {
                return null;
            }
        }

        public void SetFullRow(int Row, string[] ItemValues)
        {
            if (IsValidRow(Row))
            {
                if (ItemValues.Length > 0)
                {
                    for (int i = 0; i < ItemValues.Length; i++)
                    {
                        SetCellValue(Row, i, ItemValues[i]);
                    }
                }
                else
                {
                    MessageBox.Show(this, "SetFullRow: Empty Values Sent", "Grid Error");
                }
            }
        }

        public void SetFullRow(int Row, DataRow drow)
        {
            if (IsValidRow(Row))
            {
                if (mCols > 0)
                {
                    for (int i = 0; i < mCols; i++)
                    {
                        SetCellValue(Row, i, (string)drow[i]);
                    }
                }
                else
                {
                    MessageBox.Show(this, "SetFullRow: Empty Values Sent", "Grid Error");
                }
            }
        }

        public void SetFullColumn(int Column, string[] ItemValues)
        {
            if (IsValidColumn(Column))
            {
                if (ItemValues.Length > 0)
                {
                    for (int i = 0; i < lstvMyView.Items.Count; i++)
                    {
                        SetCellValue(i, Column, ItemValues[i]);
                    }
                }
                else
                {
                    MessageBox.Show(this, "SetFullColumn: Empty Values Sent", "Grid Error");
                }
            }
        }

        public string[] GetFullRow(int Row)
        {
            string[] ItemValues = new string[lstvMyView.Columns.Count];
            if (IsValidRow(Row))
            {
                for (int i = 0; i < lstvMyView.Items.Count; i++)
                {
                    ItemValues[i] = GetCellValue(Row, i);
                }
                return ItemValues;
            }
            else
            {
                return null;
            }
        }

        public string[] GetFullColumn(int Column)
        {
            string[] ItemValues = new string[lstvMyView.Items.Count];
            if (IsValidColumn(Column))
            {
                for (int i = 0; i < lstvMyView.Items.Count; i++)
                {
                    ItemValues[i] = GetCellValue(i, Column);
                }
                return ItemValues;
            }
            else
            {
                return null;
            }
        }

        public void AddItems(string[][] ItemValues)
        {
            if (ItemValues.Length > 0)
            {
                mRows = ItemValues.Length;
                mCols = ItemValues[0].Length;
                InitializeGridProperties();
                for (int i = 0; i < mRows; i++)
                {
                    SetFullRow(i, ItemValues[i]);
                }
            }
            else
            {
                MessageBox.Show(this, "AddItems: Empty Values Sent", "Grid Error");
            }
        }

        public string[][] GetItems()
        {
            string[][] ItemValues = new string[lstvMyView.Items.Count][];
            for (int i = 0; i < lstvMyView.Items.Count; i++)
            {
                ItemValues[i] = GetFullRow(i);
            }
            return ItemValues;
        }

        public void SetColumnHeaderNames(string[] ColumnNames)
        {
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                if (i >= lstvMyView.Columns.Count)
                {
                    lstvMyView.Columns.Add(ColumnNames[i], 25, HorizontalAlignment.Center);
                    lstvMyView.Columns[i].Width = ColumnNames[i].Length * (int)Font.SizeInPoints;
                }
                else
                {
                    lstvMyView.Columns[i].Text = ColumnNames[i];
                    lstvMyView.Columns[i].Width = ColumnNames[i].Length * (int)Font.SizeInPoints;
                }
                
            }
        }

        public void SetColumnWidths(int[] Widths)
        {
          //  MessageBox.Show("as");
            for (int i = 0; i < Widths.Length; i++)
            {
                if (i >= lstvMyView.Columns.Count)
                {
                    lstvMyView.Columns.Add("", Widths[i] * (int)lstvMyView.Font.SizeInPoints, HorizontalAlignment.Center);
                }
                else
                {
                    lstvMyView.Columns[i].Width = Widths[i] * (int)Font.SizeInPoints;
                }
            }
        }

        public void SetColumnWidth(int Column, int ColWidth)
        {
            if (IsValidColumn(Column))
            {
                lstvMyView.Columns[Column].Width = ColWidth * (int)Font.SizeInPoints;
            }
        }

        public void SetDataTable(DataTable dtable)
        {
            DataColumnCollection dcc = dtable.Columns;
            DataRowCollection drc = dtable.Rows;
            data = new string[drc.Count][];
            string[] strarrColNames = null;
            int ictr = 0;

            for (int i = 0; i < drc.Count; i++)
            {
                data[i] = new string[dcc.Count];
            }

            foreach (DataRow dr in drc)
            {
                for (int i = 0; i < dcc.Count; i++)
                {
                    data[ictr][i] = dr[i].ToString();
                }
                ictr++;
            }

            strarrColNames = new string[dcc.Count];
            for (int i = 0; i < dcc.Count; i++)
            {
                strarrColNames[i] = dcc[i].ColumnName;
            }

            AddItems(data);
            //SetColumnHeaderNames(strarrColNames);
        }

        public void SetDataTable(DataTable dtable, string[] colsToDisplay)
        {
            DataRowCollection drc = dtable.Rows;
            data = new string[drc.Count][];
            int ictr = 0;

            for (int i = 0; i < drc.Count; i++)
            {
                data[i] = new string[colsToDisplay.Length];
            }

            foreach (DataRow dr in drc)
            {
                for (int i = 0; i < colsToDisplay.Length; i++)
                {
                    data[ictr][i] = dr[colsToDisplay[i]].ToString();
                }
                ictr++;
            }

            AddItems(data);
            //SetColumnHeaderNames(colsToDisplay);
        }

        private bool IsValidRowAndColumn(int Row, int Column)
        {
            if (Column < 0 || Row < 0)
            {
                MessageBox.Show(this, "Row or Column Cannot be Negative!", "Grid Error");
                return false;
            }
            else if (Row > lstvMyView.Items.Count)
            {
                MessageBox.Show(this, "SetCellValue: Row Out Of Range", "Grid Error");
                return false;
            }
            else if (Column > lstvMyView.Columns.Count)
            {
                MessageBox.Show(this, "SetCellValue: Column Out Of Range " + Column.ToString(), "Grid Error");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidRow(int Row)
        {
            if (Row < 0)
            {
                MessageBox.Show(this, "Row Cannot be Negative!", "Grid Error");
                return false;
            }
            else if (Row > lstvMyView.Items.Count)
            {
                MessageBox.Show(this, "Row Out Of Range", "Grid Error");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsValidColumn(int Column)
        {
            if (Column < 0)
            {
                MessageBox.Show(this, "Column Cannot be Negative!", "Grid Error");
                return false;
            }
            else if (Column > lstvMyView.Columns.Count)
            {
                MessageBox.Show(this, "Column Out Of Range " + Column.ToString(), "Grid Error");
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        private void gridValue_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //
        }

        public void SetDataRows(DataRow[] drows)
        {
            mRows = drows.Length;
            mCols = drows[0].Table.Columns.Count;
            InitializeGridProperties();
            for (int i = 0; i < mRows; i++)
            {
                SetFullRow(i, drows[i]);
            }
            SetColumnHeaderNames(GetColumnNames(drows[0]));
        }

        private string[] GetColumnNames(DataRow drow)
        {
            string[] strColNames = new string[drow.Table.Columns.Count];
            for (int i = 0; i < drow.Table.Columns.Count; i++)
            {
                strColNames[i] = drow.Table.Columns[i].ColumnName;
            }
            return strColNames;
        }

        private void MultiColumnComboPopup_Deactivate(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MultiColumnComboPopup_Leave(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void MultiColumnComboPopup_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void lstvMyView_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (lstvMyView.SelectedItems.Count > 0)
                {
                    selectedRow = dataTable.Rows[lstvMyView.SelectedIndices[0]];
                    if (AfterRowSelectEvent != null)
                        AfterRowSelectEvent(this, selectedRow);
                }
                this.Close();
            }
        }

        private void lstvMyView_DoubleClick(object sender, System.EventArgs e)
        {
            if (lstvMyView.SelectedItems.Count > 0)
            {
                selectedRow = dataTable.Rows[lstvMyView.SelectedIndices[0]];
                if (AfterRowSelectEvent != null)
                    AfterRowSelectEvent(this, selectedRow);
            }
            this.Close();
        }

        private string[] GetColumnNames(DataTable dtable)
        {
            string[] strColNames = new string[dtable.Columns.Count];
            for (int i = 0; i < dtable.Columns.Count; i++)
            {
                strColNames[i] = dtable.Columns[i].ColumnName;
            }
            return strColNames;
        }

        public DataRow[] InputRows
        {
            set
            {
                inputRows = value;
                if (inputRows == null)
                    return;
                SetDataRows(inputRows);
            }
        }

        //		public string[] NonDisplayColumnNames{
        //			set{
        //				
        //			}
        //		}

        public DataTable Table
        {
            set
            {
                dataTable = value;
                if (dataTable == null)
                    return;
                SetDataTable(dataTable);
            }
        }

        public DataRow SelectedRow
        {
            get
            {
                return selectedRow;
            }
        }

        public int Cols
        {
            get
            {
                mCols = lstvMyView.Columns.Count;
                return mCols;
            }
        }

        public int Rows
        {
            get
            {
                mRows = lstvMyView.Items.Count;
                return mRows;
            }
        }

    }
}
