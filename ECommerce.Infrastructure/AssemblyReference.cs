using System.Reflection;

namespace ECommerce.Infrastructure
{
    public static class AssemblyReference
    {
        // This class is used to reference the assembly for Scrutor Package and other services
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
