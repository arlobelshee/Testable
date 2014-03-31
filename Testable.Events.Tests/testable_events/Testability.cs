// Testable
// Copyright 2014, Arlo Belshee. All rights reserved. See LICENSE.txt for usage.

using FluentAssertions;
using NUnit.Framework;

namespace Testable.Events.Tests.testable_events
{
	[TestFixture]
	public class Testability
	{
		[Test]
		public void TheTestRun_Should_Pass()
		{
			3.Should()
				.Be(3);
		}
	}
}