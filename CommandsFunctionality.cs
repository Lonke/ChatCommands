using System.Collections.Generic;
using System.Linq;
using ChatCommands.Utilities;
using Photon.Pun;

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


			if (args.Length != 1) return;

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
			}

			if (args.Length != 2) return;

			if (!float.TryParse(args[1], out var arg2AsFloat)) return;

			player.data.health = args[0] switch
			{
				"!hp" => arg2AsFloat,
				_ => player.data.health
			};
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
