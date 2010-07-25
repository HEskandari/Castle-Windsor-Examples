// Copyright 2004-2010 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.Samples.WindsorSilverlight
{
	using System;
	using System.Diagnostics;
	using System.Windows;
	using System.Windows.Browser;

	using Castle.Samples.WindsorSilverlight.Startup;

	public partial class App
	{
		private readonly GuyWire guyWire = new GuyWire();

		public App()
		{
			Startup += OnStartup;
			Exit += OnExit;
			UnhandledException += OnUnhandledException;

			InitializeComponent();
		}

		private void OnStartup(object sender, StartupEventArgs e)
		{
			guyWire.Wire();

			RootVisual = guyWire.GetRoot();
		}

		private void OnExit(object sender, EventArgs e)
		{
			guyWire.Dewire();
		}

		private void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (Debugger.IsAttached)
			{
				return;
			}

			e.Handled = true;
			Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
		}

		private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
		{
			try
			{
				var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
				errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

				HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
			}
			catch (Exception)
			{
			}
		}
	}
}