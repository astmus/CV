using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfClient.ViewModels
{
	public class BackgroundTask
	{		
		static readonly SemaphoreSlim _semaphore = new(1, 1);
		static readonly ConcurrentQueue<Func<CancellationToken, ValueTask>> _taskQueue = new();

		public static void Setup(IServiceProvider sp)
		{
			var  service = sp.GetRequiredService<BackgroundTask>();
		}

		public static async void QueueTask(Func<CancellationToken, ValueTask> task)
		{
			Debug.Assert(task != null);

			try
			{
				_taskQueue.Enqueue(task);

				await _semaphore.WaitAsync();

				while (_taskQueue.TryDequeue(out var nextAction))
				{

					try
					{
						using (CancellationTokenSource src = new CancellationTokenSource(30000))
						{
							await nextAction(src.Token);
						}
					}
					catch (Exception ex)
					{
						Debug.Write(ex, $"Operation canceled: {ex.Message}");
					}
				}
			}
			catch (OperationCanceledException ex)
			{
				_taskQueue.Clear();

				Debug.Write(ex, $"Operation canceled: {ex.Message}");
			}
			finally
			{
				_semaphore.Release();
			}
		}
	}
}
