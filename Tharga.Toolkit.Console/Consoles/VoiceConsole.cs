using System;
using System.Linq;
using System.Speech.Recognition;
using Tharga.Toolkit.Console.Interfaces;

namespace Tharga.Toolkit.Console.Consoles
{
    public class VoiceConsole : ClientConsole
    {
        private readonly SpeechRecognitionEngine _mainSpeechRecognitionEngine = new SpeechRecognitionEngine();

        public VoiceConsole(IConsoleConfiguration consoleConfiguration = null)
            :base(consoleConfiguration)
        {
            _mainSpeechRecognitionEngine.AudioLevelUpdated += _mainSpeechRecognitionEngine_AudioLevelUpdated;
            _mainSpeechRecognitionEngine.AudioSignalProblemOccurred += _mainSpeechRecognitionEngine_AudioSignalProblemOccurred;
            _mainSpeechRecognitionEngine.AudioStateChanged += _mainSpeechRecognitionEngine_AudioStateChanged;
            _mainSpeechRecognitionEngine.EmulateRecognizeCompleted += _mainSpeechRecognitionEngine_EmulateRecognizeCompleted;
            _mainSpeechRecognitionEngine.LoadGrammarCompleted += _mainSpeechRecognitionEngine_LoadGrammarCompleted;
            _mainSpeechRecognitionEngine.RecognizeCompleted += _mainSpeechRecognitionEngine_RecognizeCompleted;
            _mainSpeechRecognitionEngine.RecognizerUpdateReached += _mainSpeechRecognitionEngine_RecognizerUpdateReached;
            _mainSpeechRecognitionEngine.SpeechDetected += _mainSpeechRecognitionEngine_SpeechDetected;
            _mainSpeechRecognitionEngine.SpeechHypothesized += _mainSpeechRecognitionEngine_SpeechHypothesized;
            _mainSpeechRecognitionEngine.SpeechRecognized += _mainSpeechRecognitionEngine_SpeechRecognized;
            _mainSpeechRecognitionEngine.SpeechRecognitionRejected += _mainSpeechRecognitionEngine_SpeechRecognitionRejected;
        }

        private void _mainSpeechRecognitionEngine_AudioSignalProblemOccurred(object sender, AudioSignalProblemOccurredEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_AudioSignalProblemOccurred");
        }

        private void _mainSpeechRecognitionEngine_AudioStateChanged(object sender, AudioStateChangedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_AudioStateChanged");
        }

        private void _mainSpeechRecognitionEngine_EmulateRecognizeCompleted(object sender, EmulateRecognizeCompletedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_EmulateRecognizeCompleted");
        }

        private void _mainSpeechRecognitionEngine_RecognizerUpdateReached(object sender, RecognizerUpdateReachedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_RecognizerUpdateReached");
        }

        private void _mainSpeechRecognitionEngine_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_RecognizeCompleted");
        }

        private void _mainSpeechRecognitionEngine_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_SpeechHypothesized");
        }

        private void _mainSpeechRecognitionEngine_AudioLevelUpdated(object sender, AudioLevelUpdatedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_AudioLevelUpdated");
        }

        private void _mainSpeechRecognitionEngine_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_LoadGrammarCompleted");
        }

        private void _mainSpeechRecognitionEngine_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_SpeechDetected");
        }

        private void _mainSpeechRecognitionEngine_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_SpeechRecognitionRejected");
        }

        private void _mainSpeechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            OutputDefault("_mainSpeechRecognitionEngine_SpeechRecognized");
            //        throw new NotImplementedException();
            //        //if (_reading)
            //        //{
            //        //    _inputMethod = InputMethod.Voice;
            //        //    _input = e.Result.Text;
            //        //    //System.Console.Write(_input);
            //        //    ConsoleWriter.Write(_input);
            //        //    _autoResetEvent.Set();
            //        //}
        }

        #region User32

        //[DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        //private static extern bool PostMessage(IntPtr hwnd, uint msg, int wparam, int lparam);

        #endregion

        //    private const int Retrun = 0x0D;
        //    private const int Keydown = 0x100;

        //    private enum InputMethod
        //    {
        //        Unknown,
        //        Keyboard,
        //        Voice
        //    }

        //    private readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(false);
        //    private readonly SpeechRecognitionEngine _subSpeechRecognitionEngine = new SpeechRecognitionEngine();
        //    private string _input;
        //    private InputMethod _inputMethod;
        //    private bool _reading;
        //    private ConsoleKeyInfo _keyInput;

        //    public VoiceConsole()
        //        : base(new ConsoleManager(System.Console.Out, System.Console.In))
        //    {
        //    }

        public override void Attach(IRootCommand rootCommand)
            {
                var commands = rootCommand.SubCommands.Select(x => x.Name).ToArray();

                var choices = new Choices();
                choices.Add(commands);
                var gr = new Grammar(new GrammarBuilder(choices));
                _mainSpeechRecognitionEngine.RequestRecognizerUpdate();
                _mainSpeechRecognitionEngine.LoadGrammar(gr);

                    try
                    {
                        _mainSpeechRecognitionEngine.SetInputToDefaultAudioDevice();
                    }
                    catch (Exception exception)
                    {
            //            try
            //            {
            //                throw new InvalidOperationException("Unable to set default input audio device", exception);
            //            }
            //            catch (Exception e)
            //            {
            //                OutputError(e);
            //            }
            //            //base.WriteLine($"Unable to set default input audio device. Error: {exception.Message}", OutputLevel.Error, null, null);
            //            return;
                    }

            //        var subChoices = new Choices();
            //        subChoices.Add(new[] { "tab", "enter" });
            //        var subGr = new Grammar(new GrammarBuilder(subChoices));
            //        _subSpeechRecognitionEngine.RequestRecognizerUpdate();
            //        _subSpeechRecognitionEngine.LoadGrammar(subGr);
            //        _subSpeechRecognitionEngine.SpeechRecognized += _subSpeechRecognitionEngine_SpeechRecognized;
            //        _subSpeechRecognitionEngine.SetInputToDefaultAudioDevice();
        }

        //    //protected override void WriteLineEx(string value, OutputLevel outputLevel)
        //    //{
        //    //    base.WriteLineEx(value, outputLevel);

        //    //    //var builder = new PromptBuilder();
        //    //    //builder.StartSentence();
        //    //    //builder.AppendText(value);
        //    //    //builder.EndSentence();

        //    //    //using (var synthesizer = new SpeechSynthesizer())
        //    //    //{
        //    //    //    //synthesizer.SelectVoice("Microsoft David Desktop");
        //    //    //    //synthesizer.SelectVoice("Microsoft Hazel Desktop");
        //    //    //    synthesizer.SelectVoice("Microsoft Zira Desktop");
        //    //    //    synthesizer.Speak(builder);
        //    //    //}
        //    //}

        //    private void _subSpeechRecognitionEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //    {
        //        if (_reading)
        //        {
        //            _inputMethod = InputMethod.Voice;
        //            _input = e.Result.Text;

        //            var hwnd = Process.GetCurrentProcess().MainWindowHandle;

        //            switch (_input)
        //            {
        //                case "tab":
        //                    PostMessage(hwnd, Keydown, 9, 0);
        //                    break;
        //                case "enter":
        //                    PostMessage(hwnd, Keydown, 13, 0);
        //                    break;
        //            }
        //        }
        //    }

        //    //public override string ReadLine()
        //    //{
        //    //    _inputMethod = InputMethod.Unknown;

        //    //    var task = Task.Factory.StartNew(() =>
        //    //    {
        //    //        var s = System.Console.ReadLine();
        //    //        _inputMethod = InputMethod.Keyboard;
        //    //        if (!_reading) return;
        //    //        _input = s;
        //    //        _autoResetEvent.Set();
        //    //    });

        //    //    _mainSpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

        //    //    _reading = true;
        //    //    _autoResetEvent.WaitOne();
        //    //    _reading = false;

        //    //    _mainSpeechRecognitionEngine.RecognizeAsyncCancel();

        //    //    if (_inputMethod == InputMethod.Voice)
        //    //    {
        //    //        var hwnd = Process.GetCurrentProcess().MainWindowHandle;
        //    //        PostMessage(hwnd, Keydown, Retrun, 0);
        //    //    }

        //    //    task.Wait();
        //    //    task.Dispose();

        //    //    return _input;
        //    //}

        //    //public override ConsoleKeyInfo ReadKey()
        //    //{
        //    //    _inputMethod = InputMethod.Unknown;

        //    //    var task = Task.Factory.StartNew(() =>
        //    //    {
        //    //        var s = System.Console.ReadKey();
        //    //        //var s = _consoleReader.ReadKey();
        //    //        _inputMethod = InputMethod.Keyboard;
        //    //        if (!_reading) return;
        //    //        _keyInput = s;
        //    //        _autoResetEvent.Set();
        //    //    });

        //    //    _subSpeechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);

        //    //    _reading = true;
        //    //    _autoResetEvent.WaitOne();
        //    //    _reading = false;

        //    //    _subSpeechRecognitionEngine.RecognizeAsyncCancel();

        //    //    ////if (_inputMethod == InputMethod.Voice)
        //    //    ////{
        //    //    ////    if (_input == "tab")
        //    //    ////        _keyInput = new ConsoleKeyInfo((char)10, ConsoleKey.Tab, false, false, false);

        //    //    ////    var hWnd = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
        //    //    ////    PostMessage(hWnd, WM_KEYDOWN, _keyInput.KeyChar, 0);
        //    //    ////}

        //    //    task.Wait();
        //    //    task.Dispose();

        //    //    //return _keyInput;
        //    //    _reading = false;
        //    //    return _keyInput;
        //    //}
    }
}