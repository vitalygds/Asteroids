#if UNITY_EDITOR
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Unit.Editor")]
[assembly: InternalsVisibleTo("Tests.EditorMode")]
[assembly: InternalsVisibleTo("Tests.PlayMode")]
#endif