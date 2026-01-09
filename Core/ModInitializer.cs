using DisplayMachineryDetail.Readers;

namespace DisplayMachineryDetail.Core
{
    public static class ModInitializer
    {
        public static void RegisterMod()
        {
            RegisterToggleItem();
            RegisterShowAttributesComponent();
            RegisterEventHandlers();
        }

        private static void RegisterToggleItem()
        {
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Brick"),
                    NameOverride = "Toggle detail view only",
                    NameToOrderByOverride = "b",
                    DescriptionOverride =
                        "Toggle whether to show machinery info only in detail view (default), or always.",
                    CategoryOverride = ModAPI.FindCategory("Entities"),
                    ThumbnailOverride = ModAPI.LoadSprite("thumb_ondetailview.png"),
                    AfterSpawn = (instance) =>
                    {
                        instance.GetComponent<PhysicalBehaviour>().Disintegrate();
                        UnityEngine.Object.Destroy(instance, 3f);
                        ModSettings.OnlyDetailView = !ModSettings.OnlyDetailView;
                        string status = ModSettings.OnlyDetailView ? "on" : "off";
                        ModAPI.Notify($"Display machinery attributes in detail mode only: {status}");
                    }
                }
            );
        }

        private static void RegisterShowAttributesComponent()
        {
            ModAPI.Register<ShowAttributes>();
        }

        private static void RegisterEventHandlers()
        {
            ModAPI.OnItemSpawned += OnItemSpawned;
            ModAPI.OnItemRemoved += OnItemRemoved;
        }

        private static void OnItemSpawned(object sender, UserSpawnEventArgs args)
        {
            var instance = args.Instance;
            var targetGameObject = ReaderRegistry.GetTargetForShowAttributes(instance);

            if (targetGameObject != null)
            {
                targetGameObject.GetOrAddComponent<ShowAttributes>();
            }
        }

        private static void OnItemRemoved(object sender, UserSpawnEventArgs args)
        {
            ShowAttributes component = args.Instance.GetComponent<ShowAttributes>();
            if (component != null)
            {
                component.CleanupDisplay();
            }
        }
    }
}