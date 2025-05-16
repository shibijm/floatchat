using System;
using System.Windows.Forms;

namespace FloatChat;

public class Program {

	public static string Name { get; } = "FloatChat";
	public static ConfigHandler ConfigHandler { get; private set; }

	[STAThread]
	public static void Main() {
		ConfigHandler = new ConfigHandler();
		ApplicationConfiguration.Initialize();
		Application.Run(new ChatForm());
	}

}
