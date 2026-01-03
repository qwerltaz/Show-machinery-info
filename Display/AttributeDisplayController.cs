using UnityEngine;

namespace DisplayMachineryDetail.Display
{
    public class AttributeDisplayController
    {
        private GameObject mainTextObject;
        private GameObject damageTextObject;
        private readonly Transform objectTransform;

        public AttributeDisplayController(Transform objectTransform)
        {
            this.objectTransform = objectTransform;
            InitializeTextObjects();
        }

        private void InitializeTextObjects()
        {
            mainTextObject = CreateTextObject(Color.white);
            damageTextObject = CreateTextObject(Color.yellow);
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
            if (mainTextObject != null)
            {
                mainTextObject.GetComponent<TextMesh>().text = mainText;
            }

            if (damageTextObject != null)
            {
                damageTextObject.GetComponent<TextMesh>().text = damageText;
            }
        }

        public void UpdatePosition()
        {
            if (objectTransform == null) return;

            float height = objectTransform.localScale.y;
            float width = objectTransform.localScale.x;
            float scaleFactor = Mathf.Sqrt(height) / 50;

            if (mainTextObject != null)
            {
                mainTextObject.transform.position = objectTransform.position + new Vector3(0f, -0.03f - 0.2f * height, 0f);
                mainTextObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                mainTextObject.transform.rotation = Quaternion.identity;
            }

            if (damageTextObject != null)
            {
                damageTextObject.transform.position = objectTransform.position + new Vector3(0.03f + 0.2f * width, 0f, 0f);
                damageTextObject.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                damageTextObject.transform.rotation = Quaternion.identity;
            }
        }

        public void SetVisible(bool visible)
        {
            if (!visible)
            {
                Destroy();
            }
            else if (mainTextObject == null || damageTextObject == null)
            {
                InitializeTextObjects();
            }
        }

        public void Destroy()
        {
            if (mainTextObject != null)
            {
                UnityEngine.Object.Destroy(mainTextObject);
                mainTextObject = null;
            }

            if (damageTextObject != null)
            {
                UnityEngine.Object.Destroy(damageTextObject);
                damageTextObject = null;
            }
        }

        public bool HasText()
        {
            return (mainTextObject != null && !string.IsNullOrEmpty(mainTextObject.GetComponent<TextMesh>()?.text))
                || (damageTextObject != null && !string.IsNullOrEmpty(damageTextObject.GetComponent<TextMesh>()?.text));
        }
    }
}

