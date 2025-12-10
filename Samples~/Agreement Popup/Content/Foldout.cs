using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//expandable UI component used in popup prefab to hide/show description of data sources

namespace XRPF.Samples
{
    public class Foldout : MonoBehaviour
    {
        const float CollapsedHeight = 0.12f;
        float preferredHeight;

        [HideInInspector]
        public bool isCollapsed = true;

        public TMPro.TextMeshProUGUI TitleText;
        public TMPro.TextMeshProUGUI DescriptionText;
        public TMPro.TextMeshProUGUI RequiredText;
        public Toggle Toggle;

        public Image EnableImage;
        public Image DisableImage;

        /// <summary>
        /// internal function to calculate the required size of the rect transform
        /// </summary>
        private void RecalculateSize()
        {
            float preferredHeight = 0;
            preferredHeight += CollapsedHeight; //header
            if (DescriptionText != null)
            {
                preferredHeight += DescriptionText.preferredHeight; //description
            }
            this.preferredHeight = preferredHeight;
        }

        /// <summary>
        /// editor context menu to expand the foldout
        /// </summary>
        [ContextMenu("Expand")]
        private void EditorExpand()
        {
            RecalculateSize();
            SetCollapsed(false);
            isCollapsed = false;
        }

        /// <summary>
        /// editor context menu to collapse the foldout
        /// </summary>
        [ContextMenu("Collapse")]
        private void EditorCollapse()
        {
            RecalculateSize();
            SetCollapsed(true);
            isCollapsed = true;
        }

        public void ToggleCollapse()
        {
            isCollapsed = !isCollapsed;
            RecalculateSize();
            SetCollapsed(isCollapsed);
        }

        public void Button_Toggle(bool enabled)
        {
            if (enabled)
            {
                EnableImage.enabled = true;
                DisableImage.enabled = false;
            }
            else
            {
                EnableImage.enabled = false;
                DisableImage.enabled = true;
            }
        }

        private void SetCollapsed(bool collapsed)
        {
            isCollapsed = collapsed;
            var rectTransform = GetComponent<RectTransform>();
            Vector2 currentSize = rectTransform.sizeDelta;

            float width = currentSize.x;
            float height = preferredHeight;
            if (collapsed)
            {
                height = CollapsedHeight;
            }

            rectTransform.sizeDelta = new Vector2(width, height);
        }

        /// <summary>
        /// configures the foldout to display components and gameobject based on AgreementType
        /// </summary>
        /// <param name="agreementType">what state the toggle button and supporting text should use</param>
        internal void SetDisplay(AgreementType agreementType)
        {
            if (Toggle == null)
            {
                Debug.LogError("XRPF Canvas foldout missing a toggle button!", gameObject);
                return;
            }

            Toggle.interactable = true;
            gameObject.SetActive(true);
            if (RequiredText != null)
            {
                RequiredText.enabled = false;
            }
            switch (agreementType)
            {
                case AgreementType.DefaultOn:
                    Toggle.isOn = true;
                    break;
                case AgreementType.DefaultOff:
                    Toggle.isOn = false;
                    break;
                case AgreementType.Required:
                    Toggle.isOn = true;
                    Toggle.interactable = false;
                    if (RequiredText != null)
                    {
                        RequiredText.enabled = true;
                    }
                    break;
                case AgreementType.Unused:
                    gameObject.SetActive(false);
                    Toggle.isOn = false;
                    Toggle.interactable = false;
                    break;
                default: break;
            }
            EnableImage.enabled = Toggle.isOn;
            DisableImage.enabled = !Toggle.isOn;
        }

        /// <summary>
        /// optional function to quickly set localized strings
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="required"></param>
        public void SetLocalizationText(string title, string description, string required)
        {
            if (TitleText != null)
            {
                TitleText.text = title;
            }
            if (DescriptionText != null)
            {
                DescriptionText.text = description;
            }
            if (RequiredText != null)
            {
                RequiredText.text = required;
            }
        }
    }
}