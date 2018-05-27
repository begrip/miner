using Buzzilio.Begrip.Core.Factories;
using Buzzilio.Begrip.Core.Helpers;
using Buzzilio.Begrip.Core.Messages;
using Buzzilio.Begrip.Core.Models;
using Buzzilio.Begrip.Core.Repository.Helpers;
using Buzzilio.Begrip.Infrastructure.Mvvm.Base;
using Buzzilio.Begrip.Infrastructure.Mvvm.Commands;
using Buzzilio.Begrip.Infrastructure.Mvvm.Enumerations;
using Buzzilio.Begrip.Infrastructure.Mvvm.Interfaces;
using Buzzilio.Begrip.Infrastructure.Providers;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Buzzilio.Begrip.Infrastructure.Enumerations.Enums;
using static Buzzilio.Begrip.Resources.Enumerations.Enums;

namespace Buzzilio.Begrip.Core.ViewModels.ControlViewModels
{
    public class AboutDialogViewModel : ViewModelBase<AboutDialogViewModelMessage>, IViewModel
    {
        ImageIconProvider _ImageIconProvider;

        #region - C-tors -

        public AboutDialogViewModel()
        {
            Setup();
        }

        #endregion -C-tor -

        #region - Properties -


        public RelayCommand OpenUrlCommand { get; set; }
        public RelayCommand CloseAboutDialogCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ObservableCollection<Library> _LibraryCollection;
        public ObservableCollection<Library> LibraryCollection
        {
            get { return _LibraryCollection; }
            set { SetProperty(ref _LibraryCollection, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        string _AppVersion;
        public string AppVersion
        {
            get { return _AppVersion; }
            set { SetProperty(ref _AppVersion, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AppIcon
        {
            get
            {
                return _ImageIconProvider.GetResource(nameof(ImageIcons.PICK_48_ICO));
            }
        }

        #endregion - Properties -

        public void FillCollections()
        {
            AppVersion = $"{InstanceStore.ApplicationVersion.SoftwareVersion} ({InstanceStore.ApplicationVersion.ReleaseName})";
            _ImageIconProvider = new ImageIconProvider();
            LibraryCollection = new ObservableCollection<Library>(LibraryRepositoryHelper.FillLibraryCollection());
        }

        public void RegisterCommands()
        {
            OpenUrlCommand = new RelayCommand(OpenUrl);
            CloseAboutDialogCommand = new RelayCommand(CloseAboutDialog);
        }

        public void Setup()
        {
            RegisterCommands();
            SetViewData();
            FillCollections();
        }

        public void SetupView()
        {
            throw new NotImplementedException();
        }

        public void SetViewData()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        public void OpenUrl(object parameter)
        {
            var url = parameter as string;
            Process.Start(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void CloseAboutDialog(object obj)
        {
            DialogHost.CloseDialogCommand.Execute(null, obj as UserControl);
        }
    }
}
