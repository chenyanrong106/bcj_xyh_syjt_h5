using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SPACRM.Interface
{
   
    public interface IResource
    {
      
        string Code { get; set; }       
        string Name { get; set; }        
        string Value { get; set; }
    }
}
