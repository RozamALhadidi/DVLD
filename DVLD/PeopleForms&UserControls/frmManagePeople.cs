using DVLD_BusinessLayers;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();

            this.Size = AppTheme.MainForms;
            this.BackColor = AppTheme.FormBackColor;

            RefreshData();
            EditDataGridViewShape();
            ChangeDataGridViewHeadersName();
            PrepareFilter();

            cmsUserOptions.Renderer = new SlateRenderer();
        }

        private short GetSelectedID()
        {
            if (dgvManagePeople.CurrentRow == null)
                return -1;

            object objID = dgvManagePeople.CurrentRow.Cells["PersonID"].Value;
            return short.TryParse(objID?.ToString(), out short result) ? result : (short)-1;
        }

        private void PrepareFilter()
        {
            cbFilter.Items.Clear();
            cbFilter.Items.Add("None");
            foreach (DataGridViewColumn col in dgvManagePeople.Columns)
            {
                cbFilter.Items.Add(col.HeaderText);
            }
            cbFilter.SelectedIndex = 0;
        }

        private void RefreshData()
        {
            DataTable dtPeople = clsPerson.GetAllPeopleForDGV();
            dgvManagePeople.DataSource = dtPeople;
            lblTotalRecords.Text = dtPeople.DefaultView.Count.ToString();
        }

        private void EditDataGridViewShape()
        {
            if (dgvManagePeople.Columns.Contains("PersonID"))
            {
                dgvManagePeople.Columns["PersonID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvManagePeople.Columns["PersonID"].Width = 90;
            }

            if (dgvManagePeople.Columns.Contains("DateOfBirth"))
            {
                dgvManagePeople.Columns["DateOfBirth"].DefaultCellStyle.Format = "dd MMM yyyy";
            }
        }

        private void ChangeDataGridViewHeadersName()
        {
            dgvManagePeople.Columns["PersonID"].HeaderText = "Person ID";
            dgvManagePeople.Columns["FirstName"].HeaderText = "First Name";
            dgvManagePeople.Columns["SecondName"].HeaderText = "Second Name";
            dgvManagePeople.Columns["ThirdName"].HeaderText = "Third Name";
            dgvManagePeople.Columns["LastName"].HeaderText = "Last Name";
            dgvManagePeople.Columns["DateOfBirth"].HeaderText = "Date Of Birth";
        }

        private void dgvManagePeople_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvManagePeople.Columns[e.ColumnIndex].Name == "Gender")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    if (e.Value.ToString() == "0")
                    {
                        e.Value = "Male";
                        e.FormattingApplied = true;
                    }
                    else if (e.Value.ToString() == "1")
                    {
                        e.Value = "Female";
                        e.FormattingApplied = true;
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddNewPerson();
            frm.ShowDialog();
            RefreshData(); 
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddNewPerson();
            frm.ShowDialog();
            RefreshData(); 
        }

        private void dgvManagePeople_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvManagePeople.ClearSelection();
                dgvManagePeople.Rows[e.RowIndex].Selected = true;
                dgvManagePeople.CurrentCell = dgvManagePeople.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cmsUserOptions.Show(Cursor.Position);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            short SelectedID = GetSelectedID();
            if (SelectedID == -1)
            {
                MessageBox.Show("No person is selected, or person was not found!", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Form frm = new frmShowPersonDetails(SelectedID);
                frm.ShowDialog();
                RefreshData(); 
            }
        }

        private void EditUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            short SelectedID = GetSelectedID();
            if (SelectedID == -1)
            {
                MessageBox.Show("No person is selected, or person was not found!", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Form frm = new frmUpdatePersonDetails(SelectedID);
                frm.ShowDialog();
                RefreshData(); 
            }
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            short SelectedID = GetSelectedID();
            if (SelectedID == -1)
            {
                MessageBox.Show("Please select a valid person first!", "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete Person with ID [{SelectedID}]?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2); 

            if (confirm == DialogResult.Yes)
            {
                if (clsPerson.DeletePerson(SelectedID))
                {
                    MessageBox.Show("Person has been deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                }
                else
                {
                    MessageBox.Show("Person could not be deleted because they have data linked to them in the system.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if (cbFilter.Text == "None")
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
                txtFilter.Focus(); 

                DataGridViewColumn columnToSort = null;
                foreach (DataGridViewColumn col in dgvManagePeople.Columns)
                {
                    if (col.HeaderText == cbFilter.Text)
                    {
                        columnToSort = col;
                        break;
                    }
                }
                if (columnToSort != null)
                {
                    dgvManagePeople.Sort(columnToSort, ListSortDirection.Ascending);
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataTable dtPeople = (DataTable)dgvManagePeople.DataSource;
            string filterValue = txtFilter.Text.Trim(); 

            if (string.IsNullOrWhiteSpace(filterValue) || cbFilter.Text == "None")
            {
                dtPeople.DefaultView.RowFilter = "";
                lblTotalRecords.Text = dtPeople.DefaultView.Count.ToString(); 
                return;
            }

            string selectedHeader = cbFilter.Text;
            string realColumnName = "";

            foreach (DataGridViewColumn col in dgvManagePeople.Columns)
            {
                if (col.HeaderText == selectedHeader)
                {
                    realColumnName = col.Name;
                    break;
                }
            }

            if (selectedHeader == "Person ID")
            {
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", realColumnName, filterValue);
            }
            else
            {
                dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", realColumnName, filterValue);
            }

            lblTotalRecords.Text = dtPeople.DefaultView.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID")
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            else if (cbFilter.Text == "Phone")
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
           
        }

        private void dgvManagePeople_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            short id = GetSelectedID();
            if (id == -1)
                return;

            using (frmShowPersonDetails frm = new frmShowPersonDetails(id))
            {
                frm.ShowDialog();
            }

            RefreshData();
        }
    }
}