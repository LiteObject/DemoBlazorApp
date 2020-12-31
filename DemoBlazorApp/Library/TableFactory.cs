using DemoBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DemoBlazorApp.Library
{
    public class TableFactory : ITableFactory
    {
        private Dictionary<string, Type> availableTableTypes 
            = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

        public TableFactory() 
        {
           
        }

        public void ScanForTables(params Assembly[] assemblies) 
        {
            var foodType = typeof(BaseModel);

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(t => !t.IsAbstract || !t.IsInterface))
                {
                    if (!foodType.IsAssignableFrom(type))
                    {
                        continue;
                    }

                    if (!availableTableTypes.ContainsKey(type.Name))
                    {
                        availableTableTypes.Add(type.Name, type);
                    }                    
                }
            }
        }

        public T Create<T>() 
        {
            Type type;
            string targetTypeName = typeof(T).Name;

            if (!availableTableTypes.TryGetValue(targetTypeName, out type)) 
            {
                throw new ArgumentException($"Unable to find any table: {targetTypeName}");
            }

            return (T)Activator.CreateInstance(type);
        }

        /*public BaseModel Create(string typeName)
        {
            Type type;
            
            if (!availableTableTypes.TryGetValue(typeName, out type))
            {
                throw new ArgumentException($"Unable to find any table: {typeName}");
            }

            return (BaseModel)Activator.CreateInstance(type);
        } */
    }
}
