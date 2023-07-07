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
				case "!kickbots":
                    RemoveBots();
                    break;
				case "!resetplayers":
				case "!kickplayers":
				case "!nobots":
					RemoveBots();
					break;
				case "!hp":
					player.data.health = arg2AsFloat;
					break;
				
					
			}

			if (args.Length != 2) return;

			if (!float.TryParse(args[1], out var arg2AsFloat)) return;

			if (args[0] == "!hp")
				player.data.health = arg2AsFloat;

		}	
		
		private static void RemoveBots()
		{
        	    foreach (Player player in PlayerManager.instance.players) /* scyye is cool */
	            {
                	if (player.data.view.OwnerActorNr == PhotonNetwork.MasterClient.ActorNumber)
                	{
                    		PlayerManager.instance.RemovePlayer(player);
                	}
            	    }
        	}		
	}
}
