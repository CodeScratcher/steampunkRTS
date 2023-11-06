using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace steampunkRTS
{
    public class GuiCommand : Command
    {
        public string id;
        public List<IEntity> entities;

        public GuiCommand(string id, List<IEntity> entities)
        {
            this.id = id;
            this.entities = entities;
        }
    }
}
