using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VUtils.Services.Controls.DataCollection;
using VUtils.Services.Extensions.Arrays;
using VUtils.Services.Extensions.Enumerables;

namespace VUtils.Services.Extensions.Strings
{
	public static class StringPathInfoExt
	{

		private static SemaphoreSlim semaphore=new(Environment.ProcessorCount);

		/// <summary>
		/// Gets the file size.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		/// <exception cref="FileNotFoundException"></exception>
		public static long GetFileSize(this string path) => path.IsFile() ? new FileInfo(path).Length : throw new FileNotFoundException("The given path does not reference an existing or reachable file.", path);
		/// <summary>
		/// Gets the file size data.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static DataValue.DataValue GetFileData(this string path) => new (path.GetFileSize());
		/// <inheritdoc cref="Path.GetExtension(string?)"/>
		public static string? GetExtension(this string path) => path.HasExtension() ? path.GetExtension()?.ToLower() : null;
		/// <summary>
		/// Gets the file size as a simplified string.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static string GetFileSizeString(this string path) => path.GetFileData().ToString();

		//public static PathItem[] ScanDirectory(this string path)
		//{
		//	if(path.IsDir())
		//	{
		//		var tmp=Directory.GetFiles(path);
		//		var dirs=Directory.GetDirectories(path);

		//		PathItem[] res=tmp.Iterate(q=>new PathItem(q));
		//	}
		//}

		public static async Task AsyncGetFiles(this string path, Action<string[]> callback)
		{
			if(path.IsDir())
			{
				string[] dirs=Directory.GetDirectories(path);
				string[] res=Directory.GetFiles(path);
				List<Task> taskList=new();
				await Task.Delay(1);
				foreach(string sel in dirs)
				{
					//await semaphore.WaitAsync();
					Task task=Task.Run(async ()=>
					{
						await AsyncGetFiles(sel, callback);
						//semaphore.Release();
					});
					taskList.Add(task);
				}
				callback(res);
				await Task.WhenAll(taskList);
			}
		}

	}
}
