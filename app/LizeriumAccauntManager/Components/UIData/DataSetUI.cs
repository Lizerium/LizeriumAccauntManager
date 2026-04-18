/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 18 апреля 2026 14:44:50
 * Version: 1.0.14
 */

using System;
using System.Collections.Generic;

namespace Root.Components
{
    public class DataSetUI
    {
        public List<PIFaction> PIFactionList { get; set; } = new List<PIFaction>();
        public List<PICargo> PICargoList { get; set; } = new List<PICargo>();
        public List<PIEquipment> PIEquipmentList { get; set; } = new List<PIEquipment>();

        public void AddPICargoTableRow(PICargo pICargo)
        {
            PICargoList.Add(pICargo);
        }

        public void AddPIEquipmentTableRow(string hPName, string v1, uint itemHash, string v3, string hPType)
        {
            PIEquipmentList.Add(new PIEquipment()
            {
                itemHardpoint = hPName,
                itemDescription = v1,   
                itemHash = itemHash,
                itemGameDataType = v3,
                itemAllowedTypes = hPType,
            });
        }

        public void AddPIEquipmentTableRow(PIEquipment row)
        {
           PIEquipmentList.Add(row);
        }
    }
}
