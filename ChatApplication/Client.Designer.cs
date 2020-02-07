namespace ChatApplication
{
    partial class Client
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textIP = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textLog = new System.Windows.Forms.TextBox();
            this.textSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textIP
            // 
            this.textIP.Location = new System.Drawing.Point(67, 25);
            this.textIP.Name = "textIP";
            this.textIP.Size = new System.Drawing.Size(164, 25);
            this.textIP.TabIndex = 0;
            this.textIP.Text = "192.168.1.101";
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(248, 25);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(100, 25);
            this.textPort.TabIndex = 1;
            this.textPort.Text = "50000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(363, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "连接";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textLog
            // 
            this.textLog.Location = new System.Drawing.Point(67, 56);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.Size = new System.Drawing.Size(520, 139);
            this.textLog.TabIndex = 3;
            // 
            // textSend
            // 
            this.textSend.Location = new System.Drawing.Point(67, 201);
            this.textSend.Multiline = true;
            this.textSend.Name = "textSend";
            this.textSend.Size = new System.Drawing.Size(520, 107);
            this.textSend.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(67, 324);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(96, 30);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "发送消息";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.textSend);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textPort);
            this.Controls.Add(this.textIP);
            this.Name = "Client";
            this.Text = "客户端聊天器";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textIP;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.TextBox textSend;
        private System.Windows.Forms.Button btnSend;
    }
}

