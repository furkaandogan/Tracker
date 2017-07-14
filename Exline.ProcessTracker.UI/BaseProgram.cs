using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Exline.ProcessTracker.UI
{
    public abstract class BaseTrackerProgram
    {
        public abstract Command DefaultCommand { get; set; }
        public abstract List<Command> Commands { get; }

        public BaseTrackerProgram()
        {

        }

        protected Command FilterCommand(params string[] args)
        {
            Command cmd = DefaultCommand;
            if (args != null && args.Length > 0)
            {
                string userCode = args[0];
                if (!string.IsNullOrEmpty(userCode))
                {
                    cmd = Commands.Find(x => x.AnyCmd(userCode.ToLower()));
                    if (cmd == null)
                    {
                        cmd = DefaultCommand;
                    }
                }
            }
            return cmd;
        }
    }
}
