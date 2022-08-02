using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Command.Command
{
    //Invoker 
    internal class ProductCommandManager
    {
        private Stack<ICommand> commands;      

        public ProductCommandManager()
        {
            this.commands = new Stack<ICommand>();
        }

        public void Execute(ICommand command)
        {
            if (command != null)
            {
                commands.Push(command);
                command.Execute();
            }
        }
        public void Undo(int numberOfCommands)
        {
            if(numberOfCommands==0)
            {
                numberOfCommands=commands.Count;
            }
            for (int i = 0; i < numberOfCommands; i++)
            {
                commands.Pop().Undo();
            }
        }
    }
}
