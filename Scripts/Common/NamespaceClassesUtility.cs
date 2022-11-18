using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

public static class NamespaceClassesUtility 
{
    
    public static System.Type[] GetTypes(string namespaceName) 
    {
        var assembly = Assembly.GetExecutingAssembly();
        var types= assembly.GetTypes()
                  .Where(t => string.Equals(t.Namespace, namespaceName, System.StringComparison.Ordinal))
                  .ToArray();

        return types;
          
    }
}
