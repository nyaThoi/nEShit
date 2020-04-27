using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuraModule
{
    #region Inventory
    public enum InventoryType { IT_BackPack = 0, IT_Equipment = 1, IT_Bank = 4, IT_BackPack_Bags = 5, IT_EudemonInventory = 6 };
	// for IT_BackPack and IT_Bank
	public enum InventoryBagType
	{
		IBT_MainBag = 0,
		IBT_Bag1 = 1,
		IBT_Bag2 = 2,
		IBT_Bag3 = 3,
		IBT_Bag4 = 4,
		IBT_Bag5 = 5,
		IBT_Bag6 = 6,
		IBT_Bag7 = 7,
		IBT_Bag8 = 8,
		IBT_Bag9 = 9,
		IBT_Bag10 = 10,
		IBT_Bag11 = 11,
		IBT_Bag12 = 12,
		IBT_Bag13 = 13,
		IBT_Bag14 = 14,
		IBT_Bag15 = 15,
		IBT_Bag16 = 16,
		IBT_Bag17 = 17,
		IBT_Bag18 = 18,
		IBT_Bag19 = 19,
		IBT_Bag20 = 20,
		IBT_MAX = 20
	};
    #endregion

    #region FISHING
	public enum FishingState
	{
		Idle,
		Baiting,
		AutomaticFishing,
		ActiveFishing,
		EndAnimation
	}
    #endregion
}
