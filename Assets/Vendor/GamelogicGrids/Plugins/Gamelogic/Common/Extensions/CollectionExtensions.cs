using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic
{
	/**
		This class provides useful extension methods for collections, mostly IEnumerable<T>.
	*/
	public static class CollectionExtensions
	{
		/**
			Returns all elements of the collection wich are of FilterType.
		*/
		public static IEnumerable<FilterType> FilterByType<T, FilterType>(this IEnumerable<T> list)
			where T : class
			where FilterType : class, T
		{
			return list.Where(item => item as FilterType != null).Cast<FilterType>();
		}

		/**
			Removes all the elements in the list that does not satisfy the predicate.
		*/	
		public static void RemoveAllBut<T>(this List<T> list, Predicate<T> match)
		{
			Predicate<T> nomatch = item => !match(item);

			list.RemoveAll(nomatch);
		}

		/**
			Returns whether this collection is empty.
		*/
		public static bool IsEmpty<T>(this ICollection<T> collection)
		{
			return collection.Count == 0;
		}
		
		/**
			Add all elements of other to the given collection.
		*/
		public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> other)
		{
			if (other == null)//nothing to add
			{
				return;
			}

			foreach (var obj in other)
			{
				collection.Add(obj);
			}
		}

		/**
			Returns a pretty string representation of the given list. The resulting string looks something like
			<code>{a, b, c}</code>.
		*/
		public static string ListToString<T>(this IEnumerable<T> list)
		{
			if (list == null)
			{
				return "null";
			}

			var s = "{";

			s += list.Aggregate(s, (res, x) => res + ", " + x.ToString());
			s += "}";

			return s;
		}

		/**
			Returns an enumerable of all elements of the given list	but the first,
			keeping them in order.
		*/
		public static IEnumerable<T> ButFirst<T>(this IEnumerable<T> list)
		{
			return list.Skip(1);
		}

		/**
			Return an Enumarable off all elements in the given 
			list but the last, keeping them in order.
		*/
		public static IEnumerable<T> ButLast<T>(this IEnumerable<T> list)
		{
			var lastX = default(T);
			var first = true;

			foreach (var x in list)
			{
				if (first)
				{
					first = false;
				}
				else
				{
					yield return lastX;
				}

				lastX = x;
			}
		}

		/**
			Finds the maximum element in the element as scored by the given function.
		*/
		public static T MaxBy<T>(this IEnumerable<T> collection, Func<T, IComparable> score)
		{
			return collection.Aggregate((x, y) => score(x).CompareTo(score(y)) > 0 ? x : y);
		}

		/**
			Returns a list with elements in order, but the first element is moved to the end.
		*/
		//TODO consider changing left to something more universal
		public static IEnumerable<T> RotateLeft<T>(this IEnumerable<T> list)
		{
			var enumeratedList = list as IList<T> ?? list.ToList();
			return enumeratedList.ButFirst().Concat(enumeratedList.Take(1));
		}

		/**
			Returns a list with elements in order, but the last element is moved to the front.
		*/
		public static IEnumerable<T> RotateRight<T>(this IEnumerable<T> list)
		{
			var enumeratedList = list as IList<T> ?? list.ToList();
			yield return enumeratedList.Last();

			foreach (var item in enumeratedList.ButLast())
			{
				yield return item;
			}
		}

		/**
			Returns a random element from the list.
		*/
		public static T RandomItem<T>(this IEnumerable<T> list)
		{
			return list.SampleRandom(1).First();
		}

		/**
			Returns a random sample from the list.
		*/
		public static IEnumerable<T> SampleRandom<T>(this IEnumerable<T> list, int sampleCount)
		{
			/* Reservoir sampling. */
			var samples = new List<T>();

			//Must be 1, otherwise we have to use Range(0, i + 1)
			var i = 1;

			foreach (var item in list)
			{
				if (i <= sampleCount)
				{
					samples.Add(item);
				}
				else
				{
					// Randomly replace elements in the reservoir with a decreasing probability.
					var r = UnityEngine.Random.Range(0, i);

					if (r < sampleCount)
					{
						samples[r] = item;
					}
				}

				i++;
			}

			return samples;
		}
		
		/**	
			Shuffles a list.
		*/
		public static void Shuffle<T>(this IList<T> list)  
		{  
		    var rng = new Random();  
		    var n = list.Count;  
		    
			while (n > 1) 
			{  
		        n--;  
		        var k = rng.Next(n + 1);  
		        var value = list[k];  
		        list[k] = list[n];  
		        list[n] = value;  
		    }  
		}
	}
}