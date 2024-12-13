using System.Collections.Generic;

namespace Unit
{
    public sealed class UnitServiceResolver
    {
        internal UnitServiceResolver(UnitSpawner spawner, IReadOnlyList<IUnitConstructor> constructors)
        {
            spawner.Initialize(constructors);   
        }
    }
}