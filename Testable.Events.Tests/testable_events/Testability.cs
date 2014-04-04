// Testable
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Testable.Events.Tests.testable_events
{
	[TestFixture]
	public class Testability
	{
		private const string AnyValue = "hello";

		[Test]
		public void CallingAnEventWithNoSubscribersShouldNotCrash()
		{
			var testSubject = new Event<string>();
			testSubject.Fire(AnyValue);
		}

		[Test]
		public void ExplicitlyBindingToAnEventShouldGiveYouNotificationsWhenItFires()
		{
			var calls = new List<string>();
			var testSubject = new Event<string>();
			testSubject.Subscribe(calls.Add);
			testSubject.Fire(AnyValue);
			calls.Should()
				.Equal(AnyValue);
		}

		[Test]
		public void ExceptionsFiredByAHandlerShouldNotPreventOtherHandlersFromFiring()
		{
			var calls = new List<string>();
			var testSubject = new Event<string>();
			testSubject.Subscribe(s=>{throw new InvalidOperationException("not caught");});
			testSubject.Subscribe(calls.Add);
			testSubject.Fire(AnyValue);
			calls.Should()
				.Equal(AnyValue);
		}

		[Test]
		public void ExceptionsInHandlersShouldBeReportedToOnErrorListeners()
		{
			var failures = new List<HandlerFailure<string>>();
			var exception = new InvalidOperationException("not caught");
			Action<string> badHandler = s =>
			{
				throw exception;
			};
			var testSubject = new Event<string>();
			testSubject.Subscribe(badHandler);
			testSubject.OnError.Subscribe(failures.Add);
			testSubject.Fire(AnyValue);
			failures.Should()
				.Equal(new HandlerFailure<string>(badHandler, exception));
		}
	}
}