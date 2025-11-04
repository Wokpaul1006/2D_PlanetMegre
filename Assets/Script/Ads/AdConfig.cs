public class AdConfig
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitId();
    public static string InterstitalAdUnitId => GetInterstitialAdUnitId();
    public static string RewardedVideoAdUnitId => GetRewardedVideoAdUnitId();
    static string GetAppKey()
    {
#if UNITY_ANDROID
        return "24004759d";
#elif UNITY_IPHONE
            return "8545d445";
#else
            return "unexpected_platform";
#endif
    }
    static string GetBannerAdUnitId()
    {
#if UNITY_ANDROID
        return "g14a8nd2zry6hopi";
#elif UNITY_IPHONE
            return "iep3rxsyp9na3rw8";
#else
            return "unexpected_platform";
#endif
    }
    static string GetInterstitialAdUnitId()
    {
#if UNITY_ANDROID
        return "wm6fk34cgjq0nxzn";
#elif UNITY_IPHONE
            return "wmgt0712uuux8ju4";
#else
            return "unexpected_platform";
#endif
    }
    static string GetRewardedVideoAdUnitId()
    {
#if UNITY_ANDROID
        return "m0pnmejhjkmgcngd";
#elif UNITY_IPHONE
            return "qwouvdrkuwivay5q";
#else
            return "unexpected_platform";
#endif
    }
}
