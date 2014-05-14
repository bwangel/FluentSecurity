﻿using FluentSecurity.Policy;
using FluentSecurity.Policy.ViolationHandlers.Conventions;
using FluentSecurity.Specification.Helpers;
using FluentSecurity.Specification.TestData;
using NUnit.Framework;

namespace FluentSecurity.Specification.Policy.ViolationHandlers.Conventions
{
	[TestFixture]
	[Category("PolicyTypeToPolicyViolationHandlerInstanceConventionSpec")]
	public class When_getting_a_handler_using_PolicyTypeToPolicyViolationHandlerInstanceConvention
	{
		[Test]
		public void Should_return_handler_when_policy_type_is_match()
		{
			// Arrange
			var convention = new PolicyTypeToPolicyViolationHandlerInstanceConvention<DenyAnonymousAccessPolicy, DefaultPolicyViolationHandler>(() => new DefaultPolicyViolationHandler());
			var exception = TestDataFactory.CreateExceptionFor(new DenyAnonymousAccessPolicy());

			// Act
			var handler = convention.GetHandlerFor<IPolicyViolationHandler>(exception);

			// Assert
			Assert.That(handler, Is.InstanceOf<DefaultPolicyViolationHandler>());
		}

		[Test]
		public void Should_return_handler_when_policy_type_is_not_match()
		{
			// Arrange
			var convention = new PolicyTypeToPolicyViolationHandlerInstanceConvention<DenyAnonymousAccessPolicy, DefaultPolicyViolationHandler>(() => new DefaultPolicyViolationHandler());
			var exception = TestDataFactory.CreateExceptionFor(new IgnorePolicy());

			// Act
			var handler = convention.GetHandlerFor<IPolicyViolationHandler>(exception);

			// Assert
			Assert.That(handler, Is.Null);
		}
	}
}