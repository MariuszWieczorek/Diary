﻿
namespace Diary
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvDiary = new System.Windows.Forms.DataGridView();
            this.cboGroupOfStudent = new System.Windows.Forms.ComboBox();
            this.chkGroupOfStudents = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiary)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.YellowGreen;
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(106, 35);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(116, 49);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Dodaj";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.PeachPuff;
            this.btnEdit.Location = new System.Drawing.Point(243, 35);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(116, 49);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edycja";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.LightPink;
            this.btnDelete.Location = new System.Drawing.Point(369, 34);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(116, 49);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Usuń";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.Location = new System.Drawing.Point(520, 32);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(116, 49);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Odśwież";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvDiary
            // 
            this.dgvDiary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDiary.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDiary.Location = new System.Drawing.Point(42, 152);
            this.dgvDiary.Name = "dgvDiary";
            this.dgvDiary.RowHeadersWidth = 62;
            this.dgvDiary.RowTemplate.Height = 28;
            this.dgvDiary.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiary.Size = new System.Drawing.Size(2026, 846);
            this.dgvDiary.TabIndex = 4;
            // 
            // cboGroupOfStudent
            // 
            this.cboGroupOfStudent.FormattingEnabled = true;
            this.cboGroupOfStudent.Location = new System.Drawing.Point(1681, 53);
            this.cboGroupOfStudent.Name = "cboGroupOfStudent";
            this.cboGroupOfStudent.Size = new System.Drawing.Size(313, 28);
            this.cboGroupOfStudent.TabIndex = 23;
            this.cboGroupOfStudent.SelectedIndexChanged += new System.EventHandler(this.cboGroupOfStudent_SelectedIndexChanged);
            // 
            // chkGroupOfStudents
            // 
            this.chkGroupOfStudents.AutoSize = true;
            this.chkGroupOfStudents.Location = new System.Drawing.Point(1495, 56);
            this.chkGroupOfStudents.Name = "chkGroupOfStudents";
            this.chkGroupOfStudents.Size = new System.Drawing.Size(138, 24);
            this.chkGroupOfStudents.TabIndex = 24;
            this.chkGroupOfStudents.Text = "filtruj po grupie";
            this.chkGroupOfStudents.UseVisualStyleBackColor = true;
            this.chkGroupOfStudents.Click += new System.EventHandler(this.chkGroupOfStudents_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(2086, 1032);
            this.Controls.Add(this.chkGroupOfStudents);
            this.Controls.Add(this.cboGroupOfStudent);
            this.Controls.Add(this.dgvDiary);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Name = "Main";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvDiary;
        private System.Windows.Forms.ComboBox cboGroupOfStudent;
        private System.Windows.Forms.CheckBox chkGroupOfStudents;
    }
}

