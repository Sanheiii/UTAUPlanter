﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Planter
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static string[] args;
        protected override void OnStartup(StartupEventArgs e)
        {
            args = e.Args;
            if(args.Count()==0)
            {
#if RELEASE
                this.Shutdown();
#endif
            }
            base.OnStartup(e);
        }
    }
}
