using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//uses a small json class to serialize a user's XR Privacy Framework settings to Unity's player preferences

namespace XRPF.Samples
{
    class XRPF_Preferences
    {
        public bool hardware;
        public bool spatial;
        public bool location;
        public bool social;
        public bool biosensors;
        public override string ToString()
        {
            return string.Format("Privacy Agreement hardwareData:{0} spatialData:{1} locationData:{2} socialData:{3} bioData:{4}",
                hardware,
                spatial,
                location,
                social,
                biosensors);
        }
    }

    public class SaveToPreferences : MonoBehaviour
    {
        const string PreferenceKey = "xrpf";
        void Start()
        {
            TryLoadAgreement();
        }

        /// <summary>
        /// tries to load an existing privacy agreement saved in player prefs to the current agreement
        /// </summary>
        /// <returns>true if a privacy agreement is found</returns>
        bool TryLoadAgreement()
        {
            if (PlayerPrefs.HasKey(PreferenceKey))
            {
                var prefs = JsonUtility.FromJson<XRPF_Preferences>(PlayerPrefs.GetString(PreferenceKey));
                XRPF.PrivacyFramework.SetNewAgreement(prefs.hardware,prefs.spatial,prefs.location,prefs.social,prefs.biosensors);
                Debug.Log("Found XRPF preferences " + prefs.ToString());
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// saves the current xr privacy agreement to player preferences
        /// </summary>
        [ContextMenu("save")]
        void SaveAgreement()
        {
            var prefs = new XRPF_Preferences()
            {
                hardware = XRPF.PrivacyFramework.Agreement.IsHardwareDataAllowed,
                spatial = XRPF.PrivacyFramework.Agreement.IsSpatialDataAllowed,
                location = XRPF.PrivacyFramework.Agreement.IsLocationDataAllowed,
                social = XRPF.PrivacyFramework.Agreement.IsSocialDataAllowed,
                biosensors = XRPF.PrivacyFramework.Agreement.IsBioDataAllowed
            };
            var prefs_string = JsonUtility.ToJson(prefs);
            Debug.Log("Save XRPF preferences: "+ prefs_string);
            PlayerPrefs.SetString(PreferenceKey, prefs_string);
        }
    }
}