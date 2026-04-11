/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 11 апреля 2026 13:39:27
 * Version: 1.0.5
 */

namespace Root.Components
{
    public class HardPointItem
    {
        public long ShipHash { get; set; }
        public string HPName { get; set; }
        public string HPType { get; set; }
        public string MountableTypes { get; set; }
        public long DefaultItemHash { get; set; }
    }
}
