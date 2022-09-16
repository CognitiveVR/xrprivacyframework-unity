using System.Collections;
using UnityEngine;

//sample for checking if an agreement is valid in a loop

namespace XRPF.Samples
{
    public class LoopingComponent : MonoBehaviour
    {
        public int i;

        private void Start()
        {
            StartCoroutine(Loop());
        }

        private void Update()
        {
            if (XRPF.PrivacyFramework.Agreement.IsBioDataAllowed)
            {
                i++;
            }
        }

        IEnumerator Loop()
        {
            var wait = new WaitForSeconds(1);
            while (Application.isPlaying)
            {
                if (XRPF.PrivacyFramework.Agreement.IsBioDataAllowed)
                {
                    i++;
                }
                yield return wait;
            }
        }
    }
}