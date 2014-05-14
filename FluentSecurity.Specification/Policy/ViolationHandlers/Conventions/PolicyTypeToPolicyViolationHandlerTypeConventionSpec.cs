﻿using System;
using FluentSecurity.Policy;
using FluentSecurity.Policy.ViolationHandlers.Conventions;
using FluentSecurity.ServiceLocation;
using FluentSecurity.Specification.Helpers;
using FluentSecurity.Specification.TestData;
using Moq;
using NUnit.Framework;

namespace FluentSecurity.Specification.Policy.ViolationHandlers.Conventions
{
	[TestFixture]
	[Category("PolicyTypeToPolicyViolationHandlerTypeConventionSpec")]
	public class When_getting_a_handler_using_PolicyTypeToPolicyViolationHandlerTypeConvention
	{
		[Test]
		public void Should_return_handler_when_policy_type_is_match()
		{
			// Arrange
			var serviceLocator = new Mock<IServiceLocator>();
			serviceLocator.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(new DefaultPolicyViolationHandler());
			var convention = new PolicyTypeToPolicyViolationHandlerTypeConvention<DenyAnonymousAccessPolicy, DefaultPolicyViolationHandler>();
			convention.Inject(serviceLocator.Object);
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
			var serviceLocator = new Mock<IServiceLocator>();
			serviceLocator.Setup(x => x.Resolve(It.IsAny<Type>())).Returns(new DefaultPolicyViolationHandler());
			var convention = new PolicyTypeToPolicyViolationHandlerTypeConvention<DenyAnonymousAccessPolicy, DefaultPolicyViolationHandler>();
			convention.Inject(serviceLocator.Object);
			var exception = TestDataFactory.CreateExceptionFor(new IgnorePolicy());

			// Act
			var handler = convention.GetHandlerFor<IPolicyViolationHandler>(exception);

			// Assert
			Assert.That(handler, Is.Null);
		}
	}
}