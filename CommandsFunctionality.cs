using System.Linq;
using ChatCommands.Utilities;
namespace ChatCommands
{
	internal class CommandsFunctionality
	{
		public static void OnChatSendCmd(Player player, string message)
		{
			UnityEngine.Debug.Log($"[{nameof(OnChatSendCmd)}] OnChatSendCMD Called");

			string[] args = ChatCMDModUtilities.StringSplit(message, ' ').ToArray();

			UnityEngine.Debug.Log($"[{nameof(OnChatSendCmd)}] Called with message: {message}");

			
			UnityEngine.Debug.Log($"[{nameof(OnChatSendCmd)}] args: {args.Length}");
			for (var i = 0; i < args.Length; i++)
			{
				UnityEngine.Debug.Log($"[{nameof(OnChatSendCmd)}] arg {i}: {args[i]}");	
			}

			switch (args)
			{
				case { Length: 1 }: // 1 argument
					switch (args[0])
					{
						case "!reset": // cards + players
							PlayerManager.instance.RemovePlayers();
							ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player);
							break;
						case "!resetcards":
							ModdingUtils.Utils.Cards.instance.RemoveAllCardsFromPlayer(player);
							UnityEngine.Debug.Log("Reset Cards Called");
							break;
						case "!resetbots":
						case "!kickbots":
						case "!nobots":
							RemoveBots();
							break;
						case "!resetplayers":
						case "!kickplayers":
						case "!kickall":
							PlayerManager.instance.RemovePlayers();
							break;
						//case "!getres":
						//player.data.stats.
						//break;
					}
					break;
				case { Length: 2 }: // 2 arguments
					if (!float.TryParse(args[1], out var arg2AsFloat)) break;
					switch (args[0])
					{
						case "!hp":
							player.data.health = arg2AsFloat;
							break;
						case "!hpm":
						case "!hpmax":
						case "!maxhp":
							player.data.maxHealth = arg2AsFloat;
							break;
					}

					break;
			}
		}	
		
		private static void RemoveBots()
		{
			Player localPlayer = ChatCMDModUtilities.GetLocalPlayer();

			for (var i = PlayerManager.instance.players.Count - 1; i >= 0; i--)
			{
				var player = PlayerManager.instance.players[i];
				
				if (player == localPlayer) return;

				PlayerAssigner.instance.players.Remove(player.data);
				PlayerManager.instance.RemovePlayer(player);
			}
		}		
	}
}
