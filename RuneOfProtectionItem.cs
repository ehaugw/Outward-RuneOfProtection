
namespace RuneOfProtection
{
    using System.Collections.Generic;
    using SideLoader;
    using InstanceIDs;
    using TinyHelper;

    public class RuneOfProtectionItem
    {
        public const string SubfolderName = "RuneOfProtection";
        public const string ItemName = "Rune of Protection";

        public static Item MakeItem()
        {
            var myitem = new SL_Item()
            {
                Name = ItemName,
                EffectBehaviour = EditBehaviours.Destroy,
                Target_ItemID = IDs.blessedPotionID,
                New_ItemID = IDs.runeOfProtectionID,
                Description = "Grants Runic Protection when used.",
                
                CastType = Character.SpellCastType.Brace,
                CastModifier = Character.SpellCastModifier.Immobilized,
                CastLocomotionEnabled = false,
                MobileCastMovementMult = -1,
                CastSheatheRequired = 1,
                
                StatsHolder = new SL_ItemStats()
                {
                    RawWeight = 0.5f,
                    BaseValue = 100,
                },

                EffectTransforms = new SL_EffectTransform[] {
                    new SL_EffectTransform() {
                        TransformName = "Effects",
                        Effects = new SL_Effect[] {
                        new SL_AddStatusEffect()
                            {
                                StatusEffect = IDs.runicProtectionID
                            }
                        }
                    }
                },

                SLPackName = RuneOfProtection.ModFolderName,
                SubfolderName = SubfolderName
            };
            myitem.ApplyTemplate();
            Item item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);

            return item;
        }

        public static Item MakeRecipe()
        {
            string newUID = TinyUIDManager.MakeUID(ItemName, RuneOfProtection.GUID, "Recipe");
            new SL_Recipe()
            {
                StationType = Recipe.CraftingType.Alchemy,
                Results = new List<SL_Recipe.ItemQty>() {
                    new SL_Recipe.ItemQty() { Quantity = 1, ItemID = IDs.runeOfProtectionID},
                },
                Ingredients = new List<SL_Recipe.Ingredient>() {
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.palladiumScrapsID},
                    new SL_Recipe.Ingredient() { Type = RecipeIngredient.ActionTypes.AddSpecificIngredient, Ingredient_ItemID = IDs.crystalPowderID},
                },
                UID = newUID,
            }.ApplyTemplate();

            var myitem = new SL_RecipeItem()
            {
                Name = "Alchemy: " + ItemName,
                Target_ItemID = IDs.arbitraryAlchemyRecipeID,
                New_ItemID = IDs.runeOfProtectionRecipeID,
                EffectBehaviour = EditBehaviours.Override,
                RecipeUID = newUID
            };
            myitem.ApplyTemplate();
            Item item = ResourcesPrefabManager.Instance.GetItemPrefab(myitem.New_ItemID);
            return item;
        }
    }
}
