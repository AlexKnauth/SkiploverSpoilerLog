using CondensedSpoilerLogger;
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
    internal class SkiploverSpoiler : CslLogger
    {
        protected override IEnumerable<(string text, string filename)> CreateLogTexts(LogArguments args)
        {
            yield break;
        }
    }
}
