using Assets.Scripts.UnityExtended.CoreComponents.OWCCursorRenderer;
using Core.Weapons;
using UnityEditor;

namespace Editor
{
    class MenuItemsModificators
    {
        [MenuItem("Assets/Create/Weapon")]
        public static void CreateWeapon()
        {
            ObjectCreatorHelper.CreateAsset(typeof (Weapon));
        }

        [MenuItem("Assets/Create/Cursor")]
        public static void CreateCursor()
        {
            ObjectCreatorHelper.CreateAsset(typeof(OWCCursor));
        }
    }
}
