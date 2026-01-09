using UnityEngine;

namespace DisplayMachineryDetail.Display
{
    public class AttributeDisplayController
    {
        private GameObject _mainTextObject;
        private GameObject _damageTextObject;
        private readonly Transform _objectTransform;

        public AttributeDisplayController(Transform objectTransform)
        {
            _objectTransform = objectTransform;
            InitializeTextObjects();
        }

        private void InitializeTextObjects()
        {
            _mainTextObject = CreateTextObject(Color.white);
            _damageTextObject = CreateTextObject(Color.yellow);
        }

        private GameObject CreateTextObject(Color color)
        {
            GameObject textObj = new GameObject();
            textObj.transform.localScale = new Vector3(0.03f, 0.03f, 1f);
            TextMesh textMesh = textObj.AddComponent<TextMesh>();
            textMesh.fontSize = 32;
            textMesh.anchor = TextAnchor.MiddleCenter;
            textMesh.color = color;
            textObj.layer = 16;
            return textObj;
        }

        public void UpdateText(string mainText, string damageText)
        {
            if (_mainTextObject != null)
            {
                _mainTextObject.GetComponent<TextMesh>().text = mainText;
            }

            if (_damageTextObject != null)
            {
                _damageTextObject.GetComponent<TextMesh>().text = damageText;
            }
        }

        public void UpdatePosition()
        {
            if (!_objectTransform) return;

            var height = _objectTransform.localScale.y;
            var width = _objectTransform.localScale.x;
            var scaleFactor = Mathf.Sqrt(height) / 50;

            if (_mainTextObject != null)
            {
                _mainTextObject.transform.position =
                    _objectTransform.position + new Vector3(0f, -0.03f - 0.2f * height, 0f);
                _mainTextObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                _mainTextObject.transform.rotation = Quaternion.identity;
            }

            if (_damageTextObject != null)
            {
                _damageTextObject.transform.position =
                    _objectTransform.position + new Vector3(0.03f + 0.2f * width, 0f, 0f);
                _damageTextObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                _damageTextObject.transform.rotation = Quaternion.identity;
            }
        }

        public void SetVisible(bool visible)
        {
            if (!visible)
            {
                Destroy();
            }
            else if (!_mainTextObject || !_damageTextObject)
            {
                InitializeTextObjects();
            }
        }

        public void Destroy()
        {
            if (_mainTextObject)
            {
                Object.Destroy(_mainTextObject);
                _mainTextObject = null;
            }

            if (_damageTextObject)
            {
                Object.Destroy(_damageTextObject);
                _damageTextObject = null;
            }
        }

        public bool HasText()
        {
            return (_mainTextObject != null && !string.IsNullOrEmpty(_mainTextObject.GetComponent<TextMesh>()?.text))
                   || (_damageTextObject != null &&
                       !string.IsNullOrEmpty(_damageTextObject.GetComponent<TextMesh>()?.text));
        }
    }
}