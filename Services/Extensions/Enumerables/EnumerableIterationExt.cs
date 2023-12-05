using System;
using System.Collections.Generic;
using System.Linq;

namespace VUtils.Services.Extensions.Enumerables
{
	public static class EnumerableIterationExt
	{

		/// <summary>
		/// Iterates through the input and uses the <paramref name="predicate"/> as the process to conduct for each item in the <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TIn">The input type.</typeparam>
		/// <typeparam name="TOut">The output type.</typeparam>
		/// <param name="source">The collection to run the <paramref name="predicate"/> on for each item within this collection.</param>
		/// <param name="predicate">The process to conduct for each item within the <paramref name="source"/> collection.</param>
		/// <returns>an array of <typeparamref name="TOut"/>s.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static TOut[] Iterate<TIn, TOut>(this IEnumerable<TIn> source, Func<TIn, TOut> predicate)
		{
			if(source==null)
				throw new ArgumentNullException("The source cannot be null.");
			if(predicate==null)
				throw new ArgumentNullException("The predicate cannot be null.");
			TOut[] res={ };
			Array.Resize(ref res, source.Count());
			int i=0;
			foreach(TIn item in source)
			{
				res[i]=predicate(item);
				i++;
			}
			return res;
		}
		
	}
}
