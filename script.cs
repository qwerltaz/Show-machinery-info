using UnityEngine;
using DisplayMachineryDetail.Core;
using DisplayMachineryDetail.Display;
using DisplayMachineryDetail.Readers;

namespace DisplayMachineryDetail
{
    public class Mod
    {
        public static void Main()
        {
            ReaderRegistry.Initialize();
            ModInitializer.RegisterMod();
        }
    }

    public class ShowAttributes : MonoBehaviour
    {
        private float _timeUntilCheck;
        private const float UpdateInterval = 0.5f;

        private IAttributeReader _machineryReader;
        private IAttributeReader _damageReader;
        private AttributeDisplayController _displayController;

        private void Awake()
        {
            _displayController = new AttributeDisplayController(transform);
            InitializeReaders();
        }

        private void InitializeReaders()
        {
            _machineryReader = ReaderRegistry.CreateMachineryReader(gameObject);
            _damageReader = ReaderRegistry.CreateDamageReader(gameObject);
        }

        private void OnDestroy()
        {
            CleanupDisplay();
        }

        public void CleanupDisplay()
        {
            _displayController.Destroy();
        }

        private void Update()
        {
            _timeUntilCheck += Time.unscaledDeltaTime;
            if (_timeUntilCheck > UpdateInterval)
            {
                _timeUntilCheck = 0f;
                UpdateInfo();
            }

            var shouldDisplay = !ModSettings.OnlyDetailView || Global.main.ShowLimbStatus;

            if (shouldDisplay && _displayController.HasText())
            {
                _displayController.UpdatePosition();
            }
        }

        private void UpdateInfo()
        {
            var shouldDisplay = !ModSettings.OnlyDetailView || Global.main.ShowLimbStatus;

            if (shouldDisplay)
            {
                _displayController.SetVisible(true);

                var mainText = _machineryReader.IsValid() ? _machineryReader.GetDisplayText() : "";
                var damageText = _damageReader.IsValid() ? _damageReader.GetDisplayText() : "";

                _displayController.UpdateText(mainText, damageText);
            }
            else
            {
                _displayController.SetVisible(false);
            }
        }
    }
}