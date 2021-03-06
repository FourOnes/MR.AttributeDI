﻿using System;
using Autofac;

namespace MR.AttributeDI.Autofac
{
	/// <summary>
	/// An applier for Autofac.
	/// </summary>
	public class AutofacApplier : IApplier
	{
		private ContainerBuilder _builder;

		public AutofacApplier(ContainerBuilder builder)
		{
			if (builder == null)
				throw new ArgumentNullException(nameof(builder));

			_builder = builder;
		}

		public void Apply(ApplierContext context)
		{
			var registration = _builder
				.RegisterType(context.Service)
				.As(context.As);
			switch (context.Lifetime)
			{
				case Lifetime.Singleton:
					registration.SingleInstance();
					break;

				case Lifetime.Scoped:
					registration.InstancePerLifetimeScope();
					break;

				case Lifetime.Transient:
					registration.InstancePerDependency();
					break;
			}
		}
	}
}
