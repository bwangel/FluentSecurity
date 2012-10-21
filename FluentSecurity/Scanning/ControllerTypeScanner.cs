﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace FluentSecurity.Scanning
{
	internal class ControllerTypeScanner : ITypeScanner
	{
		public Type ControllerType { get; private set; }
		
		public ControllerTypeScanner() : this(typeof(IController)) {}

		public ControllerTypeScanner(Type controllerType)
		{
			if (controllerType == null) throw new ArgumentNullException("controllerType");
			
			ControllerType = controllerType;
		}

		public IEnumerable<Type> Scan(IEnumerable<Assembly> assemblies)
		{
			var results = new List<Type>();
			foreach (var assembly in assemblies)
			{
				var controllerTypes = assembly.GetExportedTypes().Where(type => ControllerType.IsAssignableFrom(type)).ToList();
				var filteredControllerTypes = controllerTypes.Where(type => !type.IsAbstract);
				results.AddRange(filteredControllerTypes);
			}
			return results;
		}
	}
}