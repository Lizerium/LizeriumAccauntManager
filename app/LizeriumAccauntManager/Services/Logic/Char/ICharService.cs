/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 14 апреля 2026 12:25:10
 * Version: 1.0.8
 */

using System.Threading.Tasks;

using Root.Components;

namespace Root.Services
{
    public interface ICharService
    {
        void ChangeMoneyCharacter(uint newMoney);
        void CheckFileCharacter();
        void DeleteChar(string charName, string charPath);
        CharacterItem GetSelectedCharRecord();
        Task<string> PrettyPrintCharFile(IFLGameDataService fLGameDataService, IFLDataFileService fLDataFileService);
        void RenameCharacter(string newName);
        void SaveCharFile(IFLDataFileService charFile);
        void SetSelectedCharRecord(CharacterItem newSelectedCharRecord);
    }
}
