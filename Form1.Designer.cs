namespace ChromiumWebView
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.homeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.backendToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.backToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadingToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.backGroundPanel = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusMessageLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.backGroundPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripButton,
            this.backendToolStripButton,
            this.toolStripSeparator2,
            this.reloadToolStripButton,
            this.backToolStripButton,
            this.toolStripSeparator3,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.loadingToolStripButton});
            this.toolStrip1.Name = "toolStrip1";
            // 
            // homeToolStripButton
            // 
            resources.ApplyResources(this.homeToolStripButton, "homeToolStripButton");
            this.homeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.homeToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.homeToolStripButton.Name = "homeToolStripButton";
            this.homeToolStripButton.Click += new System.EventHandler(this.HomeToolStripButton_Click);
            // 
            // backendToolStripButton
            // 
            resources.ApplyResources(this.backendToolStripButton, "backendToolStripButton");
            this.backendToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backendToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.backendToolStripButton.Name = "backendToolStripButton";
            this.backendToolStripButton.Click += new System.EventHandler(this.BackendToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // reloadToolStripButton
            // 
            resources.ApplyResources(this.reloadToolStripButton, "reloadToolStripButton");
            this.reloadToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.reloadToolStripButton.Image = global::ChromiumWebView.Properties.Resources.loop;
            this.reloadToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.reloadToolStripButton.Name = "reloadToolStripButton";
            this.reloadToolStripButton.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.reloadToolStripButton.Click += new System.EventHandler(this.ReloadToolStripButton_Click);
            // 
            // backToolStripButton
            // 
            resources.ApplyResources(this.backToolStripButton, "backToolStripButton");
            this.backToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backToolStripButton.Image = global::ChromiumWebView.Properties.Resources.back_button;
            this.backToolStripButton.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.backToolStripButton.Name = "backToolStripButton";
            this.backToolStripButton.Click += new System.EventHandler(this.BackToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ChromiumWebView.Properties.Resources.menu;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.ToolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // loadingToolStripButton
            // 
            resources.ApplyResources(this.loadingToolStripButton, "loadingToolStripButton");
            this.loadingToolStripButton.AutoToolTip = false;
            this.loadingToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.loadingToolStripButton.Image = global::ChromiumWebView.Properties.Resources.Rolling_1s_32px;
            this.loadingToolStripButton.Name = "loadingToolStripButton";
            // 
            // backGroundPanel
            // 
            resources.ApplyResources(this.backGroundPanel, "backGroundPanel");
            this.backGroundPanel.BackColor = System.Drawing.Color.Transparent;
            this.backGroundPanel.Controls.Add(this.statusStrip1);
            this.backGroundPanel.Name = "backGroundPanel";
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusMessageLabel});
            this.statusStrip1.Name = "statusStrip1";
            // 
            // statusMessageLabel
            // 
            resources.ApplyResources(this.statusMessageLabel, "statusMessageLabel");
            this.statusMessageLabel.Name = "statusMessageLabel";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.backGroundPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.backGroundPanel.ResumeLayout(false);
            this.backGroundPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Panel backGroundPanel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusMessageLabel;
        private System.Windows.Forms.ToolStripButton reloadToolStripButton;
        private System.Windows.Forms.ToolStripButton loadingToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton backToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton homeToolStripButton;
        private System.Windows.Forms.ToolStripButton backendToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

