namespace CertReqClient
{
    partial class lblname
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lblname));
            this.lbl_error_required_field = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.editSAN = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.addSanBtn = new System.Windows.Forms.Button();
            this.subjectTypeInput = new System.Windows.Forms.TextBox();
            this.subjectType = new System.Windows.Forms.ComboBox();
            this.label_common_name = new System.Windows.Forms.Label();
            this.comboBox_country = new System.Windows.Forms.ComboBox();
            this.textBox_commonName = new System.Windows.Forms.TextBox();
            this.btn_generate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textbox_alternativeNames = new System.Windows.Forms.TextBox();
            this.textbox_alternative_names = new System.Windows.Forms.Label();
            this.textbox_organization = new System.Windows.Forms.TextBox();
            this.label_organization = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_State = new System.Windows.Forms.TextBox();
            this.textBox_Department = new System.Windows.Forms.TextBox();
            this.Department = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_City = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_error_required_field
            // 
            this.lbl_error_required_field.AutoSize = true;
            this.lbl_error_required_field.Location = new System.Drawing.Point(120, 564);
            this.lbl_error_required_field.Name = "lbl_error_required_field";
            this.lbl_error_required_field.Size = new System.Drawing.Size(0, 13);
            this.lbl_error_required_field.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 542);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 20;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(-5, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 632);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.editSAN);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.addSanBtn);
            this.tabPage1.Controls.Add(this.subjectTypeInput);
            this.tabPage1.Controls.Add(this.subjectType);
            this.tabPage1.Controls.Add(this.label_common_name);
            this.tabPage1.Controls.Add(this.comboBox_country);
            this.tabPage1.Controls.Add(this.textBox_commonName);
            this.tabPage1.Controls.Add(this.btn_generate);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.textbox_alternativeNames);
            this.tabPage1.Controls.Add(this.textbox_alternative_names);
            this.tabPage1.Controls.Add(this.textbox_organization);
            this.tabPage1.Controls.Add(this.label_organization);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.textBox_State);
            this.tabPage1.Controls.Add(this.textBox_Department);
            this.tabPage1.Controls.Add(this.Department);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBox_City);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 606);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            // 
            // editSAN
            // 
            this.editSAN.Location = new System.Drawing.Point(195, 169);
            this.editSAN.Name = "editSAN";
            this.editSAN.Size = new System.Drawing.Size(217, 23);
            this.editSAN.TabIndex = 27;
            this.editSAN.Text = "edit SAN";
            this.editSAN.UseVisualStyleBackColor = true;
            this.editSAN.Click += new System.EventHandler(this.editSAN_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "SAN Type";
            // 
            // addSanBtn
            // 
            this.addSanBtn.Location = new System.Drawing.Point(195, 143);
            this.addSanBtn.Name = "addSanBtn";
            this.addSanBtn.Size = new System.Drawing.Size(217, 23);
            this.addSanBtn.TabIndex = 25;
            this.addSanBtn.Text = "add SAN";
            this.addSanBtn.UseVisualStyleBackColor = true;
            this.addSanBtn.Click += new System.EventHandler(this.addSanBtn_Click);
            // 
            // subjectTypeInput
            // 
            this.subjectTypeInput.Location = new System.Drawing.Point(280, 116);
            this.subjectTypeInput.Multiline = true;
            this.subjectTypeInput.Name = "subjectTypeInput";
            this.subjectTypeInput.Size = new System.Drawing.Size(132, 21);
            this.subjectTypeInput.TabIndex = 24;
            // 
            // subjectType
            // 
            this.subjectType.FormattingEnabled = true;
            this.subjectType.Location = new System.Drawing.Point(195, 116);
            this.subjectType.Name = "subjectType";
            this.subjectType.Size = new System.Drawing.Size(79, 21);
            this.subjectType.TabIndex = 23;
            // 
            // label_common_name
            // 
            this.label_common_name.AutoSize = true;
            this.label_common_name.Location = new System.Drawing.Point(99, 47);
            this.label_common_name.Name = "label_common_name";
            this.label_common_name.Size = new System.Drawing.Size(46, 13);
            this.label_common_name.TabIndex = 0;
            this.label_common_name.Text = "Domain:";
            // 
            // comboBox_country
            // 
            this.comboBox_country.FormattingEnabled = true;
            this.comboBox_country.Location = new System.Drawing.Point(195, 490);
            this.comboBox_country.Name = "comboBox_country";
            this.comboBox_country.Size = new System.Drawing.Size(217, 21);
            this.comboBox_country.TabIndex = 22;
            // 
            // textBox_commonName
            // 
            this.textBox_commonName.Location = new System.Drawing.Point(195, 44);
            this.textBox_commonName.Multiline = true;
            this.textBox_commonName.Name = "textBox_commonName";
            this.textBox_commonName.Size = new System.Drawing.Size(217, 26);
            this.textBox_commonName.TabIndex = 1;
            // 
            // btn_generate
            // 
            this.btn_generate.Location = new System.Drawing.Point(267, 534);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(145, 31);
            this.btn_generate.TabIndex = 17;
            this.btn_generate.Text = "full automated installation";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.Btn_generate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(107, 493);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Country:";
            // 
            // textbox_alternativeNames
            // 
            this.textbox_alternativeNames.Location = new System.Drawing.Point(195, 198);
            this.textbox_alternativeNames.Multiline = true;
            this.textbox_alternativeNames.Name = "textbox_alternativeNames";
            this.textbox_alternativeNames.Size = new System.Drawing.Size(217, 92);
            this.textbox_alternativeNames.TabIndex = 4;
            // 
            // textbox_alternative_names
            // 
            this.textbox_alternative_names.AutoSize = true;
            this.textbox_alternative_names.Location = new System.Drawing.Point(277, 100);
            this.textbox_alternative_names.Name = "textbox_alternative_names";
            this.textbox_alternative_names.Size = new System.Drawing.Size(127, 13);
            this.textbox_alternative_names.TabIndex = 2;
            this.textbox_alternative_names.Text = "Subject Alternative Name";
            // 
            // textbox_organization
            // 
            this.textbox_organization.Location = new System.Drawing.Point(195, 308);
            this.textbox_organization.Multiline = true;
            this.textbox_organization.Name = "textbox_organization";
            this.textbox_organization.Size = new System.Drawing.Size(217, 26);
            this.textbox_organization.TabIndex = 6;
            // 
            // label_organization
            // 
            this.label_organization.AutoSize = true;
            this.label_organization.Location = new System.Drawing.Point(76, 311);
            this.label_organization.Name = "label_organization";
            this.label_organization.Size = new System.Drawing.Size(69, 13);
            this.label_organization.TabIndex = 5;
            this.label_organization.Text = "Organization:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "State:";
            // 
            // textBox_State
            // 
            this.textBox_State.Location = new System.Drawing.Point(195, 445);
            this.textBox_State.Multiline = true;
            this.textBox_State.Name = "textBox_State";
            this.textBox_State.Size = new System.Drawing.Size(217, 28);
            this.textBox_State.TabIndex = 14;
            // 
            // textBox_Department
            // 
            this.textBox_Department.Location = new System.Drawing.Point(195, 354);
            this.textBox_Department.Multiline = true;
            this.textBox_Department.Name = "textBox_Department";
            this.textBox_Department.Size = new System.Drawing.Size(217, 26);
            this.textBox_Department.TabIndex = 8;
            // 
            // Department
            // 
            this.Department.AutoSize = true;
            this.Department.Location = new System.Drawing.Point(83, 357);
            this.Department.Name = "Department";
            this.Department.Size = new System.Drawing.Size(65, 13);
            this.Department.TabIndex = 7;
            this.Department.Text = "Department:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 404);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "City:";
            // 
            // textBox_City
            // 
            this.textBox_City.Location = new System.Drawing.Point(195, 401);
            this.textBox_City.Multiline = true;
            this.textBox_City.Name = "textBox_City";
            this.textBox_City.Size = new System.Drawing.Size(217, 26);
            this.textBox_City.TabIndex = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 606);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(504, 606);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblname
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(502, 627);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbl_error_required_field);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "lblname";
            this.RightToLeftLayout = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_error_required_field;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label_common_name;
        private System.Windows.Forms.ComboBox comboBox_country;
        private System.Windows.Forms.TextBox textBox_commonName;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textbox_alternativeNames;
        private System.Windows.Forms.Label textbox_alternative_names;
        private System.Windows.Forms.TextBox textbox_organization;
        private System.Windows.Forms.Label label_organization;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_State;
        private System.Windows.Forms.TextBox textBox_Department;
        private System.Windows.Forms.Label Department;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_City;
        private System.Windows.Forms.ComboBox subjectType;
        private System.Windows.Forms.Button addSanBtn;
        private System.Windows.Forms.TextBox subjectTypeInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button editSAN;
    }
}

