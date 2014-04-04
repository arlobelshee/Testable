// Testable
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using JetBrains.Annotations;

namespace Testable.Events
{
	public class Lazy<T>
	{
		public delegate T ValueCreator();

		[NotNull] private readonly ValueCreator _createValue;
		private T _value;
		private bool _hasBeenCreated;

		public Lazy([NotNull] ValueCreator createValue)
		{
			_createValue = createValue;
		}

		[CanBeNull]
		public T Value
		{
			get
			{
				if (!_hasBeenCreated)
				{
					_value = _createValue();
					_hasBeenCreated = true;
				}
				return _value;
			}
		}
	}
}