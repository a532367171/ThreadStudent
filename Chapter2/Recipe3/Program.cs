using System;
using System.Threading;
using static System.Console;
using static System.Threading.Thread;

namespace Chapter2.Recipe3
{
	class Program
	{
		static void Main(string[] args)
		{
			for (int i = 1; i <= 6; i++)
			{
				string threadName = "Thread " + i;
				int secondsToWait = 2 + 2 * i;
				var t = new Thread(() => AccessDatabase(threadName, secondsToWait));
				t.Start();
			}
		}

		static SemaphoreSlim _semaphore = new SemaphoreSlim(4);

		static void AccessDatabase(string name, int seconds)
		{
			WriteLine($"{name} waits to access a database");
			_semaphore.Wait();
			WriteLine($"{name} was granted an access to a database");
			Sleep(TimeSpan.FromSeconds(seconds));
			WriteLine($"{name} is completed");
			_semaphore.Release();
		}

        //.Wait() 标识我开始占用  如果线程够的话 我就接下来运行 如果不够 我就挂起  
        //只等到  有线程 用.Release();把自己释放掉 
        //  举例 厕所 N个坑位置  .Wait()关门  里面干事情  .Release() 开门  
        //  后面有其他人只能乖乖的等吧
    }
}
