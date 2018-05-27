using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Buzzilio.Begrip.Miner.Helpers
{
    public class CliHelper : IDisposable
    {
        delegate Boolean ConsoleCtrlDelegate(CtrlTypes type);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate handler, bool add);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GenerateConsoleCtrlEvent(CtrlTypes dwCtrlEvent, uint dwProcessGroupId);

        enum CtrlTypes : uint
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        public event DataReceivedEventHandler _outputDataReceived;
        public event DataReceivedEventHandler _errorDataReceived;

        Process _process;
        int ProcessId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return _process != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual int Open(string path, string[] args = null)
        {
            if (File.Exists(path))
            {
                if (Closed)
                {
                    var password = new SecureString();
                    password.AppendChar('c');

                    _process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(path)
                    {
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        Password = password,
                        Arguments = String.Join(" ", args),
                        WorkingDirectory = Path.GetDirectoryName(path),
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    };

                    _process.EnableRaisingEvents = true;
                    if (psi.RedirectStandardOutput) _process.OutputDataReceived += Cli_OutputDataReceived;
                    if (psi.RedirectStandardError) _process.ErrorDataReceived += Cli_ErrorDataReceived;
                    _process.StartInfo = psi;
                    _process.Start();
                    if (psi.RedirectStandardOutput) _process.BeginOutputReadLine();
                    if (psi.RedirectStandardError) _process.BeginErrorReadLine();
                    ProcessId = _process.Id;
                }
                else
                {
                    throw new Exception("CliHelper::Open(...): file is not closed!");
                }
            }
            else
            {
                throw new FileNotFoundException($"CliHelper::Open(...): file {path} does not exist.");
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cli_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                _outputDataReceived?.Invoke(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Cli_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                _errorDataReceived?.Invoke(sender, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void WriteInput(string input)
        {
            if (_process != null && _process.StartInfo != null && _process.StartInfo.RedirectStandardInput)
            {
                _process.StandardInput.WriteLine(input);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void ForceClose()
        {
            if (_process != null)
            {
                if (!_process.HasExited)
                {
                    _process.Kill();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proc"></param>
        public void StopProcess(Process proc)
        {
            if (AttachConsole((uint)proc.Id))
            {
                SetConsoleCtrlHandler(null, true);
                GenerateConsoleCtrlEvent(CtrlTypes.CTRL_C_EVENT, 0);
                proc.WaitForExit(2000);
                FreeConsole();
                SetConsoleCtrlHandler(null, false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Close()
        {
            if (_process != null)
            {
                CleanUp();
                StopProcess(_process);
                if (!_process.HasExited)
                {
                    ForceClose();
                    _process = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void CleanUp()
        {
            _process.CancelOutputRead();
            _process.CancelErrorRead();

            _process.OutputDataReceived -= Cli_OutputDataReceived;
            _process.ErrorDataReceived -= Cli_ErrorDataReceived;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Closed
        {
            get
            {
                try
                {
                    if (_process != null)
                    {
                        return _process.HasExited;
                    }
                    else return true;
                }
                catch
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_process != null)
            {
                _process.Dispose();
            }
        }
    }
}
