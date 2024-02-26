using BepInEx;
using System;
using RoR2;
using System.Security;
using System.Security.Permissions;
using UnityEngine;

namespace DedicatedServerCrossplayFix
{
    [BepInPlugin("com.Moffein.DedicatedServerCrossplayFix", "DedicatedServerCrossplayFix", "1.0.0")]
    [module: UnverifiableCode]
    [assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
    public class DedicatedServerCrossplayFix : BaseUnityPlugin
    {
        private void Awake()
        {
            On.RoR2.NetworkPlayerName.Serialize += NetworkPlayerName_Serialize;
        }

        private void NetworkPlayerName_Serialize(On.RoR2.NetworkPlayerName.orig_Serialize orig, ref NetworkPlayerName self, UnityEngine.Networking.NetworkWriter writer)
        {
            if (self.steamId == CSteamID.nil && self.nameOverride == null)
            {
                string resolvedName = self.GetResolvedName();
                if (resolvedName.Length <= 0) resolvedName = "???";
                self.nameOverride = resolvedName;
            }

            orig(ref self, writer);
        }
    }
}

namespace R2API.Utils
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ManualNetworkRegistrationAttribute : Attribute
    {
    }
}
