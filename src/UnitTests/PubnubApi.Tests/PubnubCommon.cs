using System;

namespace PubNubMessaging.Tests
{
    public static class PubnubCommon
    {
        private static readonly string EnvPAMServerSideRun = Environment.GetEnvironmentVariable("PN_PAM_SERVER_SIDE", EnvironmentVariableTarget.Machine);
        private static readonly string EnvPublishKey = Environment.GetEnvironmentVariable("PN_PUB_KEY", EnvironmentVariableTarget.Machine);
        private static readonly string EnvSubscribeKey = Environment.GetEnvironmentVariable("PN_SUB_KEY", EnvironmentVariableTarget.Machine);
        private static readonly string EnvSecretKey = Environment.GetEnvironmentVariable("PN_SEC_KEY", EnvironmentVariableTarget.Machine);

        public static readonly bool PAMServerSideRun = (!string.IsNullOrEmpty(EnvPAMServerSideRun) && EnvPAMServerSideRun == "1");
        public static readonly bool PAMServerSideGrant = !PAMServerSideRun;
        public static readonly bool SuppressAuthKey = PAMServerSideRun;
        public static readonly bool EnableStubTest = false;

        //USE demo-36 keys for unit tests 
        public static readonly string PublishKey = string.IsNullOrEmpty(EnvPublishKey) ? "demo-36" : EnvPublishKey;
        public static readonly string SubscribeKey = string.IsNullOrEmpty(EnvSubscribeKey) ? "demo-36" : EnvSubscribeKey;
        public static readonly string SecretKey = string.IsNullOrEmpty(EnvSecretKey) ? "demo-36" : EnvSecretKey;

        public static readonly string StubOrign = "localhost:9191";
        public static readonly string EncodedSDK = "PubNub%20CSharp";

        static PubnubCommon()
        {
            Console.WriteLine(string.Format("Publish Key = {0}", PublishKey.Substring(0,7)));
            Console.WriteLine(string.Format("Subscribe Key = {0}", SubscribeKey.Substring(0, 7)));
            Console.WriteLine(string.Format("Secret Key = {0}", SecretKey.Substring(0, 7)));
         }
    }
}
