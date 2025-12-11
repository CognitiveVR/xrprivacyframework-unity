using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//prefab world space canvas prefab
//allows a user to select what data sources can be collected

namespace XRPF.Samples
{
    public enum AgreementType
    {
        DefaultOn,
        DefaultOff,
        Required,
        Unused
    }

    public class PrivacyAgreementCanvas : MonoBehaviour
    {
        public string PrivacyPolicyLink = "www.example.com";

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

        public TMPro.TextMeshProUGUI TitleText;
        public TMPro.TextMeshProUGUI DescriptionText;
        public TMPro.TextMeshProUGUI ConfirmText;
        public TMPro.TextMeshProUGUI PrivacyPolicyText;

        /// <summary>
        /// optional function to quickly set localized strings
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="confirm"></param>
        /// <param name="privacyPolicy"></param>
        public void SetLocalizationText(string title, string description, string confirm, string privacyPolicy)
        {
            if (TitleText != null)
            {
                TitleText.text = title;
            }
            if (DescriptionText != null)
            {
                DescriptionText.text = description;
            }
            if (ConfirmText != null)
            {
                ConfirmText.text = confirm;
            }
            if (PrivacyPolicyText != null)
            {
                PrivacyPolicyText.text = privacyPolicy;
            }
        }

        /// <summary>
        /// set each foldout's display (in play mode or editor)
        /// </summary>
        private void Start()
        {
            Canvas canvas = GetComponent<Canvas>();
            if (canvas != null && canvas.worldCamera == null)
            {
                canvas.worldCamera = Camera.main;
            }

            if (HardwareFoldout != null)
            {
                HardwareFoldout.SetDisplay(HardwareData);
            }
            if (SpatialFoldout != null)
            {
                SpatialFoldout.SetDisplay(SpatialData);
            }
            if (LocationFoldout != null)
            {
                LocationFoldout.SetDisplay(LocationData);
            }
            if (SocialFoldout != null)
            {
                SocialFoldout.SetDisplay(SocialData);
            }
            if (BioSensorFoldout != null)
            {
                BioSensorFoldout.SetDisplay(BioSensorData);
            }
            if (AudioFoldout != null)
            {
                AudioFoldout.SetDisplay(AudioData);
            }

            //if there's an existing agreement, load that
            if (Application.isPlaying && XRPF.PrivacyFramework.Agreement.IsAgreementComplete)
            {
                AgreementType tempAgreementType;
                if (HardwareData == AgreementType.Required)
                {
                    tempAgreementType = AgreementType.Required;
                }
                else if (HardwareData == AgreementType.Unused)
                {
                    tempAgreementType = AgreementType.Unused;
                }
                else if (XRPF.PrivacyFramework.Agreement.IsHardwareDataAllowed)
                {
                    tempAgreementType = AgreementType.DefaultOn;
                }
                else
                {
                    tempAgreementType = AgreementType.DefaultOff;
                }
                if (HardwareFoldout != null)
                {
                    HardwareFoldout.SetDisplay(tempAgreementType);
                }

                if (SpatialData == AgreementType.Required)
                {
                    tempAgreementType = AgreementType.Required;
                }
                else if (SpatialData == AgreementType.Unused)
                {
                    tempAgreementType = AgreementType.Unused;
                }
                else if (XRPF.PrivacyFramework.Agreement.IsSpatialDataAllowed)
                {
                    tempAgreementType = AgreementType.DefaultOn;
                }
                else
                {
                    tempAgreementType = AgreementType.DefaultOff;
                }
                if (SpatialFoldout != null)
                {
                    SpatialFoldout.SetDisplay(tempAgreementType);
                }

                if (LocationData == AgreementType.Required)
                {
                    tempAgreementType = AgreementType.Required;
                }
                else if (LocationData == AgreementType.Unused)
                {
                    tempAgreementType = AgreementType.Unused;
                }
                else if (XRPF.PrivacyFramework.Agreement.IsLocationDataAllowed)
                {
                    tempAgreementType = AgreementType.DefaultOn;
                }
                else
                {
                    tempAgreementType = AgreementType.DefaultOff;
                }
                if (LocationFoldout != null)
                {
                    LocationFoldout.SetDisplay(tempAgreementType);
                }

                if (SocialData == AgreementType.Required)
                {
                    tempAgreementType = AgreementType.Required;
                }
                else if (SocialData == AgreementType.Unused)
                {
                    tempAgreementType = AgreementType.Unused;
                }
                else if (XRPF.PrivacyFramework.Agreement.IsSocialDataAllowed)
                {
                    tempAgreementType = AgreementType.DefaultOn;
                }
                else
                {
                    tempAgreementType = AgreementType.DefaultOff;
                }
                if (SocialFoldout != null)
                {
                    SocialFoldout.SetDisplay(tempAgreementType);
                }

                if (BioSensorData == AgreementType.Required)
                {
                    tempAgreementType = AgreementType.Required;
                }
                else if (BioSensorData == AgreementType.Unused)
                {
                    tempAgreementType = AgreementType.Unused;
                }
                else if (XRPF.PrivacyFramework.Agreement.IsBioDataAllowed)
                {
                    tempAgreementType = AgreementType.DefaultOn;
                }
                else
                {
                    tempAgreementType = AgreementType.DefaultOff;
                }
                if (BioSensorFoldout != null)
                {
                    BioSensorFoldout.SetDisplay(tempAgreementType);
                }

                if (AudioData == AgreementType.Required)
                {
                    tempAgreementType = AgreementType.Required;
                }
                else if (AudioData == AgreementType.Unused)
                {
                    tempAgreementType = AgreementType.Unused;
                }
                else if (XRPF.PrivacyFramework.Agreement.IsAudioDataAllowed)
                {
                    tempAgreementType = AgreementType.DefaultOn;
                }
                else
                {
                    tempAgreementType = AgreementType.DefaultOff;
                }
                if (AudioFoldout != null)
                {
                    AudioFoldout.SetDisplay(tempAgreementType);
                }
            }
        }

        /// <summary>
        /// update gameobjects based on inspector settings
        /// </summary>
        private void OnValidate()
        {
            //skip validating if modifying prefab
            if (!gameObject.scene.IsValid()) { return; }
            Start();
        }

        /// <summary>
        /// create user privacy agreement and close this window
        /// </summary>
        public void Button_Confirm()
        {
            bool hardwareData = HardwareFoldout != null && HardwareFoldout.Toggle != null ? HardwareFoldout.Toggle.isOn : false;
            bool spatialData = SpatialFoldout != null && SpatialFoldout.Toggle != null ? SpatialFoldout.Toggle.isOn : false;
            bool locationData = LocationFoldout != null && LocationFoldout.Toggle != null ? LocationFoldout.Toggle.isOn : false;
            bool socialData = SocialFoldout != null && SocialFoldout.Toggle != null ? SocialFoldout.Toggle.isOn : false;
            bool biosensorData = BioSensorFoldout != null && BioSensorFoldout.Toggle != null ? BioSensorFoldout.Toggle.isOn : false;
            bool audioData = AudioFoldout != null && AudioFoldout.Toggle != null ? AudioFoldout.Toggle.isOn : false;

            Debug.Log(string.Format("Set Privacy Agreement hardwareData:{0} spatialData:{1} locationData:{2} socialData:{3} bioData:{4} audioData:{5}",
                hardwareData,
                spatialData,
                locationData,
                socialData,
                biosensorData,
                audioData));

            XRPF.PrivacyFramework.SetNewAgreement(
                allowHardwareData: hardwareData,
                allowSpatialData: spatialData,
                allowLocationData: locationData,
                allowSocialData: socialData,
                allowBioData: biosensorData,
                allowAudioData: audioData);

            Destroy(gameObject);
        }

        /// <summary>
        /// open privacy policy in the default browser
        /// </summary>
        public void Button_PrivacyPolicy()
        {
            Application.OpenURL(PrivacyPolicyLink);
        }
    }
}

#if UNITY_EDITOR
namespace XRPF.Samples
{
    //display warnings if camera is null and if there isn't an event system to interact with the canvas
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
            base.OnInspectorGUI();
        }
    }
}
#endif