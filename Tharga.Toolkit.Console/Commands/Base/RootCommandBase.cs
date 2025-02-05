using System;
using System.Collections.Generic;
using System.Linq;
using Tharga.Toolkit.Console.Commands.ScreenCommands;
using Tharga.Toolkit.Console.Entities;
using Tharga.Toolkit.Console.Helpers;
using Tharga.Toolkit.Console.Interfaces;

namespace Tharga.Toolkit.Console.Commands.Base
{
    public abstract class RootCommandBase : ContainerCommandBase, IRootCommand
    {
        public IConsole Console { get; }
        internal CommandEngine CommandEngine;
        internal ICommandResolver CommandResolver;

        public event EventHandler<EventArgs> RequestCloseEvent;
        public event EventHandler<ExceptionOccuredEventArgs> ExceptionOccuredEvent;

        protected RootCommandBase(IConsole console)
            : base("root")
        {
            if (console == null) throw new ArgumentNullException(nameof(console), "No console provided.");

            Console = console;

            RegisterCommand(new ExitCommand(() => { RequestCloseEvent?.Invoke(this, new EventArgs()); }));
            RegisterCommand(new ClearCommand());
            RegisterCommand(new ScreenCommand(console));
            RegisterCommand(new CmdCommand());
            RegisterCommand(new PoshCommand());
            RegisterCommand(new ExecuteProcessCommand());
            RegisterCommand(new ExecuteCommand(this));

            WriteEvent += OnOutputEvent;

            console.Attach(this);
        }

        protected RootCommandBase(IConsole console, ICommandResolver commandResolver)
            : this(console)
        {
            CommandResolver = commandResolver;
        }

        public new void RegisterCommand<T>()
        {
            SubCommandTypes.Add(typeof(T));
        }

        public new void RegisterCommand(ICommand command)
        {
            base.RegisterCommand(command);
        }
        
        protected virtual void OnExceptionOccuredEvent(ExceptionOccuredEventArgs e)
        {
            var handler = ExceptionOccuredEvent;
            handler?.Invoke(this, e);
        }

        public override IEnumerable<HelpLine> HelpText
        {
            get { yield return new HelpLine("Root command."); }
        }

        public string QueryInput()
        {
            var tabTree = Build(SubCommands, null);
            return QueryParam(Constants.Prompt, null, tabTree, false, false);
        }

        private IEnumerable<CommandTreeNode<string>> Build(IEnumerable<ICommand> commands, string lead)
        {
            foreach (var command in commands)
            {
                var cc = command as ContainerCommandBase;
                var ac = command as ActionCommandBase;

                CommandTreeNode<string>[] subTree = null;

                if (cc != null)
                {
                    var l = (lead != null ? (lead + " ") : "") + cc.Name;
                    subTree = Build(cc.SubCommands, l).ToArray();
                }
                else if (ac != null)
                {
                    var l = (lead != null ? (lead + " ") : "") + ac.Name;
                    var sub = ac.GetOptionList().ToArray();
                    subTree = Build(sub, l).ToArray();
                }

                yield return new CommandTreeNode<string>(lead != null ? $"{lead} {command.Name}" : command.Name, command.Name, subTree);
            }
        }

        private IEnumerable<CommandTreeNode<string>> Build(IEnumerable<string>[] commands, string lead)
        {
            foreach (var command in commands[0])
            {
                yield return new CommandTreeNode<string>(lead != null ? $"{lead} {command}" : command, command, null);
            }
        }

        public bool Execute(string entry)
        {
            try
            {
                string subCommand;
                var command = GetSubCommand(entry, out subCommand);
                if (command != null)
                {
                    var bc = command as CommandBase;
                    var ac = command as ActionCommandBase;
                    var cc = command as ContainerCommandBase;

                    if (cc == null)
                    {
                        string reason;
                        if (!command.CanExecute(out reason))
                        {
                            OutputWarning(GetCanExecuteFailMessage(reason));
                            return false;
                        }
                    }

                    var param = subCommand.ToInput().ToArray();

                    if (ac != null)
                    {
                        ac.InvokeEx(param);
                    }
                    else if (bc != null)
                    {
                        bc.InvokeEx(param);
                    }
                    else
                    {
                        command.Invoke(param);
                    }

                    return true;
                }
                else
                {
                    OutputWarning($"Unknown command '{entry}'.");
                }
            }
            catch (CommandFailedException)
            {
                return false;
            }
            catch (CommandEscapeException)
            {
                return false;
            }
            catch (SystemException exception)
            {
                OnExceptionOccuredEvent(new ExceptionOccuredEventArgs(exception));
                OutputError(exception, false);
            }
            catch (AggregateException exception)
            {
                if (exception.InnerException is CommandEscapeException)
                    return false;

                OnExceptionOccuredEvent(new ExceptionOccuredEventArgs(exception));
                OutputError(exception, false);
            }
            catch (Exception exception)
            {
                OnExceptionOccuredEvent(new ExceptionOccuredEventArgs(exception));
                OutputError(exception, false);
                OutputWarning("Terminating application...");
                throw;
            }

            return false;
        }

        protected internal void Initiate(CommandEngine commandEngine)
        {
            CommandEngine = commandEngine;
            Attach(this);
        }
    }
}