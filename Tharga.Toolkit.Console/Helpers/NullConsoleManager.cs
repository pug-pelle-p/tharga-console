using System;
using System.Text;
using Tharga.Toolkit.Console.Interfaces;

namespace Tharga.Toolkit.Console.Helpers
{
    internal class NullConsoleManager : IConsoleManager
    {
        private readonly NullKeyInputEngine _keyInputEngine;

        public NullConsoleManager()
        {
            _keyInputEngine = new NullKeyInputEngine();
            BufferWidth = 80;
            BufferHeight = 300;
        }

        public Encoding Encoding => Encoding.UTF8;
        public int CursorLeft => 0;
        public int CursorTop => 0;
        public int BufferWidth { get; set; }
        public int BufferHeight { get; set; }
        public int WindowWidth { get; set; }
        public int WindowHeight { get; set; }

        public ConsoleColor ForegroundColor { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public IKeyInputEngine KeyInputEngine => _keyInputEngine;
        public string Title { get; set; }

        public void Dispose()
        {
        }

        public void WriteLine(string value)
        {
        }

        public void Write(string value)
        {
        }

        public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
        }

        public void SetCursorPosition(int left, int top)
        {
        }

        public void Clear()
        {
        }

        public void Intercept(IConsole console)
        {
        }
    }
}