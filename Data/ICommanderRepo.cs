using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        bool SaveChanges();

        IEnumerable<Command> GetAllCommands();
        
        Command GetCommandById(int id);

        void CreateCommand(Command command);

        void UpdateCommand(Command command);

        void DeleteCommand(Command command);
    }
}