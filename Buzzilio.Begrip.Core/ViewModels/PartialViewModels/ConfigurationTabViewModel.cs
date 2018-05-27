using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Core.ViewModels.Interfaces;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Core.Messages;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Models.Local;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Core.Repository.Helpers;
using System.Diagnostics;
using System.Windows.Threading;
using System;
using Buzzilio.Begrip.Miner.Helpers;
using System.Collections.ObjectModel;
using Buzzilio.Begrip.Miner.Cli;
using System.Windows;
using Buzzilio.Begrip.Core.Factories;
using static Buzzilio.Begrip.Core.Enumerations.Enums;
using Buzzilio.Begrip.Core.Jobs;
using Buzzilio.Begrip.Utilities.Generators;
using Buzzilio.Begrip.Utilities.Helpers;
using System.IO;

namespace Buzzilio.Begrip.Core.ViewModels.PartialViewModels
{
    public class ConfigurationTabViewModel : ViewModelBase<ConfigurationTabViewModelMessage>, IViewModel, ITabViewModel
    {
        /// <summary>
        /// Private fields
        /// </summary>
        DispatcherTimer _dt;
        Stopwatch _sw;
        string _currentTime;
        CcMinerCli _ccMinerCli;

        #region - C-tor -
        public ConfigurationTabViewModel()
        {
            Setup();
        }

        #endregion - C-tor - 

        #region - Commands -

        public RelayCommand SaveConfigurationCommand { get; set; }
        public RelayCommand LoadDefaultConfigurationCommand { get; set; }
        public RelayCommand CancelConfigurationChangesCommand { get; set; }
        public RelayCommand ToggleMinersCommand { get; set; }
        public RelayCommand ResetTimerCommand { get; set; }

        #endregion - Commands -

        #region - Properties -

        /// <summary>
        /// 
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> ConsoleOutput { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MinerWorkerId { get { return _ccMinerCli?.WorkerId; } }

        /// <summary>
        /// 
        /// </summary>
        public bool WorkerIsRunning { get { return _sw.IsRunning; } }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SupportJobsAreScheduled
        {
            get
            {
                if (InstanceStore.MinerJobScheduler.CheckStarted(nameof(MinerJobs.StartSupportMiner)))
                {
                    if (InstanceStore.MinerJobScheduler.CheckStarted(nameof(MinerJobs.StopSupportMiner)))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Support job state mismatch");
                    }
                }
                else
                {
                    if (InstanceStore.MinerJobScheduler.CheckStarted(nameof(MinerJobs.StopSupportMiner)))
                    {
                        throw new Exception("Support job state mismatch");
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        Configuration _selectedConfiguration;
        public Configuration SelectedConfiguration
        {
            get { return _selectedConfiguration; }
            set { SetProperty(ref _selectedConfiguration, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        Configuration _defaultConfiguration;
        public Configuration DefaultConfiguration
        {
            get { return _defaultConfiguration; }
            set { SetProperty(ref _defaultConfiguration, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _playIcon;
        public string PlayIcon
        {
            get { return _playIcon; }
            set { SetProperty(ref _playIcon, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _startTooltip;
        public string StartTooltip
        {
            get { return _startTooltip; }
            set { SetProperty(ref _startTooltip, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        bool _uiIsUnlocked;
        public bool UIIsUnlocked
        {
            get { return _uiIsUnlocked; }
            set { SetProperty(ref _uiIsUnlocked, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        MinerOutput _minerOutput;
        public MinerOutput MinerOutput
        {
            get { return _minerOutput; }
            set { SetProperty(ref _minerOutput, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _runningTime;
        public string RunningTime
        {
            get { return _runningTime; }
            set { SetProperty(ref _runningTime, value); }
        }

        #endregion - Properties -

        /// <summary>
        /// 
        /// </summary>
        void Initialize()
        {
            _dt = new DispatcherTimer();
            _sw = new Stopwatch();
            _currentTime = string.Empty;
            _ccMinerCli = new CcMinerCli(IdGenerator.GetShortId());
        }

        /// <summary>
        /// 
        /// </summary>
        public void FillCollections()
        {
            ConsoleOutput = new ObservableCollection<string>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Setup()
        {
            Initialize();
            RegisterCommands();
            FillCollections();
            SetViewData();
            SetupView();
        }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterCommands()
        {
            SaveConfigurationCommand = new RelayCommand(SaveConfiguration);
            CancelConfigurationChangesCommand = new RelayCommand(CancelConfigurationChanges);
            LoadDefaultConfigurationCommand = new RelayCommand(LoadDefaultConfiguration);
            ToggleMinersCommand = new RelayCommand(ToggleMiners);
            ResetTimerCommand = new RelayCommand(ResetTimer);

            _ccMinerCli._outputDataReceived += Cli_OutputDataReceived;
            _ccMinerCli._errorDataReceived += Cli_ErrorDataReceived;
            _ccMinerCli.ReportAvailable += Cli_ReportAvailable;
        }

        #region - CLI Reporters -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cli_ReportAvailable(object sender, ReportEventArgs e)
        {
            MinerOutput.Update(e.Report);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cli_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                ConsoleOutput.Insert(0, e.Data);
                BalanceConsoleOutputBuffer(1001);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cli_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                ConsoleOutput.Insert(0, e.Data);
                BalanceConsoleOutputBuffer(1001);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        private void BalanceConsoleOutputBuffer(int bufferSize)
        {
            if (ConsoleOutput.Count > bufferSize)
            {
                ConsoleOutput.RemoveAt(ConsoleOutput.Count - 1);
            }
        }

        #endregion - CLI Reporters -

        /// <summary>
        /// 
        /// </summary>
        public void SetupView()
        {
            SendMessage(new MainViewModelMessage() { Purpose = MessagePurpose.SET_SELECTED_TAB, Payload = this });
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetViewData()
        {
            UIIsUnlocked = true;
            PlayIcon = "Play";
            StartTooltip = "Start miner";
            _dt.Tick += new EventHandler(UpdateTimerLabel);
            _dt.Interval = new TimeSpan(0, 0, 0, 0, 1);
            RunningTime = "00:00:00";
            MinerOutput = new MinerOutput
            {
                Block = 0,
                TotalHashrate = 0,
                TotalShares = 0,
                StaleShares = 0,
                StratumDifficulty = 0,
                BlockDifficulty = 0
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void SaveConfiguration(object ob)
        {
            var changes = SelectedConfiguration.GetChanges();
            ConfigurationRepositoryHelper.UpdateConfiguration(SelectedConfiguration, changes);
            RepositoryHelper.CommitPendingDbChanges();
            SelectedConfiguration.CacheObject();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = $"Saved configuration for {SelectedConfiguration.Crypto.CryptoName} "
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void LoadDefaultConfiguration(object ob)
        {
            SelectedConfiguration.AlgorithmId = DefaultConfiguration.AlgorithmId;
            SelectedConfiguration.GPUStats = DefaultConfiguration.GPUStats;
            SelectedConfiguration.Username = DefaultConfiguration.Username;
            SelectedConfiguration.Password = DefaultConfiguration.Password;
            SelectedConfiguration.Wallet = DefaultConfiguration.Wallet;
            SelectedConfiguration.Intensity = DefaultConfiguration.Intensity;
            SelectedConfiguration.PoolURL = DefaultConfiguration.PoolURL;
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = string.Format("Loaded default configuration for {0} ", SelectedConfiguration.Crypto.CryptoName)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ob"></param>
        public void CancelConfigurationChanges(object ob)
        {
            SelectedConfiguration.RestoreCachedObject();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = "Discarded changes"
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTimerLabel(object sender, EventArgs e)
        {
            if (WorkerIsRunning)
            {
                TimeSpan ts = _sw.Elapsed;
                _currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Hours, ts.Minutes, ts.Seconds);
                RunningTime = _currentTime;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void ToggleMiners(object obj)
        {
            if (SelectedConfiguration.HasErrors) { return; }

            if (WorkerIsRunning)
            {
                try
                {
                    Stop();
                }
                catch (Exception ex)
                {
                    InstanceStore.Logger.LogException("ConfigurationTabViewModel::ToggleMiners: Stop()", ex);
                }

                SendMessage(new MainViewModelMessage()
                {
                    Purpose = MessagePurpose.OPEN_SNACKBAR,
                    Payload = $"Stopped Mining '{SelectedConfiguration.Crypto.CryptoName}' "
                });
            }
            else
            {
                try
                {
                    bool isStarted = Start();
                    if (isStarted)
                    {
                        SendMessage(new MainViewModelMessage()
                        {
                            Purpose = MessagePurpose.OPEN_SNACKBAR,
                            Payload = $"Started Mining '{SelectedConfiguration.Crypto.CryptoName}' "
                        });
                    }
                }
                catch (Exception ex)
                {
                    InstanceStore.Logger.LogException("ConfigurationTabViewModel::ToggleMiners: Start()", ex);

                    if (ex is FileNotFoundException)
                    {
                        SendMessage(new MainViewModelMessage()
                        {
                            Purpose = MessagePurpose.OPEN_SNACKBAR,
                            Payload = "Missing 'ccminer-x64.exe', try configuring antivirus"
                        });
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessStopRequestWithConfirm()
        {
            if (WorkerIsRunning)
            {
                try
                {
                    Stop();
                }
                catch (Exception ex)
                {
                    InstanceStore.Logger.LogException("ConfigurationTabViewModel::ProcessStopRequestWithConfirm: Stop()", ex);
                }
            }

            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.STOP_ALL_WORKERS_CONFIRM,
            });
        }

        #region - Start/Stop/Toggle Actions -

        /// <summary>
        /// 
        /// </summary>
        public bool Start()
        {
            var isRunning = false;
            var userSettings = UserSettingsRepositoryHelper.GetUserSettings();
            var shareTime = int.Parse(userSettings.ShareTime);
            var maxConcurrentMiners = int.Parse(userSettings.MaxConcurrentMiners);

            if (InstanceStore.WorkerStore.HasCapacity(maxConcurrentMiners))
            {
                InstanceStore.Logger.LogInfo($"Starting '{SelectedConfiguration.Crypto.CryptoName}' miner...");
                ConsoleOutput.Add($"Starting '{SelectedConfiguration.Crypto.CryptoName}' miner...");
                StartMiner(maxConcurrentMiners);
                UIIsUnlocked = false;
                PlayIcon = "Stop";
                StartTooltip = "Stop miner";

                if (SupportJobsAreScheduled == false)
                {
                    if (shareTime == 60)
                    {
                        InstanceStore.Logger.LogInfo($"Starting '{DefaultConfiguration.Crypto.CryptoName}' support miner...");
                        StartSupportMiner();
                    }
                    else if (shareTime > 0)
                    {
                        InstanceStore.Logger.LogInfo($"Scheduling '{DefaultConfiguration.Crypto.CryptoName}' support miner...");
                        ScheduleSupportMinerAndStart(shareTime);
                    }
                }

                _sw.Start();
                _dt.Start();

                isRunning = true;
            }
            else
            {
                SendMessage(new MainViewModelMessage()
                {
                    Purpose = MessagePurpose.OPEN_SNACKBAR,
                    Payload = string.Format("Limit of {0} running miners reached", maxConcurrentMiners)
                });
            }
            return isRunning;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            InstanceStore.Logger.LogInfo($"Stopping '{SelectedConfiguration.Crypto.CryptoName}' miner...");
            ConsoleOutput.Add($"Stopping '{SelectedConfiguration.Crypto.CryptoName}' miner...");
            StopMiner();
            UIIsUnlocked = true;
            PlayIcon = "Play";
            StartTooltip = "Start miner";

            if (InstanceStore.WorkerStore.HasActiveNonSupportWorkers() == false)
            {
                if (SupportJobsAreScheduled == true)
                {
                    UnscheduleSupportMinerAndStop();
                }
                else
                {
                    StopSupportMiner();
                }
            }

            _sw.Stop();
            _dt.Stop();
        }

        #endregion - Start/Stop/Toggle Actions -

        #region - Start/Stop Sub-Actions -

        /// <summary>
        /// 
        /// </summary>
        public void ScheduleSupportJobs(int shareTime)
        {
            var schedulerInterval = TimeHelper.MinutesToSeconds(60);
            var shareTimeInSeconds = TimeHelper.MinutesToSeconds(shareTime);
            InstanceStore.MinerJobScheduler.ScheduleJob<StartSupportMinerJob>(nameof(MinerJobs.StartSupportMiner), schedulerInterval);
            InstanceStore.MinerJobScheduler.ScheduleJob<StopSupportMinerJob>(nameof(MinerJobs.StopSupportMiner), schedulerInterval, shareTimeInSeconds);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnscheduleSupportJobs()
        {
            InstanceStore.MinerJobScheduler.UnscheduleJob(nameof(MinerJobs.StartSupportMiner));
            InstanceStore.MinerJobScheduler.UnscheduleJob(nameof(MinerJobs.StopSupportMiner));
        }

        /// <summary>
        /// 
        /// </summary>
        void LoadWorker()
        {
            InstanceStore.WorkerStore.AddWorker(_ccMinerCli.WorkerId);
        }

        /// <summary>
        /// 
        /// </summary>
        void LoadSupportWorker()
        {
            InstanceStore.WorkerStore.AddSupportWorker();
        }

        /// <summary>
        /// 
        /// </summary>
        void UnloadWorker()
        {
            InstanceStore.WorkerStore.RemoveWorker(_ccMinerCli.WorkerId);
        }

        /// <summary>
        /// 
        /// </summary>
        void UnloadSupportWorker()
        {
            InstanceStore.WorkerStore.RemoveSupportWorker();
        }

        /// <summary>
        /// 
        /// </summary>
        void StartMiner(int maxConcurrentMiners)
        {
            _ccMinerCli.Run(SelectedConfiguration, false);
            InstanceStore.Logger.LogInfo($"Started '{SelectedConfiguration.Crypto.CryptoName}' miner");
            LoadWorker();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.STATUS_UPDATE_MINER_RUNNING,
                Payload = SelectedConfiguration.Crypto.CryptoName
            });
        }

        /// <summary>
        /// 
        /// </summary>
        void StartSupportMiner()
        {
            LoadSupportWorker();
            InstanceStore.SupportCcMinerConfig = DefaultConfiguration;

            try
            {
                InstanceStore.SupportCcMinerCli.Run(InstanceStore.SupportCcMinerConfig);
                InstanceStore.Logger.LogInfo("Started support miner");
            }
            catch (Exception ex)
            {
                InstanceStore.Logger.LogException("StartSupportMinerJob::Execute: InstanceStore.SupportCcMinerCli.Run((...)", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shareTime"></param>
        void ScheduleSupportMinerAndStart(int shareTime)
        {
            LoadSupportWorker();
            InstanceStore.SupportCcMinerConfig = DefaultConfiguration;
            ScheduleSupportJobs(shareTime);
        }

        /// <summary>
        /// 
        /// </summary>
        void StopMiner()
        {
            _ccMinerCli.Close();
            InstanceStore.Logger.LogInfo($"Stopped '{SelectedConfiguration.Crypto.CryptoName}' miner");
            UnloadWorker();
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.STATUS_UPDATE_MINER_STOPPED,
                Payload = SelectedConfiguration.Crypto.CryptoName
            });
        }

        /// <summary>
        /// 
        /// </summary>
        void StopSupportMiner()
        {
            try
            {
                InstanceStore.SupportCcMinerCli.Close();
                InstanceStore.Logger.LogInfo("Stopped support miner");
            }
            catch (Exception ex)
            {
                InstanceStore.Logger.LogException("ConfigurationTabViewModel::StopSupportMiner: InstanceStore.SupportCcMinerCli.Close(...)", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        void UnscheduleSupportMinerAndStop()
        {
            InstanceStore.Logger.LogInfo("Unscheduling support miner");
            UnscheduleSupportJobs();
            InstanceStore.SupportCcMinerCli.Close();
            InstanceStore.Logger.LogInfo("Stopped support miner");
            UnloadSupportWorker();
        }

        #endregion - Start/Stop Sub-Actions -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void ResetTimer(object obj)
        {
            _sw.Reset();
            RunningTime = "00:00:00";
            SendMessage(new MainViewModelMessage()
            {
                Purpose = MessagePurpose.OPEN_SNACKBAR,
                Payload = "Timer was reset"
            });
        }

        #region - ActionHandler -

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public override void ActionHandler(IMediatorMessage message)
        {
            var action = message as ConfigurationTabViewModelMessage;
            switch (action.Purpose)
            {
                case MessagePurpose.STOP_ALL_WORKERS:
                    ProcessStopRequestWithConfirm();
                    break;
                default:
                    break;
            }

        }

        #endregion - ActionHanlder -
    }
}
