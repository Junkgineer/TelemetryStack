using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TelemetryStack
{
    public class MethodTelemetry : TelemetryStack
    {
        private string _sender { get; set; }
        public string Sender { get { return _sender; } }
        private Exception _exception { get; set; }
        public Exception ExceptionError { get { return _exception; } set { Success = false; _exception = value; } }
        private string _logicError { get; set; }
        public string LogicError { get { return _logicError; } set { Success = false; _logicError = value; } }
        public string Message { get; set; }
        public bool Success { get; set; } = true;
        public DateTime Timestamp { get; } = DateTime.Now;
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public Type OutputType { get; set; }
        private object _outputObject { get; set; }
        public object Output { get { return _outputObject; } set { OutputType = value.GetType(); _outputObject = value; } }
        public MethodTelemetry()
        {
            _sender = _getCurrentMethod();
        }

        public bool AddData(string datavalue, string datakey = "General")
        {
            bool _success = false;
            if (this.Data.ContainsKey(datakey))
            {
                List<string> stringlist = (List<string>)this.Data[datakey];
                stringlist.Add(datavalue);
                this.Data[datakey] = stringlist;
                _success = true;
            }
            else
            {
                this.Data.Add(datakey, new List<string>() { datavalue });
                _success = true;
            }
            return _success;
        }
        public bool AddData(List<string> listdatavalue, string datakey = "General")
        {
            bool _success = false;
            if (this.Data.ContainsKey(datakey))
            {
                List<string> appendlist = (List<string>)this.Data[datakey];
                appendlist.AddRange(listdatavalue);
                this.Data[datakey] = appendlist;
                _success = true;
            }
            else
            {
                this.Data.Add(datakey, listdatavalue);
                _success = true;
            }
            return _success;
        }
        public bool AddData(List<int> listdatavalue, string datakey = "General")
        {
            bool _success = false;
            if (this.Data.ContainsKey(datakey))
            {
                List<int> appendintlist = (List<int>)this.Data[datakey];
                appendintlist.AddRange(listdatavalue);
                this.Data[datakey] = appendintlist;
                _success = true;
            }
            else
            {
                this.Data.Add(datakey, listdatavalue);
                _success = true;
            }
            return _success;
        }
        public bool AddData(Dictionary<string, string> dictdatavalue, string datakey = "General")
        {
            bool _success = false;
            if (this.Data.ContainsKey(datakey))
            {
                Dictionary<string, string> appenddict = (Dictionary<string, string>)this.Data[datakey];
                foreach (KeyValuePair<string, string> item in dictdatavalue)
                {
                    if (appenddict.ContainsKey(item.Key))
                    {
                        string newkey = item.Key;
                        int keynum = 1;
                        while (appenddict.ContainsKey(newkey))
                        {
                            newkey = string.Format("{0}_({1})", newkey, keynum);
                            keynum++;
                        }
                        appenddict.Add(newkey, item.Value);
                    }

                }
                this.Data[datakey] = appenddict;
                _success = true;
            }
            else
            {
                this.Data.Add(datakey, dictdatavalue);
                _success = true;
            }
            return _success;
        }
        public bool AddData(Dictionary<string, List<string>> dictlistdatavalue, string datakey = "General")
        {
            bool _success = false;
            if (this.Data.ContainsKey(datakey))
            {
                Dictionary<string, List<string>> appenddict = (Dictionary<string, List<string>>)this.Data[datakey];
                foreach (KeyValuePair<string, List<string>> item in dictlistdatavalue)
                {
                    if (appenddict.ContainsKey(item.Key))
                    {
                        string newkey = item.Key;
                        int keynum = 1;
                        while (appenddict.ContainsKey(newkey))
                        {
                            newkey = string.Format("{0}_({1})", newkey, keynum);
                            keynum++;
                        }
                        appenddict.Add(newkey, item.Value);
                    }

                }
                this.Data[datakey] = appenddict;
                _success = true;
            }
            else
            {
                this.Data.Add(datakey, dictlistdatavalue);
                _success = true;
            }
            return _success;
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string _getCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
        public void LogTelemetry()
        {

        }
    }
}
