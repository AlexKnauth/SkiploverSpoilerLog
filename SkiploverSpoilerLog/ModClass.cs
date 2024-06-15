using System;
using System.Collections;
using System.Collections.Generic;
using Modding;
using UnityEngine;
using UObject = UnityEngine.Object;

using CondensedSpoilerLogger;
using CondensedSpoilerLogger.Util;
using ItemChanger;
using MenuChanger;
using RandomizerCore;
using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerCore.Randomization;
using RandomizerMod.Logging;
using RandomizerMod.RC;
using RandomizerMod.Settings;
using CondensedSpoilerLogger.Loggers;

namespace SkiploverSpoilerLog
{
    public class SkiploverSpoilerLog : Mod
    {
        internal static SkiploverSpoilerLog Instance;

        //public override List<ValueTuple<string, string>> GetPreloadNames()
        //{
        //    return new List<ValueTuple<string, string>>
        //    {
        //        new ValueTuple<string, string>("White_Palace_18", "White Palace Fly")
        //    };
        //}

        //public SkiploverSpoilerLog() : base("SkiploverSpoilerLog")
        //{
        //    Instance = this;
        //}

        public static IEnumerable<CslLogger> CreateLoggers()
        {
            yield return new SkiploverSpoiler();
        }

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {
            Log("Initializing");

            Instance = this;

            ModHooks.FinishedLoadingModsHook += () => LogManager.AddLogger(SkiploverLogWriter.Instance);

            Log("Initialized");
        }
    }
}
