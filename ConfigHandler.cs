using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FloatChat;

class ConfigHandler {

	public string appDataPath;
	public string configDirectory;
	public string configFile;
	public dynamic data;

	public ConfigHandler() {
		appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		configDirectory = appDataPath + "\\" + Program.appName;
		configFile = configDirectory + "\\" + Program.appName + ".json";
		if (!Directory.Exists(configDirectory)) {
			Directory.CreateDirectory(configDirectory);
		}
		if (!File.Exists(configFile)) {
			File.Create(configFile).Dispose();
		}
		LoadConfig();
	}

	public void LoadConfig() {
		try {
			string configContent = File.ReadAllText(configFile);
			if (string.IsNullOrEmpty(configContent)) {
				configContent = "{}";
			}
			Dictionary<string, dynamic> defaultSettings = new() {
				{"botToken", ""},
				{"channelID", 0},
				{"nick", ""},
				{"processName", ""},
				{"alwaysShowInProcess", false},
				{"hideTimer", 10},
				{"newMessageHideTimer", 10},
				{"sizeX", 500},
				{"sizeY", 250},
				{"locationX", 25},
				{"locationY", Screen.PrimaryScreen.Bounds.Bottom - 550},
				{"activeOpacity", 0.75},
				{"inactiveOpacity", 0.5},
				{"chatBoxFont", ""},
				{"chatBoxFontSize", 15},
				{"inputBoxFont", ""},
				{"inputBoxFontSize", 15}
			};
			data = JObject.Parse(configContent);
			bool save = false;
			foreach (KeyValuePair<string, dynamic> setting in defaultSettings) {
				if (data[setting.Key] == null) {
					save = true;
					data[setting.Key] = JToken.FromObject(setting.Value);
				}
			}
			if (save) {
				SaveConfig();
			}
		} catch (Exception e) {
			Program.Log(e.ToString());
			if (File.Exists(configFile + ".old")) {
				File.Delete(configFile + ".old");
			}
			File.Move(configFile, configFile + ".old");
			File.Create(configFile).Dispose();
			MessageBox.Show("Error parsing the config file. A new one has been created.\r\n\r\n" + e.Message);
			LoadConfig();
		}
	}

	public void SaveConfig() {
		try {
			File.WriteAllText(configFile, "");
			using FileStream file = File.OpenWrite(configFile);
			using StreamWriter writer = new(file);
			using JsonTextWriter jsonTextWriter = new(writer) {
				Formatting = Formatting.Indented,
				Indentation = 1,
				IndentChar = '\t'
			};
			new JsonSerializer().Serialize(jsonTextWriter, data);
		} catch (Exception e) {
			Program.Log(e.ToString());
		}
	}

}
