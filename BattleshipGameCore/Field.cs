using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipGameCore.Enums;

namespace BattleshipGameCore
{
    public class Field
    {
        public int ShipId {  get; set; }

        public FieldStateEnum FieldState {get; set;}

        public Field()
        {
            this.FieldState = FieldStateEnum.NotCheckedYet;
            this.ShipId = 0;
        }

        public FieldStateEnum DiscoverFieldState()
        {
            if (this.ShipId == 0)
            {
                this.FieldState = FieldStateEnum.Miss;
            }
            else
            {
                this.FieldState = FieldStateEnum.Hit;
            }
            return this.FieldState;
        }
    }
    
    
}
