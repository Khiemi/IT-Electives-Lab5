using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentSafeApp
{
    public partial class Form1 : Form
    {
        private readonly StudentRepository _repository = new StudentRepository();

        public Form1()
        {
            InitializeComponent();

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnRefresh.Click += btnRefresh_Click;
            dgvStudents.CellClick += dgvStudents_CellClick;

            LoadStudents();
        }

        private void StudentId_TextChanged(object sender, EventArgs e)
        {
        }

        private void LoadStudents()
        {
            try
            {
                dgvStudents.DataSource = _repository.GetAllStudents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading students: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtStudentId.Text.Trim();
            string name = txtName.Text.Trim();
            string course = txtCourse.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(course))
            {
                MessageBox.Show("Please fill in Student ID, Name, and Course.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _repository.AddStudent(id, name, course);
                LoadStudents();
                ClearInputs();
                MessageBox.Show("Student added.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not add student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtStudentId.Text.Trim();
            string name = txtName.Text.Trim();
            string course = txtCourse.Text.Trim();

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(course))
            {
                MessageBox.Show("Enter Student ID, Name, and Course to update.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rowsAffected = _repository.UpdateStudent(id, name, course);
                LoadStudents();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Student updated.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No student found with that Student ID.", "Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not update student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtStudentId.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Enter a Student ID to delete.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int rowsAffected = _repository.DeleteStudent(id);
                LoadStudents();

                if (rowsAffected > 0)
                {
                    ClearInputs();
                    MessageBox.Show("Student deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No student found with that Student ID.", "Not Found",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete student: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStudents.Rows[e.RowIndex];
                txtStudentId.Text = row.Cells["StudentId"].Value?.ToString() ?? string.Empty;
                txtName.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
                txtCourse.Text = row.Cells["Course"].Value?.ToString() ?? string.Empty;
            }
        }

        private void ClearInputs()
        {
            txtStudentId.Clear();
            txtName.Clear();
            txtCourse.Clear();
            txtStudentId.Focus();
        }
    }
}