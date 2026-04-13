/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 13 апреля 2026 12:59:47
 * Version: 1.0.7
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Root.Components
{
    public class DataSetPlayerInfo
    {
        public List<BanItem> BanList { get; set; } = new List<BanItem>();
        public List<CharacterItem> CharacterList { get; set; } = new List<CharacterItem>();

        public void Clear()
        {
            CharacterList.Clear();
            BanList.Clear();
        }

        public void MergeBanList(List<BanItem> banList)
        {
            BanList.AddRange(banList);
        }

        public void MergeCharacterList(List<CharacterItem> characterList)
        {
            CharacterList.AddRange(characterList);
        }

        public CharacterItem FindByCharPath(string charFilePath)
        {
            return CharacterList.FirstOrDefault(it => it.CharPath == charFilePath);
        }

        public BanItem FindBanByAccDir(string accDir)
        {
            return BanList.FirstOrDefault(it =>it.AccDir == accDir);
        }
    }
}
