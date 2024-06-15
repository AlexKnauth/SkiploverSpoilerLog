using CondensedSpoilerLogger.Util;
using RandomizerMod.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiploverSpoilerLog
{
    internal class SkiploverLogWriter : RandoLogger
    {
        internal static SkiploverLogWriter Instance { get; } = new();

        internal void Hook()
        {

        }

        public override void Log(LogArguments args)
        {
            Task.Factory.StartNew(CreateLoggingAction(args));
        }

        private Action CreateLoggingAction(LogArguments args)
        {
            void DoLogging()
            {
                foreach ((string text, string filename) in
                    SkiploverSpoilerLog.CreateLoggers().SelectMany(log => log.GetLogTexts(args)))
                {
                    LogManager.Write(text, filename);
                }
            }

            return DoLogging;
        }
    }
}
