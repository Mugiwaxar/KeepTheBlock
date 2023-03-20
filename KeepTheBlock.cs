using BepInEx;
using BepInEx.Logging;
using System;

namespace KeepTheBlock
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class KeepTheBlock : BaseUnityPlugin
    {

        private const string ModGUID = "Dexy-KeepTheBlock";
        private const string ModName = "KeepTheBlock";
        private const string ModVersion = "1.0.1";
        internal static new ManualLogSource Logger { get; set; }

        private void Awake()
        {

            // Get the BepInEx Logger //
            KeepTheBlock.Logger = base.Logger;

            // Log the Start of the Plugin //
            Logger.LogInfo("Plugin KeepTheBlock Added!");

            Utils.Hooks.AddHooks(typeof(MatchManager), nameof(MatchManager.ConsumeAuraCurse), AllowBlockToStay);

        }

        private static void AllowBlockToStay(Action<MatchManager, string, Character, string> orig, MatchManager self, string whenToConsume, Character character, string auraToConsume = "")
        {

            // Set the Block to not be removed at the end of the Round //
            foreach (Aura aura in character.AuraList)
            {
                if (aura.acData != null && aura.acData.id == "block")
                    aura.acData.NoRemoveBlockAtTurnEnd = true;
            }

            orig(self, whenToConsume, character, auraToConsume);
        }
    }
}