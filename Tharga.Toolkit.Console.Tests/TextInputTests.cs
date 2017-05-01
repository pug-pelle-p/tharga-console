using System;
using System.Collections.Generic;
using System.Threading;
using Moq;
using NUnit.Framework;
using Tharga.Toolkit.Console.Commands.Base;
using Tharga.Toolkit.Console.Commands.Helpers;
using Tharga.Toolkit.Console.Interfaces;

namespace Tharga.Toolkit.Console.Tests
{
    [TestFixture]
    public class TextInputTests
    {
        [Test]
        [Ignore("Fix")]
        public void When_()
        {
            //Arrange
            var consoleMock = new Mock<IConsole>(MockBehavior.Strict);
            consoleMock.Setup(x => x.Write(It.IsAny<string>()));
            consoleMock.SetupGet(x => x.CursorLeft).Returns(0);
            consoleMock.SetupGet(x => x.CursorTop).Returns(0);
            consoleMock.SetupGet(x => x.BufferWidth).Returns(80);
            consoleMock.Setup(x => x.NewLine());
            consoleMock.Setup(x => x.ReadKey(new CancellationToken())).Returns(() => new ConsoleKeyInfo((char)13, ConsoleKey.Enter, false, false, false));

            var inputManager = new InputManager(consoleMock.Object); //, "> ");

            //Act
            var response = inputManager.ReadLine("> ", new KeyValuePair<string, string>[] { }, false, new CancellationToken(), null, null);

            //Assert
            Assert.That(response, Is.EqualTo(string.Empty));
        }
    }
}