using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using bruh.Core;

namespace bruh.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewComand { get; set; }
        public RelayCommand DiscoveryViewCommand { get; set; }
        public RelayCommand KitViewCommand { get; set; }
        public RelayCommand NPCViewCommand { get; set; }
        public RelayCommand CrashViewCommand { get; set; }
        public RelayCommand FunnyViewCommand { get; set; }
        public RelayCommand MiscViewCommand { get; set; }
        public RelayCommand TestViewCommand { get; set; }
        public RelayCommand AddViewCommand { get; set; }
        public RelayCommand SettingsViewCommand { get; set; }

        public HomeViewModel HomeVm { get; set; }
        public DiscoveryViewModel DiscoveryVm { get; set; }
        public KitViewModel KitVm { get; set; }
        public NPCViewModel NPCVm { get; set; }
        public CrashViewModel CrashVm { get; set; }
        public FunnyViewModel FunnyVm { get; set; }
        public MiscViewModel MiscVm { get; set; }
        public TestViewModel TestVm { get; set; }
        public AddViewModel AddVm { get; set; }
        public SettingsViewModel SettingsVm { get; set; }


        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {   
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            DiscoveryVm = new DiscoveryViewModel();
            KitVm = new KitViewModel();
            NPCVm = new NPCViewModel();
            CrashVm = new CrashViewModel();
            FunnyVm = new FunnyViewModel();
            MiscVm = new MiscViewModel();
            TestVm = new TestViewModel();
            AddVm = new AddViewModel();
            SettingsVm = new SettingsViewModel();
            CurrentView = HomeVm;

            HomeViewComand = new RelayCommand(o =>
            {
                CurrentView = HomeVm;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVm;
            });

            KitViewCommand = new RelayCommand(o =>
            {
                CurrentView = KitVm;
            });

            NPCViewCommand = new RelayCommand(o =>
            {
                CurrentView = NPCVm;
            });

            CrashViewCommand = new RelayCommand(o =>
            {
                CurrentView = CrashVm;
            });

            FunnyViewCommand = new RelayCommand(o =>
            {
                CurrentView = FunnyVm;
            });

            MiscViewCommand = new RelayCommand(o =>
            {
                CurrentView = MiscVm;
            });

            TestViewCommand = new RelayCommand(o =>
            {
                CurrentView = TestVm;
            });

            AddViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddVm;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVm;
            });
        }
    }
}
