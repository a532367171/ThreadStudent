using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace Chapter2.Recipe4
{
	class Program
	{
		static void Main(string[] args)
		{
			var t = new Thread(() => Process(10));
			t.Start();

			WriteLine("主：Waiting for another thread to complete work");
			_workerEvent.WaitOne();
			WriteLine("主：First operation is completed!");
			WriteLine("主：Performing an operation on a main thread");
			Sleep(TimeSpan.FromSeconds(5));
			_mainEvent.Set();
			WriteLine("主：Now running the second operation on a second thread");
			_workerEvent.WaitOne();
			WriteLine("主：Second operation is completed!");
		}

		private static AutoResetEvent _workerEvent = new AutoResetEvent(false);
		private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

		static void Process(int seconds)
		{
			WriteLine("次：Starting a long running work...");
			Sleep(TimeSpan.FromSeconds(seconds));
			WriteLine("次：Work is done!");
			_workerEvent.Set();
			WriteLine("次：Waiting for a main thread to complete its work");
			_mainEvent.WaitOne();
			WriteLine("次：Starting second operation...");
			Sleep(TimeSpan.FromSeconds(seconds));
			WriteLine("次：Work is done!");
			_workerEvent.Set();
		}
	}
}
