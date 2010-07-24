using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Windsor.SLExample.Startup;
using Windsor.SLExample.Views;

namespace Windsor.SLExample
{
    public partial class App
    {
        private readonly GuyWire _guyWire = new GuyWire();

        public App()
        {
            Startup += OnStartup;
            Exit += OnExit;
            UnhandledException += OnUnhandledException;

            InitializeComponent();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _guyWire.Wire();

            //Service locator pattern! Boo hoo!
            //Try NOT to do this if possible
            RootVisual = ServiceLocator.Current.GetInstance<MainView>(); 
        }

        private void OnExit(object  sender, EventArgs e)
        {
            _guyWire.Dewire();
        }

        private void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (!Debugger.IsAttached)
            {
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
