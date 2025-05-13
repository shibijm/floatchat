using Discord;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FloatChat;

partial class ChatForm : Form {

	[DllImport("user32.dll")]
	private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
	[DllImport("user32.dll")]
	private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
	[DllImport("user32.dll")]
	private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
	[DllImport("user32.dll")]
	private static extern IntPtr GetForegroundWindow();
	[DllImport("user32.dll")]
	private static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
	[DllImport("user32.dll")]
	private static extern bool SetForegroundWindow(IntPtr hWnd);
	[DllImport("gdi32.dll")]
	private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

	delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

	private readonly int oldWindowLong;
	private bool clickThrough = false;
	private readonly WinEventDelegate winEventDelegate;
	private string lastProcess;
	private IntPtr lastIntPtr = IntPtr.Zero;
	private IntPtr lastIntPtrBuffer = IntPtr.Zero;
	private DiscordBot bot;
	private Timer hideTimer = new();
	private bool newMessage = false;
	private double activeOpacity;
	private double inactiveOpacity;
	// private PrivateFontCollection privateFontCollection;

	private static string GetActiveWindowProcessName() {
		IntPtr hWnd = GetForegroundWindow();
		GetWindowThreadProcessId(hWnd, out uint pid);
		Process p = Process.GetProcessById((int) pid);
		return p.ProcessName;
	}

	public ChatForm() {
		InitializeComponent();
		string currentProcess = Process.GetCurrentProcess().ProcessName;
		Process[] processes = Process.GetProcessesByName(currentProcess);
		int currentID = Process.GetCurrentProcess().SessionId;
		Process[] currentUserProcesses = [.. processes.Where(p => p.SessionId == currentID)];
		if (currentUserProcesses.Length > 1) {
			MessageBox.Show("FloatChat is already running");
			Environment.Exit(0);
		}
		notifyIcon.Visible = true;
		oldWindowLong = GetWindowLong(Handle, -20);
		winEventDelegate = new WinEventDelegate(WinEventProc);
		IntPtr eventHook = SetWinEventHook(3, 3, IntPtr.Zero, winEventDelegate, 0, 0, 0);
		StartBot();
	}

	private async void StartBot() {
		bot = new DiscordBot(this);
		try {
			await bot.Run();
		} catch (Exception e) {
			AddMessage("Error: " + e.Message);
		}
	}

	private void ChatForm_Load(object sender, EventArgs e) {
		Opacity = (double) Program.configHandler.data["activeOpacity"];
		/* privateFontCollection = new PrivateFontCollection();
		byte[] fontData = Properties.Resources.BurbankBigRegular_Medium;
		IntPtr data = Marshal.AllocCoTaskMem(fontData.Length);
		Marshal.Copy(fontData, 0, data, fontData.Length);
		privateFontCollection.AddMemoryFont(data, fontData.Length);
		uint dummy = 0;
		AddFontMemResourceEx(data, (uint) Properties.Resources.BurbankBigRegular_Medium.Length, IntPtr.Zero, ref dummy);
		Marshal.FreeCoTaskMem(data); */
		ApplySettings();
	}

	private void ApplySettings() {
		activeOpacity = (double) Program.configHandler.data["activeOpacity"];
		inactiveOpacity = (double) Program.configHandler.data["inactiveOpacity"];
		Location = new Point((int) Program.configHandler.data["locationX"], (int) Program.configHandler.data["locationY"]);
		Width = (int) Program.configHandler.data["sizeX"];
		Height = (int) Program.configHandler.data["sizeY"];
		int chatBoxFontSize = (int) Program.configHandler.data["chatBoxFontSize"];
		string chatBoxFont = (string) Program.configHandler.data["chatBoxFont"];
		if (string.IsNullOrEmpty(chatBoxFont)) {
			// chatBox.Font = new Font(privateFontCollection.Families[0], chatBoxFontSize);
			// chatBox.Font = new Font(privateFontCollection.Families[0], chatBoxFontSize);
		} else {
			chatBox.Font = new Font(chatBoxFont, chatBoxFontSize);
		}
		int inputBoxFontSize = (int) Program.configHandler.data["inputBoxFontSize"];
		string inputBoxFont = (string) Program.configHandler.data["inputBoxFontStr"];
		if (string.IsNullOrEmpty(inputBoxFont)) {
			// inputBox.Font = new Font(privateFontCollection.Families[0], inputBoxFontSize);
		} else {
			inputBox.Font = new Font(inputBoxFont, inputBoxFontSize);
		}
		chatBox.Location = new Point(10, 10);
		chatBox.Width = Width - 20;
		chatBox.Height = Height - inputBox.Height - 10 - 10 - 10;
		inputBox.Location = new Point(10, Height - 10 - inputBox.Height);
		inputBox.Width = Width - 20;
		chatBox.SelectionStart = chatBox.Text.Length;
		chatBox.ScrollToCaret();
	}

	public void OnBotReady() {
		if (InvokeRequired) {
			Invoke(new Action(OnBotReady));
		} else {
			AddMessage("Connected to Discord\n");
			inputBox.Enabled = true;
			if (!clickThrough) {
				inputBox.Focus();
			}
		}
	}

	public void AddMessage(string message) {
		if (InvokeRequired) {
			Invoke(new Action(() => AddMessage(message)));
		} else {
			string extra = "";
			if (!string.IsNullOrEmpty(chatBox.Text)) {
				extra = "\n";
			}
			chatBox.Text += extra + message;
			chatBox.SelectionStart = chatBox.Text.Length;
			chatBox.ScrollToCaret();
			if (clickThrough) {
				StartHideTimer((int) Program.configHandler.data["newMessageHideTimer"]);
				Opacity = inactiveOpacity;
				newMessage = true;
			}
		}
	}

	private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e) {
		inputBox.Focus();
		Process.Start(e.LinkText);
	}

	private void TextBox1_KeyDown(object sender, KeyEventArgs e) {
		if (e.KeyCode == Keys.Enter) {
			string text = inputBox.Text.Trim();
			inputBox.Text = "";
			if (!string.IsNullOrEmpty(text)) {
				e.SuppressKeyPress = true;
				string[] textSplit = text.Split(' ');
				if (textSplit[0] == "/nick") {
					if (textSplit.Length > 1) {
						Program.configHandler.data["nick"] = textSplit[1].Trim();
						Program.configHandler.SaveConfig();
						AddMessage("Your nick has been changed to \"" + textSplit[1].Trim() + "\"");
					} else {
						AddMessage("Syntax: /nick <nick>");
					}
				} else {
					ITextChannel channel = (ITextChannel) bot.bot.GetChannel((ulong) Program.configHandler.data["channelID"]);
					channel.SendMessageAsync((string) Program.configHandler.data["nick"] + ": " + text);
				}
			}
		} else if (e.KeyCode == Keys.Escape) {
			e.SuppressKeyPress = true;
			SetForegroundWindow(lastIntPtr);
		}
	}

	private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hWnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime) {
		string processName = GetActiveWindowProcessName();
		string targetProcessName = (string) Program.configHandler.data["processName"];
		if (processName == targetProcessName || targetProcessName == "*") {
			if (lastProcess == Process.GetCurrentProcess().ProcessName || (bool) Program.configHandler.data["alwaysShowInProcess"]) {
				ChatForm_Deactivate(null, null);
			}
		} else if (processName == Process.GetCurrentProcess().ProcessName) {
			ChatForm_Activated(null, null);
		} else {
			if (!newMessage) {
				Opacity = 0D;
			}
		}
		if (processName != "explorer") {
			lastProcess = processName;
			lastIntPtr = lastIntPtrBuffer;
			lastIntPtrBuffer = hWnd;
		}
	}

	private void ChatForm_Activated(object sender, EventArgs e) {
		inputBox.SelectionStart = 0;
		inputBox.SelectionLength = inputBox.Text.Length;
		StopHideTimer();
		Opacity = activeOpacity;
		_ = SetWindowLong(Handle, -20, oldWindowLong);
		clickThrough = false;
	}

	private void ChatForm_Deactivate(object sender, EventArgs e) {
		if (!hideTimer.Enabled) {
			StartHideTimer();
		}
		Opacity = inactiveOpacity;
		_ = SetWindowLong(Handle, -20, oldWindowLong | 0x80000 | 0x20);
		clickThrough = true;
	}

	private void StopHideTimer() {
		if (hideTimer.Enabled) {
			hideTimer.Stop();
		}
		newMessage = false;
	}

	private void StartHideTimer(int hideTimerInterval = -2) {
		if (hideTimerInterval == -2) {
			hideTimerInterval = (int) Program.configHandler.data["hideTimer"];
		}
		if (hideTimerInterval >= 0) {
			StopHideTimer();
			hideTimer = new Timer();
			hideTimer.Tick += new EventHandler(OnHideTimer);
			hideTimer.Interval = hideTimerInterval * 1000;
			hideTimer.Enabled = true;
		}
	}

	private void OnHideTimer(object sender, EventArgs e) {
		string processName = GetActiveWindowProcessName();
		hideTimer.Enabled = false;
		newMessage = false;
		if ((processName != (string) Program.configHandler.data["processName"] && processName != "*") || (bool) Program.configHandler.data["alwaysShowInProcess"] == false) {
			Opacity = 0D;
		}
	}

	private void NotifyIcon1_DoubleClick(object sender, EventArgs e) {
		Activate();
	}

	private void RefreshSettingsToolStripMenuItem_Click(object sender, EventArgs e) {
		Program.configHandler.LoadConfig();
		ApplySettings();
	}

	private void SettingsToolStripMenuItem_Click(object sender, EventArgs e) {
		ProcessStartInfo startInfo = new("code") {
			Arguments = Program.configHandler.configFile,
			UseShellExecute = true,
		};
		Process.Start(startInfo);
	}

	private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
		Close();
	}

}
