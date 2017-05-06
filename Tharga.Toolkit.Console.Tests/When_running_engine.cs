using System.Threading.Tasks;
using NUnit.Framework;
using Tharga.Toolkit.Console.Commands;
using Tharga.Toolkit.Console.Entities;

namespace Tharga.Toolkit.Console.Tests
{
    [TestFixture]
    public class When_running_engine
    {
        [Test]
        public void Should_prompt_cursor()
        {
            //Arrange
            var consoleManager = new FakeConsoleManager();
            var console = new TestConsole(consoleManager);
            var command = new RootCommand(console);
            var commandEngine = new CommandEngine(command);

            //Act
            Task.Run(() => { commandEngine.Run(new string[] { }); }).Wait(100);

            //Assert
            Assert.That(consoleManager.LineOutput[0], Is.EqualTo("> "));
            Assert.That(consoleManager.CursorTop, Is.EqualTo(0));
            Assert.That(consoleManager.CursorLeft, Is.EqualTo(2));
        }

        [Test]
        public void Should_prompt_cursor_after_line_output()
        {
            //Arrange
            var consoleManager = new FakeConsoleManager();
            var console = new TestConsole(consoleManager);
            var command = new RootCommand(console);
            var commandEngine = new CommandEngine(command);
            Task.Run(() => { commandEngine.Run(new string[] { }); }).Wait(100);

            //Act
            console.Output(new WriteEventArgs("A"));

            //Assert
            Assert.That(consoleManager.LineOutput[0], Is.EqualTo("A"));
            Assert.That(consoleManager.LineOutput[1], Is.EqualTo("> "));
            Assert.That(consoleManager.CursorTop, Is.EqualTo(1));
            Assert.That(consoleManager.CursorLeft, Is.EqualTo(2));
        }

        [Test]
        public void Should_prompt_cursor_after_full_line_output()
        {
            //Arrange
            var consoleManager = new FakeConsoleManager();
            var console = new TestConsole(consoleManager);
            var command = new RootCommand(console);
            var commandEngine = new CommandEngine(command);
            Task.Run(() => { commandEngine.Run(new string[] { }); }).Wait(100);

            //Act
            console.Output(new WriteEventArgs(new string('A', console.BufferWidth)));

            //Assert
            Assert.That(consoleManager.LineOutput[0], Is.EqualTo(new string('A', console.BufferWidth)));
            Assert.That(consoleManager.LineOutput[1], Is.EqualTo("> "));
            Assert.That(consoleManager.CursorTop, Is.EqualTo(1));
            Assert.That(consoleManager.CursorLeft, Is.EqualTo(2));
        }

        [Test]
        public void Should_prompt_cursor_after_more_than_full_line_output()
        {
            //Arrange
            var consoleManager = new FakeConsoleManager();
            var console = new TestConsole(consoleManager);
            var command = new RootCommand(console);
            var commandEngine = new CommandEngine(command);
            Task.Run(() => { commandEngine.Run(new string[] { }); }).Wait(100);

            //Act
            console.Output(new WriteEventArgs(new string('A', console.BufferWidth + 1)));

            //Assert
            Assert.That(consoleManager.LineOutput[0], Is.EqualTo(new string('A', console.BufferWidth)));
            Assert.That(consoleManager.LineOutput[1], Is.EqualTo("A"));
            Assert.That(consoleManager.LineOutput[2], Is.EqualTo("> "));
            Assert.That(consoleManager.CursorTop, Is.EqualTo(2));
            Assert.That(consoleManager.CursorLeft, Is.EqualTo(2));
        }
    }
}