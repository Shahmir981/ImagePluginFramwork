using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ImagePluginFrameworkBestPractice.Services
{
    public class PluginManager
    {
        private readonly Dictionary<string, Type> _plugins = new();

        public void LoadPlugins(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException(path);

            foreach (var dll in Directory.GetFiles(path, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(dll);
                foreach (var type in assembly.GetTypes().Where(t =>
                    typeof(IImageEffect).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract))
                {
                    _plugins[type.Name] = type;
                }
            }
        }

       public IImageEffect GetEffect(string name){
          if (string.IsNullOrWhiteSpace(name))
           throw new ArgumentException("Effect name must be provided.", nameof(name));

          if (!_plugins.TryGetValue(name, out var type))
           throw new KeyNotFoundException($"Plugin '{name}' not found.");

         try {
             var instance = Activator.CreateInstance(type) as IImageEffect;
             
             if (instance is null)
               throw new InvalidOperationException($"Could not create an instance of plugin '{name}'. Ensure it has a public parameterless constructor.");

            return instance;
           } catch (Exception ex) {
             throw new InvalidOperationException($"Error creating plugin instance for '{name}': {ex.Message}", ex);
          }

          }
    }
}
