﻿using Gir;
using Gir.Analysis;

namespace Generator.Factories
{
    internal class DllImportResolverFactory
    {
        public DllImportResolver Create(string sharedLibrary, NamespaceName namespaceName)
        {
            return new DllImportResolver(sharedLibrary, namespaceName);
        }
    }
}
