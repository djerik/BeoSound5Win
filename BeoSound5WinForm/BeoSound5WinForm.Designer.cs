namespace BeoSound5WinForm
{
    partial class BeoSound5WinForm
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
            this.ReadHID_VID_Input = new System.Windows.Forms.TextBox();
            this.ReadHID_PID_Input = new System.Windows.Forms.TextBox();
            this.SendHID_UsagePage_Input = new System.Windows.Forms.TextBox();
            this.SendHID_Usage_Input = new System.Windows.Forms.TextBox();
            this.SendHID_RID_Input = new System.Windows.Forms.TextBox();
            this.Read_Output = new System.Windows.Forms.TextBox();
            this.Read_Button = new System.Windows.Forms.Button();
            this.label_ReadHID_PID = new System.Windows.Forms.Label();
            this.label_ReadHID_VID = new System.Windows.Forms.Label();
            this.gb_filter = new System.Windows.Forms.GroupBox();
            this.ManufacturerName = new System.Windows.Forms.TextBox();
            this.ProductName = new System.Windows.Forms.TextBox();
            this.SerialNumber = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Byte6_Input = new System.Windows.Forms.TextBox();
            this.Byte5_Input = new System.Windows.Forms.TextBox();
            this.Byte4_Input = new System.Windows.Forms.TextBox();
            this.Byte3_Input = new System.Windows.Forms.TextBox();
            this.Byte2_Input = new System.Windows.Forms.TextBox();
            this.Byte1_Input = new System.Windows.Forms.TextBox();
            this.label_SendHID_RID = new System.Windows.Forms.Label();
            this.label_SendHID_Usage = new System.Windows.Forms.Label();
            this.Byte0_Input = new System.Windows.Forms.TextBox();
            this.label_SendHID_UsagePage = new System.Windows.Forms.Label();
            this.Send_Button = new System.Windows.Forms.Button();
            this.label_SendHID_PID = new System.Windows.Forms.Label();
            this.label_SendHID_VID = new System.Windows.Forms.Label();
            this.SendHID_VID_Input = new System.Windows.Forms.TextBox();
            this.SendHID_PID_Input = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.gb_filter.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ReadHID_VID_Input
            // 
            this.ReadHID_VID_Input.Location = new System.Drawing.Point(124, 23);
            this.ReadHID_VID_Input.Name = "ReadHID_VID_Input";
            this.ReadHID_VID_Input.Size = new System.Drawing.Size(100, 20);
            this.ReadHID_VID_Input.TabIndex = 0;
            this.ReadHID_VID_Input.Text = "0cd4";
            // 
            // ReadHID_PID_Input
            // 
            this.ReadHID_PID_Input.Location = new System.Drawing.Point(124, 57);
            this.ReadHID_PID_Input.Name = "ReadHID_PID_Input";
            this.ReadHID_PID_Input.Size = new System.Drawing.Size(100, 20);
            this.ReadHID_PID_Input.TabIndex = 1;
            this.ReadHID_PID_Input.Text = "1112";
            // 
            // SendHID_UsagePage_Input
            // 
            this.SendHID_UsagePage_Input.Location = new System.Drawing.Point(123, 85);
            this.SendHID_UsagePage_Input.Name = "SendHID_UsagePage_Input";
            this.SendHID_UsagePage_Input.Size = new System.Drawing.Size(100, 20);
            this.SendHID_UsagePage_Input.TabIndex = 2;
            this.SendHID_UsagePage_Input.Text = "3C";
            // 
            // SendHID_Usage_Input
            // 
            this.SendHID_Usage_Input.Location = new System.Drawing.Point(123, 117);
            this.SendHID_Usage_Input.Name = "SendHID_Usage_Input";
            this.SendHID_Usage_Input.Size = new System.Drawing.Size(100, 20);
            this.SendHID_Usage_Input.TabIndex = 3;
            // 
            // SendHID_RID_Input
            // 
            this.SendHID_RID_Input.Location = new System.Drawing.Point(123, 148);
            this.SendHID_RID_Input.Name = "SendHID_RID_Input";
            this.SendHID_RID_Input.Size = new System.Drawing.Size(100, 20);
            this.SendHID_RID_Input.TabIndex = 4;
            // 
            // Read_Output
            // 
            this.Read_Output.Location = new System.Drawing.Point(15, 146);
            this.Read_Output.Name = "Read_Output";
            this.Read_Output.Size = new System.Drawing.Size(209, 20);
            this.Read_Output.TabIndex = 15;
            // 
            // Read_Button
            // 
            this.Read_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Read_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Read_Button.ForeColor = System.Drawing.Color.White;
            this.Read_Button.Location = new System.Drawing.Point(15, 93);
            this.Read_Button.Name = "Read_Button";
            this.Read_Button.Size = new System.Drawing.Size(209, 43);
            this.Read_Button.TabIndex = 7;
            this.Read_Button.Text = "Read";
            this.Read_Button.UseVisualStyleBackColor = true;
            this.Read_Button.Click += new System.EventHandler(this.HIDRead_Button_Click);
            // 
            // label_ReadHID_PID
            // 
            this.label_ReadHID_PID.AutoSize = true;
            this.label_ReadHID_PID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ReadHID_PID.Location = new System.Drawing.Point(12, 57);
            this.label_ReadHID_PID.Name = "label_ReadHID_PID";
            this.label_ReadHID_PID.Size = new System.Drawing.Size(88, 13);
            this.label_ReadHID_PID.TabIndex = 6;
            this.label_ReadHID_PID.Text = "Product ID (PID):";
            // 
            // label_ReadHID_VID
            // 
            this.label_ReadHID_VID.AutoSize = true;
            this.label_ReadHID_VID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label_ReadHID_VID.Location = new System.Drawing.Point(11, 26);
            this.label_ReadHID_VID.Name = "label_ReadHID_VID";
            this.label_ReadHID_VID.Size = new System.Drawing.Size(88, 13);
            this.label_ReadHID_VID.TabIndex = 5;
            this.label_ReadHID_VID.Text = "Vendor  ID (VID):";
            // 
            // gb_filter
            // 
            this.gb_filter.Controls.Add(this.ManufacturerName);
            this.gb_filter.Controls.Add(this.ProductName);
            this.gb_filter.Controls.Add(this.SerialNumber);
            this.gb_filter.Controls.Add(this.Read_Button);
            this.gb_filter.Controls.Add(this.label_ReadHID_PID);
            this.gb_filter.Controls.Add(this.label_ReadHID_VID);
            this.gb_filter.Controls.Add(this.Read_Output);
            this.gb_filter.Controls.Add(this.ReadHID_VID_Input);
            this.gb_filter.Controls.Add(this.ReadHID_PID_Input);
            this.gb_filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.gb_filter.ForeColor = System.Drawing.Color.White;
            this.gb_filter.Location = new System.Drawing.Point(14, 14);
            this.gb_filter.Name = "gb_filter";
            this.gb_filter.Size = new System.Drawing.Size(240, 277);
            this.gb_filter.TabIndex = 17;
            this.gb_filter.TabStop = false;
            this.gb_filter.Text = "Device Description:";
            // 
            // ManufacturerName
            // 
            this.ManufacturerName.Location = new System.Drawing.Point(15, 190);
            this.ManufacturerName.Name = "ManufacturerName";
            this.ManufacturerName.Size = new System.Drawing.Size(209, 20);
            this.ManufacturerName.TabIndex = 18;
            // 
            // ProductName
            // 
            this.ProductName.Location = new System.Drawing.Point(15, 216);
            this.ProductName.Name = "ProductName";
            this.ProductName.Size = new System.Drawing.Size(209, 20);
            this.ProductName.TabIndex = 17;
            // 
            // SerialNumber
            // 
            this.SerialNumber.Location = new System.Drawing.Point(15, 242);
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.Size = new System.Drawing.Size(209, 20);
            this.SerialNumber.TabIndex = 16;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.Byte6_Input);
            this.groupBox2.Controls.Add(this.Byte5_Input);
            this.groupBox2.Controls.Add(this.Byte4_Input);
            this.groupBox2.Controls.Add(this.Byte3_Input);
            this.groupBox2.Controls.Add(this.Byte2_Input);
            this.groupBox2.Controls.Add(this.Byte1_Input);
            this.groupBox2.Controls.Add(this.label_SendHID_RID);
            this.groupBox2.Controls.Add(this.label_SendHID_Usage);
            this.groupBox2.Controls.Add(this.Byte0_Input);
            this.groupBox2.Controls.Add(this.label_SendHID_UsagePage);
            this.groupBox2.Controls.Add(this.Send_Button);
            this.groupBox2.Controls.Add(this.label_SendHID_PID);
            this.groupBox2.Controls.Add(this.label_SendHID_VID);
            this.groupBox2.Controls.Add(this.SendHID_VID_Input);
            this.groupBox2.Controls.Add(this.SendHID_PID_Input);
            this.groupBox2.Controls.Add(this.SendHID_UsagePage_Input);
            this.groupBox2.Controls.Add(this.SendHID_RID_Input);
            this.groupBox2.Controls.Add(this.SendHID_Usage_Input);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(273, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(240, 388);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Device Description:";
            // 
            // Byte6_Input
            // 
            this.Byte6_Input.Location = new System.Drawing.Point(202, 187);
            this.Byte6_Input.Name = "Byte6_Input";
            this.Byte6_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte6_Input.TabIndex = 20;
            this.Byte6_Input.TextChanged += new System.EventHandler(this.Read_Byte6_Input);
            // 
            // Byte5_Input
            // 
            this.Byte5_Input.Location = new System.Drawing.Point(171, 187);
            this.Byte5_Input.Name = "Byte5_Input";
            this.Byte5_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte5_Input.TabIndex = 19;
            this.Byte5_Input.TextChanged += new System.EventHandler(this.Read_Byte5_Input);
            // 
            // Byte4_Input
            // 
            this.Byte4_Input.Location = new System.Drawing.Point(140, 187);
            this.Byte4_Input.Name = "Byte4_Input";
            this.Byte4_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte4_Input.TabIndex = 18;
            this.Byte4_Input.TextChanged += new System.EventHandler(this.Read_Byte4_Input);
            // 
            // Byte3_Input
            // 
            this.Byte3_Input.Location = new System.Drawing.Point(109, 187);
            this.Byte3_Input.Name = "Byte3_Input";
            this.Byte3_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte3_Input.TabIndex = 17;
            this.Byte3_Input.TextChanged += new System.EventHandler(this.Read_Byte3_Input);
            // 
            // Byte2_Input
            // 
            this.Byte2_Input.Location = new System.Drawing.Point(78, 187);
            this.Byte2_Input.Name = "Byte2_Input";
            this.Byte2_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte2_Input.TabIndex = 16;
            this.Byte2_Input.TextChanged += new System.EventHandler(this.Read_Byte2_Input);
            // 
            // Byte1_Input
            // 
            this.Byte1_Input.Location = new System.Drawing.Point(47, 187);
            this.Byte1_Input.Name = "Byte1_Input";
            this.Byte1_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte1_Input.TabIndex = 15;
            this.Byte1_Input.TextChanged += new System.EventHandler(this.Read_Byte1_Input);
            // 
            // label_SendHID_RID
            // 
            this.label_SendHID_RID.AutoSize = true;
            this.label_SendHID_RID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SendHID_RID.Location = new System.Drawing.Point(15, 151);
            this.label_SendHID_RID.Name = "label_SendHID_RID";
            this.label_SendHID_RID.Size = new System.Drawing.Size(84, 13);
            this.label_SendHID_RID.TabIndex = 14;
            this.label_SendHID_RID.Text = "Report ID (RID):";
            // 
            // label_SendHID_Usage
            // 
            this.label_SendHID_Usage.AutoSize = true;
            this.label_SendHID_Usage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SendHID_Usage.Location = new System.Drawing.Point(15, 120);
            this.label_SendHID_Usage.Name = "label_SendHID_Usage";
            this.label_SendHID_Usage.Size = new System.Drawing.Size(41, 13);
            this.label_SendHID_Usage.TabIndex = 12;
            this.label_SendHID_Usage.Text = "Usage:";
            // 
            // Byte0_Input
            // 
            this.Byte0_Input.Location = new System.Drawing.Point(16, 187);
            this.Byte0_Input.Name = "Byte0_Input";
            this.Byte0_Input.Size = new System.Drawing.Size(25, 20);
            this.Byte0_Input.TabIndex = 14;
            this.Byte0_Input.Text = "40";
            this.Byte0_Input.TextChanged += new System.EventHandler(this.Read_Byte0_Input);
            // 
            // label_SendHID_UsagePage
            // 
            this.label_SendHID_UsagePage.AutoSize = true;
            this.label_SendHID_UsagePage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SendHID_UsagePage.Location = new System.Drawing.Point(15, 88);
            this.label_SendHID_UsagePage.Name = "label_SendHID_UsagePage";
            this.label_SendHID_UsagePage.Size = new System.Drawing.Size(69, 13);
            this.label_SendHID_UsagePage.TabIndex = 10;
            this.label_SendHID_UsagePage.Text = "Usage Page:";
            // 
            // Send_Button
            // 
            this.Send_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Send_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Send_Button.ForeColor = System.Drawing.Color.White;
            this.Send_Button.Location = new System.Drawing.Point(16, 219);
            this.Send_Button.Name = "Send_Button";
            this.Send_Button.Size = new System.Drawing.Size(208, 43);
            this.Send_Button.TabIndex = 7;
            this.Send_Button.Text = "Send";
            this.Send_Button.UseVisualStyleBackColor = true;
            this.Send_Button.Click += new System.EventHandler(this.HIDWrite_Button_Click);
            // 
            // label_SendHID_PID
            // 
            this.label_SendHID_PID.AutoSize = true;
            this.label_SendHID_PID.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.854546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_SendHID_PID.Location = new System.Drawing.Point(13, 57);
            this.label_SendHID_PID.Name = "label_SendHID_PID";
            this.label_SendHID_PID.Size = new System.Drawing.Size(88, 13);
            this.label_SendHID_PID.TabIndex = 6;
            this.label_SendHID_PID.Text = "Product ID (PID):";
            // 
            // label_SendHID_VID
            // 
            this.label_SendHID_VID.AutoSize = true;
            this.label_SendHID_VID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label_SendHID_VID.Location = new System.Drawing.Point(12, 26);
            this.label_SendHID_VID.Name = "label_SendHID_VID";
            this.label_SendHID_VID.Size = new System.Drawing.Size(88, 13);
            this.label_SendHID_VID.TabIndex = 5;
            this.label_SendHID_VID.Text = "Vendor  ID (VID):";
            // 
            // SendHID_VID_Input
            // 
            this.SendHID_VID_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SendHID_VID_Input.Location = new System.Drawing.Point(123, 23);
            this.SendHID_VID_Input.Name = "SendHID_VID_Input";
            this.SendHID_VID_Input.Size = new System.Drawing.Size(100, 20);
            this.SendHID_VID_Input.TabIndex = 1;
            this.SendHID_VID_Input.Text = "0cd4";
            // 
            // SendHID_PID_Input
            // 
            this.SendHID_PID_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.SendHID_PID_Input.Location = new System.Drawing.Point(123, 54);
            this.SendHID_PID_Input.Name = "SendHID_PID_Input";
            this.SendHID_PID_Input.Size = new System.Drawing.Size(100, 20);
            this.SendHID_PID_Input.TabIndex = 2;
            this.SendHID_PID_Input.Text = "1112";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(16, 268);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 43);
            this.button1.TabIndex = 21;
            this.button1.Text = "Turn On";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.818182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(16, 317);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(208, 43);
            this.button2.TabIndex = 22;
            this.button2.Text = "Turn Off";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(528, 482);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_filter);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form";
            this.Text = "BeoSound5 WinForm";
            this.gb_filter.ResumeLayout(false);
            this.gb_filter.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox ReadHID_VID_Input;
        private System.Windows.Forms.TextBox ReadHID_PID_Input;
        private System.Windows.Forms.TextBox SendHID_UsagePage_Input;
        private System.Windows.Forms.TextBox SendHID_Usage_Input;
        private System.Windows.Forms.TextBox SendHID_RID_Input;
        private System.Windows.Forms.TextBox Read_Output;
        private System.Windows.Forms.Button Read_Button;
        private System.Windows.Forms.Label label_ReadHID_PID;
        private System.Windows.Forms.Label label_ReadHID_VID;
        private System.Windows.Forms.GroupBox gb_filter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label_SendHID_RID;
        private System.Windows.Forms.Label label_SendHID_Usage;
        private System.Windows.Forms.Label label_SendHID_UsagePage;
        private System.Windows.Forms.Button Send_Button;
        private System.Windows.Forms.Label label_SendHID_PID;
        private System.Windows.Forms.Label label_SendHID_VID;
        private System.Windows.Forms.TextBox SendHID_VID_Input;
        private System.Windows.Forms.TextBox SendHID_PID_Input;
        private System.Windows.Forms.TextBox Byte0_Input;
        private System.Windows.Forms.TextBox Byte6_Input;
        private System.Windows.Forms.TextBox Byte5_Input;
        private System.Windows.Forms.TextBox Byte4_Input;
        private System.Windows.Forms.TextBox Byte3_Input;
        private System.Windows.Forms.TextBox Byte2_Input;
        private System.Windows.Forms.TextBox Byte1_Input;
        private System.Windows.Forms.TextBox SerialNumber;
        private System.Windows.Forms.TextBox ManufacturerName;
        private System.Windows.Forms.TextBox ProductName;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

