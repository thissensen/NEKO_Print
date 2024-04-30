using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace 翻译姬;
public struct KeyValue<T1,T2> {

    public T1 Key;
    public T2 Value;

    [JsonIgnore]
    public object Tag;

}
