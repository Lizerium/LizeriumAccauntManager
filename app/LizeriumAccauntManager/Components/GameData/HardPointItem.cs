/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 19 мая 2026 10:16:19
 * Version: 1.0.45
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
