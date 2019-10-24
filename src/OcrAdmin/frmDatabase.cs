using BusinessLayer;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class frmDatabase : Form
    {
        Employees employees = new Employees();
        public frmDatabase()
        {
            InitializeComponent();
        }

        private void Database_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = employees.GetEmployeeList();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Refresh();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell != null)
            {
                var emp = employees.GetEmployeeById((int)((DataGridView)sender).CurrentRow.Cells["Id"].Value);
                employees.Update(emp);
            }
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will remove all existing Employee Data.", "Please Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "CSV|*.csv";
                var res = dlg.ShowDialog();
                if (res == DialogResult.OK)
                {
                    var d = dlg.FileName;
                    var file = File.ReadAllLines(d);
                    employees.RemoveEmployees();
                    foreach (string s in file.Skip(1))
                    {
                        employees.AddNewEmployee(s.Split(',')[0], s.Split(',')[1], s.Split(',')[2], s.Split(',')[3]);
                    }
                    MessageBox.Show("Data Updated");
                }
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            File.Delete(@"C:\logs\EmployeeData.csv");
            File.AppendAllLines(@"C:\logs\EmployeeData.csv", new[] { $"DataField1,DataField2,PinCode,EmailAddress" });
            foreach (var emp in employees.GetEmployeeList())
            {
                File.AppendAllLines(@"C:\logs\EmployeeData.csv", new[] { $"{emp.DataField1},{emp.DataField2},{emp.PinCode},{emp.EmailAddress}" });
            }
            MessageBox.Show("Data Export Complete", "C:\\Logs\\EmployeeData.csv", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



    }
}
