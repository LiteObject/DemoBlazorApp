using DemoBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoBlazorApp.Library
{
    public interface ITableFactory
    {
        public void ScanForTables(params Assembly[] assemblies);

        public T Create<T>();

        // public BaseModel Create(string typeName);

    }
}
