using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FerroFTS.Data
{ 
    [DataContract()]
    public class custom_prop
    {
        [DataMember()]
        public string name;
        [DataMember()]
        public string value;
    }
}
