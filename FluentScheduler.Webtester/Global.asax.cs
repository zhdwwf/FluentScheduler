﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentScheduler.WebTester.Infrastructure.Tasks;
using ServiceStack.Logging;

namespace FluentScheduler.Webtester
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

			TaskManager.UnobservedTaskException += TaskManager_UnobservedTaskException;
			TaskManager.Initialize(new TaskRegistry());
		}

		protected void Application_End(object sender, EventArgs e)
		{
			TaskManager.Stop();
		}

		static void TaskManager_UnobservedTaskException(Task sender, UnhandledExceptionEventArgs e)
		{
			var log = LogManager.GetLogger(typeof(MvcApplication));
			log.Fatal("An error happened with a scheduled task: " + e.ExceptionObject);
		}
	}
}