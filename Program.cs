using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FloatChat;

class Program {

	public static ConfigHandler configHandler;
	public static string appName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

	[STAThread]
	public static void Main() {
		configHandler = new ConfigHandler();
		ApplicationConfiguration.Initialize();
		Application.Run(new ChatForm());
	}

	public static void Log(string text) {
		string logAppend = "";
		try {
			logAppend = "[" + DateTime.Now.ToLocalTime().ToString() + "] " + text + "\r\n";
			File.AppendAllText(configHandler.configDirectory + "\\" + appName + ".log", logAppend);
		} catch (Exception e) {
			Debug.WriteLine(e.ToString());
			Debug.WriteLine("[Program.log fallback] " + logAppend);
		}
	}

}
