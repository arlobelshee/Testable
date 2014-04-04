// Testable
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Testable.Events
{
	[PublicAPI]
	public class Event<TArg1>
	{
		[NotNull] private readonly List<Action<TArg1>> _handlers = new List<Action<TArg1>>();

		[NotNull] private readonly Lazy<Event<HandlerFailure<TArg1>>> _onError =
			new Lazy<Event<HandlerFailure<TArg1>>>(() => new Event<HandlerFailure<TArg1>>());

		[NotNull]
		public Event<HandlerFailure<TArg1>> OnError
		{
// ReSharper disable once AssignNullToNotNullAttribute
			get { return _onError.Value; }
		}

		public void Fire([CanBeNull] TArg1 arg1)
		{
			foreach (var handler in _handlers)
			{
				try
				{
					handler(arg1);
				}
				catch (Exception e)
				{
					OnError.Fire(new HandlerFailure<TArg1>(handler, e));
				}
			}
		}

		public void Subscribe([NotNull] Action<TArg1> whenFired)
		{
			_handlers.Add(whenFired);
		}
	}
}