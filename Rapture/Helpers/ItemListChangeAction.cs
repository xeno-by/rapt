using System;

namespace Rapture.Helpers
{
    [Flags]
    public enum ItemListChangeAction
    {
        Add = 1,
        Remove = 2,
        Replace = 3
    }
}