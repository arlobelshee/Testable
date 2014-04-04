// Testable
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using JetBrains.Annotations;

namespace Testable.Events
{
	public class HandlerFailure<TArg1> : IEquatable<HandlerFailure<TArg1>>
	{
		[NotNull]
		public Action<TArg1> BadHandler { get; private set; }

		[NotNull]
		public Exception ExceptionThrown { get; private set; }

		public HandlerFailure([NotNull] Action<TArg1> badHandler, [NotNull] Exception exceptionThrown)
		{
			BadHandler = badHandler;
			ExceptionThrown = exceptionThrown;
		}

		public bool Equals([CanBeNull] HandlerFailure<TArg1> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return BadHandler.Equals(other.BadHandler) && ExceptionThrown.Equals(other.ExceptionThrown);
		}

		public override bool Equals([CanBeNull] object obj)
		{
			return Equals(obj as HandlerFailure<TArg1>);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (BadHandler.GetHashCode()*397) ^ ExceptionThrown.GetHashCode();
			}
		}

		public static bool operator ==(HandlerFailure<TArg1> left, HandlerFailure<TArg1> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(HandlerFailure<TArg1> left, HandlerFailure<TArg1> right)
		{
			return !Equals(left, right);
		}

		public override string ToString()
		{
			return string.Format("handler {0} threw {1}", BadHandler, ExceptionThrown);
		}
	}
}