using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FloatChat;

public class DiscordBot(ChatForm chatForm) {

	public DiscordSocketClient Client { get; private set; }

	private readonly ChatForm chatForm = chatForm;

	public async Task Run() {
		Client = new DiscordSocketClient(new DiscordSocketConfig {
			LogLevel = LogSeverity.Debug,
			GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent,
		});
		Client.Log += Log;
		Client.MessageReceived += OnMessage;
		Client.Ready += OnReady;
		await Client.LoginAsync(TokenType.Bot, (string) Program.ConfigHandler.Data["botToken"]);
		await Client.StartAsync();
		await Task.Delay(-1);
	}

	private void ProcessMessage(IMessage message) {
		string timestamp = message.Timestamp.ToLocalTime().ToString("h:mm tt");
		if (message.Author.Id == Client.CurrentUser.Id) {
			chatForm.AddMessage($"[{timestamp}] {message.Content}");
		} else {
			chatForm.AddMessage($"[{timestamp}] {message.Author.Username}: {message.Content}");
		}
	}

	private async Task OnReady() {
		chatForm.OnBotReady();
		ITextChannel channel = (ITextChannel) Client.GetChannel((ulong) Program.ConfigHandler.Data["channelId"]);
		IEnumerable<IMessage> messages = await channel.GetMessagesAsync(100).FlattenAsync();
		foreach (IMessage message in messages.Reverse()) {
			ProcessMessage(message);
		}
	}

	private Task OnMessage(IMessage message) {
		if (message.Channel.Id == (ulong) Program.ConfigHandler.Data["channelId"]) {
			ProcessMessage(message);
		}
		return Task.CompletedTask;
	}

	private Task Log(LogMessage logMessage) {
		Debug.WriteLine(logMessage.ToString());
		return Task.CompletedTask;
	}

}
