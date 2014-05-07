using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Isic.Engine.Abstracts
{
    abstract class Nameable
    {
        String name;

        public String GetName()
        {
            return this.name;
        }

        public void SetName(String name)
        {
            this.name = name;
        }
    }
}
