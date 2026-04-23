/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 23 апреля 2026 06:53:22
 * Version: 1.0.19
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
