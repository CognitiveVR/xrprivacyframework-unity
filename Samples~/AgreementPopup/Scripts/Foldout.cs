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

        [Header("Theme References")]
        public Image rowBackground;
        public Image iconImage;
        public Image iconFrame;
        public Image RequiredFrame;

        // Store theme colors
        private Color _rowActiveBg;
        private Color _iconActive;
        private Color _iconFrameActive;
        private Color _labelActive;
        private Color _requiredBadgeBg;

        /// <summary>
        /// Set all theme colors for this foldout
        /// </summary>
        public void SetThemeColors(
            Color rowBg,
            Color iconActive,
            Color iconFrameActive,
            Color labelActive,
            Color requiredBadgeBg)
        {
            _rowActiveBg = rowBg;
            _iconActive = iconActive;
            _iconFrameActive = iconFrameActive;
            _labelActive = labelActive;
            _requiredBadgeBg = requiredBadgeBg;

            // Apply required badge colors
            if (RequiredFrame != null)
            {
                RequiredFrame.color = _requiredBadgeBg;
            }

            // Refresh current visual state
            UpdateVisualState();
        }

        /// <summary>
        /// Update visual state based on toggle
        /// </summary>
        public void UpdateVisualState()
        {
            if (rowBackground != null)
            {
                rowBackground.color = _rowActiveBg;
            }
            if (iconImage != null)
            {
                iconImage.color = _iconActive;
            }
            if (iconFrame != null)
            {
                iconFrame.color = _iconFrameActive;
            }
            if (TitleText != null)
            {
                TitleText.color = _labelActive;
            }
        }

        /// <summary>
        /// Call this when toggle value changes
        /// </summary>
        public void OnToggleValueChanged(bool isOn)
        {
            UpdateVisualState();
        }

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