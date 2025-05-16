using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FloatChat;

public class ConfigHandler {

	public dynamic Data { get; private set; }
	public string ConfigFilePath { get; private set; }

	private readonly string appDataPath;
	private readonly string configDirectoryPath;
	private readonly Dictionary<string, dynamic> defaultSettings = new() {
		{ "botToken", "" },
		{ "channelId", 0 },
		{ "nick", "" },
		{ "processName", "" },
		{ "alwaysShowInProcess", false },
		{ "hideTimer", 10 },
		{ "newMessageHideTimer", 10 },
		{ "sizeX", 500 },
		{ "sizeY", 250 },
		{ "locationX", 25 },
		{ "locationY", Screen.PrimaryScreen.Bounds.Bottom - 550 },
		{ "activeOpacity", 0.75 },
		{ "inactiveOpacity", 0.5 },
		{ "chatBoxFont", "" },
		{ "chatBoxFontSize", 15 },
		{ "inputBoxFont", "" },
		{ "inputBoxFontSize", 15 }
	};

	public ConfigHandler() {
		appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		configDirectoryPath = Path.Join(appDataPath, Program.Name);
		if (!Directory.Exists(configDirectoryPath)) {
			Directory.CreateDirectory(configDirectoryPath);
		}
		ConfigFilePath = Path.Join(configDirectoryPath, "config.json");
		if (!File.Exists(ConfigFilePath)) {
			File.Create(ConfigFilePath).Dispose();
		}
		LoadConfig();
	}

	public void LoadConfig() {
		try {
			string configContent = File.ReadAllText(ConfigFilePath);
			if (string.IsNullOrEmpty(configContent)) {
				configContent = "{}";
			}
			Data = JObject.Parse(configContent);
			bool save = false;
			foreach (KeyValuePair<string, dynamic> setting in defaultSettings) {
				if (Data[setting.Key] == null) {
					save = true;
					Data[setting.Key] = setting.Value;
				}
			}
			if (save) {
				SaveConfig();
			}
		} catch (Exception e) {
			MessageBox.Show($"Failed to read the config file\n\n{e}", Program.Name);
			Environment.Exit(1);
		}
	}

	public void SaveConfig() {
		try {
			File.WriteAllText(ConfigFilePath, "");
			using FileStream file = File.OpenWrite(ConfigFilePath);
			using StreamWriter writer = new(file);
			using JsonTextWriter jsonTextWriter = new(writer) {
				Formatting = Formatting.Indented,
				Indentation = 1,
				IndentChar = '\t'
			};
			new JsonSerializer().Serialize(jsonTextWriter, Data);
		} catch (Exception e) {
			MessageBox.Show($"Failed to write to the config file\n\n{e}", Program.Name);
		}
	}

}
