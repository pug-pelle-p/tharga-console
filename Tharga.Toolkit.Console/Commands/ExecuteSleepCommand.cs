using System.Collections.Generic;
using Tharga.Toolkit.Console.Commands.Base;
using Tharga.Toolkit.Console.Entities;

namespace Tharga.Toolkit.Console.Commands
{
    internal class ExecuteSleepCommand : ActionCommandBase
    {
        internal ExecuteSleepCommand()
            : base("sleep", "Sleep a number of milliseconds.", false)
        {
        }

        public override IEnumerable<HelpLine> HelpText { get { yield return new HelpLine("Have the application sleep for a period of time. The value is specified in milliseconds."); } }

        public override void Invoke(string[] param)
        {
            var millisecondsTimeout = QueryParam<int>("Time", param);
            System.Threading.Thread.Sleep(millisecondsTimeout);
        }
    }
}