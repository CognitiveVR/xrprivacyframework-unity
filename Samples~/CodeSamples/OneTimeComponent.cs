using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//sample code to immediately records data if allowed or subscribes to an event to check if the agreement changes later

namespace XRPF.Samples
{
    public class OneTimeComponent : MonoBehaviour
    {
        private void OnEnable()
        {
            if (XRPF.PrivacyFramework.Agreement.IsBioDataAllowed)
            {
                Debug.Log("Record Bio Data");
            }
            else
            {
                XRPF.PrivacyFramework.OnPrivacyAgreementChanged += PrivacyFramework_OnPrivacyAgreementChanged;
            }
        }

        private void PrivacyFramework_OnPrivacyAgreementChanged()
        {
            if (XRPF.PrivacyFramework.Agreement.IsBioDataAllowed)
            {
                Debug.Log("Record Bio Data");
                XRPF.PrivacyFramework.OnPrivacyAgreementChanged -= PrivacyFramework_OnPrivacyAgreementChanged;
            }
        }
        private void OnDisable()
        {
            XRPF.PrivacyFramework.OnPrivacyAgreementChanged -= PrivacyFramework_OnPrivacyAgreementChanged;
        }
    }
}