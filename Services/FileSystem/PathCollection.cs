using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using VUtils.Services.Controls.DataCollection;
using VUtils.Services.Extensions.Arrays;
using VUtils.Services.Extensions.Enumerables;
using VUtils.Services.Extensions.Strings;

namespace VUtils.Services.FileSystem
{
	public class PathCollection
	{

		public readonly ListView Container;
		private PathItem[] _items;
		private ObservableCollection<PathItem> _itemsCollection = new ObservableCollection<PathItem>();
		public ObservableCollection<PathItem> Items
		{
			get
			{
				var ins=new ObservableCollection<PathItem>();
				foreach(var sel in _items)
					ins.Add(sel);
				return ins;
			}
		}


		/// <summary>
		/// Creates a new instance of the <see cref="PathCollection"/> class object.
		/// </summary>
		/// <param name="container"></param>
		public PathCollection(ListView container)
		{
			Container = container;
			Container.ItemsSource=_items;
			//Container.DataContext=_itemsCollection;
		}

		public PathSortingMethod SortingMethod { get; set; } = PathSortingMethod.SizeDescending;

		public async void Search(string path)
		{
			await path.AsyncGetFiles(ProcessIncomingPaths);
			foreach(var sel in _items)
				_itemsCollection.Add(sel);
			Container.DataContext=_itemsCollection;
		}

		private void ProcessIncomingPaths(string[] paths)
		{
			var l=paths.Iterate(q=>new PathItem(q));
			_items=_items.Merge(l);
			SortResults();
			
		}

		private void SortResults()
		{
			Comparison<PathItem> sorter;
			switch(SortingMethod)
			{
				case PathSortingMethod.SizeDescending:
					sorter=SortBySize_FromLargest; break;
				case PathSortingMethod.SizeAscending:
					sorter=SortBySize_FromSmallest; break;
				case PathSortingMethod.NameDescending:
					sorter=SortByName_AZ; break;
				case PathSortingMethod.NameAscending:
					sorter=SortByName_ZA; break;
				case PathSortingMethod.CreatedDescending:
					sorter=SortByCreation_FromNewest; break;
				case PathSortingMethod.CreatedAscending:
					sorter=SortByCreation_FromOldest; break;
				case PathSortingMethod.ModifiedDescending:
					sorter=SortByCreation_FromNewest; break;
				case PathSortingMethod.ModifiedAscending:
					sorter=SortByCreation_FromOldest; break;
				case PathSortingMethod.AccessDescending:
					sorter=SortByCreation_FromNewest; break;
				case PathSortingMethod.AccessAscending:
					sorter=SortByCreation_FromOldest; break;
			}
			if(SortingMethod!=PathSortingMethod.None)
				Array.Sort(_items, SortBySize_FromLargest);
		}


		private static int SortBySize_FromLargest(PathItem a, PathItem b) => CompareLongValue(a.DataSize, b.DataSize);

		private static int SortBySize_FromSmallest(PathItem a, PathItem b) => CompareLongValue(a.DataSize, b.DataSize) * -1;

		private static int SortByName_AZ(PathItem a, PathItem b) => CompareStringValue(a.Name, b.Name);

		private static int SortByName_ZA(PathItem a, PathItem b) => CompareStringValue(a.Name, b.Name) * -1;

		private static int SortByType_AZ(PathItem a, PathItem b) => CompareStringValue(a.Name, b.Name);

		private static int SortByType_ZA(PathItem a, PathItem b) => CompareStringValue(a.Name, b.Name) * -1;

		private static int SortByCreation_FromNewest(PathItem a, PathItem b) => CompareLongValue(a.Created.Ticks, b.Created.Ticks);

		private static int SortByCreation_FromOldest(PathItem a, PathItem b) => CompareLongValue(a.Created.Ticks, b.Created.Ticks) * -1;

		private static int SortByModification_FromNewest(PathItem a, PathItem b) => CompareLongValue(a.Modified.Ticks, b.Modified.Ticks);

		private static int SortByModification_FromOldest(PathItem a, PathItem b) => CompareLongValue(a.Modified.Ticks, b.Modified.Ticks) * -1;

		private static int SortByAccess_FromNewest(PathItem a, PathItem b) => CompareLongValue(a.Accessed.Ticks, b.Accessed.Ticks);

		private static int SortByAccess_FromOldest(PathItem a, PathItem b) => CompareLongValue(a.Accessed.Ticks, b.Accessed.Ticks) * -1;


		private static int CompareLongValue(long a, long b) => a > b ? 1 : a==b ? 0 : -1;

		private static int CompareStringValue(string a, string b)
		{
			if(a is null && b is null || a.Equals(b))
				return 0;
			if(a is null && b is not null)
				return 1;
			if(a is not null && b is not null)
				return -1;
			a=a.ToLower();
			b=b.ToLower();
			int minLen=Math.Min(a.Length, b.Length);
			for(int i = 0;i<minLen;i++)
			{
				if(a[i]>b[i])
					return 1;
				if(a[i]<b[i])
					return -1;
			}
			return a.Length<b.Length ? 1 : -1;
		}

	}
}
