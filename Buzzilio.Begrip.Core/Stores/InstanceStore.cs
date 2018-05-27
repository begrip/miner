using Buzzilio.Begrip.Miner.Helpers;
using Buzzilio.Begrip.Miner.Interfaces;
using Buzzilio.Begrip.Core.Models.Mappings;
using Buzzilio.Begrip.Core.Scheduler;
using Buzzilio.Begrip.Core.Settings;
using Buzzilio.Begrip.Core.Store.Interfaces;
using Buzzilio.Begrip.Core.ViewModels.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Collections.Generic;
using static Buzzilio.Begrip.Core.Enumerations.Enums;
using Buzzilio.Begrip.Core.Stores;
using Buzzilio.Begrip.Utilities.Logging;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Helpers;

namespace Buzzilio.Begrip.Core.Factories
{
    public class InstanceStore : IInstanceStore
    {
        /// <summary>
        /// 
        /// </summary>
        static string _dBConnectionString = string.Empty;
        public static string DbConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_dBConnectionString))
                {
                    var connectionStringConfigPartName = RuntimeSettings._dbConnectionStringConfigPartName;
                    _dBConnectionString = ConfigurationManager.ConnectionStrings[connectionStringConfigPartName].ConnectionString;
                    _dBConnectionString = _dBConnectionString.Replace("%APPDATA%", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
                }
                return _dBConnectionString;
            }
        }

        /// <summary>
        /// Instance of DB context wrapper.
        /// </summary>
        static BgDataContext _bzDataContextInstance;
        public static BgDataContext BgDataContextInstance
        {
            get
            {
                if (_bzDataContextInstance == null)
                {
                    _bzDataContextInstance = new BgDataContext(DbConnectionString);
                }
                return _bzDataContextInstance;
            }
        }

        /// <summary>
        /// Instance of logger
        /// </summary>
        public static BzLogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        static ViewFactory _viewFactory;
        public static ViewFactory ViewFactory
        {
            get
            {
                if (_viewFactory == null)
                {
                    _viewFactory = new ViewFactory();
                }
                return _viewFactory;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static ObservableCollection<ITabViewModel> _tabViewModels;
        public static ObservableCollection<ITabViewModel> TabViewModels
        {
            get
            {
                return _tabViewModels;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static TabStore _tabStore;
        public static TabStore TabStore
        {
            get
            {
                if (_tabStore == null)
                {
                    if (_tabViewModels == null)
                    {
                        _tabViewModels = new ObservableCollection<ITabViewModel>();
                        _tabStore = new TabStore(ref _tabViewModels);
                    }
                }
                return _tabStore;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static BgJobScheduler _minerJobScheduler;
        public static BgJobScheduler MinerJobScheduler
        {
            get
            {
                if (_minerJobScheduler == null)
                {
                    _minerJobScheduler = new BgJobScheduler();
                }
                return _minerJobScheduler;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static ApplicationVersion _applicationVersion;
        public static ApplicationVersion ApplicationVersion
        {
            get
            {
                if (_applicationVersion == null)
                {
                    _applicationVersion = ApplicationVersionRepositoryHelper.GetLatestApplicationVersion();
                }
                return _applicationVersion;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static CcMinerCli _supportCcMinerCli;
        public static CcMinerCli SupportCcMinerCli
        {
            get
            {
                if (_supportCcMinerCli == null)
                {
                    _supportCcMinerCli = new CcMinerCli(nameof(Workers.SUPPORT_WORKER));
                }
                return _supportCcMinerCli;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static IConfig _supportCcMinerConfig;
        public static IConfig SupportCcMinerConfig
        {
            get
            {
                if (_supportCcMinerConfig == null)
                {
                    throw new InvalidOperationException("SupportCcMinerConfig not initialized");
                }
                return _supportCcMinerConfig;
            }
            set { _supportCcMinerConfig = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        static WorkerStore _workerStore;
        public static WorkerStore WorkerStore
        {
            get
            {
                if (_workerStore == null)
                {
                    throw new InvalidOperationException("WorkerStore not initialized");
                }
                return _workerStore;
            }
            set { _workerStore = value; }
        }
    }
}
