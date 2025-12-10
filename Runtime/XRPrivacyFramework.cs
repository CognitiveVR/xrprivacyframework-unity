namespace XRPF
{
    public interface IXRPFProvider
    {
        bool IsAgreementComplete { get; }
        bool IsSocialDataAllowed { get; }
        bool IsBioDataAllowed { get; }
        bool IsLocationDataAllowed { get; }
        bool IsSpatialDataAllowed { get; }
        bool IsHardwareDataAllowed { get; }
        bool IsAudioDataAllowed { get; }
    }

    /// <summary>
    /// core class for setting and referencing a user's privacy agreement
    /// </summary>
    public static class PrivacyFramework
    {
        public static IXRPFProvider Agreement { get; private set; }

        public delegate void privacyAgreementChanged();
        /// <summary>
        /// called after a privacy agreement has been set or updated or revoked
        /// </summary>
        public static event privacyAgreementChanged OnPrivacyAgreementChanged;

        static PrivacyFramework()
        {
            Agreement = new XRPFNullAgreement();
        }

        /// <summary>
        /// set a simple XRPF agreement
        /// </summary>
        /// <param name="allowHardwareData"></param>
        /// <param name="allowSpatialData"></param>
        /// <param name="allowLocationData"></param>
        /// <param name="allowSocialData"></param>
        /// <param name="allowBioData"></param>
        public static void SetNewAgreement(bool allowHardwareData, bool allowSpatialData, bool allowLocationData, bool allowSocialData, bool allowBioData, bool allowAudioData)
        {
            Agreement = new XRPFAgreement(allowHardwareData, allowSpatialData, allowLocationData, allowSocialData, allowBioData, allowAudioData);
            if (OnPrivacyAgreementChanged != null) { OnPrivacyAgreementChanged(); }
        }

        /// <summary>
        /// set a custom XRPF agreement
        /// </summary>
        /// <param name="provider"></param>
        public static void SetNewAgreement(IXRPFProvider provider)
        {
            Agreement = provider;
            if (OnPrivacyAgreementChanged != null) { OnPrivacyAgreementChanged(); }
        }

        /// <summary>
        /// remove all consent from a user
        /// </summary>
        public static void RevokeAgreement()
        {
            Agreement = new XRPFNullAgreement();
			if (OnPrivacyAgreementChanged != null) { OnPrivacyAgreementChanged(); }
        }
    }

    /// <summary>
    /// implementation to provide no consent before the user has seen their options
    /// </summary>
    public class XRPFNullAgreement : IXRPFProvider
    {
        public bool IsAgreementComplete { get { return false; } }
        public bool IsSocialDataAllowed { get { return false; } }
        public bool IsBioDataAllowed { get { return false; } }
        public bool IsLocationDataAllowed { get { return false; } }
        public bool IsSpatialDataAllowed { get { return false; } }
        public bool IsHardwareDataAllowed { get { return false; } }
        public bool IsAudioDataAllowed { get { return false; } }
    }

    /// <summary>
    /// basic agreement to various data sources based on the user's consent
    /// </summary>
    public class XRPFAgreement : IXRPFProvider
    {
        public XRPFAgreement(bool hardwareData, bool spatialData, bool locationData, bool socialData, bool bioData, bool audioData)
        {
            IsAgreementComplete = true;
            IsHardwareDataAllowed = hardwareData;
            IsSpatialDataAllowed = spatialData;
            IsLocationDataAllowed = locationData;
            IsSocialDataAllowed = socialData;
            IsBioDataAllowed = bioData;
            IsAudioDataAllowed = audioData;
        }

        public bool IsAgreementComplete { get; private set; }
        public bool IsSocialDataAllowed { get; private set; }
        public bool IsBioDataAllowed { get; private set; }
        public bool IsLocationDataAllowed { get; private set; }
        public bool IsSpatialDataAllowed { get; private set; }
        public bool IsHardwareDataAllowed { get; private set; }
        public bool IsAudioDataAllowed { get; private set; }
    }
}