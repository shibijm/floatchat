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
			openSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			reloadSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			contextMenuStrip.SuspendLayout();
			SuspendLayout();
			// 
			// chatBox
			// 
			chatBox.BackColor = System.Drawing.Color.Black;
			chatBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			chatBox.Font = new System.Drawing.Font("Segoe UI", 12F);
			chatBox.ForeColor = System.Drawing.Color.White;
			chatBox.Location = new System.Drawing.Point(14, 14);
			chatBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			chatBox.Name = "chatBox";
			chatBox.ReadOnly = true;
			chatBox.Size = new System.Drawing.Size(555, 228);
			chatBox.TabIndex = 0;
			chatBox.TabStop = false;
			chatBox.Text = "";
			chatBox.LinkClicked += ChatBox_LinkClicked;
			// 
			// inputBox
			// 
			inputBox.BackColor = System.Drawing.Color.Black;
			inputBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			inputBox.Enabled = false;
			inputBox.Font = new System.Drawing.Font("Segoe UI", 12F);
			inputBox.ForeColor = System.Drawing.Color.White;
			inputBox.Location = new System.Drawing.Point(14, 249);
			inputBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			inputBox.Name = "inputBox";
			inputBox.Size = new System.Drawing.Size(555, 22);
			inputBox.TabIndex = 1;
			inputBox.KeyDown += InputBox_KeyDown;
			// 
			// notifyIcon
			// 
			notifyIcon.ContextMenuStrip = contextMenuStrip;
			notifyIcon.Icon = (System.Drawing.Icon) resources.GetObject("notifyIcon.Icon");
			notifyIcon.Text = "FloatChat";
			notifyIcon.DoubleClick += NotifyIcon_DoubleClick;
			// 
			// contextMenuStrip
			// 
			contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { openSettingsToolStripMenuItem, reloadSettingsToolStripMenuItem, toolStripSeparator, exitToolStripMenuItem });
			contextMenuStrip.Name = "contextMenuStrip";
			contextMenuStrip.Size = new System.Drawing.Size(156, 76);
			// 
			// openSettingsToolStripMenuItem
			// 
			openSettingsToolStripMenuItem.Name = "openSettingsToolStripMenuItem";
			openSettingsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			openSettingsToolStripMenuItem.Text = "Open Settings";
			openSettingsToolStripMenuItem.Click += OpenSettingsToolStripMenuItem_Click;
			// 
			// reloadSettingsToolStripMenuItem
			// 
			reloadSettingsToolStripMenuItem.Name = "reloadSettingsToolStripMenuItem";
			reloadSettingsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			reloadSettingsToolStripMenuItem.Text = "Reload Settings";
			reloadSettingsToolStripMenuItem.Click += ReloadSettingsToolStripMenuItem_Click;
			// 
			// toolStripSeparator
			// 
			toolStripSeparator.Name = "toolStripSeparator";
			toolStripSeparator.Size = new System.Drawing.Size(152, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
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
		private System.Windows.Forms.ToolStripMenuItem openSettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
		private System.Windows.Forms.ToolStripMenuItem reloadSettingsToolStripMenuItem;
	}
}
