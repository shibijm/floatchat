using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FloatChat;

class DiscordBot(ChatForm chatForm) {

	private readonly ChatForm chatForm = chatForm;
	public DiscordSocketClient bot;

	public async Task Run() {
		bot = new DiscordSocketClient(new DiscordSocketConfig {
			LogLevel = LogSeverity.Debug,
			GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
		});
		bot.Log += Log;
		bot.MessageReceived += OnMessage;
		bot.Ready += OnReady;
		await bot.LoginAsync(TokenType.Bot, (string) Program.configHandler.data["botToken"]);
		await bot.StartAsync();
		await Task.Delay(-1);
	}

	private void ProcessMessage(IMessage message) {
		string timestamp = message.Timestamp.ToLocalTime().ToString("h:mm tt");
		if (message.Author.Id == bot.CurrentUser.Id) {
			chatForm.AddMessage("[" + timestamp + "] " + message.Content);
		} else {
			chatForm.AddMessage("[" + timestamp + "] " + message.Author.Username + ": " + message.Content);
		}
	}

	private async Task OnReady() {
		chatForm.OnBotReady();
		ITextChannel channel = (ITextChannel) bot.GetChannel((ulong) Program.configHandler.data["channelID"]);
		IEnumerable<IMessage> messages = await channel.GetMessagesAsync(100).FlattenAsync();
		foreach (IMessage message in messages.Reverse()) {
			ProcessMessage(message);
		}
	}

	private Task OnMessage(IMessage message) {
		if (message.Channel.Id == (ulong) Program.configHandler.data["channelID"]) {
			ProcessMessage(message);
		}
		return Task.CompletedTask;
	}

	private Task Log(LogMessage logMessage) {
		Debug.WriteLine(logMessage.ToString());
		return Task.CompletedTask;
	}

}
