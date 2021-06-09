using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C20__Ex2
{
    public interface AfterMove
    {
        void UpdateAfterPlayerMove();
        void EndGame(Player i_currPlayer);
    }
}
