/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 22 апреля 2026 18:37:56
 * Version: 1.0.18
 */

using Root.Components;
using Root.Components;
using System.Collections.Generic;
using System;
using Root.Components;

namespace Root;

public class DataModel
{
    public List<LoginIDItem> LoginIDList { get; set; } = new List<LoginIDItem>();
    public List<IPItem> IPList { get; set; } = new List<IPItem>();
    public List<HashListItem> HashListItems { get; set; } = new List<HashListItem>();
    public List<HardPointItem> HardPointList { get; set; } = new List<HardPointItem>();
    public List<EquipInfoItem> EquipInfoList { get; set; } = new List<EquipInfoItem>();
    public List<ShipInfoItem> ShipInfoList { get; set; } = new List<ShipInfoItem>();
    public List<GeneralStatistics> GeneralStatisticsList { get; set; } = new List<GeneralStatistics>();
    public DataSetUI DataSetUI { get; set; } = new DataSetUI();
    public DataSetPlayerInfo DataSetPlayerInfo { get; set; } = new DataSetPlayerInfo();

    public void Clear()
    {
        DataSetPlayerInfo.Clear();
        LoginIDList.Clear();
        IPList.Clear();
        HashListItems.Clear();
        HardPointList.Clear();
        EquipInfoList.Clear();
        ShipInfoList.Clear();
        GeneralStatisticsList.Clear();
    }

    public void AddIPListRow(string accDir, string ip, DateTime now)
    {
        IPItem ipItem = new IPItem()
        { 
            AccDir = accDir, 
            IP = ip, 
            AccessTime = now 
        };
        IPList.Add(ipItem);
    }
}
