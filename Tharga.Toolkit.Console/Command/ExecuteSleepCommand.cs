using System.Collections.Generic;
using System.Threading.Tasks;
using Tharga.Toolkit.Console.Command.Base;

namespace Tharga.Toolkit.Console.Command
{
    internal class ExecuteSleepCommand : ActionCommandBase
    {
        internal ExecuteSleepCommand(IConsole console)
            : base(console, new [] { "sleep" }, "Sleep a number of milliseconds.", false)
        {
        }

        public override IEnumerable<HelpLine> HelpText { get { yield return new HelpLine("Have the application sleep for a period of time. The value is specified in milliseconds."); } }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            var millisecondsTimeout = QueryParam<int>("Time", GetParam(paramList, 0));

            System.Threading.Thread.Sleep(millisecondsTimeout);

            return true;
        }
    }
}