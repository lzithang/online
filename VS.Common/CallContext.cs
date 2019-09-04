using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace VS.Common
{

    public static class CallContext
    {

        static ConcurrentDictionary<string, AsyncLocal<object>> state = new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static void SetData(string name, object data) =>

            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;


        public static object GetData(string name) =>

            state.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;

        public static T GetData<T>(string key) =>
            state.TryGetValue(key, out AsyncLocal<object> data) ? (T)data.Value : default(T);
    }

}
