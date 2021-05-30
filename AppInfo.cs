using SteamKit2;
using System;
using System.Diagnostics;

namespace BeatSaberNoUpdate
{
    class AppInfo
    {
        // Beat Saber's app id
        const uint APPID = 620980;

        private static SteamClient steamClient;
        private static SteamApps steamApps;
        private static SteamUser steamUser;
        private static CallbackManager manager;

        private static bool isRunning;

        private static JobID infoRequest = JobID.Invalid;
        private static string manifestId = "";
        private static bool success = false;

        public (bool, string) Initialize()
        {
            // initialize a client and a callback manager for responding to events
            steamClient = new SteamClient();
            manager = new CallbackManager(steamClient);

            // get a handler and register to callbacks we are interested in
            steamUser = steamClient.GetHandler<SteamUser>();
            steamApps = steamClient.GetHandler<SteamApps>();
            manager.Subscribe<SteamApps.PICSProductInfoCallback>(OnProductInfo);
            manager.Subscribe<SteamClient.ConnectedCallback>(OnConnected);
            manager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnected);
            manager.Subscribe<SteamUser.LoggedOnCallback>(OnLoggedOn);
            manager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);


            isRunning = true;

            Debug.WriteLine("Connecting to Steam...");

            steamClient.Connect();

            while (isRunning)
            {
                manager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
            }

            return (success, manifestId);
        }


        static void OnConnected(SteamClient.ConnectedCallback callback)
        {
            Debug.WriteLine("Successfully connected to Steam");
            // log on anonymously
            steamUser.LogOnAnonymous();
        }

        static void OnDisconnected(SteamClient.DisconnectedCallback callback)
        {
            Debug.WriteLine("Disconnected from Steam");
            isRunning = false;
        }

        static void OnLoggedOn(SteamUser.LoggedOnCallback callback)
        {
            if (callback.Result != EResult.OK)
            {
                if (callback.Result == EResult.AccountLogonDenied)
                {
                    // this shouldn't happen when logging in anonymously
                    Debug.WriteLine("Unable to login to Steam");
                    isRunning = false;
                    return;
                }
                // if there is another error
                Debug.WriteLine("Unable to login to Steam: {0} / {1}", callback.Result, callback.ExtendedResult);
                isRunning = false;
                return;
            }

            Debug.WriteLine("Successfully logged in to Steam");


            // request product info
            infoRequest = steamApps.PICSGetProductInfo(APPID, null);
        }

        private void OnProductInfo(SteamApps.PICSProductInfoCallback callback)
        {
            Debug.WriteLine("In OnProductInfo callback");
            if (callback.JobID != infoRequest)
            {
                return;
            }

            // not sure if this is game specific or applies to other games
            manifestId = callback.Apps[APPID].KeyValues.Children[4].Children[1].Children[1].Children[0].Value;

            if (manifestId.Length == 19) success = true;
            Debug.WriteLine(manifestId);
            // log off after getting the manifest
            steamUser.LogOff();
        }

        static void OnLoggedOff(SteamUser.LoggedOffCallback callback)
        {
            Debug.WriteLine("Logged off Steam: {0}", callback.Result);
            isRunning = false;
        }
    }
}
