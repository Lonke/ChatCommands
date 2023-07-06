using System;
using System.Collections.Generic;
using System.Text;
using ChatCommands.Utilities;
using HarmonyLib;
using Photon.Pun;

using static ChatCommands.CommandsFunctionality;

/*
 * 	private void Send(string message)
	{
		if (Application.isEditor || (GM_Test.instance && GM_Test.instance.gameObject.activeSelf))
		{
			this.SpawnCard(message);
		}
		if (Application.isEditor)
		{
			this.SpawnMap(message);
		}
		int viewID = PlayerManager.instance.GetPlayerWithActorID(PhotonNetwork.LocalPlayer.ActorNumber).data.view.ViewID;
		base.GetComponent<PhotonView>().RPC("RPCA_SendChat", RpcTarget.All, new object[] { message, viewID });
	}
 */

/*
	[PunRPC]
	private void RPCA_SendChat(string message, int playerViewID)
	{
		PhotonNetwork.GetPhotonView(playerViewID).GetComponentInChildren<PlayerChat>().Send(message);
	}
*/

		//public static bool Prefix(DevConsole instance, string message, int playerViewID)
			//UnityEngine.Debug.Log($"Message Recieved from {playerViewID}: {message}");
			//UnityEngine.Debug.Log(instance);
			//Debug.Log("False!");

namespace ChatCommands.Patches
{


	[HarmonyPatch(typeof(DevConsole), "Send")]
	public static class ChatSendMessagePatch
	{
		public static bool Prefix(DevConsole __instance, string message)
		{
			Player sender = ChatCMDModUtilities.GetPlayerWithActorID(PhotonNetwork.LocalPlayer.ActorNumber);

			UnityEngine.Debug.Log(message);
			UnityEngine.Debug.Log(sender);
			if (message.StartsWith("!"))
			{
				OnChatSendCmd(sender, message);
				return false; // stop RPCA_SendChat from executing
			};
			return true; 
		}

	}
}
