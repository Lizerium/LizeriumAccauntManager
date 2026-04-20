/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 20 апреля 2026 16:22:14
 * Version: 1.0.16
 */

using System;

namespace Root.Components.GameData
{
    public class ThreadSearchHardpointItem : IEquatable<ThreadSearchHardpointItem>
    {
        public ThreadSearchHardpointItem(long shipHash, string hpName)
        {
            ShipHash = shipHash;
            HPName = hpName;
        }

        public long ShipHash { get; set; }
        public string HPName { get; set; }

        public bool Equals(ThreadSearchHardpointItem other)
        {
            if (other == null) return false;
            return ShipHash == other.ShipHash && HPName == other.HPName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + ShipHash.GetHashCode();
                hash = hash * 23 + (HPName != null ? HPName.GetHashCode() : 0);
                return hash;
            }
        }
    }
}
