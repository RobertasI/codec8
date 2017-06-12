using System;
using System.Collections;

namespace Codec8
{
    class Data
    {
        public ArrayList DataList = new ArrayList();

        public static implicit operator ArrayList(Data v)
        {
            throw new NotImplementedException();
        }
    }
}
