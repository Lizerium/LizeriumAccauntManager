/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 29 апреля 2026 06:52:59
 * Version: 1.0.25
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
