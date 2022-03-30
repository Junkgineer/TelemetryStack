using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TelemetryStack
{
    public class TelemetryStack
    {
        private SortedList<int, MethodTelemetry> _stack { get; set; }
        public int Count { get { return _stack.Count; } }
        public int MaxItems { get; set; } = 20;
        private int _index { get; set; } = 0;
        public void AddToStack(MethodTelemetry telemetry)
        {
            if (_stack.Count >= MaxItems)
            {
                _trimStack();
            }
            this._stack.Add(_index, telemetry);
            this._index++;
        }
        private void _trimStack()
        {
            SortedList<int, MethodTelemetry> _holdStack = new SortedList<int, MethodTelemetry>();
            for (int i = 1; i < _stack.Count - 1; i++)
            {
                _holdStack.Add(i - 1, _stack[i]);
            }
            _index = _stack.Count;
            _stack = _holdStack;
        }
        public void RemoveFromStack(int index)
        {
            SortedList<int, MethodTelemetry> _holdStack = new SortedList<int, MethodTelemetry>();
            for (int i = 0; i < _stack.Count - 1; i++)
            {
                if (i < index)
                {
                    _holdStack.Add(i, _stack[i]);
                }
                else if (i > index)
                {
                    _holdStack.Add(i - 1, _stack[i - 1]);
                }
            }
            _stack = _holdStack;
        }
    }
    
}
