namespace RuneOfProtection
{
    using BepInEx;
    using HarmonyLib;
    using InstanceIDs;
    using SideLoader;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using UnityEngine;

    [BepInPlugin(GUID, NAME, VERSION)]
    public class RuneOfProtection : BaseUnityPlugin
    {
        public const string GUID = "com.ehaugw.runeofprotection";
        public const string VERSION = "1.0.0";
        public const string NAME = "Rune of Protection";
        public static string ModFolderName = Directory.GetParent(typeof(RuneOfProtection).Assembly.Location).Name.ToString();

        internal void Awake()
        {
            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            SL.OnPacksLoaded += OnPackLoaded;
            SL.OnSceneLoaded += OnSceneLoaded;
        }
        private void OnPackLoaded()
        {
            RuneOfProtectionItem.MakeItem();
            RuneOfProtectionItem.MakeRecipe();
        }

        private void OnSceneLoaded()
        {
            foreach (GameObject obj in Resources.FindObjectsOfTypeAll<GameObject>().Where(x => x.name == "UNPC_LaineAberforthA" || x.name == "HumanSNPC_CounterAlchemist"))
            {
                if (obj.GetComponentsInChildren<GuaranteedDrop>()?.FirstOrDefault(table => table.ItemGenatorName == "Recipes") is GuaranteedDrop recipes)
                {
                    if (At.GetField<GuaranteedDrop>(recipes, "m_itemDrops") is List<BasicItemDrop> drops)
                    {
                        foreach (Item item in new Item[] { ResourcesPrefabManager.Instance.GetItemPrefab(IDs.runeOfProtectionRecipeID) })
                        {
                            drops.Add(new BasicItemDrop() { ItemRef = item, MaxDropCount = 1, MinDropCount = 1 });
                        }
                    }
                }
            }
        }
    }
}