using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 翻译姬;
public class Xml指令集数值更改 : Json指令集数值更改 {

    public Xml指令集数值更改(DataRow Json指令row) : base(Json指令row) {
        Text = "Xml指令集数值更改";
    }

}
