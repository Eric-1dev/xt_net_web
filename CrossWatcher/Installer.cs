using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;

namespace CrossWatcher
{
    [RunInstaller(true)]
    public partial class WatcherInstaller : Installer
    {
        ServiceInstaller serviceInstaller;
        ServiceProcessInstaller processInstaller;

        public WatcherInstaller()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            serviceInstaller.DisplayName = "Crossing Watcher";
            serviceInstaller.Description = "Наблюдает за папкой, указанной в параметрах запуска и переименовывает файлы с базами данных в соответствии с шаблоном (h->a, g->l)." +
                "А также переименовывает base.txt в base(1, 2, 3 и т.д.).txt";
            serviceInstaller.ServiceName = "Watcher";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
