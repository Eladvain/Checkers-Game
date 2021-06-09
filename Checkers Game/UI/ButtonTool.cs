using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C20__Ex2.UI
{
    public class ButtonTool : Button
    {
       private Position m_ButtonPosition;
        
      public ButtonTool(int i_Row, int i_Col, Board.eCheckersOption i_ButtonSign)
        {
            m_ButtonPosition = new Position(i_Row, i_Col, i_ButtonSign);
        }
        public ButtonTool()
        {
            m_ButtonPosition = new Position(0,0);
        }

      public Position ButtonPosition
      {
            get { return this.m_ButtonPosition; }
      }
    }
}
