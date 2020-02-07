using FormsApp.UserInterface.ConsoleGui;
using FormsApp.UserInterface.TerminalGui;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp.UserInterface
{
    class MenuFactory
    {
        public IMenu GetMenu(MenuType type)
        {
            switch (type)
            {
                case MenuType.Console:
                    return new MenuConsole();
                case MenuType.TerminalGui:
                    //return new MenuTerminal();
                default:
                    throw new NotSupportedException($"Menu: {type.ToString()}");
            }
        }
    }
}
