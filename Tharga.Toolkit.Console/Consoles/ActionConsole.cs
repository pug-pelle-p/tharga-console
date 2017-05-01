//using System;
//using Tharga.Toolkit.Console.Commands.Base;
//using Tharga.Toolkit.Console.Commands.Entities;
//using Tharga.Toolkit.Console.Interfaces;

//namespace Tharga.Toolkit.Console.Consoles
//{
//    public class ActionConsole : SystemConsoleBase
//    {
//        private readonly Action<IActionConsoleOutput> _action;

//        private class ActionConsoleOutput : IActionConsoleOutput
//        {
//            public ActionConsoleOutput(string value, OutputLevel item2)
//            {
//                Value = value;
//                Level = item2;
//            }

//            public string Value { get; }
//            public OutputLevel Level { get; }
//        }

//        public ActionConsole(Action<IActionConsoleOutput> action)
//            : base(System.Console.Out)
//        {
//            _action = action;
//        }

//        protected internal override void WriteLineEx(string value, OutputLevel level)
//        {
//            _action(new ActionConsoleOutput(value, level));
//        }
//    }
//}