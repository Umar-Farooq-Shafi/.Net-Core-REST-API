using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo: ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return new List<Command> {
                new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kattel & pen" },
                new Command { Id = 1, HowTo = "Cut a bread", Line = "Get a knife", Platform = "Knife and chopping bread" },
                new Command { Id = 2, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kattel & cup" },
            };
        }

        public Command GetCommandById(int id) 
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Kattel & pen" };
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new System.NotImplementedException();
        }
    }
}