using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exline.ProcessTracker.UI
{
    public class Command
    {
        public Command(string cmd, Action<string[]> action) : this(action, cmd)
        {

        }
        public Command(Action<string[]> action, params string[] cmd)
        {
            Cmd = cmd;
            Action = action;
        }

        protected string[] Cmd { get; set; }
        protected Action<string[]> Action { get; set; }

        public void Execute(string[] args)
        {
            Action(args);
        }

        public bool AnyCmd(string cmd)
        {
            return Cmd.Contains(cmd);
        }
    }
}
