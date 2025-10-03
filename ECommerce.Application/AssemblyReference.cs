using System.Reflection;

namespace ECommerce.Application
{
    public static class AssemblyReference
    {
        // This class is used to reference the assembly for MediatR and other services
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
