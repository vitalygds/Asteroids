#if UNITY_EDITOR
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Weapon.Editor")]
[assembly: InternalsVisibleTo("Tests.EditorMode")]
[assembly: InternalsVisibleTo("Tests.PlayMode")]
#endif