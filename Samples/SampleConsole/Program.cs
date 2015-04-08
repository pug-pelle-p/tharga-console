﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using Tharga.Toolkit.Console;
using Tharga.Toolkit.Console.Command;
using Tharga.Toolkit.Console.Command.Base;

namespace SampleConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var console = new ClientConsole();
            ////var console = new VoiceConsole();
            ////var console = new ServerConsole(string.Empty);

            var command = new RootCommand(console);
            command.RegisterCommand(new SomeContainerCommand());
            command.RegisterCommand(new EngineContainerCommand());
            new CommandEngine(command).Run(args);
        }
    }

    internal class SomeContainerCommand : ContainerCommandBase
    {
        public SomeContainerCommand()
            : base("some")
        {
            RegisterCommand(new SomeListCommand());
            RegisterCommand(new SomeItemCommand());
            RegisterCommand(new SomeHugeItemCommand());
        }
    }

    internal class SomeItemCommand : ActionCommandBase
    {
        public SomeItemCommand()
            : base("item", "Gets a single item")
        {
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            var index = 0;
            var id = QueryParam<Guid>("Some Id", GetParam(paramList, index), KeyNameList);

            OutputInformation("Some data for {0}", id);

            return true;
        }

        private List<KeyValuePair<Guid, string>> KeyNameList()
        {
            return new List<KeyValuePair<Guid, string>>
            {
                new KeyValuePair<Guid, string>(Guid.Parse("4779177e-2c27-432a-825d-22f9f151391e"), "A"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "BB"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "CCC"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "D"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "F"),
            };
        }
    }

    internal class SomeHugeItemCommand : ActionCommandBase
    {
        public SomeHugeItemCommand()
            : base("huge", "Gets a single huge item")
        {
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            var index = 0;
            var id = QueryParam<Guid>("Some Huge Id", GetParam(paramList, index), HugeKeyNameList);

            OutputInformation("Some data for {0}", id);

            return true;
        }

        private List<KeyValuePair<Guid, string>> HugeKeyNameList()
        {
            return new List<KeyValuePair<Guid, string>>
            {
                new KeyValuePair<Guid, string>(Guid.Parse("4779177e-2c27-432a-825d-22f9f151391e"), "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "CCCCCCCCCC"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD"),
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE"),
            };
        }
    }

    internal class SomeListCommand : ActionCommandBase
    {
        public SomeListCommand()
            : base("list", "Lists some information")
        {
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            for (var i = 0; i < 5; i++) OutputInformation("Some data {0}", i);
            return true;
        }
    }

    public class EngineContainerCommand : ContainerCommandBase
    {
        public EngineContainerCommand()
            : base("Engine")
        {
            RegisterCommand(new WorkingOutputCommand());
            RegisterCommand(new FailingOutputCommand());
        }
    }

    public class WorkingOutputCommand : ActionCommandBase
    {
        private readonly Timer _timer;

        public WorkingOutputCommand()
            : base("work", "An engine output example that works")
        {
            _timer = new Timer { Interval = 3000 };
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var rng = new Random();
            var output = new string('X', rng.Next(30, 200));

            switch (rng.Next(3))
            {
                case 0:
                    OutputInformation(output);
                    break;

                case 1:
                    OutputWarning(output);
                    break;

                case 2:
                    OutputError(output);
                    break;

                case 3:
                    OutputEvent(output);
                    break;
            }
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            _timer.Start();

            return true;
        }
    }

    public class FailingOutputCommand : ActionCommandBase
    {
        private readonly Timer _timer;

        public FailingOutputCommand()
            : base("fail", "An engine output example that fails")
        {
            _timer = new Timer { Interval = 3000 };
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var rng = new Random();
            var output = new string('X', rng.Next(3, 10));

            Console.WriteLine(output);
        }

        public override async Task<bool> InvokeAsync(string paramList)
        {
            _timer.Start();

            return true;
        }
    }
}