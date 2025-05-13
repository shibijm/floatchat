namespace FloatChat {
	partial class ChatForm {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
			chatBox = new System.Windows.Forms.RichTextBox();
			inputBox = new System.Windows.Forms.TextBox();
			notifyIcon = new System.Windows.Forms.NotifyIcon(components);
			contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
			refreshSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// richTextBox1
			// 
			chatBox.BackColor = System.Drawing.Color.Black;
			chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			chatBox.Font = new System.Drawing.Font("Segoe UI", 12F);
			chatBox.ForeColor = System.Drawing.Color.White;
			chatBox.Location = new System.Drawing.Point(14, 14);
			chatBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			chatBox.Name = "richTextBox1";
			chatBox.ReadOnly = true;
			chatBox.Size = new System.Drawing.Size(555, 228);
			chatBox.TabIndex = 0;
			chatBox.TabStop = false;
			chatBox.Text = "";
			chatBox.LinkClicked += RichTextBox1_LinkClicked;
			// 
			// textBox1
			// 
			inputBox.BackColor = System.Drawing.Color.Black;
			inputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			inputBox.Enabled = false;
			inputBox.Font = new System.Drawing.Font("Segoe UI", 12F);
			inputBox.ForeColor = System.Drawing.Color.White;
			inputBox.Location = new System.Drawing.Point(14, 249);
			inputBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			inputBox.Name = "textBox1";
			inputBox.Size = new System.Drawing.Size(555, 22);
			inputBox.TabIndex = 1;
			inputBox.KeyDown += TextBox1_KeyDown;
			// 
			// notifyIcon1
			// 
			notifyIcon.ContextMenuStrip = contextMenuStrip;
			notifyIcon.Icon = (System.Drawing.Icon) resources.GetObject("notifyIcon1.Icon");
			notifyIcon.Text = "FloatChat";
			notifyIcon.DoubleClick += NotifyIcon1_DoubleClick;
			// 
			// contextMenuStrip1
			// 
			contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { refreshSettingsToolStripMenuItem, settingsToolStripMenuItem, toolStripSeparator, exitToolStripMenuItem });
			contextMenuStrip.Name = "contextMenuStrip1";
			contextMenuStrip.Size = new System.Drawing.Size(159, 76);
			// 
			// refreshSettingsToolStripMenuItem
			// 
			refreshSettingsToolStripMenuItem.Name = "refreshSettingsToolStripMenuItem";
			refreshSettingsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			refreshSettingsToolStripMenuItem.Text = "Refresh Settings";
			refreshSettingsToolStripMenuItem.Click += RefreshSettingsToolStripMenuItem_Click;
			// 
			// settingsToolStripMenuItem
			// 
			settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			settingsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			settingsToolStripMenuItem.Text = "Settings";
			settingsToolStripMenuItem.Click += SettingsToolStripMenuItem_Click;
			// 
			// toolStripMenuItem1
			// 
			toolStripSeparator.Name = "toolStripMenuItem1";
			toolStripSeparator.Size = new System.Drawing.Size(155, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
			// 
			// ChatForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Black;
			ClientSize = new System.Drawing.Size(583, 288);
			Controls.Add(inputBox);
			Controls.Add(chatBox);
			DoubleBuffered = true;
			ForeColor = System.Drawing.SystemColors.ControlText;
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			Icon = (System.Drawing.Icon) resources.GetObject("$this.Icon");
			Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			Name = "ChatForm";
			Opacity = 0.75D;
			ShowInTaskbar = false;
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "FloatChat";
			TopMost = true;
			Activated += ChatForm_Activated;
			Deactivate += ChatForm_Deactivate;
			Load += ChatForm_Load;
			contextMenuStrip.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.RichTextBox chatBox;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem refreshSettingsToolStripMenuItem;
	}
}
