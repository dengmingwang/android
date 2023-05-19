namespace POCT实验室管理软件
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_checkid = new System.Windows.Forms.Button();
            this.btn_createID = new System.Windows.Forms.Button();
            this.btn_parameter = new System.Windows.Forms.Button();
            this.btn_line = new System.Windows.Forms.Button();
            this.btn_test = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Systime = new System.Windows.Forms.ToolStripStatusLabel();
            this.Connect = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_testitem = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.btn_testitem);
            this.panel1.Controls.Add(this.btn_checkid);
            this.panel1.Controls.Add(this.btn_createID);
            this.panel1.Controls.Add(this.btn_parameter);
            this.panel1.Controls.Add(this.btn_line);
            this.panel1.Controls.Add(this.btn_test);
            this.panel1.Controls.Add(this.btn_Exit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1684, 54);
            this.panel1.TabIndex = 0;
            // 
            // btn_checkid
            // 
            this.btn_checkid.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_checkid.Location = new System.Drawing.Point(565, 9);
            this.btn_checkid.Name = "btn_checkid";
            this.btn_checkid.Size = new System.Drawing.Size(120, 35);
            this.btn_checkid.TabIndex = 5;
            this.btn_checkid.Text = "HEX查看";
            this.btn_checkid.UseVisualStyleBackColor = true;
            this.btn_checkid.Click += new System.EventHandler(this.btn_checkid_Click_1);
            // 
            // btn_createID
            // 
            this.btn_createID.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_createID.Location = new System.Drawing.Point(424, 10);
            this.btn_createID.Name = "btn_createID";
            this.btn_createID.Size = new System.Drawing.Size(120, 35);
            this.btn_createID.TabIndex = 4;
            this.btn_createID.Text = "生成ID文件";
            this.btn_createID.UseVisualStyleBackColor = true;
            this.btn_createID.Click += new System.EventHandler(this.btn_createID_Click);
            // 
            // btn_parameter
            // 
            this.btn_parameter.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_parameter.Location = new System.Drawing.Point(12, 10);
            this.btn_parameter.Name = "btn_parameter";
            this.btn_parameter.Size = new System.Drawing.Size(120, 35);
            this.btn_parameter.TabIndex = 3;
            this.btn_parameter.Text = "项目参数";
            this.btn_parameter.UseVisualStyleBackColor = false;
            this.btn_parameter.Click += new System.EventHandler(this.btn_parameter_Click);
            // 
            // btn_line
            // 
            this.btn_line.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_line.Location = new System.Drawing.Point(283, 10);
            this.btn_line.Name = "btn_line";
            this.btn_line.Size = new System.Drawing.Size(120, 35);
            this.btn_line.TabIndex = 2;
            this.btn_line.Text = "曲线生成";
            this.btn_line.UseVisualStyleBackColor = false;
            this.btn_line.Click += new System.EventHandler(this.btn_line_Click);
            // 
            // btn_test
            // 
            this.btn_test.BackColor = System.Drawing.SystemColors.Control;
            this.btn_test.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_test.Location = new System.Drawing.Point(710, 10);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(120, 35);
            this.btn_test.TabIndex = 1;
            this.btn_test.Text = "信号测试";
            this.btn_test.UseVisualStyleBackColor = false;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Exit.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Exit.Location = new System.Drawing.Point(1609, 0);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 54);
            this.btn_Exit.TabIndex = 0;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 54);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1684, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1684, 949);
            this.panel2.TabIndex = 2;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter2.Location = new System.Drawing.Point(0, 1006);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(1684, 3);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.statusStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 1018);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1684, 25);
            this.panel3.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Systime,
            this.Connect,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1684, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Systime
            // 
            this.Systime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.Systime.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.Systime.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Systime.Name = "Systime";
            this.Systime.Size = new System.Drawing.Size(556, 17);
            this.Systime.Spring = true;
            this.Systime.Text = "动态时间显示";
            this.Systime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Connect
            // 
            this.Connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Connect.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(556, 17);
            this.Connect.Spring = true;
            this.Connect.Text = "通讯状态";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(556, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_testitem
            // 
            this.btn_testitem.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_testitem.Location = new System.Drawing.Point(145, 10);
            this.btn_testitem.Name = "btn_testitem";
            this.btn_testitem.Size = new System.Drawing.Size(120, 35);
            this.btn_testitem.TabIndex = 6;
            this.btn_testitem.Text = "测试实验";
            this.btn_testitem.UseVisualStyleBackColor = true;
            this.btn_testitem.Click += new System.EventHandler(this.btn_testitem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1684, 1043);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("楷体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "实验室管理软件";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Systime;
        private System.Windows.Forms.ToolStripStatusLabel Connect;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btn_line;
        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_parameter;
        private System.Windows.Forms.Button btn_createID;
        private System.Windows.Forms.Button btn_checkid;
        private System.Windows.Forms.Button btn_testitem;

    }
}

