using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RequestUserInfo 
    {
        public RequestUserInfo()
        {
            KeyValueList = new Dictionary<string, string>();
        }
        public Dictionary<string,string> KeyValueList { get; set; }
    }
}
