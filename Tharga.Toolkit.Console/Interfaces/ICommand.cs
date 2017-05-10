using System;
using System.Collections.Generic;
using Tharga.Toolkit.Console.Entities;

namespace Tharga.Toolkit.Console.Interfaces
{
    public interface ICommand
    {
        event EventHandler<WriteEventArgs> WriteEvent;
        string Name { get; }
        IEnumerable<string> Names { get; }
        string Description { get; }
        bool CanExecute(out string reasonMessage);
        IEnumerable<HelpLine> HelpText { get; }
        bool IsHidden { get; } //TODO: Change to IsVisible
        void Invoke(params string[] input);
    }
}