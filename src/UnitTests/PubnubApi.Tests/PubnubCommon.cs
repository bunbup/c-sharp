﻿namespace PubNubMessaging.Tests
{
    public static class PubnubCommon
    {
		public static readonly bool PAMEnabled = true;
		public static readonly bool EnableStubTest = false;

        //USE demo-36 keys for unit tests
        public static readonly string PublishKey = "pub-c-03f156ea-a2e3-4c35-a733-9535824be897";//"demo-36";
        public static readonly string SubscribeKey = "sub-c-d7da9e58-c997-11e9-a139-dab2c75acd6f";// "demo-36";
        public static readonly string SecretKey = "sec-c-MmUxNTZjMmYtNzFkNS00ODkzLWE2YjctNmQ4YzE5NWNmZDA3";// "demo-36";

        public static readonly string StubOrign = "localhost:9191";
        public static readonly string EncodedSDK = "PubNub%20CSharp";

        static PubnubCommon()
        {
        }
    }
}
