using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XRPF.Samples
{
    public enum AgreementType
    {
        DefaultOn,
        DefaultOff,
        Required,
        Unused
    }

    [System.Serializable]
    public class ColorTheme
    {
        [Header("Background Gradient")]
        public Color gradientTop = new Color(0.1f, 0.1f, 0.18f, 1f);
        public Color gradientBottom = new Color(0.09f, 0.13f, 0.24f, 1f);
        [Range(0, 360)] public float gradientAngle = 135f;

        [Header("Card Colors")]
        public Color cardBackground = new Color(0.12f, 0.12f, 0.24f, 0.95f);

        [Header("Toggle Row Colors")]
        public Color rowBackground = new Color(0.39f, 0.4f, 0.95f, 0.1f);

        [Header("Icon Colors")]
        public Color iconActiveColor = new Color(0.65f, 0.55f, 0.98f, 1f);
        public Color iconFrameActiveColor = new Color(0.39f, 0.4f, 0.95f, 0.3f);

        [Header("Text Colors")]
        public Color titleColor = Color.white;
        public Color descriptionColor = new Color(1f, 1f, 1f, 0.6f);
        public Color labelActiveColor = Color.white;
        public Color requiredBadgeBgColor = new Color(0.65f, 0.55f, 0.98f, 0.25f);

        [Header("Button Colors")]
        public Color confirmButtonTop = new Color(0.02f, 0.71f, 0.83f, 1f);
        public Color confirmButtonBottom = new Color(0.13f, 0.83f, 0.93f, 1f);
        public Color confirmButtonText = Color.white;
        public Color privacyLinkColor = new Color(0.65f, 0.55f, 0.98f, 1f);

        [Header("Glow Effects")]
        public Color glowColor = new Color(0.39f, 0.4f, 0.95f, 0.4f);
        [Range(0, 1)] public float glowIntensity = 0.5f;
    }

    public class PrivacyAgreementCanvas : MonoBehaviour
    {
        public string PrivacyPolicyLink = "www.example.com";

        [Header("Color Theme")]
        public ColorTheme theme = new ColorTheme();

        [Header("Defaults")]
        public AgreementType HardwareData;
        public AgreementType SpatialData;
        public AgreementType LocationData;
        public AgreementType SocialData;
        public AgreementType BioSensorData;
        public AgreementType AudioData;

        [Header("UI References")]
        public Foldout HardwareFoldout;
        public Foldout SpatialFoldout;
        public Foldout LocationFoldout;
        public Foldout SocialFoldout;
        public Foldout BioSensorFoldout;
        public Foldout AudioFoldout;

        [Header("Background References")]
        public Image backgroundImage;
        public Material backgroundMaterial;

        [Header("Card Settings (finds by name in children)")]
        public string cardBackgroundName = "CardBackground";

        [Header("Button References")]
        public Image confirmButtonImage;
        public Material confirmButtonMaterial;

        [Header("Text References")]
        public TMPro.TextMeshProUGUI TitleText;
        public TMPro.TextMeshProUGUI DescriptionText;
        public TMPro.TextMeshProUGUI ConfirmText;
        public TMPro.TextMeshProUGUI PrivacyPolicyText;

        #region Theme Methods

        public void ApplyTheme()
        {
            // Background gradient
            if (backgroundMaterial != null)
            {
                backgroundMaterial.SetColor("_ColorTop", theme.gradientTop);
                backgroundMaterial.SetColor("_ColorBottom", theme.gradientBottom);
                backgroundMaterial.SetFloat("_Angle", theme.gradientAngle);
            }
            else if (backgroundImage != null)
            {
                backgroundImage.color = theme.gradientTop;
            }

            // Card - find all cards in children by name
            ApplyColorToChildrenByName(cardBackgroundName, theme.cardBackground);

            // Confirm button
            if (confirmButtonMaterial != null)
            {
                confirmButtonMaterial.SetColor("_ColorTop", theme.confirmButtonTop);
                confirmButtonMaterial.SetColor("_ColorBottom", theme.confirmButtonBottom);
            }
            else if (confirmButtonImage != null)
            {
                confirmButtonImage.color = theme.confirmButtonTop;
            }

            // Text colors
            if (TitleText != null)
            {
                TitleText.color = theme.titleColor;
            }
            if (DescriptionText != null)
            {
                DescriptionText.color = theme.descriptionColor;
            }
            if (ConfirmText != null)
            {
                ConfirmText.color = theme.confirmButtonText;
            }
            if (PrivacyPolicyText != null)
            {
                PrivacyPolicyText.color = theme.privacyLinkColor;
            }

            // Apply to all foldouts
            ApplyThemeToFoldout(HardwareFoldout);
            ApplyThemeToFoldout(SpatialFoldout);
            ApplyThemeToFoldout(LocationFoldout);
            ApplyThemeToFoldout(SocialFoldout);
            ApplyThemeToFoldout(BioSensorFoldout);
            ApplyThemeToFoldout(AudioFoldout);
        }

        private void ApplyThemeToFoldout(Foldout foldout)
        {
            if (foldout == null) return;

            foldout.SetThemeColors(
                rowBg: theme.rowBackground,
                iconActive: theme.iconActiveColor,
                iconFrameActive: theme.iconFrameActiveColor,
                labelActive: theme.labelActiveColor,
                requiredBadgeBg: theme.requiredBadgeBgColor
            );
        }

        private void ApplyColorToChildrenByName(string objectName, Color color)
        {
            if (string.IsNullOrEmpty(objectName)) return;

            Image[] allImages = GetComponentsInChildren<Image>(true);
            foreach (Image img in allImages)
            {
                if (img.gameObject.name == objectName)
                {
                    img.color = color;
                }
            }
        }

        #endregion

        #region Localization

        public void SetLocalizationText(string title, string description, string confirm, string privacyPolicy)
        {
            if (TitleText != null) TitleText.text = title;
            if (DescriptionText != null) DescriptionText.text = description;
            if (ConfirmText != null) ConfirmText.text = confirm;
            if (PrivacyPolicyText != null) PrivacyPolicyText.text = privacyPolicy;
        }

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null && canvas.worldCamera == null)
            {
                canvas.worldCamera = Camera.main;
            }

            ApplyTheme();

            if (HardwareFoldout != null) HardwareFoldout.SetDisplay(HardwareData);
            if (SpatialFoldout != null) SpatialFoldout.SetDisplay(SpatialData);
            if (LocationFoldout != null) LocationFoldout.SetDisplay(LocationData);
            if (SocialFoldout != null) SocialFoldout.SetDisplay(SocialData);
            if (BioSensorFoldout != null) BioSensorFoldout.SetDisplay(BioSensorData);
            if (AudioFoldout != null) AudioFoldout.SetDisplay(AudioData);

            if (Application.isPlaying && XRPF.PrivacyFramework.Agreement.IsAgreementComplete)
            {
                LoadExistingAgreement();
            }
        }

        private void OnValidate()
        {
            if (!gameObject.scene.IsValid()) return;
            ApplyTheme();
        }

        #endregion

        #region Agreement Loading

        private void LoadExistingAgreement()
        {
            SetFoldoutFromAgreement(HardwareFoldout, HardwareData, XRPF.PrivacyFramework.Agreement.IsHardwareDataAllowed);
            SetFoldoutFromAgreement(SpatialFoldout, SpatialData, XRPF.PrivacyFramework.Agreement.IsSpatialDataAllowed);
            SetFoldoutFromAgreement(LocationFoldout, LocationData, XRPF.PrivacyFramework.Agreement.IsLocationDataAllowed);
            SetFoldoutFromAgreement(SocialFoldout, SocialData, XRPF.PrivacyFramework.Agreement.IsSocialDataAllowed);
            SetFoldoutFromAgreement(BioSensorFoldout, BioSensorData, XRPF.PrivacyFramework.Agreement.IsBioDataAllowed);
            SetFoldoutFromAgreement(AudioFoldout, AudioData, XRPF.PrivacyFramework.Agreement.IsAudioDataAllowed);
        }

        private void SetFoldoutFromAgreement(Foldout foldout, AgreementType configuredType, bool isAllowed)
        {
            if (foldout == null) return;

            AgreementType resultType;
            if (configuredType == AgreementType.Required || configuredType == AgreementType.Unused)
            {
                resultType = configuredType;
            }
            else
            {
                resultType = isAllowed ? AgreementType.DefaultOn : AgreementType.DefaultOff;
            }
            foldout.SetDisplay(resultType);
        }

        #endregion

        #region Button Handlers

        public void Button_Confirm()
        {
            bool hardwareData = HardwareFoldout != null && HardwareFoldout.Toggle != null ? HardwareFoldout.Toggle.isOn : false;
            bool spatialData = SpatialFoldout != null && SpatialFoldout.Toggle != null ? SpatialFoldout.Toggle.isOn : false;
            bool locationData = LocationFoldout != null && LocationFoldout.Toggle != null ? LocationFoldout.Toggle.isOn : false;
            bool socialData = SocialFoldout != null && SocialFoldout.Toggle != null ? SocialFoldout.Toggle.isOn : false;
            bool biosensorData = BioSensorFoldout != null && BioSensorFoldout.Toggle != null ? BioSensorFoldout.Toggle.isOn : false;
            bool audioData = AudioFoldout != null && AudioFoldout.Toggle != null ? AudioFoldout.Toggle.isOn : false;
            Debug.Log(string.Format("Set Privacy Agreement hardwareData:{0} spatialData:{1} locationData:{2} socialData:{3} bioData:{4} audioData:{5}",
                hardwareData, spatialData, locationData, socialData, biosensorData, audioData));

            XRPF.PrivacyFramework.SetNewAgreement(
                allowHardwareData: hardwareData,
                allowSpatialData: spatialData,
                allowLocationData: locationData,
                allowSocialData: socialData,
                allowBioData: biosensorData,
                allowAudioData: audioData);

            Destroy(gameObject);
        }

        public void Button_PrivacyPolicy()
        {
            Application.OpenURL(PrivacyPolicyLink);
        }

        #endregion
    }
}

#if UNITY_EDITOR
namespace XRPF.Samples
{
    [UnityEditor.CustomEditor(typeof(PrivacyAgreementCanvas))]
    class PrivacyAgreementCanvasInspector : UnityEditor.Editor
    {
        UnityEngine.EventSystems.EventSystem cachedEventSystem;
        Camera cachedCamera;

        public override void OnInspectorGUI()
        {
            var t = target as PrivacyAgreementCanvas;

            if (cachedEventSystem == null)
            {
                cachedEventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
                UnityEditor.EditorGUILayout.HelpBox("Can't find Event System", UnityEditor.MessageType.Warning);
            }
            if (cachedCamera == null)
            {
                cachedCamera = t.GetComponent<Canvas>().worldCamera;
                if (cachedCamera == null)
                {
                    cachedCamera = Camera.main;
                    UnityEditor.EditorGUILayout.HelpBox("Can't find Main Camera", UnityEditor.MessageType.Warning);
                }
            }

            UnityEditor.EditorGUILayout.Space();
            if (GUILayout.Button("Apply Theme", GUILayout.Height(30)))
            {
                t.ApplyTheme();
                UnityEditor.EditorUtility.SetDirty(t);
            }
            UnityEditor.EditorGUILayout.Space();

            base.OnInspectorGUI();
        }
    }
}
#endif