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
    }
}
