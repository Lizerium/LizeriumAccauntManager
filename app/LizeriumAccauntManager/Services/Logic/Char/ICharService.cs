/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 28 апреля 2026 14:26:16
 * Version: 1.0.24
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
