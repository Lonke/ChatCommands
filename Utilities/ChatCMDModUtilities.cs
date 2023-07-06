using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace ChatCommands.Utilities
{
	/*
		  internal Player GetPlayerWithActorID(int actorID)
		{
			for (int i = 0; i < this.players.Count; i++)
			{
				if (this.players[i].data.view.OwnerActorNr == actorID)
				{
					return this.players[i];
				}
			}
			return null;
		}
		*/

	//public static 

	public class ChatCMDModUtilities
	{
#pragma warning disable CS0169
		private static Assembly? _mscorLib;

		// mscorlib singleton for late-binding
		public static Assembly mscorlib
		{
			get
			{
				if (_mscorLib == null)
				{
					_mscorLib = Assembly.Load("mscorlib.dll");
					return _mscorLib;
				}
				else
					return _mscorLib;
			}
		}


		public static Player GetPlayerWithActorID(int actorID)
		{
			List<Player> players = PlayerManager.instance.players;

			for (int index = 0; index < players.Count; ++index)
			{
				if (players[index].data.view.OwnerActorNr == actorID)
					return players[index];
			}

			return (Player)null;
		}

		public static ICollection<string> StringSplit(string inputString, char delimiter)
		{
			var splitStrings = new List<string>();

			StringBuilder sbCurrentBuffer = new StringBuilder(100);

			foreach (char c in inputString)
			{
				if (c != delimiter)
					sbCurrentBuffer.Append(c);
				else
				{
					splitStrings.Add( sbCurrentBuffer.ToString() );
					sbCurrentBuffer.Clear();
				}
			}
			// handle string end
			splitStrings.Add(sbCurrentBuffer.ToString());
			sbCurrentBuffer.Clear();

			return splitStrings;
		}
	}
}