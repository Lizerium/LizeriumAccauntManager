/*
 * Author: Nikolay Dvurechensky
 * Site: https://dvurechensky.pro/
 * Gmail: dvurechenskysoft@gmail.com
 * Last Updated: 06 июня 2026 08:48:30
 * Version: 1.0.63
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
