using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Extensions {
	/// <summary>
	/// Determines whether the collection is null or contains no elements
	/// </summary>
	/// <typeparam name="T">The IEnumerable type.</typeparam>
	/// <param name="enumerable">The enumberable, which may be null or empty</param>
	/// <returns>
	///		<c>true</c> if the IEnumberable is null or empty; otherwise <c>false</c>.
	/// </returns>
	public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) {
		if (enumerable == null)
			return true;

		/* If this is a list, use the count property.
		 * The Count property is 0(1) while IEnumerable.Count is 0(N). */
		var collection = enumerable as ICollection<T>;
		if (collection != null)
			return collection.Count < 1;

		return enumerable.Any();
	}

	public static T GetSafeComponent<T>(this GameObject obj) where T : MonoBehaviour {
		T component = obj.GetComponent<T>();

		if (component == null)
			Debug.LogError(string.Format("Expected to find component of type {0}, but found none", typeof(T)));

		return component;
	}
}
