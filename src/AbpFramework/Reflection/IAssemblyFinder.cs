using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AbpFramework.Reflection
{
    /// <summary>
    /// This interface is used to get assemblies in the application.
    /// It may not return all assemblies, but those are related with modules.
    /// </summary>
    public interface IAssemblyFinder
    {
        /// <summary>
        /// Gets all assemblies.
        /// </summary>
        /// <returns>List of assemblies</returns>
        List<Assembly> GetAllAssemblies();
    }
}
