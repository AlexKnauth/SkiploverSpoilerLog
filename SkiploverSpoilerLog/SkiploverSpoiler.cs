using CondensedSpoilerLogger;
using CondensedSpoilerLogger.Loggers;
using CondensedSpoilerLogger.Util;
using Modding;
using RandomizerCore;
using RandomizerCore.Logic;
using RandomizerCore.LogicItems;
using RandomizerCore.Randomization;
using RandomizerMod.Logging;
using RandomizerMod.RC;
using RandomizerMod.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SkiploverSpoilerLog
{
    internal class SkiploverSpoiler : ItemProgressionSpoiler
    {
        protected override IEnumerable<(string text, string filename)> CreateLogTexts(LogArguments args)
        {
            List<List<ItemPlacement>> spheredPlacements = CreateSpheredPlacements(args.ctx);
            if (spheredPlacements is null) yield break;

            List<List<ItemPlacement>> importantPlacements = ComputeImportantPlacements(spheredPlacements, args.ctx);

            List<List<ItemPlacement>> filteredPlacements = FilterImportantPlacements(importantPlacements, args.ctx);

            List<List<ItemPlacement>> skiploverPlacements = SkiploverPlacements(filteredPlacements, args.ctx);
            yield return (LogSpheres(skiploverPlacements, args), "SkiploverSpoilerLog.txt");
        }

        public static List<List<ItemPlacement>> SkiploverPlacements(List<List<ItemPlacement>> spheredPlacements, RandoModContext ctx)
        {
            // enable All skips
            GenerationSettings foolishGS = ctx.GenerationSettings.Clone() as GenerationSettings;
            foolishGS.SkipSettings = RandomizerMod.Settings.Presets.SkipPresetData.Foolish.Clone() as SkipSettings;
            foolishGS.Clamp();
            RandoModContext foolishCtx = new(foolishGS, ctx.StartDef);
            foolishCtx.notchCosts = ctx.notchCosts.ToList();
            foolishCtx.itemPlacements = ctx.itemPlacements.ToList();
            foolishCtx.transitionPlacements = ctx.transitionPlacements.ToList();
            foolishCtx.Vanilla = ctx.Vanilla.ToList();
            RCUtil.SetupPM(foolishCtx, out LogicManager lm, out ProgressionManager pm, out MainUpdater mu);

            List<List<ItemPlacement>> skiploverPlacements = new();

            for (int present = 0; present < spheredPlacements.Count; present++)
            {
                List<ItemPlacement> skiploverSphere = new();

                // placements from present & future spheres that are reachable with skips
                for (int future = present; future < spheredPlacements.Count; future++)
                {
                    foreach (ItemPlacement pmt in spheredPlacements[future])
                    {
                        bool inPastSpheres = false;
                        for (int past = 0; past < present; past++)
                        {
                            if (skiploverPlacements[past].Contains(pmt))
                            {
                                inPastSpheres = true;
                                break;
                            }
                        }
                        if (!inPastSpheres && pmt.Location.CanGet(pm))
                        {
                            skiploverSphere.Add(pmt);
                        }
                    }
                }

                // add present sphere to skiploverPlacements and pm last
                skiploverPlacements.Add(skiploverSphere);
                foreach (ItemPlacement pmt in spheredPlacements[present])
                {
                    pm.Add(pmt.Item, pmt.Location);
                }
            }

            return skiploverPlacements;
        }
    }
}
