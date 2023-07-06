using BepInEx;
using UnityEngine;
using UnboundLib;
using UnboundLib.Cards;
using HarmonyLib;
using ModsPlus;


using CardChoiceSpawnUniqueCardPatch.CustomCategories;
namespace ChatCommands
{
	// These are the mods required for our mod to work
	[BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
	[BepInDependency("com.willis.rounds.modsplus", BepInDependency.DependencyFlags.HardDependency)]
	//[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
	// Declares our mod to Bepin
	[BepInPlugin(ModId, ModName, Version)]
	// The game our mod is associated with
	[BepInProcess("Rounds.exe")]
	public class ChatCommands : BaseUnityPlugin
	{
		private const string ModId = "com.lonke.rounds.ChatCommands";
		private const string ModName = "ChatCommands";
		public const string Version = "0.1.0"; // What version are we on (major.minor.patch)?

		public const string ModInitials = "ChCMD";

#pragma warning disable CS8618
		public static ChatCommands instance { get; private set; }

		void Awake()
		{
			// Use this to call any harmony patch files your mod may have
			var harmony = new Harmony(ModId);
			harmony.PatchAll();
		}
		void Start()
		{
			instance = this;
		}

		private const float timeBetweenSandboxBtnCheck = 2f;
		private float nextCheck = Time.time + timeBetweenSandboxBtnCheck;
		private bool shouldStartSandbox = true;

		void Update()
		{
			if (!shouldStartSandbox) return;
			if (Time.time > nextCheck)
			{
				nextCheck = Time.time + timeBetweenSandboxBtnCheck;
				UnityEngine.Debug.Log("Checking...");

			} else return;
			
			UnityEngine.Debug.Log("Checks passed");
			
			string localBtnPath        = "Game/UI/UI_MainMenu/Canvas/ListSelector/Main/Group/LOCAL";
			string startSandboxBtnPath = "Game/UI/UI_MainMenu/Canvas/ListSelector/LOCAL/Group/Grid/Scroll View/Viewport/Content/Test";

			GameObject menuLocalBtn = GameObject.Find(localBtnPath);
			GameObject menuSandboxBtn = GameObject.Find(startSandboxBtnPath);
			
			if (menuLocalBtn == null || menuSandboxBtn == null) return;
			UnityEngine.Debug.Log("Objects Found");
			if (menuLocalBtn.activeInHierarchy)
			{
				UnityEngine.Debug.Log("Local btn is active, clicking");
				menuLocalBtn.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
				nextCheck = Time.time + 0.5f;
			} 
			else if (menuSandboxBtn.activeInHierarchy)
			{
				UnityEngine.Debug.Log("Sandbox btn is active, clicking");
				menuSandboxBtn.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
				shouldStartSandbox = false;
			}
		}
	}
}