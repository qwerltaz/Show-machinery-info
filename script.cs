using DisplayMachineryAttributes.Core;
using DisplayMachineryAttributes.Display;
using DisplayMachineryAttributes.Readers;
using UnityEngine;

namespace DisplayMachineryAttributes
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
        private const float UpdateInterval = 0.5f;
        private IAttributeReader _damageReader;
        private AttributeDisplayController _displayController;

        private IAttributeReader _machineryReader;
        private float _timeUntilCheck;

        private void Awake()
        {
            _displayController = new AttributeDisplayController(transform);
            InitializeReaders();
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

            if (shouldDisplay && _displayController?.HasText() == true) _displayController.UpdatePosition();
        }

        private void OnDestroy()
        {
            CleanupDisplay();
        }

        private void InitializeReaders()
        {
            _machineryReader = ReaderRegistry.CreateMachineryReader(gameObject);
            _damageReader = ReaderRegistry.CreateDamageReader(gameObject);
        }

        public void CleanupDisplay()
        {
            _displayController?.Destroy();
        }

        private void UpdateInfo()
        {
            var shouldDisplay = !ModSettings.OnlyDetailView || Global.main.ShowLimbStatus;

            if (shouldDisplay)
            {
                _displayController?.SetVisible(true);

                var mainText = _machineryReader?.IsValid() == true ? _machineryReader.GetDisplayText() : "";
                var damageText = _damageReader?.IsValid() == true ? _damageReader.GetDisplayText() : "";

                _displayController?.UpdateText(mainText, damageText);
            }
            else
            {
                _displayController?.SetVisible(false);
            }
        }
    }
}
