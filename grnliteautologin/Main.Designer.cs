namespace GrnLiteAutoLogin
{
    partial class Main
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
            this.btnRunLogin = new System.Windows.Forms.Button();
            this.outputArea = new System.Windows.Forms.TextBox();
            this.checkedListBox_accounts = new System.Windows.Forms.CheckedListBox();
            this.btnAppendAccount = new System.Windows.Forms.Button();
            this.cbAutoRun = new System.Windows.Forms.CheckBox();
            this.btnEditAccount = new System.Windows.Forms.Button();
            this.btnDeleteAccount = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRunLogin
            // 
            this.btnRunLogin.Location = new System.Drawing.Point(261, 289);
            this.btnRunLogin.Name = "btnRunLogin";
            this.btnRunLogin.Size = new System.Drawing.Size(85, 42);
            this.btnRunLogin.TabIndex = 0;
            this.btnRunLogin.Text = "运行";
            this.btnRunLogin.UseVisualStyleBackColor = true;
            this.btnRunLogin.Click += new System.EventHandler(this.btnRunLogin_Click);
            // 
            // outputArea
            // 
            this.outputArea.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.outputArea.Location = new System.Drawing.Point(21, 20);
            this.outputArea.Multiline = true;
            this.outputArea.Name = "outputArea";
            this.outputArea.ReadOnly = true;
            this.outputArea.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputArea.Size = new System.Drawing.Size(328, 259);
            this.outputArea.TabIndex = 1;
            // 
            // checkedListBox_accounts
            // 
            this.checkedListBox_accounts.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkedListBox_accounts.FormattingEnabled = true;
            this.checkedListBox_accounts.Location = new System.Drawing.Point(368, 23);
            this.checkedListBox_accounts.Name = "checkedListBox_accounts";
            this.checkedListBox_accounts.Size = new System.Drawing.Size(266, 256);
            this.checkedListBox_accounts.TabIndex = 3;
            this.checkedListBox_accounts.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox_accounts_ItemCheck);
            this.checkedListBox_accounts.SelectedIndexChanged += new System.EventHandler(this.checkedListBox_accounts_SelectedIndexChanged);
            // 
            // btnAppendAccount
            // 
            this.btnAppendAccount.Location = new System.Drawing.Point(368, 289);
            this.btnAppendAccount.Name = "btnAppendAccount";
            this.btnAppendAccount.Size = new System.Drawing.Size(80, 42);
            this.btnAppendAccount.TabIndex = 4;
            this.btnAppendAccount.Text = "添加账号";
            this.btnAppendAccount.UseVisualStyleBackColor = true;
            this.btnAppendAccount.Click += new System.EventHandler(this.appendAccountBtn_Click);
            // 
            // cbAutoRun
            // 
            this.cbAutoRun.AutoSize = true;
            this.cbAutoRun.Location = new System.Drawing.Point(25, 303);
            this.cbAutoRun.Name = "cbAutoRun";
            this.cbAutoRun.Size = new System.Drawing.Size(96, 16);
            this.cbAutoRun.TabIndex = 5;
            this.cbAutoRun.Text = "启动自动运行";
            this.cbAutoRun.UseVisualStyleBackColor = true;
            this.cbAutoRun.CheckedChanged += new System.EventHandler(this.cbAutoRun_CheckedChanged);
            // 
            // btnEditAccount
            // 
            this.btnEditAccount.Location = new System.Drawing.Point(463, 289);
            this.btnEditAccount.Name = "btnEditAccount";
            this.btnEditAccount.Size = new System.Drawing.Size(78, 42);
            this.btnEditAccount.TabIndex = 6;
            this.btnEditAccount.Text = "编辑账号";
            this.btnEditAccount.UseVisualStyleBackColor = true;
            this.btnEditAccount.Click += new System.EventHandler(this.btnEditAccount_Click);
            // 
            // btnDeleteAccount
            // 
            this.btnDeleteAccount.Location = new System.Drawing.Point(558, 289);
            this.btnDeleteAccount.Name = "btnDeleteAccount";
            this.btnDeleteAccount.Size = new System.Drawing.Size(76, 42);
            this.btnDeleteAccount.TabIndex = 6;
            this.btnDeleteAccount.Text = "删除账号";
            this.btnDeleteAccount.UseVisualStyleBackColor = true;
            this.btnDeleteAccount.Click += new System.EventHandler(this.btnDeleteAccount_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 348);
            this.Controls.Add(this.btnDeleteAccount);
            this.Controls.Add(this.btnEditAccount);
            this.Controls.Add(this.cbAutoRun);
            this.Controls.Add(this.btnAppendAccount);
            this.Controls.Add(this.checkedListBox_accounts);
            this.Controls.Add(this.outputArea);
            this.Controls.Add(this.btnRunLogin);
            this.Name = "Main";
            this.Text = "GaroonLite自动登录";
            this.Activated += new System.EventHandler(this.Main_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunLogin;
        private System.Windows.Forms.TextBox outputArea;
        private System.Windows.Forms.CheckedListBox checkedListBox_accounts;
        private System.Windows.Forms.Button btnAppendAccount;
        private System.Windows.Forms.CheckBox cbAutoRun;
        private System.Windows.Forms.Button btnEditAccount;
        private System.Windows.Forms.Button btnDeleteAccount;
    }
}

